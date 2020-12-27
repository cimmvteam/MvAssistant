using CToolkit.v1_1;
using MaskAutoCleaner.v1_0.Msg;
using MvAssistant.v0_2;
using MvAssistant.v0_2.Mac.Hal;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine
{
    /// <summary>
    /// Machine Controller Base
    /// </summary>
    public abstract class MacMachineCtrlBase : IMvaContextFlow, IMacMsgProcessor, IDisposable
    {
        public MacHalAssemblyBase HalAssembly { get { return this.MsAssembly.halAssembly; } set { this.MsAssembly.halAssembly = value; } }
        public MacMachineStateBase MsAssembly;
        public MacMachineMediater Mediater { get { return this.MsAssembly.Mediater; } set { this.MsAssembly.Mediater = value; } }
        public ConcurrentBag<MacMachineCtrlBase> CtrlMachines = new ConcurrentBag<MacMachineCtrlBase>();



        ~MacMachineCtrlBase() { this.Dispose(false); }




        /// <summary>
        /// 訊息接收窗口, 只能從這邊交辨任務
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public abstract int RequestProcMsg(IMacMsg msg);




        #region IMvContextFlow
        public virtual int MvCfInit() { return 0; }
        public virtual int MvCfLoad() { return 0; }
        public virtual int MvCfUnload() { return 0; }
        public virtual int MvCfFree()
        {
            this.DisposeSelf();
            return 0;
        }
        #endregion


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
            //Hal統一由HalContext釋放
            //MvUtil.DisposeObjTry(this.halAssembly);
            CtkEventUtil.RemoveEventHandlersOfOwnerByFilter(this, (dlgt) => true);
        }

        #endregion

        #region Other
        #endregion

    }
}
