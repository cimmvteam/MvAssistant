using CodeExpress.v1_0.Secs;
using CToolkit;
using CToolkit.v1_1;
using CToolkit.v1_1.Net;
using CToolkit.v1_1.Protocol;
using MvAssistant.v0_2;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SensingNet.v0_2.DvcSensor.Protocol
{

    /// <summary>
    /// 僅進行連線通訊, 不處理Protocol Format
    /// </summary>
    public class SNetProtoConnTcp : ISNetProtoConnectBase, IDisposable
    {

        public bool isListener = true;
        public Uri LocalUri;
        public Uri RemoteUri;
        public DateTime? timeOfBeginConnect;
        CtkTcpClient client;
        CtkTcpListener listener;
        ManualResetEvent mreHasMsg = new ManualResetEvent(false);

        public SNetProtoConnTcp(Uri l, Uri r, bool isListener)
        {
            this.LocalUri = l;
            this.RemoteUri = r;

            this.isListener = isListener;
        }
        ~SNetProtoConnTcp() { this.Dispose(false); }

        [JsonIgnore]
        public NetworkStream ActiveWorkStream { get { return this.activeWorkTcpClient == null ? null : this.activeWorkTcpClient.GetStream(); } }
        TcpClient activeWorkTcpClient { get { return this.client == null ? null : this.client.ActiveWorkClient as TcpClient; } }
        ICtkProtocolNonStopConnect ctkProtoConnect { get { return this.client == null ? this.listener : this.client as ICtkProtocolNonStopConnect; } }



        public void ReloadClient()
        {

            try
            {
                if (this.client != null)
                    using (var obj = this.client) obj.Disconnect();
            }
            catch (Exception) { /*斷線例外不處理?*/ }



            this.client = new CtkTcpClient();
            this.client.LocalUri = this.LocalUri;
            this.client.RemoteUri = this.RemoteUri;

            this.client.EhFirstConnect += (sender, e) =>
            {
                var ea = e as CtkNonStopTcpStateEventArgs;
                //this.ActiveWorkClient = ea.workClient;
                this.OnFirstConnect(e);
            };
            this.client.EhFailConnect += (sender, e) => this.OnFailConnect(e);
            this.client.EhDisconnect += (sender, e) => this.OnDisconnect(e);
            this.client.EhDataReceive += (sender, e) => this.OnDataReceive(e);
            this.client.EhErrorReceive += (sender, e) => this.OnErrorReceive(e);

            this.client.IntervalTimeOfConnectCheck = this.IntervalTimeOfConnectCheck;
        }
        public void ReloadListener()
        {
            try
            {
                if (this.listener != null)
                    using (var obj = this.listener) obj.Disconnect();
            }
            catch (Exception) { /*斷線例外不處理?*/ }


            this.listener = new CtkTcpListener();
            this.listener.LocalUri = this.LocalUri;

            this.listener.EhFirstConnect += (sender, e) =>
            {
                var ea = e as CtkNonStopTcpStateEventArgs;
                //this.ActiveWorkClient = ea.workClient;
                //this.listener.CleanExclude(this.activeWorkTcpClient);   
                this.OnFirstConnect(e);
            };
            this.listener.EhFailConnect += (sender, e) => this.OnFailConnect(e);
            this.listener.EhDisconnect += (sender, e) => this.OnDisconnect(e);
            this.listener.EhDataReceive += (sender, e) => this.OnDataReceive(e);
            this.listener.EhErrorReceive += (sender, e) => this.OnErrorReceive(e);

            this.listener.IntervalTimeOfConnectCheck = this.IntervalTimeOfConnectCheck;
        }

        public void WriteBytes(byte[] buff, int offset, int length)
        {
            var stream = this.ActiveWorkStream;
            stream.WriteTimeout = 10 * 1000;
            stream.Write(buff, offset, length);
            stream.Flush();
        }



        #region IProtoConnectBase

        public event EventHandler<CtkProtocolEventArgs> EhDataReceive;
        public event EventHandler<CtkProtocolEventArgs> EhDisconnect;
        public event EventHandler<CtkProtocolEventArgs> EhErrorReceive;
        public event EventHandler<CtkProtocolEventArgs> EhFailConnect;
        public event EventHandler<CtkProtocolEventArgs> EhFirstConnect;

        [JsonIgnore]
        public object ActiveWorkClient { get { return this.ctkProtoConnect.ActiveWorkClient; } set { this.ctkProtoConnect.ActiveWorkClient = value; } }
        public bool IsLocalReadyConnect { get { return this.ctkProtoConnect == null ? false : this.ctkProtoConnect.IsLocalReadyConnect; } }//Local連線成功=遠端連線成功
        public bool IsOpenRequesting { get { return this.ctkProtoConnect == null ? false : this.ctkProtoConnect.IsOpenRequesting; } }
        public bool IsRemoteConnected { get { var obj = this.ctkProtoConnect; return obj == null ? false : obj.IsRemoteConnected; } }

        //用途是避免重複要求連線
        public int ConnectTry() { throw new NotImplementedException(); }
        public int ConnectTryStart()
        {
            if (this.IsNonStopRunning) return 0;//NonStopConnect 己在進行中的話, 不需再用ConnectTry
            if (this.IsRemoteConnected || this.IsOpenRequesting) return 0;


            var now = DateTime.Now;
            //超過10秒沒有連線, 會再連線
            if (this.timeOfBeginConnect.HasValue && (now - this.timeOfBeginConnect.Value).TotalSeconds < 10) return 0;
            this.timeOfBeginConnect = DateTime.Now;

            if (this.isListener)
            {
                this.ReloadListener();
                this.listener.IsAsynAutoRead = true;
                this.listener.ConnectTryStart();
            }
            else
            {
                this.ReloadClient();
                this.client.IsAsynAutoRead = true;
                this.client.ConnectTryStart();
            }

            return 0;
        }
        public void Disconnect()
        {
            if (this.client != null) { this.client.Disconnect(); this.client.Dispose(); this.client = null; }
            if (this.listener != null) { this.listener.Disconnect(); this.listener.Dispose(); this.listener = null; }
            if (this.mreHasMsg != null) this.mreHasMsg.Dispose();

        }
        public void WriteMsg(CtkProtocolTrxMessage msg)
        {
            if (msg.As<string>() != null)
            {
                var buff = Encoding.UTF8.GetBytes(msg.As<string>());
                this.WriteBytes(buff, 0, buff.Length);
            }
            else if (msg.As<CxHsmsMessage>() != null)
            {
                var secsMsg = msg.As<CxHsmsMessage>();
                var buffer = secsMsg.ToBytes();
                this.WriteBytes(buffer, 0, buffer.Length);
            }
            else
            {
                throw new ArgumentException("未定義該型別的寫入操作");
            }
        }




        #region  ICtkProtocolNonStopConnect

        public int IntervalTimeOfConnectCheck { get; set; }
        public bool IsNonStopRunning { get { return this.ctkProtoConnect == null ? false : this.ctkProtoConnect.IsNonStopRunning; } }
        public void NonStopRunStop() { this.ctkProtoConnect.NonStopRunStop(); }
        public void NonStopRunStart()
        {
            if (this.IsRemoteConnected || this.IsOpenRequesting) return;

            var now = DateTime.Now;
            //上次要求連線在10秒內也不會再連線
            if (this.timeOfBeginConnect.HasValue && (now - this.timeOfBeginConnect.Value).TotalSeconds < 10) return;
            this.timeOfBeginConnect = now;

            if (this.isListener)
            {
                this.ReloadListener();
                this.listener.NonStopRunStart();
            }
            else
            {
                this.ReloadClient();
                this.client.NonStopRunStart();
            }
        }

        #endregion


        #region Event


        void OnDataReceive(CtkProtocolEventArgs ea)
        {
            if (this.EhDataReceive == null) return;
            this.EhDataReceive(this, ea);
        }
        void OnDisconnect(CtkProtocolEventArgs ea)
        {
            if (this.EhDisconnect == null) return;
            this.EhDisconnect(this, ea);
        }
        void OnErrorReceive(CtkProtocolEventArgs ea)
        {
            if (this.EhErrorReceive == null) return;
            this.EhErrorReceive(this, ea);
        }
        void OnFailConnect(CtkProtocolEventArgs ea)
        {
            if (this.EhFailConnect == null) return;
            this.EhFailConnect(this, ea);
        }
        void OnFirstConnect(CtkProtocolEventArgs ea)
        {
            if (this.EhFirstConnect == null) return;
            this.EhFirstConnect(this, ea);
        }

        #endregion

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
        protected virtual void Dispose(bool disposing)
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
            this.Disconnect();
            CtkEventUtil.RemoveEventHandlersOfOwnerByFilter(this, (dlgt) => true);
        }

        #endregion



    }
}
