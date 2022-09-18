﻿using CToolkitCs.v1_1.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.DeviceDrive.GudengLoadPort
{
    [Obsolete]
    public class MvGudengLoadPortCollection : IDisposable
    {

        public List<MvGudengLoadPortLdd> LoadPorts { get; private set; }

        public MvGudengLoadPortCollection()
        {
            LoadPorts = new List<MvGudengLoadPortLdd>();
        }

        public MvGudengLoadPortLdd CreateLoadPort(string serverIP,int serverPort,int loadportNO)
        {
            MvGudengLoadPortLdd loadport = new MvGudengLoadPortLdd(serverIP, serverPort, loadportNO);
            LoadPorts.Add(loadport);
            return loadport;

        }
        public int ConnectTry(string ip = null, int? port = null)
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
