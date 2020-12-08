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
   public  class MvKjMachineDrawerLddPool:IDisposable
    {
        /// <summary>這個物件的實體</summary>
        private static MvKjMachineDrawerLddPool _instance;
        /// <summary></summary>
        private static object lockGetInstanceObj = new object();
        /// <summary>存放 Ldd 的容器</summary>
        public List<MvKjMachineDrawerLdd> _ldd = null;
        /// <summary>存放可用 port 的 Dictionary</summary>
        public IDictionary<int, bool?> PortStatusDictionary { get; private set; }
        /// <summary>接收到Drawer 硬體發送的 SysStartUp以 及 OnButton 事件時 須Invoke 的方法 </summary>
        public SysStartUpEventListener SysEventListener;
        public void SetSysStartUpEventListener(SysStartUpEventListener listener)
        {
            SysEventListener=  listener;
        }
        public static MvKjMachineDrawerLddPool GetInstance(int listenDrawerPortMin, int listenDrawerPortMax, int sysStartUpEventListenPort)
        {
            try
            {
                if (_instance == null)
                {
                    lock (lockGetInstanceObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new MvKjMachineDrawerLddPool(listenDrawerPortMin, listenDrawerPortMax, sysStartUpEventListenPort,false);
                        }
                    }
                }
                return _instance;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        /// <summary>for create a fake MvKjMachineDrawerLddPool instance </summary>
        /// <remarks>
        /// <para>2020/10/23 10:12 King  [C]</para>
        /// <para>暫時保留</para>
        /// </remarks>
        /// <returns></returns>
        public static MvKjMachineDrawerLddPool GetFakeInstance(int listenDrawerPortMin, int listenDrawerPortMax, int sysStartUpEventListenPort)
        {

             if (_instance == null)
              {
                    lock (lockGetInstanceObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new MvKjMachineDrawerLddPool(listenDrawerPortMin, listenDrawerPortMax, sysStartUpEventListenPort, true);
                        }
                    }
                }
                return _instance;
           
            
        }

        /// <summary>建構式</summary>
        private MvKjMachineDrawerLddPool()
        {
            _ldd = new List<MvKjMachineDrawerLdd>();
            //ReceiveInfos = new List<ReceiveInfo>();

        }


        /// <summary></summary>
        /// <param name="listenDrawerPortMin">監聽 Udp Port 的最小值</param>
        /// <param name="listenDrawerPortMax">監聽 Udp Port 的最大值</param>
        /// <param name="bindLocalIp">本地端 繫結 的IP</param>
        /// <param name="bindLocalPort">本地端 繫結 的port</param>
        /// <remarks>
        /// <para>暫時保留</para>
        /// </remarks>
        public  MvKjMachineDrawerLddPool(int listenDrawerPortMin, int listenDrawerPortMax, int sysStartUpEventListenPort,bool isFake):this()
        {

            /**設定可用 Port 的最初狀況狀*/
            Action initialPortStatusDictionary = () => {
                PortStatusDictionary = new Dictionary<int, bool?>();
                for (int i = listenDrawerPortMin; i <= listenDrawerPortMax; i++)
                {
                    PortStatusDictionary.Add(i, default(bool?));
                }
            };
            initialPortStatusDictionary();
            if (!isFake)
            {
                SysEventListener = new SysStartUpEventListener(sysStartUpEventListenPort);
                ListenSystStartUpEvent();
            }

        }

        public void ListenSystStartUpEvent()
        {
            this.SysEventListener.Listen(OnSysStartUp);
        }

        public void OnSysStartUp(string message, IPEndPoint endPoint)
        {
            MvKjMachineDrawerLdd ldd = this.GetDrawerByDeviceIP(endPoint.Address.ToString());
            if (ldd != null)
            {
                ldd.InvokeMethod(message);
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

        public MvKjMachineDrawerLdd CreateLdd(string drawerIndex, IPEndPoint deviceEndpoint, string localIP)
        {

            try
            {

                // 檢查一下, ldd 是否存在, 如果存在, 就將 Ldd 找出來,回傳 Note: 2020/11/13 King
                MvKjMachineDrawerLdd ldd = new MvKjMachineDrawerLdd( drawerIndex, deviceEndpoint, localIP, this.PortStatusDictionary);
                _ldd.Add(ldd);
                return ldd;
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        /// <summary>Create a fake MvKjMachineDrawerLdd instance</summary>
        /// <param name="drawerIndex"></param>
        /// <param name="deviceEndpoint"></param>
        /// <param name="localIP"></param>
        /// <remarks>
        /// <para>2020/10/23 10:50 King [C]</para>
        /// <para>暫時保留</para>
        /// </remarks>
        /// <returns></returns>
        public MvKjMachineDrawerLdd CreateFakeLdd(string drawerIndex, IPEndPoint deviceEndpoint, string localIP)
        {
            MvKjMachineDrawerLdd ldd = new MvKjMachineDrawerLdd(true, drawerIndex, deviceEndpoint, localIP, this.PortStatusDictionary);
                _ldd.Add(ldd);
                return ldd;
        }


        /// <summary>由IP 取得 Drawer</summary>
        /// <param name="deviceIP">Drawer IP</param>
        /// <returns></returns>
        public MvKjMachineDrawerLdd GetDrawerByDeviceIP(string deviceIP)
        {
            try
            {
                var drawer = _ldd.Where(m => m.DeviceIP.Equals(deviceIP)).FirstOrDefault();
                Debug.WriteLine(drawer.DeviceIP);
                return drawer;
            }
            catch (Exception ex)
            {
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
}
