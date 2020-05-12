using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Dal
{
    public abstract class MacDbMgr : IDisposable
    {

        protected Dictionary<string, bool> isOpennedDb = new Dictionary<string, bool>();



        public virtual DbConnection DbBasic_Get() { throw new NotImplementedException(); }
        public virtual MacDbContext GetDbContext(DbConnection dbConn) { throw new NotImplementedException(); }



        #region IDisposable

        // Flag: Has Dispose already been called?
        bool disposed = false;

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
                this.DisposeManaged();
            }

            // Free any unmanaged objects here.
            //
            this.DisposeUnmanaged();

            this.DisposeSelf();

            disposed = true;
        }



        void DisposeManaged()
        {
        }

        void DisposeUnmanaged()
        {
        }

        void DisposeSelf()
        {
        }

        #endregion

    }
}
