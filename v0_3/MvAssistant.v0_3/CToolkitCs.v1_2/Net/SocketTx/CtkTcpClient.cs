using CToolkitCs.v1_2.Logging;
using CToolkitCs.v1_2.Net;
using CToolkitCs.v1_2.Protocol;
using CToolkitCs.v1_2.Threading;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CToolkitCs.v1_2.Net.SocketTx
{
    public class CtkTcpClient : ICtkProtocolNonStopConnect, IDisposable
    {

        /// <summary>
        /// 若開敋自動讀取,
        /// 在連線完成 及 讀取完成 時, 會自動開始下一次的讀取.
        /// 這不適用在Sync作業, 因為Sync讀完會需要使用者處理後續.
        /// 因此只有非同步類的允許自動讀取
        /// 邏輯上來說, 預設為true, 因為使用者不希望太複雜的控制.
        /// 但Flag仍需存在, 當使用者想要時, 就可以直接控制.
        /// </summary>
        public bool IsAsynAutoRead = true;
        public Uri LocalUri;
        public String Name;
        public Uri RemoteUri;
        protected int m_IntervalTimeOfConnectCheck = 5000;
        bool IsReceiveLoop = false;
        TcpClient m_myTcpClient;
        ManualResetEvent mreIsConnecting = new ManualResetEvent(true);
        ManualResetEvent mreIsReading = new ManualResetEvent(true);
        CtkTask runningTask;

        public CtkTcpClient(Uri remote = null, Uri local = null)
        {
            this.RemoteUri = remote;
            this.LocalUri = local;
        }
        public CtkTcpClient(string remoteIp, int remotePort, string localIp = null, int localPort = 0)
        {
            if (!string.IsNullOrEmpty(remoteIp))
            {
                IPAddress.Parse(remoteIp);//Check format
                this.RemoteUri = new Uri("net.tcp://" + remoteIp + ":" + remotePort);
            }

            if (!string.IsNullOrEmpty(localIp))
            {
                IPAddress.Parse(localIp);//Check format
                this.LocalUri = new Uri("net.tcp://" + localIp + ":" + localPort);
            }
        }

        ~CtkTcpClient() { this.Dispose(false); }
        [JsonIgnore]
        protected TcpClient MyTcpClient { get { return m_myTcpClient; } set { lock (this) m_myTcpClient = value; } }

        /// <summary>
        /// 開始讀取Socket資料, Begin 代表非同步.
        /// 用於 1. IsAutoRead被關閉, 每次讀取需自行執行;
        ///     2. 若連線還在, 但讀取異常中姒, 可以再度開始;
        /// </summary>
        public void BeginRead()
        {
            var myea = new CtkNetStateEventArgs();
            var client = this.ActiveWorkClient as TcpClient;
            myea.Sender = this;
            myea.WorkTcpClient = client;
            var trxBuffer = myea.TrxMessageBuffer;
            var stream = client.GetStream();
            stream.BeginRead(trxBuffer.Buffer, 0, trxBuffer.Buffer.Length, new AsyncCallback(EndReadCallback), myea);
        }
        /// <summary>
        /// Syncnized Read Loop
        /// </summary>
        /// <returns></returns>
        public int ReadLoop()
        {
            try
            {
                this.IsReceiveLoop = true;
                while (this.IsReceiveLoop && !this.disposed)
                {
                    this.ReadOnce();
                }
            }
            catch (Exception ex)
            {
                this.IsReceiveLoop = false;
                throw ex;//同步型作業, 直接拋出例外, 不用寫Log
            }
            return 0;
        }
        /// <summary>
        /// Cancel Read Loop
        /// </summary>
        public void ReadLoopCancel()
        {
            this.IsReceiveLoop = false;
        }
        /// <summary>
        /// Syncnized Read Once
        /// </summary>
        /// <returns></returns>
        public int ReadOnce()
        {
            try
            {
                if (!Monitor.TryEnter(this, 1000)) return -1;//進不去先離開
                if (!this.mreIsReading.WaitOne(10)) return 0;//接收中先離開
                this.mreIsReading.Reset();//先卡住, 不讓後面的再次進行

                var ea = new CtkProtocolEventArgs()
                {
                    Sender = this,
                };

                ea.TrxMessage = new CtkProtocolBufferMessage(1518);
                var trxBuffer = ea.TrxMessage.ToBuffer();

                var stream = this.MyTcpClient.GetStream();
                trxBuffer.Length = stream.Read(trxBuffer.Buffer, 0, trxBuffer.Buffer.Length);
                if (trxBuffer.Length == 0) return -1;
                this.OnDataReceive(ea);
            }
            catch (Exception ex)
            {
                this.OnErrorReceive(new CtkProtocolEventArgs() { Message = "Read Fail" });
                this.Disconnect();//讀取失敗先斷線
                throw ex;//同步型作業, 直接拋出例外, 不用寫Log
            }
            finally
            {
                this.mreIsReading.Set();//同步型的, 結束就可以Set
                if (Monitor.IsEntered(this)) Monitor.Exit(this);
            }
            return 0;
        }

        /// <summary>
        /// Remember use stream.Flush() to force data send, Tcp Client always write data into buffer.
        /// </summary>
        /// <param name="buff"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        public void WriteBytes(byte[] buff, int offset, int length)
        {
            if (this.MyTcpClient == null) return;
            if (!this.MyTcpClient.Connected) return;


            try
            {
                var stm = this.MyTcpClient.GetStream();
                stm.Write(buff, offset, length);
                stm.Flush();

            }
            catch (Exception ex)
            {
                //資料寫入錯誤, 普遍是斷線造成, 先中斷連線清除資料
                this.Disconnect();
                CtkLog.WarnNs(this, ex);
            }
            //stm.BeginWrite(buff, offset, length, new AsyncCallback((ar) =>
            //{
            //    //CtkLog.WriteNs(this, "" + ar.IsCompleted);
            //}), this);


        }
        void EndConnectCallback(IAsyncResult ar)
        {
            var myea = new CtkNetStateEventArgs();
            var trxBuffer = myea.TrxMessageBuffer;
            try
            {
                //Lock使用在短碼保護, 例如: 保護一個變數的get/set
                //Monitor使用在保護一段代碼

                Monitor.Enter(this);//一定要等到進去
                var state = (CtkTcpClient)ar.AsyncState;
                var client = state.MyTcpClient;
                client.EndConnect(ar);

                myea.Sender = state;
                myea.WorkTcpClient = client;

                if (!ar.IsCompleted || client.Client == null || !client.Connected)
                    throw new CtkSocketException("Connection Fail");

                //呼叫他人不應影響自己運作, catch起來
                try { this.OnFirstConnect(myea); }
                catch (Exception ex) { CtkLog.WarnNs(this, ex); }

                if (this.IsAsynAutoRead)
                {
                    var stream = client.GetStream();
                    stream.BeginRead(trxBuffer.Buffer, 0, trxBuffer.Buffer.Length, new AsyncCallback(EndReadCallback), myea);
                }
            }
            //catch (SocketException ex) { }
            catch (Exception ex)
            {
                //失敗就中斷連線, 清除
                this.Disconnect();
                myea.Message = ex.Message;
                myea.Exception = ex;
                this.OnFailConnect(myea);
                CtkLog.WarnNs(this, ex);
            }
            finally
            {
                this.mreIsConnecting.Set();
                Monitor.Exit(this);
            }
        }
        void EndReadCallback(IAsyncResult ar)
        {
            //var stateea = (CtkNonStopTcpStateEventArgs)ar.AsyncState;
            var myea = (CtkNetStateEventArgs)ar.AsyncState;
            var client = myea.WorkTcpClient;
            try
            {
                if (!ar.IsCompleted || client == null || client.Client == null || !client.Connected)
                {
                    throw new CtkSocketException("Read Fail");
                }

                var ctkBuffer = myea.TrxMessageBuffer;
                var stream = client.GetStream();
                int bytesRead = stream.EndRead(ar);
                ctkBuffer.Length = bytesRead;

                //呼叫他人不應影響自己運作, catch起來
                try { this.OnDataReceive(myea); }
                catch (Exception ex) { CtkLog.Write(ex); }

                if (this.IsAsynAutoRead)
                    stream.BeginRead(ctkBuffer.Buffer, 0, ctkBuffer.Buffer.Length, new AsyncCallback(EndReadCallback), myea);

            }
            //catch (IOException ex) { CtkLog.Write(ex); }
            catch (Exception ex)
            {
                CtkNetUtil.DisposeTcpClientTry(client);//僅關閉讀取失敗的連線
                myea.Message = ex.Message;
                myea.Exception = ex;
                this.OnErrorReceive(myea);//但要呼叫 OnErrorReceive
                CtkLog.WarnNs(this, ex);
            }
            finally
            {

            }
        }







        #region ICtkProtocolConnect

        public event EventHandler<CtkProtocolEventArgs> EhDataReceive;
        public event EventHandler<CtkProtocolEventArgs> EhDisconnect;
        public event EventHandler<CtkProtocolEventArgs> EhErrorReceive;
        public event EventHandler<CtkProtocolEventArgs> EhFailConnect;
        public event EventHandler<CtkProtocolEventArgs> EhFirstConnect;

        [JsonIgnore]
        public object ActiveWorkClient { get { return this.MyTcpClient; } set { if (this.MyTcpClient != value) throw new ArgumentException("不可傳入Active Client"); } }
        public bool IsLocalReadyConnect { get { return this.IsRemoteConnected; } }//Local連線成功=遠端連線成功
        public bool IsOpenRequesting { get { return !this.mreIsConnecting.WaitOne(10); } }
        public bool IsRemoteConnected { get { return CtkNetUtil.IsConnected(this.MyTcpClient); } }

        public int ConnectTry()
        {
            try
            {
                if (!Monitor.TryEnter(this, 1000)) return -1;//進不去先離開
                if (!mreIsConnecting.WaitOne(10)) return 0;//連線中就離開
                this.mreIsConnecting.Reset();//先卡住, 不讓後面的再次進行連線

                //在Lock後才判斷, 避免判斷無連線後, 另一邊卻連線好了
                if (this.MyTcpClient != null && this.MyTcpClient.Connected) return 0;//連線中直接離開
                if (this.MyTcpClient != null)
                {
                    CtkNetUtil.DisposeTcpClientTry(this.MyTcpClient);
                    this.MyTcpClient = null;
                }


                IPAddress ip = null;
                if (this.LocalUri != null && IPAddress.TryParse(this.LocalUri.Host, out ip))
                    this.MyTcpClient = new TcpClient(new IPEndPoint(ip, this.LocalUri.Port));
                else this.MyTcpClient = new TcpClient();

                this.MyTcpClient.NoDelay = true;
                this.MyTcpClient.Connect(this.RemoteUri.Host, this.RemoteUri.Port);

                return 0;
            }
            catch (Exception ex)
            {
                //停止連線
                this.Disconnect();
                throw ex;
            }
            finally
            {
                //同步作業, 最後要解除連線鎖
                this.mreIsConnecting.Set();
                if (Monitor.IsEntered(this)) Monitor.Exit(this);
            }
        }
        public int ConnectTryStart()
        {
            try
            {
                if (!Monitor.TryEnter(this, 1000)) return -1;//進不去先離開
                if (!mreIsConnecting.WaitOne(10)) return 0;//連線中就離開
                this.mreIsConnecting.Reset();//先卡住, 不讓後面的再次進行連線

                //在Lock後才判斷, 避免判斷無連線後, 另一邊卻連線好了
                if (this.MyTcpClient != null && this.MyTcpClient.Connected) return 0;//連線中直接離開
                if (this.MyTcpClient != null)
                {
                    CtkNetUtil.DisposeTcpClientTry(this.MyTcpClient);
                    this.MyTcpClient = null;
                }


                IPAddress ip = null;
                if (this.LocalUri != null && IPAddress.TryParse(this.LocalUri.Host, out ip))
                {
                    this.MyTcpClient = new TcpClient(new IPEndPoint(ip, this.LocalUri.Port));
                }
                else this.MyTcpClient = new TcpClient();
                //this.activeWorkClient = this.LocalUri == null ? new TcpClient() : new TcpClient(LocalUri.Host, LocalUri.Port);
                this.MyTcpClient.NoDelay = true;
                this.MyTcpClient.BeginConnect(this.RemoteUri.Host, this.RemoteUri.Port, new AsyncCallback(EndConnectCallback), this);

                return 0;
            }
            catch (Exception ex)
            {
                //若中間有失效, 解除Event鎖
                this.mreIsConnecting.Set();
                //停止連線
                this.Disconnect();
                throw ex;
            }
            finally { if (Monitor.IsEntered(this)) Monitor.Exit(this); }

        }
        public void Disconnect()
        {
            CtkUtil.DisposeTaskTry(this.runningTask);
            CtkNetUtil.DisposeTcpClientTry(this.MyTcpClient);
            this.OnDisconnect(new CtkNetStateEventArgs() { Message = "Disconnect method is executed" });
        }
        public void WriteMsg(CtkProtocolTrxMessage msg)
        {
            if (msg == null) throw new ArgumentException("Paramter cannot be null");
            var msgStr = msg.As<string>();
            if (msgStr != null)
            {
                var buff = Encoding.UTF8.GetBytes(msgStr);
                this.WriteBytes(buff, 0, buff.Length);
                return;
            }

            var msgBuffer = msg.As<CtkProtocolBufferMessage>();
            if (msgBuffer != null)
            {
                this.WriteBytes(msgBuffer.Buffer, msgBuffer.Offset, msgBuffer.Length);
                return;
            }

            var msgBytes = msg.As<byte[]>();
            if (msgBytes != null)
            {
                this.WriteBytes(msgBytes, 0, msgBytes.Length);
                return;
            }

            throw new ArgumentException("Cannot support this type: " + msg.ToString());




        }

        #endregion


        #region ICtkProtocolNonStopConnect

        public int IntervalTimeOfConnectCheck { get { return this.m_IntervalTimeOfConnectCheck; } set { this.m_IntervalTimeOfConnectCheck = value; } }
        public bool IsNonStopRunning { get { return this.runningTask != null && this.runningTask.Status < TaskStatus.RanToCompletion; } }
        public void NonStopRunStop()
        {
            CtkUtil.DisposeTaskTry(this.runningTask);
            this.runningTask = null;
        }
        public void NonStopRunStart()
        {
            this.NonStopRunStop();
            this.runningTask = CtkTask.RunOnce(() =>
            {
                //TODO: 重啟時, 會有執行緒被中止的狀況
                while (!this.disposed)
                {
                    try
                    {
                        this.ConnectTryStart();
                    }
                    catch (Exception ex) { CtkLog.Write(ex); }

                    Thread.Sleep(this.IntervalTimeOfConnectCheck);
                }
            });
        }

        #endregion


        #region Event

        void OnDataReceive(CtkProtocolEventArgs ea)
        {
            if (this.EhDataReceive == null) return;
            this.EhDataReceive(this, ea);
        }
        void OnDisconnect(CtkProtocolEventArgs tcpstate)
        {
            if (this.EhDisconnect == null) return;
            this.EhDisconnect(this, tcpstate);
        }
        void OnErrorReceive(CtkProtocolEventArgs ea)
        {
            if (this.EhErrorReceive == null) return;
            this.EhErrorReceive(this, ea);
        }
        void OnFailConnect(CtkProtocolEventArgs tcpstate)
        {
            if (this.EhFailConnect == null) return;
            this.EhFailConnect(this, tcpstate);
        }
        void OnFirstConnect(CtkProtocolEventArgs tcpstate)
        {
            if (this.EhFirstConnect == null) return;
            this.EhFirstConnect(this, tcpstate);
        }

        #endregion



        #region IDisposable
        // Flag: Has Dispose already been called?
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //

            this.DisposeSelf();

            disposed = true;
        }


        void DisposeSelf()
        {
            try { this.Disconnect(); }
            catch (Exception ex) { CtkLog.Write(ex); }
            //斷線不用清除Event, 但Dispsoe需要, 因為即使斷線此物件仍存活著
            CtkEventUtil.RemoveEventHandlersOfOwnerByFilter(this, (dlgt) => true);
        }

        #endregion

    }
}
