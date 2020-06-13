using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.DeviceDrive.GudengLoadPort;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Manifest;

namespace MvAssistant.Mac.TestMy.MachineRealHal
{
    [TestClass]
    public class UtHalLoadPort
    {

        LoadPort LoadPort1 = null;
        MvGudengLoadPortLdd ldd = new MvGudengLoadPortLdd();
      public UtHalLoadPort()
      {
            //LoadPort1 =  ldd.CreateLoadPort("192.168.0.11", 2013, 1);
            LoadPort1 = ldd.CreateLoadPort("192.168.0.20", 1024, 1);
            LoadPort1.ListenServer();
        }

        [TestMethod]
        public void TestCommandInitialRequest()
        {
             LoadPort1.CommandInitialRequest();
        }

        #region TestMethod
        [TestMethod]
        public void CommandUndockRequest()
        {
            LoadPort1.CommandUndockRequest();
        }

        [TestMethod]
        public void TestCommandDockRequest()
        {
            LoadPort1.CommandDockRequest();
        }

        [TestMethod]
        public void TestCommandAskPlacementStatus()
        {
           LoadPort1.CommandAskPlacementStatus();
        }

        [TestMethod]
        public void CommandAskPresentStatus()
        {
           LoadPort1.CommandAskPresentStatus();
        }

        [TestMethod]
        public void TestCommandAskClamperStatus()
        {
             LoadPort1.CommandAskClamperStatus();
        }

        [TestMethod]
        public void TestCommandAskRFIDStatus()
        {
            LoadPort1.CommandAskRFIDStatus();
        }

        [TestMethod]
        public void TestCommandAskBarcodeStatus()
        {
            LoadPort1.CommandAskBarcodeStatus();
        }

        [TestMethod]
        public void TestCommandAskVacuumStatus()
        {
            LoadPort1.CommandAskVacuumStatus();
        }

        [TestMethod]
        public void TestCommandAskReticleExistStatus()
        {
            LoadPort1.CommandAskReticleExistStatus();
        }

        [TestMethod]
        public void TestCommandAlarmReset()
        {
            LoadPort1.CommandAlarmReset();
        }

        [TestMethod]
        public void TestCommandAskStagePosition()
        {
            LoadPort1.CommandAskStagePosition();
        }

        [TestMethod]
        public void TestCommandAskLoadportStatus()
        {
            LoadPort1.CommandAskLoadportStatus();
        }

        [TestMethod]
        public void TestCommandManualClamperLock()
        {
            LoadPort1.CommandManualClamperLock();
        }

        [TestMethod]
        public void TestCommandManualClamperUnlock()
        {
            LoadPort1.CommandManualClamperUnlock();
        }

        [TestMethod]
        public void TestCommandManualClamperOPR()
        {
            LoadPort1.CommandManualClamperOPR();
        }

        [TestMethod]
        public void TestCommandManualStageUp()
        {
            LoadPort1.CommandManualStageUp();
        }

        [TestMethod]
        public void TestCommandManualStageInspection()
        {
            LoadPort1.CommandManualStageInspection();
        }

        [TestMethod]
        public void TestCommandManualStageDown()
        {
            LoadPort1.CommandManualStageDown();
        }

        [TestMethod]
        public void TestCommandManualStageOPR()
        {
            LoadPort1.CommandManualStageOPR();
        }

        [TestMethod]
        public void TestCommandManualVacuumOn()
        {
            LoadPort1.CommandManualVacuumOn();
        }

        [TestMethod]
        public void TestCommandManualVacuumOff()
        {
            LoadPort1.CommandManualVacuumOff();
        }
        #endregion

        [TestMethod]
        public void TestSetParameter()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvCfLoad();

                var lp = halContext.HalDevices[MacEnumDevice.loadport_assembly.ToString()] as MacHalLoadPort;
                var uni = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                uni.HalConnect();
                lp.HalConnect();

                lp.SetPressureDiffLimit(40, 50);
            }
        }

        [TestMethod]
        public void TestReadParameter()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvCfLoad();

                var lp = halContext.HalDevices[MacEnumDevice.loadport_assembly.ToString()] as MacHalLoadPort;
                var uni = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                uni.HalConnect();
                lp.HalConnect();

                lp.ReadPressureDiffLimitSrtting();
            }
        }

        [TestMethod]
        public void TestReadComponentValue()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvCfLoad();

                var lp = halContext.HalDevices[MacEnumDevice.loadport_assembly.ToString()] as MacHalLoadPort;
                var uni = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                uni.HalConnect();
                lp.HalConnect();

                lp.ReadPressureDiff();
                lp.ReadLP_Light_Curtain();
            }
        }

        [TestMethod]
        public void TestAssemblyWork()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvCfLoad();

                var lp = halContext.HalDevices[MacEnumDevice.loadport_assembly.ToString()] as MacHalLoadPort;
                var uni = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                uni.HalConnect();
                lp.HalConnect();
            }
        }
    }
}
