using MaskAutoCleaner.v1_0.Machine;
using MaskAutoCleaner.v1_0.Recipe;
using MvAssistant;
using MvAssistant.Mac.v1_0.Hal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine
{
    public class MacMachineMgr : IMvContextFlow, IDisposable
    {
        public Dictionary<string, MacMachineCtrlBase> CtrlMachines = new Dictionary<string, MacMachineCtrlBase>();
        public MacMachineMediater Mediater;
        protected MacMachineMgrCfg Config;
        protected MacHalContext HalContext;
        public MacRecipeMgr RecipeMgr;

        /// <summary>
        /// 程式關閉或垃圾回收時會進行解構
        /// </summary>
        ~MacMachineMgr() { this.Dispose(false); }



        #region IMvContextFlow

        public int MvCfInit()
        {
            this.Mediater = new MacMachineMediater(this);
            this.RecipeMgr = new MacRecipeMgr(this);


            this.Config = MacMachineMgrCfg.LoadFromXmlFile();//先載入整機的Config

            this.HalContext = new MacHalContext(this.Config.ManifestCfgPath);//將Manifest路徑交給HalContext載入
            this.HalContext.MvCfInit();

            foreach (var row in this.Config.MachineCtrls)
            {
                //Create machine controller
                var machine = Activator.CreateInstance(row.MachineCtrlType.Type) as MacMachineCtrlBase;
                this.CtrlMachines[row.ID] = machine;

                //Initial machine controller
                machine.Mediater = this.Mediater;



                //Assign HAL to machine controller
                var hal = this.HalContext.HalDevices.Where(x => x.Value.ID == row.HalId).FirstOrDefault();

                if (Config.ManifestCfgPath.Contains("fake"))
                {
                    machine.HalAssembly = hal.Value as MacHalAssemblyBase;
                }
                else
                {
                    machine.HalAssembly = hal.Value as MacHalAssemblyBase;
                    machine.HalAssembly.HalConnect();
                }
            }
            MvUtil.Foreach(this.CtrlMachines.Values, m => m.MvCfInit());

            return 0;
        }

        public int MvCfLoad()
        {
            this.HalContext.MvCfLoad();
            MvUtil.Foreach(this.CtrlMachines.Values, m => m.MvCfLoad());
            return 0;
        }

        public int MvCfUnload()
        {
            this.HalContext.MvCfUnload();
            MvUtil.Foreach(this.CtrlMachines.Values, m => m.MvCfUnload());
            return 0;
        }

        public int MvCfFree()
        {
            this.MvCfFree();
            MvUtil.Foreach(this.CtrlMachines.Values, m => m.MvCfFree());
            this.DisposeSelf();
            return 0;
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

            MvUtil.DisposeObjTry(this.CtrlMachines.Values);
        }


        #endregion



        #region Other
        #endregion

    }
}
