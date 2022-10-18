using MvaCToolkitCs.v1_2.Protocol;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MvaCToolkitCs.v1_2.Net.SocketTx
{
    public abstract class CtkSocket: ICtkProtocolNonStopConnect, IDisposable
    {


        /// <summary> Client? or Listener </summary>
        public bool IsActively = false;
        /// <summary>
        /// 若開啟自動讀取,
        /// 在連線完成 及 讀取完成 時, 會自動開始下一次的讀取.
        /// 這不適用在Sync作業, 因為Sync讀完會需要使用者處理後續.
        /// 因此只有非同步類的允許自動讀取
        /// 邏輯上來說, 預設為true, 因為使用者不希望太複雜的控制.
        /// 但Flag仍需存在, 當使用者想要時, 就可以直接控制.
        /// </summary>
        public bool IsAsynAutoReceive = true;
        public Uri LocalUri;
        public Uri RemoteUri;
        public ConcurrentQueue<Socket> SocketWorks = new ConcurrentQueue<Socket>();
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
        public Socket SocketWorkActive { get; protected set; }
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
            var client = this.ActiveWorkClient as Socket;
            myea.Sender = this;
            myea.WorkSocket = client;
            var trxBuffer = myea.TrxMessageBuffer;
            client.BeginReceive(trxBuffer.Buffer, 0, trxBuffer.Buffer.Length, SocketFlags.None, new AsyncCallback(EndReceiveCallback), myea);

            //EndReceive 是由 BeginReceive 回呼的函式中去執行
            //也就是收到後, 通知結束工作的函式
            //你無法在其它地方呼叫, 因為你沒有 IAsyncResult 的物件
        }
        public void CleanInvalidWorks()
        {
            try
            {
                Monitor.TryEnter(this.SocketWorks, 1000);
                var list = new List<Socket>();
                Socket client = null;
                while (!this.SocketWorks.IsEmpty)
                {
                    if (!this.SocketWorks.TryDequeue(out client)) break;
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
                    this.SocketWorks.Enqueue(tc);
            }
            catch (Exception ex) { CtkLog.Write(ex); }
            finally { Monitor.Exit(this.SocketWorks); }
        }

        /// <summary> 指定的 Socket or Last </summary>
        public Socket GetSocketActiveOrLast()
        {
            if (this.SocketWorkActive != null) return this.SocketWorkActive;
            return this.SocketWorks.LastOrDefault();//最後取得的
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


        /// <summary>
        /// Server Accept End
        /// </summary>
        /// <param name="ar"></param>
        void EndAcceptCallback(IAsyncResult ar)
        {
            var myea = new CtkNetStateEventArgs();
            var trxmBuffer = myea.TrxMessageBuffer;
            Socket client = null;
            try
            {
                Monitor.Enter(this);//一定要等到進去
                var state = (CtkSocketTcp)ar.AsyncState;
                client = state.SocketConn.EndAccept(ar);
                state.SocketWorks.Enqueue(client);

                myea.Sender = state;
                myea.WorkSocket = client;//觸發EndAccept的Socket未必是同一個, 所以要帶WorkSocket

                //預設是不斷的去聆聽, 但是否要維持連線, 由應用端決定
                state.SocketConn.BeginAccept(new AsyncCallback(EndAcceptCallback), state);


                if (!ar.IsCompleted || client == null || !client.Connected)
                    throw new CtkSocketException("Connection Fail");


                //呼叫他人不應影響自己運作, catch起來
                try { this.OnFirstConnect(myea); }
                catch (Exception ex) { CtkLog.WarnNs(this, ex); }

                if (this.IsAsynAutoReceive)
                    client.BeginReceive(trxmBuffer.Buffer, 0, trxmBuffer.Buffer.Length, SocketFlags.None, new AsyncCallback(EndReceiveCallback), myea);
            }
            //catch (SocketException ex) { }
            catch (Exception ex)
            {

                CtkNetUtil.DisposeSocketTry(client);//失敗清除, Listener不用
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
            var myea = new CtkNetStateEventArgs();
            var trxBuffer = myea.TrxMessageBuffer;
            Socket client = null;
            try
            {
                Monitor.Enter(this);//一定要等到進去
                var state = (CtkSocketTcp)ar.AsyncState;
                client.EndConnect(ar);
                client = state.SocketConn;
                state.SocketWorks.Enqueue(client);//作為Client時, Work = Conn

                myea.Sender = state;
                myea.WorkSocket = client;


                if (!ar.IsCompleted || client == null || !client.Connected)
                    throw new CtkSocketException("Connection Fail");

                //呼叫他人不應影響自己運作, catch起來
                try { this.OnFirstConnect(myea); }
                catch (Exception ex) { CtkLog.WarnNs(this, ex); }

                if (this.IsAsynAutoReceive)
                    client.BeginReceive(trxBuffer.Buffer, 0, trxBuffer.Buffer.Length, SocketFlags.None, new AsyncCallback(EndReceiveCallback), myea);
            }
            //catch (SocketException ex) { }
            catch (Exception ex)
            {
                //失敗就中斷連線, 清除
                CtkNetUtil.DisposeSocketTry(client);
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
            //var stateea = (CtkNonStopTcpStateEventArgs)ar.AsyncState;
            var myea = (CtkNetStateEventArgs)ar.AsyncState;
            var client = myea.WorkSocket;
            try
            {

                if (!ar.IsCompleted || client == null || !client.Connected)
                {
                    throw new CtkSocketException("Read Fail");
                }

                var trxmBuffer = myea.TrxMessageBuffer;
                var bytesRead = client.EndReceive(ar);
                trxmBuffer.Length = bytesRead;

                //呼叫他人不應影響自己運作, catch起來
                try { this.OnDataReceive(myea); }
                catch (Exception ex) { CtkLog.Write(ex); }

                if (this.IsAsynAutoReceive)
                    client.BeginReceive(trxmBuffer.Buffer, 0, trxmBuffer.Buffer.Length, SocketFlags.None, new AsyncCallback(EndReceiveCallback), myea);

            }
            //catch (IOException ex) { CtkLog.Write(ex); }
            catch (Exception ex)
            {
                CtkNetUtil.DisposeSocketTry(client);//僅關閉讀取失敗的連線
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
            if (this.SocketWorks.Count == 0) return;//沒連線不執行
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

        public object ActiveWorkClient { get { return this.SocketWorkActive; } set { this.SocketWorkActive = value as Socket; } }
        public bool IsLocalReadyConnect { get { return this.SocketConn != null && this.SocketConn.IsBound; } }
        public bool IsOpenRequesting { get { return !this.mreIsConnecting.WaitOne(10); } }
        public bool IsRemoteConnected { get { this.CleanInvalidWorks(); return this.SocketWorks.Count > 0; } }

        public int ConnectTry()
        {

            if (this.IsOpenRequesting || this.IsRemoteConnected) return 0;
            //if (this.IsLocalReadyConnect) return; //同步連線是等到連線才離開method, 不需判斷 IsLocalReadyConnect

            try
            {
                if (!Monitor.TryEnter(this, 1000)) return -1;
                if (!this.mreIsConnecting.WaitOne(10)) return 0;//連線中先離開
                this.mreIsConnecting.Reset();//先卡住, 不讓後面的再次進行


                //若連線不曾建立, 或聆聽/連線被關閉
                if (this.SocketConn == null || !this.SocketConn.Connected)
                {
                    CtkNetUtil.DisposeSocketTry(this.SocketConn);//Dispose舊的
                    this.SocketConn = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//建立新的
                }


                if (this.IsActively)
                {
                    if (this.LocalUri != null && !this.SocketConn.IsBound)
                        this.SocketConn.Bind(CtkNetUtil.ToIPEndPoint(this.LocalUri));
                    if (this.RemoteUri == null)
                        throw new CtkSocketException("remote field can not be null");
                    this.SocketConn.Connect(CtkNetUtil.ToIPEndPoint(this.RemoteUri));
                    this.OnFirstConnect(new CtkProtocolEventArgs() { Message = "Connect Success" });
                    this.SocketWorks.Enqueue(this.SocketConn);
                }
                else
                {
                    if (this.LocalUri == null)
                        throw new Exception("local field can not be null");
                    if (!this.SocketConn.IsBound)
                        this.SocketConn.Bind(CtkNetUtil.ToIPEndPoint(this.LocalUri));
                    this.SocketConn.Listen(100);
                    this.SocketWorks.Enqueue(this.SocketConn.Accept());
                    this.OnFirstConnect(new CtkProtocolEventArgs() { Message = "Connect Success" });
                }

                if (this.IsAsynAutoReceive) this.BeginReceive();


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
                try { this.mreIsConnecting.Set(); /*同步型的, 結束就可以Set*/ }
                catch (ObjectDisposedException) { }
                if (Monitor.IsEntered(this)) Monitor.Exit(this);
            }
        }
        public int ConnectTryStart()
        {
            try
            {
                if (!Monitor.TryEnter(this, 1000)) return -1;//進不去先離開
                this.CleanInvalidWorks();
                if (!this.mreIsConnecting.WaitOne(10)) return 0;//連線中先離開
                this.mreIsConnecting.Reset();//先卡住, 不讓後面的再次進行


                //若連線不曾建立, 或聆聽/連線被關閉
                if (this.SocketConn == null || !this.SocketConn.Connected)
                {
                    CtkNetUtil.DisposeSocketTry(this.SocketConn);//Dispose舊的
                    this.SocketConn = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//建立新的
                }



                if (this.IsActively)
                {//Client
                    if (this.LocalUri != null && !this.SocketConn.IsBound)
                        this.SocketConn.Bind(CtkNetUtil.ToIPEndPoint(this.LocalUri));
                    if (this.RemoteUri == null)
                        throw new CtkSocketException("remote field can not be null");

                    this.SocketConn.BeginConnect(CtkNetUtil.ToIPEndPoint(this.RemoteUri), new AsyncCallback(EndConnectCallback), this);
                }
                else
                {//Server
                    if (this.LocalUri == null)
                        throw new Exception("local field can not be null");
                    if (!this.SocketConn.IsBound)
                        this.SocketConn.Bind(CtkNetUtil.ToIPEndPoint(this.LocalUri));
                    this.SocketConn.Listen(100);
                    this.SocketConn.BeginAccept(new AsyncCallback(EndAcceptCallback), this);
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

            foreach (var socket in this.SocketWorks) CtkNetUtil.DisposeSocketTry(socket);
            CtkNetUtil.DisposeSocketTry(this.SocketConn);
            this.OnDisconnect(new CtkProtocolEventArgs() { Message = "Disconnect method is executed" });
        }
        public void WriteMsg(CtkProtocolTrxMessage msg)
        {
            try
            {
                //寫入不卡Monitor, 並不會造成impact
                //但如果卡了Monitor, 你無法同時 等待Receive 和 要求Send

                //其它作業可以卡 Monitor.TryEnter
                //此物件會同時進行的只有 Receive 和 Send
                //所以其它作業卡同一個沒問題: Monitor.TryEnter(this, 1000)

                var buffer = msg.ToBuffer();
                var mySocketActive = this.GetSocketActiveOrLast();
                mySocketActive.Send(buffer.Buffer, buffer.Offset, buffer.Length, SocketFlags.None);
            }
            catch (Exception ex)
            {
                this.Disconnect();//寫入失敗就斷線
                CtkLog.WarnNs(this, ex);
                throw ex;//就例外就拋出, 不吃掉
            }
        }

        #endregion


        #region ICtkProtocolNonStopConnect

        public int IntervalTimeOfConnectCheck { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsNonStopRunning => throw new NotImplementedException();
        public void NonStopRunStart()
        {
            throw new NotImplementedException();
        }

        public void NonStopRunStop()
        {
            throw new NotImplementedException();
        }
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
