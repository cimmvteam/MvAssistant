using CToolkitCs.v1_2.Protocol;
using CToolkitCs.v1_2.Threading;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
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
    public class CtkTcpListener : ICtkProtocolNonStopConnect, IDisposable
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
        protected int m_IntervalTimeOfConnectCheck = 5000;
        bool IsReceiveLoop = false;
        ConcurrentQueue<TcpClient> m_tcpClientList = new ConcurrentQueue<TcpClient>();
        ManualResetEvent mreIsConnecting = new ManualResetEvent(true);
        ManualResetEvent meReading = new ManualResetEvent(true);
        CtkTcpListenerEx myTcpListener = null;
        TcpClient myWorkClient;
        public String Name;
        CtkTask runningTask;
        // = new BackgroundWorker();
        public CtkTcpListener() : base() { }

        public CtkTcpListener(string localIp, int localPort)
        {
            if (localIp != null)
            {
                IPAddress.Parse(localIp);//Check format
                this.LocalUri = new Uri("net.tcp://" + localIp + ":" + localPort);
            }
        }

        ~CtkTcpListener() { this.Dispose(false); }
        public ConcurrentQueue<TcpClient> TcpClientList { get => m_tcpClientList; set => m_tcpClientList = value; }
        /// <summary>
        /// 開始讀取Socket資料, Begin 代表非同步.
        /// 用於 1. IsAutoRead被關閉, 每次讀取需自行執行;
        ///     2. 若連線還在, 但讀取異常中姒, 可以再度開始;
        /// </summary>
        public void BeginRead()
        {
            var myea = new CtkNetStateEventArgs();
            myea.Sender = this;
            var client = this.ActiveWorkClient as TcpClient;
            myea.WorkTcpClient = client;
            var trxBuffer = myea.TrxMessageBuffer;
            var stream = client.GetStream();
            stream.BeginRead(trxBuffer.Buffer, 0, trxBuffer.Buffer.Length, new AsyncCallback(EndReadCallback), myea);
        }
        public void CleanExclude(TcpClient remindClient)
        {
            var sourceList = this.TcpClientList;
            try
            {
                Monitor.TryEnter(sourceList, 1000);
                var list = new List<TcpClient>();
                TcpClient client = null;
                while (!sourceList.IsEmpty)
                {
                    if (!sourceList.TryDequeue(out client)) break;
                    if (client == remindClient)
                    {
                        list.Add(client);
                    }
                    else
                    {
                        CtkNetUtil.DisposeTcpClientTry(client);
                    }
                }

                foreach (var tc in list)
                    sourceList.Enqueue(tc);
            }
            catch (Exception ex) { CtkLog.Write(ex); }
            finally { Monitor.Exit(sourceList); }
        }
        public void CleanInvalidWorks()
        {
            try
            {
                Monitor.TryEnter(this.TcpClientList, 1000);
                var list = new List<TcpClient>();
                TcpClient client = null;
                while (!this.TcpClientList.IsEmpty)
                {
                    if (!this.TcpClientList.TryDequeue(out client)) break;
                    if (client.Client != null && client.Connected)
                    {
                        list.Add(client);
                    }
                    else
                    {
                        CtkNetUtil.DisposeTcpClientTry(client);
                    }
                }

                foreach (var tc in list)
                    this.TcpClientList.Enqueue(tc);
            }
            catch (Exception ex) { CtkLog.Write(ex); }
            finally { Monitor.Exit(this.TcpClientList); }
        }
        public void CleanUntilLast()
        {
            var sourceList = this.TcpClientList;
            try
            {
                Monitor.TryEnter(sourceList, 1000);
                var list = new List<TcpClient>();
                TcpClient client = null;


                while (!sourceList.IsEmpty)
                {
                    if (!sourceList.TryDequeue(out client)) break;
                    if (sourceList.IsEmpty)
                    {
                        list.Add(client);
                    }
                    else
                    {
                        CtkNetUtil.DisposeTcpClientTry(client);
                    }
                }

                foreach (var tc in list)
                    sourceList.Enqueue(tc);
            }
            catch (Exception ex) { CtkLog.Write(ex); }
            finally { Monitor.Exit(sourceList); }
        }
        public int ConnectCount()
        {
            var cnt = 0;
            foreach (var tc in this.TcpClientList)
            {
                if (tc == null) continue;
                if (tc.Client == null) continue;
                if (!tc.Connected) continue;

                cnt++;
            }
            return cnt;
        }

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
        public void ReadLoopCancel()
        {
            this.IsReceiveLoop = false;
        }
        public int ReadOnce()
        {
            try
            {
                if (!Monitor.TryEnter(this, 1000)) return -1;//進不去先離開
                if (!this.meReading.WaitOne(10)) return 0;//接收中先離開
                this.meReading.Reset();//先卡住, 不讓後面的再次進行

                var ea = new CtkProtocolEventArgs()
                {
                    Sender = this,
                };

                ea.TrxMessage = new CtkProtocolBufferMessage(1518);
                var trxBuffer = ea.TrxMessage.ToBuffer();

                var stream = this.myWorkClient.GetStream();
                trxBuffer.Length = stream.Read(trxBuffer.Buffer, 0, trxBuffer.Buffer.Length);
                if (trxBuffer.Length == 0) return -1;
                this.OnDataReceive(ea);
            }
            catch (Exception ex)
            {
                this.OnErrorReceive(new CtkProtocolEventArgs() { Message = "Read Fail" });
                CtkNetUtil.DisposeTcpClientTry(this.myWorkClient);//執行出現例外, 先釋放
                throw ex;//同步型作業, 直接拋出例外, 不用寫Log
            }
            finally
            {
                this.meReading.Set();//同步型的, 結束就可以Set
                if (Monitor.IsEntered(this)) Monitor.Exit(this);
            }
            return 0;
        }
        public void WriteBytes(byte[] buff, int offset, int length)
        {
            if (this.myWorkClient == null) return;
            if (!this.myWorkClient.Connected) return;

            var stm = this.myWorkClient.GetStream();
            stm.Write(buff, offset, length);

        }

        protected virtual void EndAcceptCallback(IAsyncResult ar)
        {
            var myea = new CtkNetStateEventArgs();
            var trxmBuffer = myea.TrxMessageBuffer;
            TcpClient client = null;
            try
            {
                Monitor.Enter(this);//一定要等到進去
                var state = (CtkTcpListener)ar.AsyncState;
                client = state.myTcpListener.EndAcceptTcpClient(ar);
                state.TcpClientList.Enqueue(client);//有連成功才記錄

                myea.Sender = state;
                myea.WorkTcpClient = client;

                //預設是不斷的去聆聽, 但是否要維持連線, 由應用端決定
                state.myTcpListener.BeginAcceptTcpClient(new AsyncCallback(EndAcceptCallback), state);

                if (!ar.IsCompleted || client.Client == null || !client.Connected)
                    throw new CtkSocketException("連線失敗");

                //呼叫他人不應影響自己運作, catch起來
                try { this.OnFirstConnect(myea); }
                catch (Exception ex) { CtkLog.Write(ex); }

                if (this.IsAsynAutoRead)
                {
                    var stream = client.GetStream();
                    stream.BeginRead(trxmBuffer.Buffer, 0, trxmBuffer.Buffer.Length, new AsyncCallback(EndReadCallback), myea);
                }        

            }
            catch (Exception ex)
            {
                CtkNetUtil.DisposeTcpClientTry(client);
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
        void EndReadCallback(IAsyncResult ar)
        {
            var myea = (CtkNetStateEventArgs)ar.AsyncState;
            var client = myea.WorkTcpClient;

            try
            {
                if (!ar.IsCompleted || client == null || !client.Connected)
                {
                    throw new CtkSocketException("Read Fail");
                }

                var trxmBuffer = myea.TrxMessageBuffer;
                var stream = client.GetStream();
                int bytesRead = stream.EndRead(ar);
                trxmBuffer.Length = bytesRead;

                //呼叫他人不應影響自己運作, catch起來
                try { this.OnDataReceive(myea); }
                catch (Exception ex) { CtkLog.Write(ex); }

                if (this.IsAsynAutoRead)
                    stream.BeginRead(trxmBuffer.Buffer, 0, trxmBuffer.Buffer.Length, new AsyncCallback(EndReadCallback), myea);

            }
            catch (Exception ex)
            {
                //僅關閉讀取失敗的連線
                CtkNetUtil.DisposeTcpClientTry(client);
                myea.Message = ex.Message;
                myea.Exception = ex;
                this.OnErrorReceive(myea);//但要呼叫 OnErrorReceive
                CtkLog.Write(ex);
            }
            finally
            {
                //會有多個Socket進入 Receive Callback, 所以不用 Reset Event
            }

        }


        #region ICtkProtocolConnect

        public event EventHandler<CtkProtocolEventArgs> EhDataReceive;
        public event EventHandler<CtkProtocolEventArgs> EhDisconnect;
        public event EventHandler<CtkProtocolEventArgs> EhErrorReceive;
        public event EventHandler<CtkProtocolEventArgs> EhFailConnect;
        public event EventHandler<CtkProtocolEventArgs> EhFirstConnect;

        [JsonIgnore]
        public object ActiveWorkClient
        {
            get { return this.myWorkClient; }
            set
            {
                if (!this.TcpClientList.Contains(value)) throw new ArgumentException("不可傳入別人的Tcp Client");
                this.myWorkClient = value as TcpClient;
            }
        }

        public bool IsLocalReadyConnect { get { return this.myTcpListener != null && this.myTcpListener.Active; } }
        public bool IsOpenRequesting { get { return !this.mreIsConnecting.WaitOne(10); } }
        public bool IsRemoteConnected { get { return this.ConnectCount() > 0; } }

        //用途是避免重複要求連線
        public int ConnectTry()
        {
            try
            {
                if (!Monitor.TryEnter(this, 1000)) return -1;//進不去先離開
                this.CleanInvalidWorks();
                if (!mreIsConnecting.WaitOne(10)) return 0;//連線中就離開
                this.mreIsConnecting.Reset();//先卡住, 不讓後面的再次進行連線


                if (this.myTcpListener != null) return 0;//若要重新再聆聽, 請先清除Listener
                //this.m_tcpListener.Stop();
                this.myTcpListener = new CtkTcpListenerEx(IPAddress.Parse(this.LocalUri.Host), this.LocalUri.Port);
                this.myTcpListener.Start();
                var tcpClient = this.myTcpListener.AcceptTcpClient();
                this.TcpClientList.Enqueue(tcpClient);
                this.ActiveWorkClient = tcpClient;


                /*[d20210722] 一般Sync方法要開始讀取, 應該使用者決定*/
                //if (this.IsAsynAutoRead) this.BeginRead();



                return 0;
            }
            finally
            {
                this.mreIsConnecting.Set();
                Monitor.Exit(this);
            }
        }
        public int ConnectTryStart()
        {
            try
            {
                if (!Monitor.TryEnter(this, 1000)) return -1;//進不去先離開
                this.CleanInvalidWorks();
                if (!mreIsConnecting.WaitOne(10)) return 0;//連線中就離開
                this.mreIsConnecting.Reset();//先卡住, 不讓後面的再次進行連線


                if (this.myTcpListener != null) return 0;//若要重新再聆聽, 請先清除Listener
                //this.m_tcpListener.Stop();
                this.myTcpListener = new CtkTcpListenerEx(IPAddress.Parse(this.LocalUri.Host), this.LocalUri.Port);
                this.myTcpListener.Start();
                this.myTcpListener.BeginAcceptTcpClient(new AsyncCallback(EndAcceptCallback), this);

                return 0;
            }
            finally
            {
                Monitor.Exit(this);
            }
        }



        public void Disconnect()
        {

            CtkUtil.DisposeTaskTry(this.runningTask);

            foreach (var tc in this.TcpClientList)
            {
                if (tc == null) continue;
                CtkNetUtil.DisposeTcpClientTry(tc);
            }
            if (this.myTcpListener != null) this.myTcpListener.Stop();
            this.myTcpListener = null;

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
            NonStopRunStop();

            this.runningTask = CtkTask.RunOnce(() =>
            {

                while (!disposed)
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
            CtkEventUtil.RemoveEventHandlersOfOwnerByFilter(this, (dlgt) => true);

        }

        #endregion
    }
}
