using CToolkit;
using CToolkit.v1_1.Numeric;
using CToolkit.v1_1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using CToolkit.v1_1.Net;
using CToolkit.v1_1.Threading;
using CodeExpress.v1_0.Secs;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using CToolkit.v1_1.ContextFlow;

namespace SensingNet.v0_2.QSecs
{
    /// <summary>
    /// 提供一個IP/Port的處理機制
    /// 具Secs被動連線功能
    /// 可將Device收到的資料做簡易處理
    /// 若不需要簡易資料處理, 可自行撰寫, 提供Secs通訊
    /// </summary>
    public class SNetQSecsHandler : ICtkContextFlowRun, IDisposable
    {
        public SNetQSecsCfg cfg;
        [JsonIgnore]
        public CxHsmsConnector HsmsConnector;
        CtkTask RunningTask;

        public bool IsWaitDispose;

        /// <summary>
        /// 一個Secs Handler需要一組IP/Port
        /// 對一個 IP/Port 而言, Svid 不應該重複, 因此用 Query SVID 作為Key
        /// </summary>
        public SNetEnumHandlerStatus status = SNetEnumHandlerStatus.None;


        #region ICtkContextFlowRun

        public bool CfIsRunning { get; set; }
        public int CfFree()
        {
            this.Dispose(false);
            return 0;
        }
        public int CfInit()
        {
            this.HsmsConnector = new CxHsmsConnector();
            //hsmsConnector.ctkConnSocket.isActively = true;

            var localUri = string.IsNullOrEmpty(this.cfg.LocalUri) ? null : new Uri(this.cfg.LocalUri);
            var remoteUri = string.IsNullOrEmpty(this.cfg.RemoteUri) ? null : new Uri(this.cfg.RemoteUri);

            var localIp = CtkNetUtil.GetIpAdr1stLikelyOr127(localUri == null ? null : localUri.Host, remoteUri == null ? null : remoteUri.Host);
            if (localIp == null) throw new Exception("無法取得在地IP");
            this.HsmsConnector.LocalUri = CtkNetUtil.ToUri(localIp.ToString(), localUri == null ? 0 : localUri.Port);
            this.HsmsConnector.EhReceiveData += delegate (Object sen, CxHsmsConnectorRcvDataEventArg ea)
            {

                var myMsg = ea.msg;


                //System.Diagnostics.Debug.WriteLine("S{0}F{1}", myMsg.header.StreamId, myMsg.header.FunctionId);
                //System.Diagnostics.Debug.WriteLine("SType= {0}", myMsg.header.SType);

                switch (myMsg.header.SType)
                {
                    case 1:
                        HsmsConnector.Send(CxHsmsMessage.CtrlMsg_SelectRsp(0));
                        return;
                    case 5:
                        HsmsConnector.Send(CxHsmsMessage.CtrlMsg_LinktestRsp());
                        return;
                }

                this.OnReceiveData(myMsg);

            };



            return 0;
        }
        public int CfLoad() { return 0; }
        public int CfRunLoop()
        {
            while (!this.disposed)
            {
                this.CfRunOnce();
                Thread.Sleep(5 * 1000);
            }

            return 0;
        }
        public int CfRunLoopStart()
        {
            //還在執行中, 不接受重新執行
            if (this.RunningTask != null && this.RunningTask.Status < TaskStatus.RanToCompletion) return 1;

            //需要重複確認機台的功能是活著的, 因此使用RunLoop重複運作
            this.RunningTask = CtkTask.RunLoop(() =>
            {
                try
                {
                    this.CfRunOnce();
                }
                catch (Exception ex)
                {
                    //Task root method 需要try/catch: 已經到最上層, 就必須捕抓->寫log, 否則就看不到這個例外
                    CtkLog.WriteNs(this, ex);
                }
                Thread.Sleep(5 * 1000);
                return !this.disposed;//d20201210 機台仍舊要持續運作
            });

            return 0;
        }
        public int CfRunOnce()
        {
            //持續確認連線狀態, 有需要就重新連線
            this.HsmsConnector.ConnectTryStart();
            //HsmsConnector.ReceiveLoop();//IsAutoReceive=true
            return 0;
        }
        public int CfUnLoad()
        {
            return 0;
        }

        #endregion



        public SNetQSvidCfg GetQSvidCfg(UInt32 svid)
        {
            var query = from row in this.cfg.QSvidCfgList
                        where row.QSvid == svid
                        select row;

            return query.FirstOrDefault();
        }



        #region Event

        public event EventHandler<CxHsmsConnectorRcvDataEventArg> EhReceiveData;
        public void OnReceiveData(CxHsmsMessage msg)
        {
            if (this.EhReceiveData == null)
                return;

            this.EhReceiveData(this, new CxHsmsConnectorRcvDataEventArg() { msg = msg });
        }

        #endregion


        #region Dispose

        bool disposed = false;


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }



        public void DisposeSelf()
        {
            if (this.HsmsConnector != null)
            {
                this.HsmsConnector.Dispose();
            }

        }



        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any managed objects here.
            }

            // Free any unmanaged objects here.
            //
            this.DisposeSelf();
            disposed = true;
        }
        #endregion

    }
}
