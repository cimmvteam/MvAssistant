using CodeExpress.v1_0.Secs;
using CToolkit.v1_1;
using CToolkit.v1_1.Logging;
using CToolkit.v1_1.Net;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SensingNet.v0_2.Simulate
{
    public class SNetSimulateSecsClient : IDisposable
    {
        CtkTcpClient client;
        public volatile bool IsSendRequest = false;

        ~SNetSimulateSecsClient() { this.Dispose(false); }

        public void RunAsyn()
        {

            client = new CtkTcpClient("127.0.0.1", 10002);
            client.EhFirstConnect += (ss, ee) => { CtkLog.InfoNs(this, "evtFirstConnect"); };
            client.EhFailConnect += (ss, ee) => { CtkLog.InfoNs(this, "evtFailConnect"); };
            client.EhErrorReceive += (ss, ee) => { CtkLog.InfoNs(this, "evtErrorReceive"); };
            client.EhDataReceive += (ss, ee) =>
            {
                CtkLog.InfoNs(this, "evtDataReceive");
            };

            client.NonStopRunAsyn();
        }




        public void Command(string cmd)
        {
                switch (cmd)
                {
                    case "send":
                        this.Send();
                        break;
                    case "state":
                        Console.WriteLine("State={0}", this.client.IsRemoteConnected);
                        break;
                }
        }

        public void Send()
        {
            var txMsg = new CxHsmsMessage();
            txMsg.header.StreamId = 1;
            txMsg.header.FunctionId = 3;
            txMsg.header.WBit = true;
            var sList = new CxSecsIINodeList();
            //var sSvid = new CToolkit.v1_0.Secs.SecsIINodeInt64();


            var list = new List<UInt64>();
            list.Add(0);
            list.Add(1);
            list.Add(2);
            list.Add(168);


            foreach (var scfg in list)
            {
                var sSvid = new CxSecsIINodeUInt64();
                sSvid.Data.Add(scfg);
                sList.Data.Add(sSvid);
            }

            txMsg.rootNode = sList;

            this.client.WriteMsg(txMsg);

        }


        public void Stop()
        {
            if (this.client != null)
                this.client.AbortNonStopRun();
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
            this.Stop();
        }



        #endregion


    }
}
