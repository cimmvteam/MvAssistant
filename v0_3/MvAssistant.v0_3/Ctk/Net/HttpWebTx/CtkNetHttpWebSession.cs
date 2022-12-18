using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Cache;
using System.Text;

namespace MvaCToolkitCs.v1_2.Net.HttpWebTx
{
    public class CtkNetHttpWebSession : IDisposable
    {
        public CookieContainer CookieContainer = new CookieContainer();
        public List<CtkNetHttpWebTransaction> Transaction = new List<CtkNetHttpWebTransaction>();

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


        public CtkNetHttpWebTransaction CreateHttpGet(String url, RequestCacheLevel cachePolicy = RequestCacheLevel.Default) { return this.Create(url, cachePolicy, "GET"); }
        public CtkNetHttpWebTransaction CreateHttpPost(String url, RequestCacheLevel cachePolicy = RequestCacheLevel.Default) { return this.Create(url, cachePolicy, "POST"); }

        public String HttpGet(String url, RequestCacheLevel cachePolicy = RequestCacheLevel.Default)
        {
            var tx = this.Create(url, cachePolicy, "GET");
            return tx.GetHwResponseData();
        }
        public String HttpPost(String url, RequestCacheLevel cachePolicy = RequestCacheLevel.Default)
        {
            var tx = this.Create(url, cachePolicy, "POST");
            return tx.GetHwResponseData();
        }





        #region IDisposable
        // Flag: Has Dispose already been called?
        protected bool disposed = false;
        ~CtkNetHttpWebSession() { this.Dispose(false); }
        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void DisposeClose()
        {
            try
            {
                foreach (var tx in this.Transaction)
                {
                    try { tx.Dispose(); }
                    catch (Exception) { }
                }
            }
            catch (Exception ex) { CtkLog.Warn(ex); }

            //斷線不用清除Event, 但Dispsoe需要, 因為即使斷線此物件仍存活著
            CtkEventUtil.RemoveEventHandlersOfOwnerByFilter(this, (dlgt) => true);
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

            this.DisposeClose();

            disposed = true;
        }
        #endregion

    }
}
