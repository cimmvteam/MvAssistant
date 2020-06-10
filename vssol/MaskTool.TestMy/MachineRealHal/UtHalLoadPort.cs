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

        #region global variables
        private LoadPort Loadport1 = null;
       // private LoadPort Loadport2 = null;
        #endregion
      public UtHalLoadPort()
      {
            Loadport1=  new LoadPort("192.168.0.11", 2013, 1);
            Loadport1.ListenServer();
        }

        [TestMethod]
        public void TestCommandInitialRequest()
        {
             Loadport1.CommandInitialRequest();
        }

        #region TestMethod
        [TestMethod]
        public void CommandUndockRequest()
        {
            Loadport1.CommandUndockRequest();
        }

        [TestMethod]
        public void TestCommandDockRequest()
        {
            Loadport1.CommandDockRequest();
        }

        [TestMethod]
        public void TestCommandAskPlacementStatus()
        {
           Loadport1.CommandAskPlacementStatus();
        }

        [TestMethod]
        public void CommandAskPresentStatus()
        {
           Loadport1.CommandAskPresentStatus();
        }

        [TestMethod]
        public void TestCommandAskClamperStatus()
        {
             Loadport1.CommandAskClamperStatus();
        }

        [TestMethod]
        public void TestCommandAskRFIDStatus()
        {
            Loadport1.CommandAskRFIDStatus();
        }

        [TestMethod]
        public void TestCommandAskBarcodeStatus()
        {
            Loadport1.CommandAskBarcodeStatus();
        }

        [TestMethod]
        public void TestCommandAskVacuumStatus()
        {
            Loadport1.CommandAskVacuumStatus();
        }

        [TestMethod]
        public void TestCommandAskReticleExistStatus()
        {
            Loadport1.CommandAskReticleExistStatus();
        }

        [TestMethod]
        public void TestCommandAlarmReset()
        {
            Loadport1.CommandAlarmReset();
        }

        [TestMethod]
        public void TestCommandAskStagePosition()
        {
            Loadport1.CommandAskStagePosition();
        }

        [TestMethod]
        public void TestCommandAskLoadportStatus()
        {
            Loadport1.CommandAskLoadportStatus();
        }

        [TestMethod]
        public void TestCommandManualClamperLock()
        {
            Loadport1.CommandManualClamperLock();
        }

        [TestMethod]
        public void TestCommandManualClamperUnlock()
        {
            Loadport1.CommandManualClamperUnlock();
        }

        [TestMethod]
        public void TestCommandManualClamperOPR()
        {
            Loadport1.CommandManualClamperOPR();
        }

        [TestMethod]
        public void TestCommandManualStageUp()
        {
            Loadport1.CommandManualStageUp();
        }

        [TestMethod]
        public void TestCommandManualStageInspection()
        {
            Loadport1.CommandManualStageInspection();
        }

        [TestMethod]
        public void TestCommandManualStageDown()
        {
            Loadport1.CommandManualStageDown();
        }

        [TestMethod]
        public void TestCommandManualStageOPR()
        {
            Loadport1.CommandManualStageOPR();
        }

        [TestMethod]
        public void TestCommandManualVacuumOn()
        {
            Loadport1.CommandManualVacuumOn();
        }

        [TestMethod]
        public void TestCommandManualVacuumOff()
        {
            Loadport1.CommandManualVacuumOff();
        }
        #endregion

        [TestMethod]
        public void TestSetParameter()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvCfLoad();

                var lp = halContext.HalDevices[MacEnumDevice.loadport_assembly.ToString()] as MacHalLoadPort;
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

            }
        }
    }
}
