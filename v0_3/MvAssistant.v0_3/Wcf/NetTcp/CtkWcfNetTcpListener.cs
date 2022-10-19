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


namespace MvaCToolkitCs.v1_2.Wcf.NetTcp
{





    public class CtkWcfNetTcpListener<TService> : IDisposable
    {

        /// <summary>
        /// Type必須為Interface
        /// </summary>
        public Dictionary<string, System.Type> AddressMap = new Dictionary<string, System.Type>();
        public string Uri;
        protected Binding binding;
        protected ServiceHost host;
        protected TService serviceInstance;
        public CtkWcfNetTcpListener(TService _svrInst, Binding _binding = null)
        {
            this.serviceInstance = _svrInst;
            this.binding = _binding;
        }
        public CtkWcfNetTcpListener(TService _svrInst, string uri, Binding _binding = null)
        {
            this.serviceInstance = _svrInst;
            this.Uri = uri;
            this.binding = _binding;
        }
        ~CtkWcfNetTcpListener() { this.Dispose(false); }



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

            if (instance == null) { this.host = new ServiceHost(typeof(TService), new Uri(this.Uri)); }
            else
            {
                if (typeof(Object).Equals(instance.GetType())) throw new ArgumentException("不應使用Object作為服務本體");

                if (instance is System.Type)
                    this.host = new ServiceHost(instance as System.Type, new Uri(this.Uri));
                else
                    this.host = new ServiceHost(instance, new Uri(this.Uri));
            }

            if (this.binding == null) this.binding = new NetTcpBinding();


            //預設至少有一個
            if (this.AddressMap == null || this.AddressMap.Count == 0)
                this.host.AddServiceEndpoint(typeof(TService), this.binding, "");
            else
            {
                foreach (var kv in this.AddressMap)
                {
                    var ep = this.host.AddServiceEndpoint(kv.Value, this.binding, kv.Key);
                }
            }
        }

        public virtual void WcfListen()
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




    public class CtkWcfNetTcpListener : CtkWcfNetTcpListener<Object>
    {
        public CtkWcfNetTcpListener(Object _svrInst, Binding _binding = null) : base(_svrInst, _binding)
        {
        }
        public CtkWcfNetTcpListener(Object _svrInst, string uri, Binding _binding = null) : base(_svrInst, uri, _binding)
        {
        }
        ~CtkWcfNetTcpListener() { this.Dispose(false); }




        /// <summary> 單實體服務 </summary>
        public static void UnitTest_SingleModel()
        {
            var are = new AutoResetEvent(false);

            CtkTask.RunOnce(() =>
            {
                var sample1 = new SampleCtkWcfNetTcpInst01();
                using (var listener1 = new CtkWcfNetTcpListener<SampleICtkWcfNetTcp0101>(sample1, "net.tcp://127.0.0.1:5050/"))
                {
                    listener1.WcfListen();
                    are.WaitOne();
                }
            });


            using (var client = new CtkWcfNetTcpClient<SampleICtkWcfNetTcp0101>("net.tcp://127.0.0.1:5050/"))
            {
                client.WcfConnect();
                var c = client.Channel.Add(12, 3);
            }


            are.Set();
        }
        /// <summary> 多個實體服務, 不同路徑提供不同介面服務 </summary>
        public static void UnitTest_MultModel()
        {
            var are = new AutoResetEvent(false);

            CtkTask.RunOnce(() =>
            {
                var sample1 = new SampleCtkWcfNetTcpInst01();
                var sample2 = new SampleCtkWcfNetTcpInst02();
                using (var listener1 = new CtkWcfNetTcpListener(sample1, "net.tcp://127.0.0.1:5050/"))
                using (var listener2 = new CtkWcfNetTcpListener(sample2, "net.tcp://127.0.0.1:5050/"))
                {
                    listener1.AddressMap["Svc0101"] = typeof(SampleICtkWcfNetTcp0101);
                    listener1.AddressMap["Svc0102"] = typeof(SampleICtkWcfNetTcp0102);

                    listener2.AddressMap["Svc0201"] = typeof(SampleICtkWcfNetTcp0201);
                    listener2.AddressMap["Svc0202"] = typeof(SampleICtkWcfNetTcp0202);

                    listener1.WcfListen();
                    listener2.WcfListen();

                    are.WaitOne();
                }
            });


            using (var client = new CtkWcfNetTcpClient<SampleICtkWcfNetTcp0101>("net.tcp://127.0.0.1:5050/Svc0101"))
            {
                client.WcfConnect();
                var c = client.Channel.Add(12, 3);
            }

            using (var client = new CtkWcfNetTcpClient<SampleICtkWcfNetTcp0102>("net.tcp://127.0.0.1:5050/Svc0102"))
            {
                client.WcfConnect();
                var c = client.Channel.Minus(12, 3);
            }

            using (var client = new CtkWcfNetTcpClient<SampleICtkWcfNetTcp0201>("net.tcp://127.0.0.1:5050/Svc0201"))
            {
                client.WcfConnect();
                var c = client.Channel.Multiple(12, 3);
            }

            using (var client = new CtkWcfNetTcpClient<SampleICtkWcfNetTcp0202>("net.tcp://127.0.0.1:5050/Svc0202"))
            {
                client.WcfConnect();
                var c = client.Channel.Divide(12, 3);
            }

            are.Set();
        }


    }


}
