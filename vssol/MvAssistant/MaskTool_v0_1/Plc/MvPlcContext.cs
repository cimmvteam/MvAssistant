using MvAssistant.DeviceDrive.OmronPlc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvAssistant.MaskTool_v0_1.Plc
{
    public class MvPlcContext:IDisposable
    {

        public MvOmronPlcLdd OmronPlcCompolet;

        public MvPlcInspChStage IcStage;

        public MvPlcContext()
        {
            this.IcStage = new MvPlcInspChStage(this);
        }
        ~MvPlcContext() { this.Dispose(false); }



        public T Read<T>(MvEnumPlcVariable plcvar)
        {
            throw new NotImplementedException();
        }
        public void Write(MvEnumPlcVariable plcvar, object data)
        {
            throw new NotImplementedException();
        }



        public int Connect()
        {
            this.OmronPlcCompolet = new MvOmronPlcLdd();
            this.OmronPlcCompolet.NLPLC_Initial("192.168.0.200", 5000);
            
            this.Read<bool>(MvEnumPlcVariable.plc_on);

            //TODO:



            return 0;
        }

        public void Close()
        {
            using(var obj = this.OmronPlcCompolet)
            {
            }
        }




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
            this.Close();
        }



        #endregion


    }
}
