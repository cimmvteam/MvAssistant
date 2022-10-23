using MvaCToolkitCs.v1_2.Protocol;
using MvaCToolkitCs.v1_2.Threading;
using MvaCToolkitCs.v1_2.Wcf.DuplexTcp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MvaCToolkitCs.v1_2.Wcf.NonStop
{

    /// <summary>
    /// 尚不完整, 除了自己的專案以外, 盡量不要用
    /// </summary>
    public class CtkWcfDuplexTcpNonStopClient<TService, TCallback> : CtkWcfDuplexTcpClient<TService, TCallback>
        , IDisposable
        , ICtkProtocolNonStopConnect
        where TService : ICtkWcfDuplexTcpService//Server提供的, 必須是interface
        where TCallback : ICTkWcfDuplexTcpCallback//提供給Server呼叫的
    {
        CtkTask NonStopTask;
        protected int m_IntervalTimeOfConnectCheck = 5000;


        public CtkWcfDuplexTcpNonStopClient(TCallback _callbackInst, NetTcpBinding _binding = null) : base(_callbackInst, _binding)
        {

        }


        #region ICtkProtocolConnect



        public event EventHandler<CtkProtocolEventArgs> EhDataReceive;
        public event EventHandler<CtkProtocolEventArgs> EhDisconnect;
        public event EventHandler<CtkProtocolEventArgs> EhErrorReceive;
        public event EventHandler<CtkProtocolEventArgs> EhFailConnect;
        public event EventHandler<CtkProtocolEventArgs> EhFirstConnect;

        public object ActiveTarget { get { return this.Channel; } set { this.Channel = (TService)value; } }
        public bool IsLocalReadyConnect { get { return this.IsWcfConnected; } }
        public bool IsOpenRequesting { get { try { return Monitor.TryEnter(this, 10); } finally { Monitor.Exit(this); } } }
        public bool IsRemoteConnected { get { return this.ChannelFactory.State == CommunicationState.Opened; } }

        public int ConnectTry() { return this.ConnectTryStart(); }
        public int ConnectTryStart()
        {
            if (this.IsLocalReadyConnect) return 0;
            this.WcfConnect(cf =>
            {
                cf.Opened += (ss, ee) =>
                {
                    var ea = new CtkWcfDuplexEventArgs();
                    ea.WcfChannel = this.Channel;
                    this.OnFirstConnect(ea);
                };
                cf.Closed += (ss, ee) =>
                {
                    var ea = new CtkWcfDuplexEventArgs();
                    ea.WcfChannel = this.Channel;
                    this.OnDisconnect(ea);
                };
            });
            return 0;

        }

        public void Disconnect()
        {
            this.NonStopRunStop();
            if (this.ChannelFactory != null)
            {
                using (var obj = this.ChannelFactory)
                {
                    obj.Abort();
                    obj.Close();
                }
            }

            CtkEventUtil.RemoveEventHandlersOfOwnerByFilter(this, (dlgt) => true);

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
                this.Channel.CtkSend(msg.As<CtkWcfMessage>());
                return;
            }
            throw new ArgumentException("No support type");
        }



        #region ICtkProtocolNonStopConnect

        public bool IsNonStopRunning { get { return this.NonStopTask != null && this.NonStopTask.Task.Status < TaskStatus.RanToCompletion; } }
        public int IntervalTimeOfConnectCheck { get { return this.m_IntervalTimeOfConnectCheck; } set { this.m_IntervalTimeOfConnectCheck = value; } }
        public void NonStopRunStop()
        {
            if (this.NonStopTask != null)
            {
                using (var obj = this.NonStopTask)
                    obj.Cancel();
            }
        }
        public void NonStopRunStart()
        {
            NonStopRunStop();

            this.NonStopTask = CtkTask.RunOnce((ct) =>
            {
                while (!this.disposed && !ct.IsCancellationRequested)
                {
                    ct.ThrowIfCancellationRequested();
                    try
                    {
                        this.ConnectTry();
                    }
                    catch (Exception ex) { CtkLog.Write(ex); }
                    Thread.Sleep(this.m_IntervalTimeOfConnectCheck);
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

        #endregion




        #region IDispose

        public override void DisposeSelf()
        {
            this.Disconnect();
            base.DisposeSelf();
        }

        #endregion
    }






}
