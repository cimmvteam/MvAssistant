using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.DeviceDrive.KjMachineDrawer;
using MvAssistant.DeviceDrive.KjMachineDrawer.UDPCommand.HostToEquipment;
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
        public string ClientIP_01_02 = "127.0.0.1";
        public string ClientIP_01_03 = "127.0.0.1";
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

            Drawer_01_01.INI();
            Drawer_01_02.INI();
            Drawer_01_03.INI();

        }
        [TestMethod]// 20%,15%,10%
        public void SetMotionSpeed()
        {
            Drawer_01_01.SetMotionSpeed(20);
            Drawer_01_02.SetMotionSpeed(15);
            Drawer_01_03.SetMotionSpeed(10);
        }
        [TestMethod] // 30 seconds, 60 seconds,10 seconds
        public void SetTimeOut()
        {
            Drawer_01_01.SetTimeOut(30);
            Drawer_01_02.SetTimeOut(60);
            Drawer_01_03.SetTimeOut(10);
        }

        [TestMethod] //???
        public void SetParameter()
        {
            Drawer_01_01.SetParameterHomePosition("003");
            Drawer_01_01.SetParameterOutSidePosition("004");
            Drawer_01_02.SetParameterInSidePosition("005");
            Drawer_01_03.SetParameterIPAddress("006");
            Drawer_01_02.SetParameterSubMask("007");
            Drawer_01_03.SetParameterGetwayAddress("008");
        }
        [TestMethod]
        public void TrayMotionHome()
        {
            Drawer_01_01.TrayMotionHome();
            Drawer_01_02.TrayMotionHome();
            Drawer_01_03.TrayMotionHome();
        }
        [TestMethod]
        public void TrayMotionOut()
        {

            Drawer_01_01.TrayMotionOut();

            Drawer_01_02.TrayMotionOut();

            Drawer_01_03.TrayMotionOut();
        }
        [TestMethod]
        public void TrayMotionIn()
        {
            Drawer_01_01.TrayMotionIn();

            Drawer_01_02.TrayMotionIn();

            Drawer_01_03.TrayMotionIn();
        }
        [TestMethod]
        public void BrightLEDAllOn()
        {

            Drawer_01_01.BrightLEDAllOn();

            Drawer_01_02.BrightLEDAllOn();

            Drawer_01_03.BrightLEDAllOn();
        }
        [TestMethod]
        public void BrightLedAllOff()
        {

            Drawer_01_01.BrightLedAllOff();

            Drawer_01_02.BrightLedAllOff();

            Drawer_01_03.BrightLedAllOff();
        }
        [TestMethod]
        public void BrightLEDGreenOn()
        {
           
            Drawer_01_01.BrightLEDGreenOn();

            Drawer_01_02.BrightLEDGreenOn();

            Drawer_01_03.BrightLEDGreenOn(); ;
        }
        [TestMethod]
        public void BrightLEDRedOn()
        {

            Drawer_01_01.BrightLEDRedOn();

            Drawer_01_02.BrightLEDRedOn();

            Drawer_01_03.BrightLEDRedOn();
        }
        [TestMethod]
        public void PositionRead()
        {

            Drawer_01_01.PositionRead();

            Drawer_01_02.PositionRead();

            Drawer_01_03.PositionRead();
        }
        [TestMethod]
        public void BoxDetection()
        {
           
            Drawer_01_01.BoxDetection();

            Drawer_01_02.BoxDetection();

            Drawer_01_03.BoxDetection();
        }
        [TestMethod]
        public void WriteNetSetting()
        {

            Drawer_01_01.WriteNetSetting();

            Drawer_01_02.WriteNetSetting();

            Drawer_01_03.WriteNetSetting();
        }
        [TestMethod]
        public void LCDMsg()
        {
          
            Drawer_01_01.LCDMsg("01_01");

           
            Drawer_01_02.LCDMsg("01_02");


            Drawer_01_03.LCDMsg("01_03");
        }
        [TestMethod]
        public void ButtonEvent()
        {

        }
        [TestMethod]
        public void TimeOutEvent()
        {

        }
        [TestMethod]
        public void  SysStartUp()
        {

        }

    }

    public class ReceiveInfo
    {
        public Drawer Drawer { get; set; }
        public string Message { get; set; }
    }
}
