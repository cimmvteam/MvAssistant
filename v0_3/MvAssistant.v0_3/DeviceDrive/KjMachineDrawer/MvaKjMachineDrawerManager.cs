using CToolkit.v1_1.Net;
using MvAssistant.v0_2.DeviceDrive.KjMachineDrawer.ReplyCode;
using MvAssistant.v0_2.DeviceDrive.KjMachineDrawer.UDPCommand;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.DeviceDrive.KjMachineDrawer
{
    /// <summary></summary>
    [Obsolete]
    public class MvaKjMachineDrawerManager : IDisposable
    {
      public List<MvaKjMachineDrawerLdd> Drawers = null;
      
        public IDictionary<int,bool?> PortStatusDictionary { get; private set; }
        //private List<ReceiveInfo> ReceiveInfos = null;
        public SysStartUpEventListener SysStartUpEventListener;
        /// <summary>建構式</summary>
        public   MvaKjMachineDrawerManager()
        {
            Drawers = new List<MvaKjMachineDrawerLdd>();
            //ReceiveInfos = new List<ReceiveInfo>();
           
        }

       
        /// <summary></summary>
        /// <param name="listenDrawerPortMin">監聽 Udp Port 的最小值</param>
        /// <param name="listenDrawerPortMax">監聽 Udp Port 的最大值</param>
        /// <param name="bindLocalIp">本地端 繫結 的IP</param>
        /// <param name="bindLocalPort">本地端 繫結 的port</param>
        public MvaKjMachineDrawerManager(int listenDrawerPortMin,int listenDrawerPortMax,int sysStartUpEventListenPort):this()
        {

            Action initialPortStatusDictionary = () =>{
                PortStatusDictionary = new Dictionary<int, bool?>();
                for (int i=listenDrawerPortMin;i<= listenDrawerPortMax; i++)
                {
                    PortStatusDictionary.Add(i, default(bool?));
                }
            };
            initialPortStatusDictionary();
            SysStartUpEventListener = new SysStartUpEventListener(sysStartUpEventListenPort);
            ListenSystStartUpEvent();

        }

        public void ListenSystStartUpEvent()
        {
            this.SysStartUpEventListener.Listen(OnSysStartUp);
        }

        public void OnSysStartUp(string message,IPEndPoint endPoint)
        {
            MvaKjMachineDrawerLdd drawer = this.GetDrawerByDeviceIP(endPoint.Address.ToString());
            if(drawer != null)
            {
                drawer.InvokeMethod(message);
            }
        }
        public int ListenDrawerPortMin
        {
            get
            {
                return PortStatusDictionary.OrderBy(m => m.Key).First().Key;
            }
        }
        public int ListenDrawerportMax
        {
            get
            {
                return PortStatusDictionary.OrderByDescending(m => m.Key).First().Key;
            }
         }

       

        /// <summary>產生 Drawer</summary>
        /// <param name="cabinetNo">Cabinet 編號</param>
        /// <param name="drawerNo">Drawer 編號</param>
        /// <param name="deviceIP">裝置 IP</param>
        /// <param name="udpServerPort">Drawer 內建 UDP Server Port</param>
        /// <returns></returns>
        public MvaKjMachineDrawerLdd CreateDrawer(int cabinetNo,string drawerNo,IPEndPoint deviceEndpoint,string localIP)
        {
            try
            {
                
                MvaKjMachineDrawerLdd drawer = new MvaKjMachineDrawerLdd(cabinetNo, drawerNo, deviceEndpoint, localIP, this.PortStatusDictionary);
                
                Drawers.Add(drawer);
                return drawer;
            }
            catch(Exception ex)
            {
                MvaLog.WarnNs(this, ex);
                return null;
            }
           
        }
        public MvaKjMachineDrawerLdd CreateLdd(int cabinetNo, string drawerNo, IPEndPoint deviceEndpoint, string localIP)
        {

            try
            {

                MvaKjMachineDrawerLdd ldd = new MvaKjMachineDrawerLdd(cabinetNo, drawerNo, deviceEndpoint, localIP, this.PortStatusDictionary);

                return ldd;
            }
            catch (Exception )
            {
                return null;
            }


        }


        /// <summary>由IP 取得 Drawer</summary>
        /// <param name="deviceIP">Drawer IP</param>
        /// <returns></returns>
        public MvaKjMachineDrawerLdd GetDrawerByDeviceIP(string deviceIP)
        {
            try
            {
                var drawer = this.Drawers.Where(m => m.DeviceIP.Equals(deviceIP)).FirstOrDefault();
                Debug.WriteLine(drawer.DeviceIP);
                return drawer;
            }
            catch(Exception ex)
            {
                MvaLog.WarnNs(this, ex);
                return null;
            }
        }

       


        public ReplyMessage ParseReplyMessage(string rtnMessage)
        {
            // 移除 訊息前後綴符號 ~,@ 
            var message = rtnMessage.Replace(BaseCommand.CommandPostfixText, "").Replace(BaseCommand.CommandPrefixText, "");
            // 以 ',' 符號將 message 切割 成 2 或 3 個部分 
            var messageAry = message.Split(new string[] { BaseCommand.CommandSplitSign }, StringSplitOptions.RemoveEmptyEntries);
            var replyMessage = new ReplyMessage
            {
                StringCode = messageAry[0],// 代碼(111, 100,....)
                StringFunc = messageAry[1],// function name(ReplySetSpeed,ReplyTrayMotion......)
                Value = messageAry.Length == 3 ? Convert.ToInt32(messageAry[2]) : default(int?),
            };
            return replyMessage;

        }
        public int ConnectTry(string ip = null, int? port = null)
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

    /**
    public class ReceiveInfo
    {
        public Drawer Drawer { get; set; }
        public string Message { get; set; }
    }*/
}
