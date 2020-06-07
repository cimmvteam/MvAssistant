using CToolkit.v1_1.Net;
using MvAssistant.DeviceDrive.KjMachineDrawer.UDPCommand;
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

            // 向 UdpServer註冊收到訊息事件後的處理函式 
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

        /// <summary>收到各 Drawer 送回的訊息後的處理函式</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnReceiveMessage(object sender, EventArgs args)
        {
            var ip = ((OnReciveMessageEventArgs)args).IP;
            var message = ((OnReciveMessageEventArgs)args).Message;
            var drawer = this.GetDrawerByDeviceIP(ip);
            var replyMessage = ParseReplyMessage(message);
            ExecuteMethodDispatch(drawer, replyMessage);
            
           
        }

        private void ExecuteMethodDispatch(Drawer drawer, ReplyMessage replyMessage)
        {
            typeof(Drawer).GetMethod(replyMessage.SringFunc).Invoke(drawer, new object[] { replyMessage });
        }
        /// <summary>由IP 取得 Drawer</summary>
        /// <param name="deviceIP">Drawer IP</param>
        /// <returns></returns>
        public Drawer GetDrawerByDeviceIP(string deviceIP)
        {
            var drawer = this.Drawers.Where(m => m.DeviceIP.Equals(deviceIP)).FirstOrDefault();
            return drawer;
        }

        /// <summary>由編號取得 Drawer </summary>
        /// <param name="cabinetNo">Cabinet No</param>
        /// <param name="drawerNo">Drawer No</param>
        /// <returns></returns>
        public Drawer GetDrawerByNO(int cabinetNo,int drawerNo)
        {
            var drawer = this.Drawers.Where(m => m.CabinetNO==cabinetNo).Where(m=>m.DrawerNO==drawerNo).FirstOrDefault();
            return drawer;
        }


        public ReplyMessage ParseReplyMessage(string rtnMessage)
        {
            var message = rtnMessage.Replace(BaseCommand.CommandPostfixText, "").Replace(BaseCommand.CommandPrefixText, "");
            var messageAry = message.Split(new string[] { BaseCommand.CommandSplitSign }, StringSplitOptions.RemoveEmptyEntries);
            var replyMessage = new ReplyMessage
            {
                StringCode = messageAry[0],
                SringFunc = messageAry[1],
                Value = messageAry.Length == 3 ? Convert.ToInt32(messageAry[2]) : default(int?),
            };
            return replyMessage;

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
