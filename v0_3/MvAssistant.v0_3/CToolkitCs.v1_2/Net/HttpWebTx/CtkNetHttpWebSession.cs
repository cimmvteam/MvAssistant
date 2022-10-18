using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MvaCToolkitCs.v1_2.Net.HttpWebTx
{
    public class CtkNetHttpWebSession : IDisposable
    {
        public List<CtkNetHttpWebTransaction> Transaction = new List<CtkNetHttpWebTransaction>();
        public CookieContainer CookieContainer = new CookieContainer();

        ~CtkNetHttpWebSession() { this.Dispose(false); }





        public CtkNetHttpWebTransaction Create(String url)
        {
            var tx = CtkNetHttpWebTransaction.HttpRequestTx(url);
            tx.HwRequest.CookieContainer = this.CookieContainer;
            this.Transaction.Add(tx);
            return tx;
        }


        public CtkNetHttpWebTransaction Create(String url, System.Net.Cache.RequestCacheLevel cachePolicy, String httpMethod = "GET")
        {
            var tx = CtkNetHttpWebTransaction.HttpRequestTx(url, cachePolicy, httpMethod);
            var hwreq = tx.HwRequest;
            hwreq.CookieContainer = this.CookieContainer;

            this.Transaction.Add(tx);
            return tx;
        }

        public CtkNetHttpWebTransaction CreateHttpGet(String url, System.Net.Cache.RequestCacheLevel cachePolicy) { return this.Create(url, cachePolicy, "GET"); }
        public CtkNetHttpWebTransaction CreateHttpPost(String url, System.Net.Cache.RequestCacheLevel cachePolicy) { return this.Create(url, cachePolicy, "POST"); }


        public void Close()
        {
            foreach (var tx in this.Transaction)
            {
                try { tx.Dispose(); }
                catch (Exception) { }
            }

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
        protected void Dispose(bool disposing)
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
        void DisposeSelf()
        {
            try { this.Close(); }
            catch (Exception ex) { CtkLog.Write(ex); }
            //斷線不用清除Event, 但Dispsoe需要, 因為即使斷線此物件仍存活著
            CtkEventUtil.RemoveEventHandlersOfOwnerByFilter(this, (dlgt) => true);
        }
        #endregion

    }
}
