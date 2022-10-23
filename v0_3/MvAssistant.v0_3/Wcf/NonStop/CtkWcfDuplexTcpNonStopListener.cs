using MvaCToolkitCs.v1_2.Protocol;
using MvaCToolkitCs.v1_2.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MvaCToolkitCs.v1_2.Wcf.DuplexTcp
{
    public class CtkWcfDuplexTcpNonStopListener<TService> : CtkWcfDuplexTcpListener<TService>, ICtkProtocolNonStopConnect
         where TService : ICtkWcfDuplexTcpService
    {
        protected int m_IntervalTimeOfConnectCheck = 5000;
        ICTkWcfDuplexTcpCallback activeWorkClient;
        CtkTask runningTask;



        public CtkWcfDuplexTcpNonStopListener(TService _svrInst, NetTcpBinding _binding = null) : base(_svrInst, _binding)
        {
        }

        ~CtkWcfDuplexTcpNonStopListener() { this.Dispose(false); }


        #region ICtkProtocolConnect


        public event EventHandler<CtkProtocolEventArgs> EhDataReceive;
        public event EventHandler<CtkProtocolEventArgs> EhDisconnect;
        public event EventHandler<CtkProtocolEventArgs> EhErrorReceive;
        public event EventHandler<CtkProtocolEventArgs> EhFailConnect;
        public event EventHandler<CtkProtocolEventArgs> EhFirstConnect;

        public object ActiveTarget { get { return this.activeWorkClient; } set { this.activeWorkClient = value as ICTkWcfDuplexTcpCallback; } }
        public bool IsLocalReadyConnect { get { return this.host != null && this.host.State <= CommunicationState.Opened; } }
        public bool IsOpenRequesting { get { try { return Monitor.TryEnter(this, 10); } finally { Monitor.Exit(this); } } }
        public bool IsRemoteConnected { get { return this.GetAllChannels().Count > 0; } }

        public int ConnectTry() { return this.ConnectTryStart(); }
        public int ConnectTryStart()
        {
            if (this.IsLocalReadyConnect) return 0;
            try
            {
                if (!Monitor.TryEnter(this, 1000)) return -1;//進不去先離開
                if (this.IsLocalReadyConnect) return 0;
                this.CleanDisconnected();
                this.CleanHost();
                this.NewHost();

                this.host.Opened += (ss, ee) =>
                {
                    var ea = new CtkWcfDuplexEventArgs();
                    //ea.WcfChannel = this.GetCallback();//Listener(or call Host, Service) 開啟後, 並沒有Channel連線進來
                    this.OnFirstConnect(ea);
                };


                this.SvrInst.EhReceiveMsg += (ss, ee) =>
                {
                    var ea = ee;
                    ea.WcfChannel = this.GetCallback();
                    this.OnDataReceive(ea);
                };

                this.host.Closed += (ss, ee) =>
                {
                    var ea = new CtkWcfDuplexEventArgs();
                    //ea.WcfChannel = this.GetCallback();//Listerner關閉, 會關閉所有Channel, 並沒有特定哪一個
                    this.OnDisconnect(ea);
                };
                this.Open();

                return 0;
            }
            finally
            {
                Monitor.Exit(this);
            }
        }
        public void Disconnect()
        {
            this.NonStopRunStop();
            this.Close();
        }

        /// <summary>
        /// 只支援 CtkWcfMessage
        /// </summary>
        /// <param name="msg"></param>
        public void WriteMsg(CtkProtocolTrxMessage msg)
        {
            var wcfmsg = msg.As<CtkWcfMessage>();
            if (wcfmsg != null)
            {
                this.activeWorkClient.CtkSend(msg.As<CtkWcfMessage>());
                return;
            }
            throw new ArgumentException("No support type");
        }

        #endregion


        #region ICtkProtocolNonStopConnect

        public bool IsNonStopRunning { get { return this.runningTask != null && this.runningTask.Task.Status < TaskStatus.RanToCompletion; } }
        public int IntervalTimeOfConnectCheck { get { return this.m_IntervalTimeOfConnectCheck; } set { this.m_IntervalTimeOfConnectCheck = value; } }
        public void NonStopRunStart()
        {
            NonStopRunStop();

            this.runningTask = CtkTask.RunOnce((ct) =>
            {
                while (!this.disposed && !ct.IsCancellationRequested)
                {
                    ct.ThrowIfCancellationRequested();
                    try
                    {
                        this.ConnectTry();
                    }
                    catch (Exception ex) { CtkLog.Write(ex); }
                    Thread.Sleep(this.IntervalTimeOfConnectCheck);
                }

            });
        }
        public void NonStopRunStop()
        {
            if (this.runningTask != null)
            {
                using (var obj = this.runningTask)
                    obj.Cancel();
            }
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

        public override void DisposeSelf()
        {
            this.Disconnect();
            base.DisposeSelf();
        }

        #endregion

    }
}
