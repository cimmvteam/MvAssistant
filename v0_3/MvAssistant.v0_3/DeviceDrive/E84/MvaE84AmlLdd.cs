using MvaCToolkitCs.v1_2;
using MvaCToolkitCs.v1_2.DigitalPort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.E84
{
    public class MvaE84AmlLdd : IDisposable
    {
        public CtkNonStopSerialPort SerialPort = new CtkNonStopSerialPort();





        public int Connect(string portName = null, int? buadRate = null)
        {
            if (!string.IsNullOrEmpty(portName))
                this.SerialPort.Config.PortName = portName;
            if (buadRate.HasValue)
                this.SerialPort.Config.BaudRate = buadRate.Value;



            this.SerialPort.ConnectTry();

            return 0;
        }



        public void Close()
        {
            try
            {
                if (this.SerialPort != null)
                    using (var obj = this.SerialPort) { obj.Disconnect(); }
            }
            catch (Exception ex) { CtkLog.WarnAn(this, ex); }
        }



        public void ReadFirmware()
        {
            var msg = new byte[] { 0x55, 0x00, 0x00, 0xbb };
            this.SerialPort.WriteMsg(msg);






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
