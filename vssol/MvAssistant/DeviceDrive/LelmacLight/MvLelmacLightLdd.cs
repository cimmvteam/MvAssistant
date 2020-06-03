using CToolkit.v1_1.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.LelmacLight
{
    public class MvLelmacLightLdd : IDisposable
    {

        public CtkNonStopTcpClient TcpClient;



        public MvLelmacLightLdd()
        {

        }


        public int ConnectIfNo(string ip = null, int? port = null)
        {
            this.TcpClient = new CtkNonStopTcpClient();
            this.TcpClient.ConnectIfNo();



            return 0;
        }


        public void Close()
        {
            try
            {
                if (this.TcpClient != null)
                    using (var obj = this.TcpClient) { obj.Disconnect(); }
            }
            catch (Exception ex) { MvLog.WarnNs(this, ex); }
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
