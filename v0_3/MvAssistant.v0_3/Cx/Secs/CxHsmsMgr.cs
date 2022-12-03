using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MvaCodeExpress.v1_1.Secs
{



    /// <summary>
    /// HSMS connect manager, 包含Thread, 重新連線等等, 幫使用者管理
    /// </summary>
    public class CxHsmsMgr : IDisposable
    {

        Thread thread;


        public CxHsmsConnector hsmsConnector = new CxHsmsConnector();

        public int reConnTime = 5000;
        bool m_isReConn = false;
        public bool IsReConn
        {
            get { return this.m_isReConn; }
            set { lock (this) { this.m_isReConn = value; } }
        }


        //可Googld查一下為何要在解構用Dispose(false), 主要是Managed Code 在 GC 回收時會叫Dispose(true)
        ~CxHsmsMgr() { this.Dispose(false); }


        public static  void CxSetup()
        {
            CxUtil.CheckLicenseNonEncrypt();
        }






        #region event


        public event EventHandler eventConnectComplete;
        public void OnConnectComplete()
        {
            if (this.eventConnectComplete == null)
                return;

            this.eventConnectComplete(this, new EventArgs());
        }



        public event EventHandler eventDisconnect;
        public void OnDisconnect()
        {
            if (this.eventDisconnect == null)
                return;

            this.eventDisconnect(this, new EventArgs());
        }



        #endregion






        #region Dispose

        bool disposed = false;
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any managed objects here.
            }

            // Free any unmanaged objects.
            //
            this.DisposeSelf();
            disposed = true;
        }


    


        public virtual void DisposeSelf()
        {
            this.IsReConn = false;
            if (this.thread != null && this.thread.IsAlive)
                this.thread.Abort();
            this.thread = null;

            if (this.hsmsConnector != null)
                this.hsmsConnector.Dispose();
        }

        #endregion
    }
}
