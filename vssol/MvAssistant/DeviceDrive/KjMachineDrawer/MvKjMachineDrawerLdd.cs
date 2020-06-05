using CToolkit.v1_1.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.KjMachineDrawer
{
    public class MvKjMachineDrawerLdd : IDisposable
    {
        public UdpServerSocket UdpServer;
        public int ListenPort = 6000;
        public int ClientPort = 5000;
        public List<Drawer> Drawers = null;
        private static readonly object Lockobj = new object();
        public   MvKjMachineDrawerLdd()
        {
            Drawers = new List<Drawer>();
        }

      
        public Drawer CreateDrawer(int cabinetNo,int drawerNo,string targetIP,int targetPort)
        {

            Drawer drawer = new Drawer(cabinetNo, drawerNo, targetIP, targetPort);
            Drawers.Add(drawer);
            return drawer;

        }



        public int ConnectIfNo(string ip = null, int? port = null)
        {
        



            return 0;
        }


        public void Close()
        {


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
