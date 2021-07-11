using CToolkit;
using CToolkit.v1_1.Net;
using CToolkit.v1_1.Protocol;
using CToolkit.v1_1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CToolkit.v1_1.Threading;
using CToolkit.v1_1.Logging;
using SensingNet.v0_2.DvcSensor.Protocol;
using SensingNet.v0_2.DvcSensor.SignalTrans;
using CToolkit.v1_1.ContextFlow;
using MvAssistant.v0_2.Sensor.Proto;

namespace MvAssistant.v0_2.Sensor.Device
{
    public class MvaSsTcpCmd : ICtkContextFlowRun, IDisposable
    {
        CtkTcpClient tcpClient;
        /// <summary> 是否  </summary>
        public bool IsActivelyTx;

        MvaSsProtoCmd Buffer = new MvaSsProtoCmd();

        AutoResetEvent areMsg = new AutoResetEvent(false);
        DateTime prevAckTime = DateTime.Now;
        CtkTask task;
        public MvaSsTcpCmd() { }
        ~MvaSsTcpCmd() { this.Dispose(false); }

        protected virtual int RealExec()
        {

            if (!SpinWait.SpinUntil(() => !this.CfIsRunning || this.tcpClient.IsRemoteConnected, 100))
            {
                Thread.Sleep(1000);
                return 0;
            }
            if (!this.CfIsRunning) return 0;//若不再執行->return 0


            if (this.IsActivelyTx)
            {
                
                var ackDataMsg = this.SignalTran.CreateAckMsg(this.Config.SignalCfgList);
                if (ackDataMsg != null)
                    this.ProtoConn.WriteMsg(ackDataMsg);
            }
            else
            {
                //等待下次要求資料的間隔
                if (this.Config.TxInterval > 0)
                {
                    var interval = DateTime.Now - prevAckTime;
                    while (interval.TotalMilliseconds < this.Config.TxInterval)
                    {
                        if (!this.CfIsRunning) return 0;//若不再執行->直接return
                        var sleep = this.Config.TxInterval - (int)interval.TotalMilliseconds;
                        if (sleep > 0)
                            Thread.Sleep(sleep);
                        interval = DateTime.Now - prevAckTime;
                    }
                }
                prevAckTime = DateTime.Now;

                var reqDataMsg = this.SignalTran.CreateDataReqMsg(this.Config.SignalCfgList);
                this.ProtoConn.WriteMsg(reqDataMsg);

            }


            //收到資料 或 Timeout 就往下走
            this.areMsg.WaitOne(this.Config.TimeoutResponse);

            return 0;
        }
        protected virtual void SignalHandle()
        {
            while (this.ProtoFormat.HasMessage())
            {
                object msg = null;
                if (!this.ProtoFormat.TryDequeueMsg(out msg)) return;

                if (this.ProtoSession.ProcessSession(this.ProtoConn, msg)) continue;


                var eaSignals = this.SignalTran.AnalysisSignal(this, msg, this.Config.SignalCfgList);
                for (var idx = 0; idx < eaSignals.Count; idx++)
                {
                    var eaSignal = eaSignals[idx];

                    eaSignal.Sender = this;
                    eaSignal.CalibrateData = new List<double>();

                    if (eaSignal.Svid == null && this.Config.SignalCfgList.Count > idx)
                        eaSignal.Svid = this.Config.SignalCfgList[idx].Svid;


                    var signalCfg = this.Config.SignalCfgList.FirstOrDefault(x => x.Svid == eaSignal.Svid);
                    if (signalCfg == null) continue;
                    for (int idx_data = 0; idx_data < eaSignal.Data.Count; idx_data++)
                    {
                        var signal = eaSignal.Data[idx_data];
                        //var signal = d / (Math.Pow(2, 23) - 1) * 5; //轉回電壓
                        signal = signal * signalCfg.CalibrateSysScale + signalCfg.CalibrateSysOffset;//轉成System值
                        eaSignal.CalibrateData.Add(signal * signalCfg.CalibrateUserScale + signalCfg.CalibrateUserOffset);//轉入User Define
                    }

                    eaSignal.SignalConfig = signalCfg;
                    eaSignal.RcvDateTime = DateTime.Now;
                    this.OnSignalCapture(eaSignal);
                }
            }
        }



        #region Event
        public event EventHandler<SNetSignalTransEventArgs> EhReceive;
        void OnSignalCapture(SNetSignalTransEventArgs e)
        {
            if (EhReceive == null) return;
            this.EhReceive(this, e);
        }
        #endregion



        #region ICtkContextFlowRun

