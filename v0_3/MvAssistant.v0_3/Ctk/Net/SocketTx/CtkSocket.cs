using MvaCToolkitCs.v1_2.Protocol;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace MvaCToolkitCs.v1_2.Net.SocketTx
{
    public abstract class CtkSocket : ICtkProtocolNonStopConnect, IDisposable
    {


        /// <summary> Client? or Listener </summary>
        public bool IsActively = false;
        /// <summary>
        /// 若開啟自動讀取,
        /// 在連線完成 及 讀取完成 時, 會自動開始下一次的讀取.
        /// 這不適用在Sync作業, 因為Sync讀完會需要使用者處理後續.
        /// 因此只有非同步類的允許自動讀取
        /// 預設=true, 邏輯上來說, 使用者不希望太複雜的控制.
        /// </summary>
        public bool IsAsynAutoReceive = true;
        public bool IsAsynAutoListen = true;
        public Uri LocalUri;
        /// <summary> Client 才需指定 欲連線的對象 </summary>
        public Uri RemoteUri;
        /// <summary> 預設接受Any來源 </summary>
        public EndPoint RemoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
        public ConcurrentQueue<Socket> TargetSockets = new ConcurrentQueue<Socket>();
        public ConcurrentQueue<EndPoint> TargetEndPoints = new ConcurrentQueue<EndPoint>();
        public String Name;

        /// <summary>
        /// 不能用 AutoResetEvent.
        /// WaitOne 會自動 Set.
        /// 但有時WaintOne只是拿來判斷是否在忙.
        /// </summary>
        ManualResetEvent mreIsConnecting = new ManualResetEvent(true);
        ManualResetEvent mreIsReceiving = new ManualResetEvent(true);
        bool m_isReceiveLoop = false;
        ~CtkSocket() { this.Dispose(false); }

        public bool IsReceiveLoop { get { return m_isReceiveLoop; } private set { lock (this) m_isReceiveLoop = value; } }
        public bool IsWaitReceive { get { return this.mreIsReceiving.WaitOne(10); } }
        public Socket SocketConn { get; protected set; }
        /// <summary>
        /// 開始讀取Socket資料, Begin 代表非同步.
        /// 用於 1. IsAsynAutoReceive, 每次讀取需自行執行;
        ///     2. 若連線還在, 但讀取異常中止, 可以再度開始;
        /// </summary>
        public void BeginReceive()
        {
            var myea = new CtkNetStateEventArgs();

            //採用現正操作中的Socket進行接收
            var client = this.ActiveTarget as Socket;
            myea.Sender = this;
            myea.TargetSocket = client;
            var trxBuffer = myea.TrxBuffer;
            client.BeginReceive(trxBuffer.Buffer, 0, trxBuffer.Buffer.Length, SocketFlags.None, new AsyncCallback(EndReceiveCallback), myea);

            //EndReceive 是由 BeginReceive 回呼的函式中去執行
            //也就是收到後, 通知結束工作的函式
            //你無法在其它地方呼叫, 因為你沒有 IAsyncResult 的物件
        }
        public void CleanInvalidWorks()
        {
            try
            {
                Monitor.TryEnter(this.TargetSockets, 1000);
                var list = new List<Socket>();
                Socket client = null;
                while (!this.TargetSockets.IsEmpty)
                {
                    if (!this.TargetSockets.TryDequeue(out client)) break;
                    if (client != null && client.Connected)
                    {
                        list.Add(client);
                    }
                    else
                    {
                        CtkNetUtil.DisposeSocketTry(client);
                    }
                }

                foreach (var tc in list)
                    this.TargetSockets.Enqueue(tc);
            }
            catch (Exception ex) { CtkLog.Write(ex); }
            finally { Monitor.Exit(this.TargetSockets); }
        }

        /// <summary> 指定的 Socket or Last </summary>
        public Socket GetSocketActiveOrLast()
        {
            var rtn = this.ActiveTarget as Socket;
            if (rtn != null) return rtn;
            return this.TargetSockets.LastOrDefault();//最後取得的
        }
        /// <summary> 指定的 Socket or Last </summary>
        public EndPoint GetEndPointActiveOrLast()
        {
            var rtn = this.ActiveTarget as EndPoint;
            if (rtn != null) return rtn;
            return this.TargetEndPoints.LastOrDefault();//最後取得的
        }


        public int ReceiveLoop()
        {
            try
            {
                this.IsReceiveLoop = true;
                while (this.IsReceiveLoop && !this.disposed)
                {
                    this.ReceiveOnce();
                }
            }
            catch (Exception ex)
            {
                this.IsReceiveLoop = false;
                throw ex;//同步型作業, 直接拋出例外, 不用寫Log
            }
            return 0;
        }
        public void ReceiveLoopCancel()
        {
            this.IsReceiveLoop = false;
        }
        public int ReceiveOnce()
        {
            var mySocketActive = this.GetSocketActiveOrLast();
            try
            {
                if (!Monitor.TryEnter(this, 1000)) return -1;//進不去先離開
                if (!this.mreIsReceiving.WaitOne(10)) return 0;//接收中先離開
                this.mreIsReceiving.Reset();//先卡住, 不讓後面的再次進行

                var ea = new CtkProtocolEventArgs()
                {
                    Sender = this,
                };

                ea.TrxMessage = new CtkProtocolBufferMessage(1024);
                var trxBuffer = ea.TrxMessage.ToBuffer();

                trxBuffer.Length = mySocketActive.Receive(trxBuffer.Buffer, 0, trxBuffer.Buffer.Length, SocketFlags.None);
                if (trxBuffer.Length == 0) return -1;
                this.OnDataReceive(ea);
            }
            catch (Exception ex)
            {
                this.OnErrorReceive(new CtkProtocolEventArgs() { Message = "Read Fail" });
                //當 this.ConnSocket == this.WorkSocket 時, 代表這是 client 端
                this.Disconnect();
                if (this.SocketConn != mySocketActive)
                    CtkNetUtil.DisposeSocketTry(mySocketActive);//執行出現例外, 先釋放Socket
                throw ex;//同步型作業, 直接拋出例外, 不用寫Log
            }
            finally
            {
                try { this.mreIsReceiving.Set(); /*同步型的, 結束就可以Set*/ }
                catch (ObjectDisposedException) { }
                if (Monitor.IsEntered(this)) Monitor.Exit(this);
            }
            return 0;
        }





        #region Connect Status Function

        public bool CheckConnectReadable()
        {
            var socket = this.GetSocketActiveOrLast();
            if (socket == null) return false;
            if (!socket.Connected) return false;
            return !(socket.Poll(1000, SelectMode.SelectRead) && (socket.Available == 0));
        }
        public void DisconnectIfUnReadable()
        {
            if (this.IsOpenRequesting) return;//開啟中不執行
            if (this.SocketConn == null) return;//沒連線不執行
            if (this.TargetSockets.Count == 0) return;//沒連線不執行
            if (!this.CheckConnectReadable())//確認無法連線
                this.Disconnect();//先斷線再
        }

        #endregion



        #region ICtkProtocolConnect

        public event EventHandler<CtkProtocolEventArgs> EhDataReceive;
        public event EventHandler<CtkProtocolEventArgs> EhDisconnect;
        public event EventHandler<CtkProtocolEventArgs> EhErrorReceive;
        public event EventHandler<CtkProtocolEventArgs> EhFailConnect;
        public event EventHandler<CtkProtocolEventArgs> EhFirstConnect;

        /// <summary> 設定主要通訊對象 </summary>
        public object ActiveTarget { get; set; }
        public bool IsLocalReadyConnect { get { return this.SocketConn != null && this.SocketConn.IsBound; } }
        public bool IsOpenRequesting { get { return !this.mreIsConnecting.WaitOne(10); } }
        public bool IsRemoteConnected { get { this.CleanInvalidWorks(); return this.TargetSockets.Count > 0 || this.TargetEndPoints.Count > 0; } }






        public int ConnectTry() { /*暫時不支援同步型, 也很少用*/ throw new NotImplementedException(); }
        public int ConnectTryStart()
        {
            if (this.IsOpenRequesting || this.IsRemoteConnected) return 0;
            if (this.IsLocalReadyConnect) return 0; //非同步連線 有可能在等待連線, 此時不需重啟連線.

            try
            {
                if (!Monitor.TryEnter(this, 1000)) return -1;//進不去先離開
                this.CleanInvalidWorks();
                if (!this.mreIsConnecting.WaitOne(10)) return 0;//連線中先離開 = IsOpenRequesting
                this.mreIsConnecting.Reset();//先卡住, 不讓後面的再次進行



                //若連線不曾建立, 或聆聽/連線被關閉
                if (this.SocketConn == null || !this.SocketConn.Connected)
                {
                    CtkNetUtil.DisposeSocketTry(this.SocketConn);//Dispose舊的
                    this.SocketConn = this.CreateSocket();//建立新的
                }



                if (this.IsActively)
                {//Client + TCP/UDP
                    if (this.LocalUri != null && !this.SocketConn.IsBound)
                        this.SocketConn.Bind(CtkNetUtil.ToIPEndPoint(this.LocalUri));
                    if (this.RemoteUri == null)
                        throw new CtkSocketException("remote field can not be null");

                    if (this.SocketConn.ProtocolType == ProtocolType.Tcp
                        || this.SocketConn.ProtocolType == ProtocolType.Udp)
                    {
                        this.SocketConn.BeginConnect(CtkNetUtil.ToIPEndPoint(this.RemoteUri), new AsyncCallback(EndConnectCallback), this);
                    }
                    else { throw new CtkSocketException("Not support protocol type"); }
                }
                else
                {//Listener + TCP/UDP
                    if (this.LocalUri == null)
                        throw new Exception("local field can not be null");
                    if (!this.SocketConn.IsBound)
                        this.SocketConn.Bind(CtkNetUtil.ToIPEndPoint(this.LocalUri));

                    if (this.SocketConn.ProtocolType == ProtocolType.Tcp)
                    { /* TCP 需要 Handshake = Accept 連線*/
                        this.SocketConn.Listen(100);
                        this.SocketConn.BeginAccept(new AsyncCallback(EndAcceptCallback), this);
                    }
                    else if (this.SocketConn.ProtocolType == ProtocolType.Udp)
                    { /* UDP 不需 Handshake = 直接開始接收*/
                        var myea = new CtkNetStateEventArgs();
                        myea.Sender = this;
                        myea.TargetSocket = this.SocketConn;
                        this.TargetSockets.Enqueue(myea.TargetSocket);
                        var trxmBuffer = myea.TrxBuffer;

                        this.SocketConn.BeginReceiveFrom(trxmBuffer.Buffer, 0, trxmBuffer.Buffer.Length, SocketFlags.None, ref this.RemoteEndPoint, new AsyncCallback(EndReceiveFromCallback), myea);
                    }
                    else { throw new CtkSocketException("Not support protocol type"); }
                }

                return 0;
            }
            catch (Exception ex)
            {
                //一旦聆聽/連線失敗, 直接關閉所有Socket, 重新來過
                this.Disconnect();
                this.OnFailConnect(new CtkProtocolEventArgs() { Message = "Connect Fail" });
                throw ex;//同步型作業, 直接拋出例外, 不用寫Log
            }
            finally
            {
                if (Monitor.IsEntered(this)) Monitor.Exit(this);
            }
        }

        public void Disconnect()
        {
            try { this.mreIsReceiving.Set();/*僅Set不釋放, 可能還會使用*/ } catch (ObjectDisposedException) { }
            try { this.mreIsConnecting.Set();/*僅Set不釋放, 可能還會使用*/ } catch (ObjectDisposedException) { }

            foreach (var socket in this.TargetSockets) CtkNetUtil.DisposeSocketTry(socket);
            CtkNetUtil.DisposeSocketTry(this.SocketConn);
            this.OnDisconnect(new CtkProtocolEventArgs() { Message = "Disconnect method is executed" });
        }
        public void WriteMsg(CtkProtocolTrxMessage msg)
        {
            if (this.SocketConn.ProtocolType == ProtocolType.Udp)
            {
                var ep = this.GetEndPointActiveOrLast();//預設對象
                if (ep == null)
                {
                    if (this.RemoteUri == null) throw new CtkSocketException("Can not send message in UDP mode");
                    ep = CtkNetUtil.ToIPEndPoint(this.RemoteUri);//注意 RemoteUri 載明的 IPEndPoint, 若為 Any 時, 無法傳送.
                }
                this.WriteMsgTo(msg,ep);
                return;
            }

            try
            {
                //寫入不卡Monitor, 並不會造成impact
                //但如果卡了Monitor, 你無法同時 等待Receive 和 要求Send

                //其它作業可以卡 Monitor.TryEnter
                //此物件會同時進行的只有 Receive 和 Send
                //所以其它作業卡同一個沒問題: Monitor.TryEnter(this, 1000)

                var buffer = msg.ToBuffer();
                var mySocketActive = this.GetSocketActiveOrLast();//預設對象
                mySocketActive.Send(buffer.Buffer, buffer.Offset, buffer.Length, SocketFlags.None);
            }
            catch (Exception ex)
            {
                this.Disconnect();//寫入失敗就斷線
                CtkLog.WarnNs(this, ex);
                throw ex;//就例外就拋出, 不吃掉
            }
        }


        public void WriteMsgTo(CtkProtocolTrxMessage msg, Uri uri) { this.WriteMsgTo(msg, CtkNetUtil.ToIPEndPoint(uri)); }
        public void WriteMsgTo(CtkProtocolTrxMessage msg, EndPoint ep)
        {
            try
            {
                var buffer = msg.ToBuffer();
                var mySocketActive = this.GetSocketActiveOrLast();
                mySocketActive.SendTo(buffer.Buffer, buffer.Offset, buffer.Length, SocketFlags.None, ep);
            }
            catch (Exception ex)
            {
                this.Disconnect();//寫入失敗就斷線
                CtkLog.WarnNs(this, ex);
                throw ex;//就例外就拋出, 不吃掉
            }
        }



        #endregion



        #region Callback

        /// <summary>
        /// Server Accept End
        /// </summary>
        /// <param name="ar"></param>
        void EndAcceptCallback(IAsyncResult ar)
        {
            var myea = new CtkNetStateEventArgs(); //每個新連線都有新的 EventArgs
            var trxmBuffer = myea.TrxBuffer;
            Socket target = null;
            try
            {
                Monitor.Enter(this);//一定要等到進去
                var state = (CtkSocketTcp)ar.AsyncState;
                target = state.SocketConn.EndAccept(ar);
                state.TargetSockets.Enqueue(target);

                myea.Sender = state;
                myea.TargetSocket = target; //觸發EndAccept的Socket未必是同一個, 因此註明 TargetSocket

                if (!ar.IsCompleted || target == null || !target.Connected)
                    throw new CtkSocketException("Connection Fail");


                //呼叫他人不應影響自己運作, catch起來
                try { this.OnFirstConnect(myea); }
                catch (Exception ex) { CtkLog.WarnNs(this, ex); }


                if (this.IsAsynAutoListen)
                    state.SocketConn.BeginAccept(new AsyncCallback(EndAcceptCallback), state);
                if (this.IsAsynAutoReceive)
                    target.BeginReceive(trxmBuffer.Buffer, 0, trxmBuffer.Buffer.Length, SocketFlags.None, new AsyncCallback(EndReceiveCallback), myea);
            }
            catch (Exception ex)
            {

                CtkNetUtil.DisposeSocketTry(target);//失敗清除, Listener不用
                myea.Message = ex.Message;
                myea.Exception = ex;
                this.OnFailConnect(myea);
                CtkLog.WarnNs(this, ex);
            }
            finally
            {
                try { this.mreIsConnecting.Set(); /*同步型的, 結束就可以Set*/ }
                catch (ObjectDisposedException) { }
                Monitor.Exit(this);
            }
        }
        /// <summary>
        /// Clinet Connect End
        /// </summary>
        /// <param name="ar"></param>
        void EndConnectCallback(IAsyncResult ar)
        {
            var myea = new CtkNetStateEventArgs(); //每個新連線都有新的 EventArgs
            var trxBuffer = myea.TrxBuffer;
            Socket target = null;
            try
            {
                Monitor.Enter(this);//一定要等到進去
                var state = (CtkSocket)ar.AsyncState;
                state.SocketConn.EndConnect(ar);
                target = state.SocketConn; //作為Client時, Target = Conn
                state.TargetSockets.Enqueue(target);

                myea.Sender = state;
                myea.TargetSocket = target;

                if (!ar.IsCompleted || target == null || !target.Connected)
                    throw new CtkSocketException("Connection Fail");


                //呼叫他人不應影響自己運作, catch起來
                try { this.OnFirstConnect(myea); }
                catch (Exception ex) { CtkLog.WarnNs(this, ex); }


                if (this.IsAsynAutoReceive)
                {
                    if (state.SocketConn.ProtocolType == ProtocolType.Tcp)
                    {
                        target.BeginReceive(trxBuffer.Buffer, 0, trxBuffer.Buffer.Length, SocketFlags.None, new AsyncCallback(EndReceiveCallback), myea);
                    }
                    else if (state.SocketConn.ProtocolType == ProtocolType.Udp)
                    {
                        target.BeginReceiveFrom(trxBuffer.Buffer, 0, trxBuffer.Buffer.Length, SocketFlags.None, ref state.RemoteEndPoint, new AsyncCallback(EndReceiveFromCallback), myea);
                    }
                    else { throw new CtkSocketException("Not support protocol type"); }
                }
            }
            catch (Exception ex)
            {
                //失敗就中斷連線, 清除
                CtkNetUtil.DisposeSocketTry(target);
                myea.Message = ex.Message;
                myea.Exception = ex;
                this.OnFailConnect(myea);
                CtkLog.WarnNs(this, ex);
            }
            finally
            {
                try { this.mreIsConnecting.Set(); /*同步型的, 結束就可以Set*/ }
                catch (ObjectDisposedException) { }
                Monitor.Exit(this);
            }
        }
        /// <summary>
        /// Client/Server Receive End
        /// </summary>
        /// <param name="ar"></param>
        void EndReceiveCallback(IAsyncResult ar)
        {
            var myea = (CtkNetStateEventArgs)ar.AsyncState;
            var state = myea.Sender as CtkSocket;
            var targetSocket = myea.TargetSocket;
            var trxBuffer = myea.TrxBuffer;
            try
            {

                if (!ar.IsCompleted || targetSocket == null)
                    throw new CtkSocketException("Read Fail");
                if (state.SocketConn.ProtocolType == ProtocolType.Tcp && !targetSocket.Connected)
                    throw new CtkSocketException("Read Fail");

                var bytesRead = targetSocket.EndReceive(ar);
                trxBuffer.Length = bytesRead;

                //呼叫他人不應影響自己運作, catch起來
                try { this.OnDataReceive(myea); }
                catch (Exception ex) { CtkLog.Write(ex); }

                if (this.IsAsynAutoReceive)
                    targetSocket.BeginReceive(trxBuffer.Buffer, 0, trxBuffer.Buffer.Length, SocketFlags.None, new AsyncCallback(EndReceiveCallback), myea);
            }
            catch (Exception ex)
            {
                CtkNetUtil.DisposeSocketTry(targetSocket);//僅關閉讀取失敗的連線
                myea.Message = ex.Message;
                myea.Exception = ex;
                this.OnErrorReceive(myea);//但要呼叫 OnErrorReceive
                CtkLog.WarnNs(this, ex);
            }
            finally
            {
                //會有多個Socket進入 Receive Callback, 所以不用 Reset Event
            }

        }


        void EndReceiveFromCallback(IAsyncResult ar)
        {
            var myea = (CtkNetStateEventArgs)ar.AsyncState;
            var state = myea.Sender as CtkSocket;
            var targetSocket = myea.TargetSocket;
            var trxBuffer = myea.TrxBuffer;
            try
            {

                if (!ar.IsCompleted || targetSocket == null)
                    throw new CtkSocketException("Read Fail");

                var bytesRead = targetSocket.EndReceiveFrom(ar, ref myea.TargetEndPoint);
                trxBuffer.Length = bytesRead;

                //儲存曾接收資料的對象
                var targetEndPoint = myea.TargetEndPoint;
                if (!this.TargetEndPoints.Contains(targetEndPoint))
                {/* Linq 的 Contains 採用 Equals, 所以內容相同就算是 Contain */
                    this.TargetEndPoints.Enqueue(targetEndPoint);
                }

                //呼叫他人不應影響自己運作, catch起來
                try { this.OnDataReceive(myea); }
                catch (Exception ex) { CtkLog.Write(ex); }


                if (this.IsAsynAutoReceive)
                    targetSocket.BeginReceiveFrom(trxBuffer.Buffer, 0, trxBuffer.Buffer.Length, SocketFlags.None, ref state.RemoteEndPoint, new AsyncCallback(EndReceiveFromCallback), myea);
            }
            catch (Exception ex)
            {
                CtkNetUtil.DisposeSocketTry(targetSocket);//僅關閉讀取失敗的連線
                myea.Message = ex.Message;
                myea.Exception = ex;
                this.OnErrorReceive(myea);//但要呼叫 OnErrorReceive
                CtkLog.WarnNs(this, ex);
            }
            finally
            {
                //會有多個Socket進入 Receive Callback, 所以不用 Reset Event
            }

        }


        #endregion


        #region 連線模式 Method
        public abstract Socket CreateSocket();






        #endregion




        #region Event

        protected void OnDataReceive(CtkProtocolEventArgs ea)
        {
            if (this.EhDataReceive == null) return;
            this.EhDataReceive(this, ea);
        }
        protected void OnDisconnect(CtkProtocolEventArgs ea)
        {
            if (this.EhDisconnect == null) return;
            this.EhDisconnect(this, ea);
        }
        protected void OnErrorReceive(CtkProtocolEventArgs ea)
        {
            if (this.EhErrorReceive == null) return;
            this.EhErrorReceive(this, ea);
        }
        protected void OnFailConnect(CtkProtocolEventArgs ea)
        {
            if (this.EhFailConnect == null) return;
            this.EhFailConnect(this, ea);
        }
        protected void OnFirstConnect(CtkProtocolEventArgs ea)
        {
            if (this.EhFirstConnect == null) return;
            this.EhFirstConnect(this, ea);
        }

        #endregion





        #region ICtkProtocolNonStopConnect

        public int IntervalTimeOfConnectCheck { get; set; }
        public bool IsNonStopRunning { get; set; }
        public void NonStopRunStart()
        {
            throw new NotImplementedException();
        }

        public void NonStopRunStop()
        {
            throw new NotImplementedException();
        }
        #endregion




        #region Dispose

        bool disposed = false;
        public void Dispose()
        {
            Dispose(false);
            GC.SuppressFinalize(this);
        }
        public void DisposeClose()
        {
            this.Disconnect();
            CtkUtil.DisposeObjTry(this.mreIsConnecting);
            CtkUtil.DisposeObjTry(this.mreIsReceiving);
            CtkEventUtil.RemoveEventHandlersOfOwnerByFilter(this, (dlgt) => true);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {
                // Free any managed objects here.
            }
            // Free any unmanaged objects here.
            //
            this.DisposeClose();
            disposed = true;
        }


        #endregion





    }
}
