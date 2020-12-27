using CToolkit;
using CToolkit.v1_1;
using CToolkit.v1_1.Config;
using SensingNet.v0_2.DvcSensor;
using SensingNet.v0_2.DvcSensor.SignalTrans;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SensingNet.v0_2.Framework
{
    public class SNetDeviceSensorMgr : IDisposable, ICtkContextFlowRun
    {

        public String DefaultConfigsFilder = "Config/DeviceConfigs";
        public CtkConfigCollector<SNetDvcSensorCfg> configs = new CtkConfigCollector<SNetDvcSensorCfg>();
        Dictionary<String, SNetDvcSensorHandler> handlers = new Dictionary<String, SNetDvcSensorHandler>();
        Task<int> runTask;



        ~SNetDeviceSensorMgr() { this.Dispose(false); }


        public bool CfIsRunning { get; set; }
        public int CfInit()
        {

            return 0;
        }
        public int CfLoad()
        {
            this.CfIsRunning = true;
            this.configs.UpdateFromFolder(DefaultConfigsFilder);

            return 0;
        }
        public int CfRunOnce()
        {
            this.configs.UpdateIfTimeout();
            this.UpdateHandlerStatus();
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
                    System.Threading.Thread.Sleep(1000);
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
            this.CfIsRunning = false;
            this.configs.ClearAll();
            this.UpdateHandlerStatus();
            return 0;
        }
        public int CfFree()
        {
            this.CfIsRunning = false;
            this.configs.ClearAll();
            this.UpdateHandlerStatus();
            return 0;
        }



        void UpdateHandlerStatus()
        {
            //是否存在, 不需dispose
            var dict = new Dictionary<string, bool>();

            //先全部設定為: 等待Dispose
            foreach (var kvhdl in this.handlers)
                dict[kvhdl.Key] = false;


            //Run過所有Config
            //有Config的會解除等待Dispoe
            //有Config的會執行CfRun
            foreach (var dictcfg in this.configs)
                foreach (var kvcfg in dictcfg.Value)
                {
                    SNetDvcSensorHandler hdl = null;
                    if (!this.handlers.ContainsKey(kvcfg.Key))
                    {
                        hdl = new SNetDvcSensorHandler();
                        this.handlers.Add(kvcfg.Key, hdl);
                    }
                    else { hdl = this.handlers[kvcfg.Key]; }
                    hdl.Config = kvcfg.Value;

                    //解除等待Dispoe
                    dict[kvcfg.Key] = true;

                    if (hdl.Status == SNetEnumHandlerStatus.None)
                    {
                        hdl.CfInit();
                        hdl.EhSignalCapture += delegate (object sender, SNetSignalTransEventArgs e)
                        {
                            e.Sender = hdl;
                            this.OnSignalCapture(e);
                        };

                        hdl.Status = SNetEnumHandlerStatus.Init;
                    }
                    if (hdl.Status == SNetEnumHandlerStatus.Init)
                    {
                        hdl.CfLoad();
                        hdl.Status = SNetEnumHandlerStatus.Load;
                    }

                    //有Config的持續作業
                    if (hdl.Status == SNetEnumHandlerStatus.Load || hdl.Status == SNetEnumHandlerStatus.Run)
                    {
                        hdl.Status = SNetEnumHandlerStatus.Run;
                        if (!hdl.CfIsRunning)
                            hdl.CfRunLoopAsyn();
                    }

                }


            //沒有Config的會關閉Device
            foreach (var kv in dict)
            {
                if (kv.Value) continue;
                var hdl = this.handlers[kv.Key];

                if (hdl.Status == SNetEnumHandlerStatus.Run)
                {
                    hdl.CfUnLoad();
                    hdl.Status = SNetEnumHandlerStatus.Unload;
                }
                if (hdl.Status == SNetEnumHandlerStatus.Unload)
                {
                    hdl.CfFree();
                    hdl.Status = SNetEnumHandlerStatus.Free;
                }
                if (hdl.Status == SNetEnumHandlerStatus.Free)
                {
                    this.handlers.Remove(kv.Key);
                }
            }


        }





        #region Event
        public event EventHandler<SNetSignalTransEventArgs> EhSignalCapture;
        void OnSignalCapture(SNetSignalTransEventArgs e)
        {
            if (EhSignalCapture == null) return;
            this.EhSignalCapture(this, e);
        }

        #endregion

        #region Event Handler



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
            this.CfIsRunning = false;
        }

        #endregion
    }
}
