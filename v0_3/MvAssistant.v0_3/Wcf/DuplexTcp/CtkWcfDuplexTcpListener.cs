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

namespace MvaCToolkitCs.v1_2.Wcf.DuplexTcp
{


    /// <summary>
    /// 提供簡易訊息交換 & 收集 Channel
    /// 尚不完整, 除了自己的專案以外, 盡量不要用
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    public class CtkWcfDuplexTcpListener<TService> : IDisposable
        where TService : ICtkWcfDuplexTcpService
    {

        public Dictionary<string, System.Type> AddressMapInterface = new Dictionary<string, System.Type>();
        public string Uri;
        protected Binding binding;
        protected Dictionary<string, CtkWcfChannelInfo<ICTkWcfDuplexTcpCallback>> channelMapper = new Dictionary<string, CtkWcfChannelInfo<ICTkWcfDuplexTcpCallback>>();
        protected ServiceHost host;
        public TService SvrInst;
        public CtkWcfDuplexTcpListener(TService _svrInst, NetTcpBinding _binding = null)
        {
            this.SvrInst = _svrInst;
            this.binding = _binding;
        }
        ~CtkWcfDuplexTcpListener() { this.Dispose(false); }


        /// <summary>
        /// 清除己經斷線的
        /// </summary>
        public void CleanDisconnected()
        {
            var query = (from row in this.channelMapper
                         where row.Value.Channel.State > CommunicationState.Opened
                         select row).ToList();

            foreach (var row in query)
                this.channelMapper.Remove(row.Key);
        }

        public virtual void Close()
        {
            foreach (var chinfo in this.channelMapper)
            {
                var ch = chinfo.Value.Channel;
                ch.Abort();
                ch.Close();
            }

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

        public List<ICTkWcfDuplexTcpCallback> GetAllChannels()
        {
            this.CleanDisconnected();
            return (this.channelMapper.Select(row => row.Value.Callback)).ToList();
        }

        public T GetCallback<T>(string sessionId = null) where T : class, ICTkWcfDuplexTcpCallback { return this.GetCallback(sessionId) as T; }

        public ICTkWcfDuplexTcpCallback GetCallback(string sessionId = null)
        {
            this.CleanDisconnected();
            var oc = OperationContext.Current;
            if (sessionId == null)
                sessionId = oc.SessionId;
            if (this.channelMapper.ContainsKey(sessionId)) return this.channelMapper[sessionId].Callback;

            var chinfo = new CtkWcfChannelInfo<ICTkWcfDuplexTcpCallback>();
            chinfo.OpContext = oc;
            chinfo.SessionId = sessionId;
            chinfo.Channel = oc.Channel;
            chinfo.Callback = oc.GetCallbackChannel<ICTkWcfDuplexTcpCallback>();
            this.channelMapper[sessionId] = chinfo;
            return chinfo.Callback;
        }

        public virtual void NewHost()
        {
            var instance = this.SvrInst;

            if (instance == null)
                this.host = new ServiceHost(typeof(TService), new Uri(this.Uri));
            else
                this.host = new ServiceHost(instance, new Uri(this.Uri));

            if (this.binding == null) this.binding = new NetTcpBinding();//預設值
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

        protected void CleanHost()
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
