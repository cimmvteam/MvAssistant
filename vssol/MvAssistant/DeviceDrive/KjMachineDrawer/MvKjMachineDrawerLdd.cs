using CToolkit.v1_1.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.KjMachineDrawer
{
    public class MvKjMachineDrawerLdd : IDisposable
    {
        public UdpServerSocket UdpServer;
        public int LocalPort = 6000;
        public List<Drawer> Drawers = null;
        private List<ReceiveInfo> ReceiveInfos = null;

        /// <summary>建構式</summary>
        public   MvKjMachineDrawerLdd()
        {
            Drawers = new List<Drawer>();
            ReceiveInfos = new List<ReceiveInfo>();
            InitialUdpServer();
        }

        /// <summary>初始化 Udp Server</summary>
        private void InitialUdpServer()
        {
            UdpServer = new UdpServerSocket(LocalPort);
            UdpServer.OnReceiveMessage += this.OnReceiveMessage;
        }

        /// <summary>產生 Drawer</summary>
        /// <param name="cabinetNo">Cabinet 編號</param>
        /// <param name="drawerNo">Drawer 編號</param>
        /// <param name="deviceIP">裝置 IP</param>
        /// <param name="udpServerPort">Drawer 內建 UDP Server Port</param>
        /// <returns></returns>
        public Drawer CreateDrawer(int cabinetNo,int drawerNo,string deviceIP, int udpServerPort)
        {

            Drawer drawer = new Drawer(cabinetNo, drawerNo, deviceIP, udpServerPort);
            Drawers.Add(drawer);
            return drawer;

        }
        private void OnReceiveMessage(object sender, EventArgs args)
        {
            var ip = ((OnReciveMessageEventArgs)args).IP;
            var message = ((OnReciveMessageEventArgs)args).Message;
            var drawer = Drawers.Where(m => m.DeviceIP == ip).FirstOrDefault();
            var cabinet = drawer.CabinetNO;
            var drawerNo = drawer.DrawerNO;
            if (ReceiveInfos != null)
            {
                ReceiveInfos.Add(
                    new ReceiveInfo
                    {
                        Drawer = drawer,
                        Message = message
                    }
                    );
            }
        }


        public int ConnectIfNo(string ip = null, int? port = null)
        {
        



            return 0;
        }


        public void Close()
        {


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
    public class ReceiveInfo
    {
        public Drawer Drawer { get; set; }
        public string Message { get; set; }
    }
}
