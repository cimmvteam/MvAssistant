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
        public DateTime LastSend;
        public DateTime LastReceive;
        public int[] Values = new int[4];//目前控制器最多4個channel


        IPEndPoint RemoteEp
        {
            get
            {
                if (this.TcpClient.remoteEP == null) this.TcpClient.remoteEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 0);
                return this.TcpClient.remoteEP;
            }
        }
        public string RemoteIp { get { return this.RemoteEp.Address.ToString(); } set { this.RemoteEp.Address = IPAddress.Parse(value); } }
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

            this.TcpClient.EhDataReceive += TcpClient_EhDataReceive;

            this.TcpClient.ConnectIfNo();



            return 0;
        }

        private void TcpClient_EhDataReceive(object sender, CToolkit.v1_1.Protocol.CtkProtocolEventArgs e)
        {
            var resp = e.TrxMessage.GetString();

            var cmdType = resp.Substring(0, 3);
            var data = resp.Substring(3);

            if (cmdType == "R12")
            {
                switch (this.Model)
                {
                    case MvEnumLeimacModel.IWDV_100S_24:
                    case MvEnumLeimacModel.IWDV_600M2_24:
                        for (var idx = 0; data.Length > 0; idx++)
                        {
                            this.Values[idx] = Int32.Parse(data.Substring(0, 4));
                            data = data.Substring(4);
                        }
                        break;
                }
            }

            if (cmdType == "R11")
            {
                switch (this.Model)
                {
                    case MvEnumLeimacModel.IDGB_50M2PG_12_TP:
                    case MvEnumLeimacModel.IDGB_50M4PG_24_TP:
                        for (var idx = 0; data.Length > 0; idx++)
                        {
                            this.Values[idx] = Int32.Parse(data.Substring(0, 4));
                            data = data.Substring(4);
                        }
                        break;
                }
            }

            this.LastReceive = DateTime.Now;






        }




        public int[] GetValues()
        {
            //需等連線完成
            if (!MvSpinWait.SpinUntil(() => this.TcpClient.IsRemoteConnected, 5000))
                return null;


            var cmdType = "R12";
            var cmd = "";
            switch (this.Model)
            {
                case MvEnumLeimacModel.IWDV_100S_24:
                    cmd = string.Format("{0}0000", cmdType);
                    break;
                case MvEnumLeimacModel.IWDV_600M2_24:
                    cmd = string.Format("{0}0000", cmdType);
                    break;
                case MvEnumLeimacModel.IDGB_50M2PG_12_TP:
                case MvEnumLeimacModel.IDGB_50M4PG_24_TP:
                    cmdType = "R11";
                    cmd = string.Format("{0}0000", cmdType);
                    break;
                default: throw new MvException("No assign Model.");
            }



            this.LastSend = DateTime.Now;
            this.TcpClient.WriteMsg(cmd);

            if (!MvSpinWait.SpinUntil(() => this.LastReceive > this.LastSend, 1000)) return null;
            return this.Values;
        }



        public int SetValue(int ch, int value)
        {
            //需等連線完成
            if (!MvSpinWait.SpinUntil(() => this.TcpClient.IsRemoteConnected, 5000))
                return -1;


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
