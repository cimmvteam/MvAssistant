using MaskAutoCleaner.Machine;
using MvAssistant.Mac.v1_0.Hal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Machine
{
    public class MacMachineMgr : IDisposable
    {
        protected MacHalContext HalContext = new MacHalContext();
        public MacMachineMediater Mediater = new MacMachineMediater();
        public Dictionary<string, MacMachineBase> Machines = new Dictionary<string, MacMachineBase>();


        public void Initial()
        {



        }






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
            foreach (var kv in this.Machines)
            {
                if (kv.Value == null) continue;
                kv.Value.Dispose();

            }


        }

        #endregion


    }
}
