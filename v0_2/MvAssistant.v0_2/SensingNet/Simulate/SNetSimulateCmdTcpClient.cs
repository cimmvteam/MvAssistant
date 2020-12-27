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
    public class SNetSimulateCmdTcpClient : IDisposable
    {
        CtkNonStopTcpClient client;
        public volatile bool IsSendRequest = false;

        ~SNetSimulateCmdTcpClient() { this.Dispose(false); }

        public void RunAsyn()
        {

            client = new CtkNonStopTcpClient("127.0.0.1", 5003);
            client.EhFirstConnect += (ss, ee) => { Write("evtFirstConnect"); };
            client.EhFailConnect += (ss, ee) =>
            {
                var sb = new StringBuilder();
                sb.Append("evtFailConnect: ");
                sb.Append(ee.Exception.StackTrace);
                Write(sb.ToString());
            };
            client.EhErrorReceive += (ss, ee) => { Write("evtErrorReceive"); };
            client.EhDataReceive += (ss, ee) =>
            {
                var ea = ee as CtkNonStopTcpStateEventArgs;
                var ctkBuffer = ea.TrxMessageBuffer;
                this.Write(ctkBuffer.GetString());


            };

            client.NonStopConnectAsyn();
        }


        public void Write(string msg, params object[] obj)
        {
            Console.WriteLine();
            Console.WriteLine(msg, obj);
            Console.Write(">");
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
            this.client.WriteMsg("cmd\n");
        }


        public void Stop()
        {
            if (this.client != null)
            {
                using (this.client)
                {
                    this.client.AbortNonStopConnect();
                    this.client.Disconnect();
                }
            }
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
