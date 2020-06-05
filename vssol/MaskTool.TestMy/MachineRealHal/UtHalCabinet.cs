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
            Drawer_01_01.Command_INI();
            Drawer_01_02.Command_INI();
            Drawer_01_03.Command_INI();
            

        }
        [TestMethod]// 20%,15%,10%
        public void SetMotionSpeed()
        {
            Drawer_01_01.Command_SetMotionSpeed(20);
            Drawer_01_02.Command_SetMotionSpeed(15);
            Drawer_01_03.Command_SetMotionSpeed(10);
        }
        [TestMethod] // 30 seconds, 60 seconds,10 seconds
        public void SetTimeOut()
        {
            Drawer_01_01.Command_SetTimeOut(30);
            Drawer_01_02.Command_SetTimeOut(60);
            Drawer_01_03.Command_SetTimeOut(10);
        }

        [TestMethod] //???
        public void SetParameter()
        {
            Drawer_01_01.Command_SetParameterHomePosition("003");
            Drawer_01_01.Command_SetParameterOutSidePosition("004");
            Drawer_01_02.Command_SetParameterInSidePosition("005");
            Drawer_01_03.Command_SetParameterIPAddress("006");
            Drawer_01_02.Command_SetParameterSubMask("007");
            Drawer_01_03.Command_SetParameterGetwayAddress("008");
        }
        [TestMethod]
        public void TrayMotionHome()
        {
            Drawer_01_01.Command_TrayMotionHome();
            Drawer_01_02.Command_TrayMotionHome();
            Drawer_01_03.Command_TrayMotionHome();
        }
        [TestMethod]
        public void TrayMotionOut()
        {

            Drawer_01_01.Command_TrayMotionOut();

            Drawer_01_02.Command_TrayMotionOut();

            Drawer_01_03.Command_TrayMotionOut();
        }
        [TestMethod]
        public void TrayMotionIn()
        {
            Drawer_01_01.Command_TrayMotionIn();

            Drawer_01_02.Command_TrayMotionIn();

            Drawer_01_03.Command_TrayMotionIn();
        }
        [TestMethod]
        public void BrightLEDAllOn()
        {

            Drawer_01_01.Command_BrightLEDAllOn();

            Drawer_01_02.Command_BrightLEDAllOn();

            Drawer_01_03.Command_BrightLEDAllOn();
        }
        [TestMethod]
        public void BrightLedAllOff()
        {

            Drawer_01_01.Command_BrightLedAllOff();

            Drawer_01_02.Command_BrightLedAllOff();

            Drawer_01_03.Command_BrightLedAllOff();
        }
        [TestMethod]
        public void BrightLEDGreenOn()
        {

            Drawer_01_01.Command_BrightLEDGreenOn();

            Drawer_01_02.Command_BrightLEDGreenOn();

            Drawer_01_03.Command_BrightLEDGreenOn(); ;
        }
        [TestMethod]
        public void BrightLEDRedOn()
        {

            Drawer_01_01.Command_BrightLEDRedOn();

            Drawer_01_02.Command_BrightLEDRedOn();

            Drawer_01_03.Command_BrightLEDRedOn();
        }
        [TestMethod]
        public void PositionRead()
        {

            Drawer_01_01.Command_PositionRead();

            Drawer_01_02.Command_PositionRead();

            Drawer_01_03.Command_PositionRead();
        }
        [TestMethod]
        public void BoxDetection()
        {

            Drawer_01_01.Command_BoxDetection();

            Drawer_01_02.Command_BoxDetection();

            Drawer_01_03.Command_BoxDetection();
        }
        [TestMethod]
        public void WriteNetSetting()
        {

            Drawer_01_01.Command_WriteNetSetting();

            Drawer_01_02.Command_WriteNetSetting();

            Drawer_01_03.Command_WriteNetSetting();
        }
        [TestMethod]
        public void LCDMsg()
        {

            Drawer_01_01.Command_LCDMsg("01_01");


            Drawer_01_02.Command_LCDMsg("01_02");


            Drawer_01_03.Command_LCDMsg("01_03");
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
