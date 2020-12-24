using CodeExpress.v1_0.Secs;
using CToolkit;
using CToolkit.v1_1;
using CToolkit.v1_1.Config;
using CToolkit.v1_1.Logging;
using SensingNet.v0_2.QSecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SensingNet.v0_2.Framework
{
    public class SNetQSecsMgr : ICtkContextFlowRun, IDisposable
    {
        public CtkConfigCollector<SNetQSecsCfg> configs = new CtkConfigCollector<SNetQSecsCfg>();
        public String DefaultConfigsFolder = "Config/QSecsConfigs/";
        public Dictionary<String, SNetQSecsHandler> handlers = new Dictionary<String, SNetQSecsHandler>();
        Task<int> runTask;


        ~SNetQSecsMgr() { this.Dispose(false); }


        #region ICtkContextFlowRun
        public bool CfIsRunning { get; set; }
        public int CfRunOnce()
        {
            try
            {
                if (!Monitor.TryEnter(this, 5 * 1000)) return -1;

                this.configs.UpdateIfTimeout();
                this.RunHandlerStatus();
            }
            finally
            {
                Monitor.Exit(this);
            }
            try { this.OnAfterEachExec(new EventArgs()); }
            catch (Exception ex) { CtkLog.Write(ex, CtkLoggerEnumLevel.Warn); }

            return 0;
        }
        public int CfFree()
        {
            this.configs.ClearAll();
            this.RunHandlerStatus();
            this.Dispose(false);
            return 0;
        }
        public int CfInit()
        {
            this.configs.UpdateFromFolder(DefaultConfigsFolder);


            return 0;
        }
        public int CfLoad()
        {
            return 0;
        }
        public int CfRunLoop()
        {
            this.CfIsRunning = true;
            while (!this.disposed && this.CfIsRunning)
            {
                try
                {
                    this.CfRunOnce();
                    Thread.Sleep(1000);
                }
                catch (Exception ex) { CtkLog.Write(ex); }
            }

            return 0;
        }
        public int CfRunLoopAsyn()
        {
            if (this.runTask != null)
                if (!this.runTask.Wait(100)) return 0;//正在工作

            this.runTask = Task.Factory.StartNew<int>(() => this.CfRunLoop());
            return 0;
        }
        public int CfUnLoad()
        {
            this.configs.ClearAll();
            this.RunHandlerStatus();
            return 0;
        }
        #endregion 

        void RunHandlerStatus()
        {
            if (this.disposed) return;

            //先全部設定為: 等待Dispose
            foreach (var hdl in this.handlers)
            {
                hdl.Value.IsWaitDispose = true;
            }


            //Run過所有Config
            //有Config的會解除等待Dispoe
            //有Config的會執行CfRun
            foreach (var dict in this.configs)
                foreach (var cfg in dict.Value)
                {
                    SNetQSecsHandler hdl = null;
                    if (!this.handlers.ContainsKey(cfg.Key))
                    {
                        hdl = new SNetQSecsHandler();
                        this.handlers.Add(cfg.Key, hdl);
                    }
                    else { hdl = this.handlers[cfg.Key]; }
                    hdl.cfg = cfg.Value;

                    //解除等待Dispoe
                    hdl.IsWaitDispose = false;

                    if (hdl.status == SNetEnumHandlerStatus.None)
                    {
                        hdl.CfInit();
                        hdl.EhReceiveData += delegate (object ss, CxHsmsConnectorRcvDataEventArg ea)
                        {
                            this.OnReceiveData(new SNetQSecsRcvDataEventArgs()
                            {
                                handler = ss as SNetQSecsHandler,
                                message = ea.msg
                            });
                        };
                        hdl.status = SNetEnumHandlerStatus.Init;
                    }


                    if (hdl.status == SNetEnumHandlerStatus.Init)
                    {
                        hdl.CfLoad();
                        hdl.status = SNetEnumHandlerStatus.Load;
                    }

                    //有Config的持續作業
                    if (hdl.status == SNetEnumHandlerStatus.Load || hdl.status == SNetEnumHandlerStatus.Run)
                    {

                        hdl.status = SNetEnumHandlerStatus.Run;
                        hdl.CfRunLoop();
                    }

                }


            //沒有Config的會關閉
            var removeHandlers = new Dictionary<String, SNetQSecsHandler>();
            foreach (var qsh in this.handlers)
            {
                if (!qsh.Value.IsWaitDispose) continue;

                qsh.Value.CfUnLoad();
                qsh.Value.status = SNetEnumHandlerStatus.Unload;


                qsh.Value.CfFree();
                qsh.Value.status = SNetEnumHandlerStatus.Free;
                removeHandlers[qsh.Key] = qsh.Value;
            }
            foreach (var kvdh in removeHandlers)
            {
                this.handlers.Remove(kvdh.Key);
            }
        }

        #region Event

        public event EventHandler<SNetQSecsRcvDataEventArgs> EhReceiveData;
        public void OnReceiveData(SNetQSecsRcvDataEventArgs ea)
        {
            if (this.EhReceiveData == null) return;
            this.EhReceiveData(this, ea);
        }

        public event EventHandler EhAfterEachExec;
        public void OnAfterEachExec(EventArgs ea)
        {
            if (this.EhAfterEachExec == null) return;
            this.EhAfterEachExec(this, ea);
        }


        #endregion

        #region Dispose

        protected bool disposed = false;
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        protected virtual void DisposeSelf()
        {

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
