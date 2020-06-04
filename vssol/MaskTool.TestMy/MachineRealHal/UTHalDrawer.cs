using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.DeviceDrive.KjMachineDrawer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.TestMy.MachineRealHal
{
    [TestClass]
    public class UtHalDrawer
    {
        // 建一個 Listen 的 Server;
        public UdpServerSocket UdpServer;
        public int ListenPort = 6000;
        public int ClientPort = 5000;
        public string ClientIP_01_01 = "127.0.0.1";
        public string ClientIP_01_02 = "192.168.0.1";
        public string ClientIP_01_03 = "192.168.0.2";
        public Drawer Drawer_01_01 = null;
        public Drawer Drawer_01_02 = null;
        public Drawer Drawer_01_03 = null;
        private MvKjMachineDrawerLdd ldd = null;

        private List<ReceiveInfo> ReceiveInfos = null;



        private void InitialDrawers()
        {
            Drawer_01_01 = ldd.CreateDrawer(1, 1, ClientIP_01_01, 5000);
            Drawer_01_02 = ldd.CreateDrawer(1, 2, ClientIP_01_02, 5000);
            Drawer_01_03 = ldd.CreateDrawer(1, 3, ClientIP_01_03, 5000);
        }
        private void InitialUdpServer()
        {
            UdpServer = new UdpServerSocket(ListenPort);
            UdpServer.OnReceiveMessage += OnReceiveMessage;
        }
        public UtHalDrawer()
        {
            ldd = new MvKjMachineDrawerLdd();
            ReceiveInfos = new List<ReceiveInfo>();
            InitialUdpServer();
            InitialDrawers();
        }
        private void OnReceiveMessage(object sender, EventArgs args)
        {
            var ip = ((OnReciveMessageEventArgs)args).IP;
            var message = ((OnReciveMessageEventArgs)args).Message;
            var drawer = ldd.Drawers.Where(m => m.UDPServerIP == ip).FirstOrDefault();
            var cabinet = drawer.CabinetNO;
            var drawerNo = drawer.DrawerNO;
            ReceiveInfos?.Add(
                new ReceiveInfo
                {
                    Drawer = drawer,
                    Message = message
                }
                );
        }
        [TestMethod]
        public void INI()
        {


        }
        [TestMethod]// 60%, 30%
        public void SetMotionSpeed()
        {

        }
        [TestMethod] // 30 seconds, 60 seconds
        public void SetTimeOut()
        {

        }
        [TestMethod] //???
        public void SetParameter()
        {

        }
        [TestMethod]
        public void TrayMotion_Home()
        {

        }
        [TestMethod]
        public void TrayMotion_Out()
        {

        }
        [TestMethod]
        public void TrayMotion_In()
        {

        }
        [TestMethod]
        public void BrightLED_AllOn()
        {
        }
        [TestMethod]
        public void BrightLed_AllOff()
        {

        }
        [TestMethod]
        public void BrightLED_GreenOn()
        {

        }
        [TestMethod]
        public void BrightLED_RedOn()
        {

        }
        [TestMethod]
        public void PositionRead()
        {
        }
        [TestMethod]
        public void BoxDetection()
        {

        }
        [TestMethod]
        public void WriteNetSetting()
        {
            
        }
        [TestMethod]
        public void LCDMsg()
        {

        }

    }

    public class ReceiveInfo
    {
        public Drawer Drawer { get; set; }
        public string Message { get; set; }
    }
}