        /// <summary>
        /// 取得是否正在執行, 可由User設定為false
        /// </summary>
        public bool CfIsRunning { get; set; }
        public virtual int CfRunOnce()
        {
            try
            {
                this.ProtoConn.ConnectIfNoAsyn();//內部會處理重複要求連線
                this.RealExec();

            }
            catch (Exception ex)
            {
                CtkLog.WarnNs(this, ex);
                if (this.Config != null)
                {
                    var mymsg = string.Format("Loca={0}, Remote={1}, DeviceUid={2}", this.Config.LocalUri, this.Config.RemoteUri, this.Config.DeviceUid);
                    CtkLog.WarnNs(this, mymsg);
                }
                Thread.Sleep(3000);//異常斷線後, 先等3秒再繼續
            }
            return 0;
        }
        public virtual int CfFree()
        {
            this.Close();
            return 0;
        }
        public virtual int CfInit()
        {
            if (this.Config == null) throw new SNetException("沒有設定參數");

            var remoteUri = new Uri(this.Config.RemoteUri);
            var localUri = string.IsNullOrEmpty(this.Config.LocalUri) ? null : new Uri(this.Config.LocalUri);
            var localIpAddr = CtkNetUtil.GetSuitableIp(localUri == null ? null : localUri.Host, remoteUri.Host);
            localUri = CtkNetUtil.ToUri(localIpAddr.ToString(), localUri == null ? 0 : localUri.Port);


            switch (this.Config.ProtoConnect)
            {
                case SNetEnumProtoConnect.Tcp:
                    this.ProtoConn = new SNetProtoConnTcp(localUri, remoteUri, this.Config.IsActivelyConnect);
                    break;
                case SNetEnumProtoConnect.Custom:
                    //由使用者自己實作
                    break;
                default: throw new ArgumentException("ProtoConn"); ;
            }

            this.ProtoConn.IntervalTimeOfConnectCheck = this.Config.IntervalTimeOfConnectCheck;
            this.ProtoConn.EhFirstConnect += (ss, ee) =>
            {
                this.ProtoSession.FirstConnect(this.ProtoConn);

                if (this.Config.IsActivelyTx)
                {
                    var ackDataMsg = this.SignalTran.CreateAckMsg(this.Config.SignalCfgList);
                    if (ackDataMsg != null)
                        this.ProtoConn.WriteMsg(ackDataMsg);
                }
                else
                {
                    var reqDataMsg = this.SignalTran.CreateDataReqMsg(this.Config.SignalCfgList);
                    if (reqDataMsg != null)
                    {
                        this.ProtoConn.WriteMsg(reqDataMsg);
                    }
                }
            };
            this.ProtoConn.EhDataReceive += (ss, ee) =>
            {
                var ea = ee as CtkProtocolEventArgs;
                this.ProtoFormat.ReceiveMsg(ea.TrxMessage);
                this.areMsg.Set();

                if (this.ProtoFormat.HasMessage())
                    SignalHandle();
            };




            switch (this.Config.ProtoFormat)
            {
                case SNetEnumProtoFormat.SNetCmd:
                    this.ProtoFormat = new SNetProtoFormatSNetCmd();
                    break;
                case SNetEnumProtoFormat.Secs:
                    this.ProtoFormat = new SNetProtoFormatSecs();
                    break;
                case SNetEnumProtoFormat.Custom:
                    //由使用者自己實作
                    break;
                default: throw new ArgumentException("必須指定ProtoFormat");
            }



            switch (this.Config.ProtoSession)
            {
                case SNetEnumProtoSession.SNetCmd:
                    this.ProtoSession = new SNetProtoSessionSNetCmd();
                    break;
                case SNetEnumProtoSession.Secs:
                    this.ProtoSession = new SNetProtoSessionSecs();
                    break;
                case SNetEnumProtoSession.Custom:
                    //由使用者自己實作
                    break;
                default: throw new ArgumentException("必須指定ProtoFormat");
            }


            switch (this.Config.SignalTran)
            {
                case SNetEnumSignalTrans.SNetCmd:
                    this.SignalTran = new SNetSignalTransSNetCmd();
                    break;
                case SNetEnumSignalTrans.Secs001:
                    this.SignalTran = new SNetSignalTransSecs001();
                    break;
                case SNetEnumSignalTrans.Custom:
                    //由使用者自己實作
                    break;
                default: throw new ArgumentException("必須指定ProtoFormat");
            }






            return 0;
        }
        public virtual int CfLoad()
        {



            return 0;
        }
        public virtual int CfRunLoop()
        {
            this.CfIsRunning = true;
            try
            {

                //三個方法(三個保護)控管執行
                while (!disposed && this.CfIsRunning)
                {
                    if (task != null)
                    {
                        if (this.task.CancelToken.IsCancellationRequested) break;
                        this.task.CancelToken.ThrowIfCancellationRequested();//一般cancel task 在 while 和 第一行
                    }
                    this.CfRunOnce();
                }


                CtkLog.Info("Finish SensorDevice");
            }
            catch (Exception ex) { CtkLog.Write(ex, CtkLoggerEnumLevel.Error); }
            return 0;
        }
        public virtual int CfRunLoopStart()
        {
            if (this.task != null)
                if (!this.task.Wait(100)) return 0;//正在工作

            //the CfRun is loop function
            this.task = CtkTask.RunOnce((ct) => { this.CfRunLoop(); });
            return 0;
        }
        public virtual int CfUnLoad()
        {
            this.Close();
            return 0;
        }

        #endregion

        public void Close()
        {
            this.CfIsRunning = false;
            this.areMsg.Set();//若在等訊號也通知結束等待

            if (this.task != null)
            {
                this.task.Cancel();//取消執行Task
                this.task.Wait(1000);
                this.task.Dispose();
                this.task = null;
            }
            if (this.ProtoConn != null)
            {
                this.ProtoConn.Disconnect();
                this.ProtoConn.Dispose();
                this.ProtoConn = null;
            }
            CtkEventUtil.RemoveEventHandlersOfOwnerByFilter(this, (dlgt) => true);

        }



        #region IDisposable
        // Flag: Has Dispose already been called?
        protected bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public virtual void Dispose()
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





        protected virtual void DisposeSelf()
        {
            this.Close();
        }
        #endregion

    }
}
