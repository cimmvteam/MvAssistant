using MvaCToolkitCs.v1_2.Protocol;
using MvaCToolkitCs.v1_2.Threading;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MvaCToolkitCs.v1_2.Wcf.DuplexTcp
{

    /// <summary>
    /// 尚不完整, 除了自己的專案以外, 盡量不要用
    /// </summary>
    public class CtkWcfDuplexTcpClient<TService, TCallback> : IDisposable
        where TService : ICtkWcfDuplexTcpService//Server提供的, 必須是interface
        where TCallback : ICTkWcfDuplexTcpCallback//提供給Server呼叫的
    {


        public TService Channel;
        public DuplexChannelFactory<TService> ChannelFactory;
        public TCallback Callback;
        public string Uri;
        public string EntryAddress;
        public NetTcpBinding Binding;
        public bool IsWcfConnected { get { return this.ChannelFactory != null && this.ChannelFactory.State <= CommunicationState.Opened; } }

        ~CtkWcfDuplexTcpClient() { this.Dispose(false); }


        public CtkWcfDuplexTcpClient(TCallback callbackInstance, NetTcpBinding _binding = null)
        {
            this.Callback = callbackInstance;
            this.Binding = _binding;
        }




        public void WcfConnect(Action<DuplexChannelFactory<TService>> beforeOpen = null)
        {
            if (string.IsNullOrEmpty(this.Uri)) throw new ArgumentNullException("The Uri must has value");
            if (this.IsWcfConnected) return;
            try
            {
                if (!Monitor.TryEnter(this, 1000)) return;//進不去先離開

                var site = new InstanceContext(this.Callback);
                var address = this.Uri;
                if (this.EntryAddress != null) address = Path.Combine(this.Uri, this.EntryAddress);
                var endpointAddress = new EndpointAddress(address);
                this.ChannelFactory = new DuplexChannelFactory<TService>(site, this.Binding, endpointAddress);

                if (beforeOpen != null) beforeOpen.Invoke(this.ChannelFactory);//一次性使用的東西, 可以不寫event


                this.Channel = this.ChannelFactory.CreateChannel();
                this.Channel.CtkSend(new CtkWcfMessage());
            }
            finally
            {
                Monitor.Exit(this);
            }
        }



        #region IDispose

        protected bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void DisposeSelf()
        {

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
