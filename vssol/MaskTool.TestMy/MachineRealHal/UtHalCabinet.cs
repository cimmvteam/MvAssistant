using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.DeviceDrive.KjMachineDrawer;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Manifest;

namespace MvAssistant.Mac.TestMy.MachineRealHal
{
    [TestClass]
    public class UtHalCabinet
    {
        #region
        // 建一個 Listen 的 Server;
        public UdpServerSocket UdpServer;
        public int RemotePort = 5000;
        public string ClientIP_01_01 = "127.0.0.1";
        public string ClientIP_01_02 = "127.0.0.1";
        public string ClientIP_01_03 = "127.0.0.1";
        public Drawer Drawer_01_01 = null;
        public Drawer Drawer_01_02 = null;
        public Drawer Drawer_01_03 = null;
        private MvKjMachineDrawerLdd ldd = null;

       



        private void InitialDrawers()
        {
            Drawer_01_01 = ldd.CreateDrawer(1, 1, ClientIP_01_01, RemotePort);
            Drawer_01_02 = ldd.CreateDrawer(1, 2, ClientIP_01_02, RemotePort);
            Drawer_01_03 = ldd.CreateDrawer(1, 3, ClientIP_01_03, RemotePort);
        }
       
        public UtHalCabinet()
        {
            ldd = new MvKjMachineDrawerLdd();
            InitialDrawers();
        }
       
        [TestMethod]
        public void INI()
        {
            Drawer_01_01.CommandINI();
            Drawer_01_02.CommandINI();
            Drawer_01_03.CommandINI();
            

        }
        [TestMethod]// 20%,15%,10%
        public void SetMotionSpeed()
        {
            Drawer_01_01.CommandSetMotionSpeed(20);
            Drawer_01_02.CommandSetMotionSpeed(15);
            Drawer_01_03.CommandSetMotionSpeed(10);
        }
        [TestMethod] // 30 seconds, 60 seconds,10 seconds
        public void SetTimeOut()
        {
            Drawer_01_01.CommandSetTimeOut(30);
            Drawer_01_02.CommandSetTimeOut(60);
            Drawer_01_03.CommandSetTimeOut(10);
        }

        [TestMethod] //???
        public void SetParameter()
        {
            Drawer_01_01.CommandSetParameterHomePosition("003");
            Drawer_01_01.CommandSetParameterOutSidePosition("004");
            Drawer_01_02.CommandSetParameterInSidePosition("005");
            Drawer_01_03.CommandSetParameterIPAddress("006");
            Drawer_01_02.CommandSetParameterSubMask("007");
            Drawer_01_03.CommandSetParameterGetwayAddress("008");
        }
        [TestMethod]
        public void TrayMotionHome()
        {
            Drawer_01_01.CommandTrayMotionHome();
            Drawer_01_02.CommandTrayMotionHome();
            Drawer_01_03.CommandTrayMotionHome();
        }
        [TestMethod]
        public void TrayMotionOut()
        {

            Drawer_01_01.CommandTrayMotionOut();

            Drawer_01_02.CommandTrayMotionOut();

            Drawer_01_03.CommandTrayMotionOut();
        }
        [TestMethod]
        public void TrayMotionIn()
        {
            Drawer_01_01.CommandTrayMotionIn();

            Drawer_01_02.CommandTrayMotionIn();

            Drawer_01_03.CommandTrayMotionIn();
        }
        [TestMethod]
        public void BrightLEDAllOn()
        {

            Drawer_01_01.CommandBrightLEDAllOn();

            Drawer_01_02.CommandBrightLEDAllOn();

            Drawer_01_03.CommandBrightLEDAllOn();
        }
        [TestMethod]
        public void BrightLedAllOff()
        {

            Drawer_01_01.CommandBrightLedAllOff();

            Drawer_01_02.CommandBrightLedAllOff();

            Drawer_01_03.CommandBrightLedAllOff();
        }
        [TestMethod]
        public void BrightLEDGreenOn()
        {

            Drawer_01_01.CommandBrightLEDGreenOn();

            Drawer_01_02.CommandBrightLEDGreenOn();

            Drawer_01_03.CommandBrightLEDGreenOn(); ;
        }
        [TestMethod]
        public void BrightLEDRedOn()
        {

            Drawer_01_01.CommandBrightLEDRedOn();

            Drawer_01_02.CommandBrightLEDRedOn();

            Drawer_01_03.CommandBrightLEDRedOn();
        }
        [TestMethod]
        public void PositionRead()
        {

            Drawer_01_01.CommandPositionRead();

            Drawer_01_02.CommandPositionRead();

            Drawer_01_03.CommandPositionRead();
        }
        [TestMethod]
        public void BoxDetection()
        {

            Drawer_01_01.CommandBoxDetection();

            Drawer_01_02.CommandBoxDetection();

            Drawer_01_03.CommandBoxDetection();
        }
        [TestMethod]
        public void WriteNetSetting()
        {

            Drawer_01_01.CommandWriteNetSetting();

            Drawer_01_02.CommandWriteNetSetting();

            Drawer_01_03.CommandWriteNetSetting();
        }
        [TestMethod]
        public void LCDMsg()
        {

            Drawer_01_01.CommandLCDMsg("01_01");


            Drawer_01_02.CommandLCDMsg("01_02");


            Drawer_01_03.CommandLCDMsg("01_03");
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
        public void SysStartUp()
        {

        }

        #endregion






        [TestMethod]
        public void TestSetParameter()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvCfLoad();

                var cbn = halContext.HalDevices[MacEnumDevice.cabinet_assembly.ToString()] as MacHalCabinet;

                cbn.SetPressureDiffLimit(50, 60);
                cbn.SetExhaustFlow(20, 35);
            }
        }

        [TestMethod]
        public void TestReadParameter()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvCfLoad();

                var cbn = halContext.HalDevices[MacEnumDevice.cabinet_assembly.ToString()] as MacHalCabinet;

                cbn.ReadPressureDiffLimitSetting();
                cbn.ReadExhaustFlowSetting();
            }
        }

        [TestMethod]
        public void TestReadComponentValue()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvCfLoad();

                var cbn = halContext.HalDevices[MacEnumDevice.cabinet_assembly.ToString()] as MacHalCabinet;

                cbn.ReadPressureDiff();
                cbn.ReadLightCurtain();
            }
        }

        [TestMethod]
        public void TestAssemblyWork()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvCfLoad();

                var cbn = halContext.HalDevices[MacEnumDevice.cabinet_assembly.ToString()] as MacHalCabinet;

            }
        }
    }
    
}
