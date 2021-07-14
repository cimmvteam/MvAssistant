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

        MvaSsProtoCmdBuffer Buffer = new MvaSsProtoCmdBuffer();
        public MvaSsProtoCmdCfg Config = new MvaSsProtoCmdCfg();


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

                var ackDataMsg = this.Buffer.CreateAckMsg(this.Config.SvidConfigs);
                if (ackDataMsg != null)
                    this.tcpClient.WriteMsg(ackDataMsg);
            }
            else
            {
                //等待下次要求資料的間隔
                if (this.Config.TxIntervalMs > 0)
                {
                    var interval = DateTime.Now - prevAckTime;
                    while (interval.TotalMilliseconds < this.Config.TxIntervalMs)
                    {
                        if (!this.CfIsRunning) return 0;//若不再執行->直接return
                        var sleep = this.Config.TxIntervalMs - (int)interval.TotalMilliseconds;
                        if (sleep > 0)
                            Thread.Sleep(sleep);
                        interval = DateTime.Now - prevAckTime;
                    }
                }
                prevAckTime = DateTime.Now;

                var reqDataMsg = this.Buffer.CreateDataReqMsg(this.Config.SvidConfigs);
                this.tcpClient.WriteMsg(reqDataMsg);

            }


            //收到資料 或 Timeout 就往下走
            this.areMsg.WaitOne(this.Config.TimeoutResponse);

            return 0;
        }
        protected virtual void SignalHandle()
        {
            while (this.Buffer.HasMessage())
            {
                string msg = null;
                if (!this.Buffer.TryDequeueMsg(out msg)) return;

                if (this.Buffer.ProcessSession(this.tcpClient, msg)) continue;


                var msgEventArgs = this.Buffer.Parse(msg as string);

                for (var idx = 0; idx < msgEventArgs.Count; idx++)
                {
                    var ea = msgEventArgs[idx];

                    ea.Sender = this;

                    if (ea.Svid == null || this.Config.SvidConfigs.Count > idx)
                        continue;

                    var svidConfig = this.Config.SvidConfigs[idx];
                    ea.SvidConfig = svidConfig;
                    ea.Svid = svidConfig.Svid;
                    ea.RcvDateTime = DateTime.Now;

                    for (int idx_data = 0; idx_data < ea.Data.Count; idx_data++)
                    {
                        var signal = ea.Data[idx_data];
                        //var signal = d / (Math.Pow(2, 23) - 1) * 5; //轉回電壓
                        signal = signal * svidConfig.CalibrateSysScale + svidConfig.CalibrateSysOffset;//轉成System值
                        ea.CalibrateData.Add(signal * svidConfig.CalibrateUserScale + svidConfig.CalibrateUserOffset);//轉入User Define
                    }


                    this.OnSignalCapture(ea);
                }
            }
        }



        #region Event
        public event EventHandler<MvaSsProtoCmdMsg> EhReceive;
        void OnSignalCapture(MvaSsProtoCmdMsg e)
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
                this.tcpClient.ConnectIfNoAsyn();//內部會處理重複要求連線
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
            if (this.Config == null) throw new MvaException("沒有設定參數");

            var remoteUri = new Uri(this.Config.RemoteUri);
            var localUri = string.IsNullOrEmpty(this.Config.LocalUri) ? null : new Uri(this.Config.LocalUri);
            var localIpAddr = CtkNetUtil.GetSuitableIp(localUri == null ? null : localUri.Host, remoteUri.Host);
            localUri = CtkNetUtil.ToUri(localIpAddr.ToString(), localUri == null ? 0 : localUri.Port);



            this.tcpClient = new CtkTcpClient(remoteUri, localUri);
            this.tcpClient.EhFirstConnect += (ss, ee) =>
            {
                this.Buffer.FirstConnect(this.tcpClient);

                if (this.Config.IsActivelyTx)
                {
                    var ackDataMsg = this.Buffer.CreateAckMsg(this.Config.SvidConfigs);
                    if (ackDataMsg != null)
                        this.tcpClient.WriteMsg(ackDataMsg);
                }
                else
                {
                    var reqDataMsg = this.Buffer.CreateDataReqMsg(this.Config.SvidConfigs);
                    if (reqDataMsg != null)
                    {
                        this.tcpClient.WriteMsg(reqDataMsg);
                    }
                }
            };
            this.tcpClient.EhDataReceive += (ss, ee) =>
            {
                var ea = ee as CtkProtocolEventArgs;
                this.Buffer.ReceiveMsg(ea.TrxMessage);
                this.areMsg.Set();

                if (this.Buffer.HasMessage())
                    SignalHandle();
            };


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
            if (this.tcpClient != null)
            {
                CtkNetUtil.DisposeTcpClientTry(this.tcpClient);
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
