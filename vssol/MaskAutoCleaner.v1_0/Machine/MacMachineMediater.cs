using MaskAutoCleaner.v1_0.Machine.Cabinet.DrawerQueues;
using MvAssistant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine
{
    /// <summary>
    /// Design Pattern - Mediator Pattern
    /// </summary>
    public class MacMachineMediater : IDisposable
    {

        protected MacMachineMgr MachineMgr;//存取此物件需要透過Mediater, 不得開放給其它物件使用

        public MaskBoxForBankOutQue MaskBoxForBankOutQue = MaskBoxForBankOutQue.GetInstance();

        public MacMachineMediater(MacMachineMgr machineMgr) { this.MachineMgr = machineMgr; }
        ~MacMachineMediater() { this.Dispose(false); }




        public MacMachineCtrlBase GetCtrlMachine(string machineId)
        {
            if (!this.MachineMgr.CtrlMachines.ContainsKey(machineId)) return null;
            return this.MachineMgr.CtrlMachines[machineId];
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

        }


        #endregion


    }
}
