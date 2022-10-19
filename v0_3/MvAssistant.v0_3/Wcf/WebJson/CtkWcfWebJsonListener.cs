using MvaCToolkitCs.v1_2;
using MvaCToolkitCs.v1_2.Protocol;
using MvaCToolkitCs.v1_2.Threading;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MvaCToolkitCs.v1_2.Wcf.WebJson
{

    public class CtkWcfWebJsonListener<TService> : IDisposable
        where TService : ICtkWcfWebJsonListener
    {

        public Dictionary<string, System.Type> AddressMapInterface = new Dictionary<string, System.Type>();
        public string Uri;
        protected Binding binding;
        protected ServiceHost host;
        protected int m_IntervalTimeOfConnectCheck = 5000;
        protected TService serviceInstance;
        public CtkWcfWebJsonListener(TService _svrInst, WebHttpBinding _binding)
        {
            this.serviceInstance = _svrInst;
            this.binding = _binding;
        }
        ~CtkWcfWebJsonListener() { this.Dispose(false); }




        public virtual void Close()
        {

            if (this.host != null)
            {
                using (var obj = this.host)
                {
                    obj.Abort();
                    obj.Close();
                }
            }

            CtkEventUtil.RemoveEventHandlersOfOwnerByFilter(this, (dlgt) => true);//關閉就代表此類別不用了
        }

        public virtual void NewHost()
        {
            var instance = this.serviceInstance;

            if (instance == null)
                this.host = new ServiceHost(typeof(TService), new Uri(this.Uri));
            else
                this.host = new ServiceHost(instance, new Uri(this.Uri));

            this.host.AddServiceEndpoint(typeof(TService), this.binding, "");


            if (this.AddressMapInterface != null)
            {
                foreach (var kv in this.AddressMapInterface)
                {
                    var ep = this.host.AddServiceEndpoint(kv.Value, this.binding, kv.Key);
                }
            }
        }

        public virtual void Open()
        {
            if (this.host == null) this.NewHost();
            this.host.Open();
        }

        void CleanHost()
        {
            //CtkEventUtil.RemoveEventHandlersFromOwningByFilter(this, (dlgt) => true);//不用清除自己的
            CtkEventUtil.RemoveEventHandlersOfOwnerByTarget(this.host, this);
        }
     
        

        #region IDisposable

        protected bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        public virtual void DisposeSelf()
        {
            this.Close();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any managed objects here.
            }

            // Free any unmanaged objects here.
            //
            this.DisposeSelf();
            disposed = true;
        }
  
        #endregion



    }





}
