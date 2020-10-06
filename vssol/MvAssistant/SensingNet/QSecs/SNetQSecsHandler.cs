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

        public CxHsmsConnector hsmsConnector;

        /// <summary>
        /// 一個Secs Handler需要一組IP/Port
        /// 對一個 IP/Port 而言, Svid 不應該重複, 因此用 Query SVID 作為Key
        /// </summary>
        public SNetEnumHandlerStatus status = SNetEnumHandlerStatus.None;
        public bool IsWaitDispose;

        #region ICtkContextFlowRun

        public bool CfIsRunning { get; set; }
        public int CfRunOnce()
        {
            return 0;
        }

        public int CfFree()
        {
            this.Dispose(false);
            return 0;
        }

        public int CfInit()
        {
            hsmsConnector = new CxHsmsConnector();
            //hsmsConnector.ctkConnSocket.isActively = true;

            var localUri = string.IsNullOrEmpty(this.cfg.LocalUri) ? null : new Uri(this.cfg.LocalUri);
            var remoteUri = string.IsNullOrEmpty(this.cfg.RemoteUri) ? null : new Uri(this.cfg.RemoteUri);

            var localIp = CtkNetUtil.GetLikelyFirst127Ip(localUri == null ? null : localUri.Host, remoteUri == null ? null : remoteUri.Host);
            if (localIp == null) throw new Exception("無法取得在地IP");
            hsmsConnector.Local = new IPEndPoint(localIp, localUri.Port);
            hsmsConnector.EhReceiveData += delegate(Object sen, CxHsmsConnectorRcvDataEventArg ea)
            {

                var myMsg = ea.msg;


                //System.Diagnostics.Debug.WriteLine("S{0}F{1}", myMsg.header.StreamId, myMsg.header.FunctionId);
                //System.Diagnostics.Debug.WriteLine("SType= {0}", myMsg.header.SType);

                switch (myMsg.header.SType)
                {
                    case 1:
                        hsmsConnector.Send(CxHsmsMessage.CtrlMsg_SelectRsp(0));
                        return;
                    case 5:
                        hsmsConnector.Send(CxHsmsMessage.CtrlMsg_LinktestRsp());
                        return;
                }

                this.OnReceiveData(myMsg);

            };



            return 0;
        }
        public int CfLoad()
        {
            CtkThreadingUtil.RunWorkerAsyn(delegate(object sender, DoWorkEventArgs e)
            {
                for (int idx = 0; !this.disposed; idx++)
                {
                    try
                    {
                        hsmsConnector.ConnectIfNo();
                    }
                    catch (Exception ex)
                    {
                        CtkLog.Write(ex);
                        System.Threading.Thread.Sleep(5000);//無法連線等待5秒
                    }


                    try
                    {
                        hsmsConnector.ReceiveRepeat();
                    }
                    catch (Exception ex)
                    {
                        CtkLog.Write(ex);
                        System.Threading.Thread.Sleep(5000);//無法連線等待5秒
                    }
                    finally { System.Threading.Thread.Sleep(1000); }
                }
            });
            return 0;
        }
        public int CfRunLoop() { return 0; }
        public int CfRunLoopAsyn() { return 0; }
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
            if (this.hsmsConnector != null)
            {
                this.hsmsConnector.Dispose();
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
