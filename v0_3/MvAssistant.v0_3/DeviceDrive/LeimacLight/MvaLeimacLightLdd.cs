using MvaCToolkitCs.v1_2;
using MvaCToolkitCs.v1_2.Net;
using MvaCToolkitCs.v1_2.Net.SocketTx;
using MvaCToolkitCs.v1_2.Protocol;
using MvAssistant.v0_3.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.LeimacLight
{
    public class MvaLeimacLightLdd : IDisposable
    {
        public CtkTcpClient TcpClient = new CtkTcpClient();
        public DateTime LastSend;
        public DateTime LastReceive;
        public int[] Values = new int[4];//目前控制器最多4個channel


        IPEndPoint RemoteEp
        {
            get
            {
                if (this.TcpClient.RemoteUri == null) this.TcpClient.RemoteUri = CtkNetUtil.ToUri("127.0.0.1", 0);
                return CtkNetUtil.ToIPEndPoint(this.TcpClient.RemoteUri);
            }
        }
        public string RemoteIp { get { return this.RemoteEp.Address.ToString(); } set { this.TcpClient.RemoteUri = CtkNetUtil.ToUri(value, this.RemotePort); } }
        public int RemotePort { get { return this.RemoteEp.Port; } set { this.TcpClient.RemoteUri = CtkNetUtil.ToUri(this.RemoteIp, value); } }
        public MvaEnumLeimacModel Model = MvaEnumLeimacModel.None;


        public MvaLeimacLightLdd()
        {
            this.TcpClient.EhDataReceive += TcpClient_EhDataReceive;
        }


        public int ConnectTry(string ip = null, int? port = null)
        {
            lock (this)
            {
                if (this.TcpClient.IsNonStopRunning) return 0;

                if (this.TcpClient.RemoteUri == null)
                    this.TcpClient.RemoteUri = CtkNetUtil.ToUri(ip, 1000);

                if (!string.IsNullOrEmpty(ip)) this.RemoteIp = ip;
                if (port.HasValue) this.RemotePort = port.Value;

                if (!this.TcpClient.IsOpenConnecting && !this.TcpClient.IsRemoteConnected)
                    this.TcpClient.ConnectTry();
                return 0;
            }
        }

        private void TcpClient_EhDataReceive(object sender, CtkProtocolEventArgs e)
        {
            var resp = e.TrxMessage.GetString();

            var cmdType = resp.Substring(0, 3);
            var data = resp.Substring(3);

            if (cmdType == "R12")
            {
                switch (this.Model)
                {
                    case MvaEnumLeimacModel.IWDV_100S_24:
                    case MvaEnumLeimacModel.IWDV_600M2_24:
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
                    case MvaEnumLeimacModel.IDGB_50M2PG_12_TP:
                    case MvaEnumLeimacModel.IDGB_50M4PG_24_TP:
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
            if (!MvaSpinWait.SpinUntil(() => this.TcpClient.IsRemoteConnected, 5000))
                return null;


            var cmdType = "R12";
            var cmd = "";
            switch (this.Model)
            {
                case MvaEnumLeimacModel.IWDV_100S_24:
                    cmd = string.Format("{0}0000", cmdType);
                    break;
                case MvaEnumLeimacModel.IWDV_600M2_24:
                    cmd = string.Format("{0}0000", cmdType);
                    break;
                case MvaEnumLeimacModel.IDGB_50M2PG_12_TP:
                case MvaEnumLeimacModel.IDGB_50M4PG_24_TP:
                    cmdType = "R11";
                    cmd = string.Format("{0}0000", cmdType);
                    break;
                default: throw new MvaException("No assign Model.");
            }



            this.LastSend = DateTime.Now;
            this.TcpClient.WriteMsg(cmd);

            if (!MvaSpinWait.SpinUntil(() => this.LastReceive > this.LastSend, 1000)) return null;
            return this.Values;
        }



        public int SetValue(int ch, int value)
        {
            //需等連線完成
            if (!MvaSpinWait.SpinUntil(() => this.TcpClient.IsRemoteConnected, 5000))
                return -1;


            if (value > 999) throw new MvaException("Light value can not set over 999");
            if (value < 0) throw new MvaException("Light value can not set under zero");

            var cmdType = "W12";
            var cmd = "";
            switch (this.Model)
            {
                case MvaEnumLeimacModel.IWDV_100S_24:
                    cmd = string.Format("{0}{1:0000}", cmdType, value);
                    break;
                case MvaEnumLeimacModel.IWDV_600M2_24:
                    cmd = string.Format("{0}{1:00}{2:0000}", cmdType, ch, value);
                    break;
                case MvaEnumLeimacModel.IDGB_50M2PG_12_TP:
                case MvaEnumLeimacModel.IDGB_50M4PG_24_TP:
                    cmdType = "W11";
                    cmd = string.Format("{0}{1:00}{2:0000}", cmdType, ch, value);
                    break;
                default: throw new MvaException("No assign Model.");
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
            catch (Exception ex) { CtkLog.WarnAn(this, ex); }
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
