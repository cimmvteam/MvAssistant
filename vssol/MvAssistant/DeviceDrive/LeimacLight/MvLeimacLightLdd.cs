using CToolkit.v1_1.Net;
using MvAssistant.Tasking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.LeimacLight
{
    public class MvLeimacLightLdd : IDisposable
    {

        public CtkNonStopTcpClient TcpClient = new CtkNonStopTcpClient();
        public string RemoteIp { get { return this.TcpClient.remoteEP.Address.ToString(); } set { this.TcpClient.remoteEP.Address = IPAddress.Parse(value); } }
        public int RemotePort { get { return this.TcpClient.remoteEP.Port; } set { this.TcpClient.remoteEP.Port = value; } }
        public MvEnumLeimacModel Model = MvEnumLeimacModel.None;


        public MvLeimacLightLdd()
        {


        }


        public int ConnectIfNo(string ip = null, int? port = null)
        {

            if (this.TcpClient.remoteEP == null)
                this.TcpClient.remoteEP = new IPEndPoint(IPAddress.Parse("192.168.0.30"), 1000);

            if (!string.IsNullOrEmpty(ip)) this.RemoteIp = ip;
            if (port.HasValue) this.RemotePort = port.Value;

            this.TcpClient.ConnectIfNo();



            return 0;
        }

        public int GetValue(int ch)
        {


            return 0;
        }



        public int SetValue(int ch, int value)
        {
            //需等連線完成
            MvSpinWait.SpinUntil(() => this.TcpClient.IsRemoteConnected);


            if (value > 999) throw new MvException("Light value can not set over 999");
            if (value < 0) throw new MvException("Light value can not set under zero");

            var cmdType = "W12";
            var cmd = "";
            switch (this.Model)
            {
                case MvEnumLeimacModel.IWDV_100S_24:
                    cmd = string.Format("{0}{1:0000}", cmdType, value);
                    break;
                case MvEnumLeimacModel.IWDV_600M2_24:
                    cmd = string.Format("{0}{1:00}{2:0000}", cmdType, ch, value);
                    break;
                case MvEnumLeimacModel.IDGB_50M2PG_12_TP:
                case MvEnumLeimacModel.IDGB_50M4PG_24_TP:
                    cmdType = "W11";
                    cmd = string.Format("{0}{1:00}{2:0000}", cmdType, ch, value);
                    break;
                default: throw new MvException("No assign Model.");
            }


            this.TcpClient.WriteMsg(cmd);



            return 0;
        }



        public void Close()
        {
            try
            {
                if (this.TcpClient != null)
                    using (var obj = this.TcpClient) { obj.Disconnect(); }
            }
            catch (Exception ex) { MvLog.WarnNs(this, ex); }
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
            this.Close();
        }



        #endregion

    }
}
