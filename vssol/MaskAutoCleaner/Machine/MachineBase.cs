using MaskAutoCleaner.Msg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Machine
{
    public abstract class MachineBase : IDisposable
    {
        ~MachineBase() { this.Dispose(false); }


        public virtual int Init() { return 0; }
        public virtual int Load() { return 0; }
        public virtual int RunLoop() { return 0; }
        public virtual int Unload() { return 0; }
        public virtual int Free()
        {
            this.Dispose(false);
            return 0;
        }



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
        }
        #endregion


    }
}
