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
    public class SNetSimulateDeviceVibration : IDisposable
    {

        ~SNetSimulateDeviceVibration() { this.Dispose(false); }
        public CtkTcpListener listener;

        public void RunAsyn()
        {

            CtkLog.RegisterEveryLogWrite((ss, ea) =>
            {
                var now = DateTime.Now;
                var sb = new StringBuilder();
                sb.AppendFormat("[{0}] ", now.ToString("yyyyMMdd HH:mm:ss"));
                sb.AppendFormat("{0} ", ea.Message);
                if (ea.Exception != null)
                    sb.AppendFormat("{0}", ea.Exception.StackTrace);
                CtkLog.InfoNs(this, sb.ToString());
            });


            var len = 512;
            var sampleRate = 512.0;

            var sin1 = new DenseVector(Generate.Sinusoidal(len, sampleRate, 10.0, 1.0));
            var sin2 = new DenseVector(Generate.Sinusoidal(len, sampleRate, 60.0, 0.5));
            var wave = sin1 + sin2;
            var waveIndex = 0;


            DateTime? prevTime = DateTime.Now;
            this.listener = new CtkTcpListener("127.0.0.1", 5003);
            listener.NonStopRunStart();

            listener.EhFirstConnect += (ss, ee) =>
            {
                var myea = ee as CtkNonStopTcpStateEventArgs;
                var sb = new StringBuilder();
                sb.Append("evtFirstConnect:\n");
                sb.Append(this.CmdState());
                CtkLog.InfoNs(this, sb.ToString());
            };
            listener.EhDataReceive += (ss, ee) =>
            {
                var myea = ee as CtkNonStopTcpStateEventArgs;
                var ctkBuffer = myea.TrxMessageBuffer;
                var msg = Encoding.UTF8.GetString(ctkBuffer.Buffer, ctkBuffer.Offset, ctkBuffer.Length);
                if (!msg.Contains("\n")) return;
                var sb = new StringBuilder();
                sb.Append("cmd -respData -svid 0 -data");

                var now = DateTime.Now;
                var ts = now - prevTime.Value;
                prevTime = now;

                var limit = ts.Ticks * 1.0 / TimeSpan.TicksPerSecond * sampleRate;
                if (limit <= 0) limit = 1;
                if (ts.TotalMilliseconds > 500) limit = 1;

                for (var idx = 0; idx < limit; idx++)
                {
                    sb.AppendFormat(" {0}", wave[waveIndex++]);
                    if (waveIndex >= wave.Count) waveIndex = 0;
                }
                sb.AppendLine();

                myea.WriteMsg(sb.ToString());

            };



        }

        public void Command(string cmd)
        {
            switch (cmd)
            {
                case "state":
                    CtkLog.InfoNs(this, this.CmdState());
                    break;
            }
        }

        string CmdState()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("Connected Count={0}\n", this.listener.ConnectCount());
            sb.AppendFormat("Client Count={0}\n", this.listener.TcpClientList.Count);
            return sb.ToString();
        }




        public void Stop()
        {
            this.listener.NonStopRunStop();
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
