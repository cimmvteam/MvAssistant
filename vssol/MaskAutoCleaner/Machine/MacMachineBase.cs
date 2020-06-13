using MaskAutoCleaner.Msg;
using MvAssistant;
using MvAssistant.Mac.v1_0.Hal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Machine
{
    public abstract class MacMachineBase
    {
        protected MacHalAssemblyBase halAssembly;
        protected MacMachineSmBase smAssembly;

        ~MacMachineBase() { this.Dispose(false); }




        /// <summary>
        /// 訊息接收窗口, 只能從這邊交辨任務
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public abstract int RequestProcMsg(MsgBase msg);



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
            MvEventUtil.RemoveEventHandlersOfOwnerByFilter(this, (dlgt) => true);
        }
        #endregion

    }
}
