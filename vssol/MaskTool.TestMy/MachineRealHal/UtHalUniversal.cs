using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Manifest;

namespace MvAssistant.Mac.TestMy.MachineRealHal
{
    [TestClass]
    public class UtHalUniversal
    {
        [TestMethod]
        public void TestSetParameter()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.Load();

                var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;


            }
        }

        [TestMethod]
        public void TestReadParameter()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.Load();

                var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;


            }
        }

        [TestMethod]
        public void TestReadComponentValue()
        {
            try
            {

                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.Load();

                    var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;

                    unv.ReadCoverFanSpeed();
                    unv.ReadPowerON();
                    unv.ReadBCP_Maintenance();
                    unv.ReadCB_Maintenance();
                    unv.ReadBCP_EMO();
                    unv.ReadCB_EMO();
                    unv.ReadLP1_EMO();
                    unv.ReadLP2_EMO();
                    unv.ReadBCP_Door();
                    unv.ReadLP1_Door();
                    unv.ReadLP2_Door();
                    unv.ReadBCP_Smoke();

                    unv.ReadAlarm_General();
                    unv.ReadAlarm_Cabinet();
                    unv.ReadAlarm_CleanCh();
                    unv.ReadAlarm_BTRobot();
                    unv.ReadAlarm_MTRobot();
                    unv.ReadAlarm_OpenStage();
                    unv.ReadAlarm_InspCh();
                    unv.ReadAlarm_LoadPort();
                    unv.ReadAlarm_CoverFan();
                    unv.ReadAlarm_MTClampInsp();

                    unv.ReadWarning_General();
                    unv.ReadWarning_Cabinet();
                    unv.ReadWarning_CleanCh();
                    unv.ReadWarning_BTRobot();
                    unv.ReadWarning_MTRobot();
                    unv.ReadWarning_OpenStage();
                    unv.ReadWarning_InspCh();
                    unv.ReadWarning_LoadPort();
                    unv.ReadWarning_CoverFan();
                    unv.ReadWarning_MTClampInsp();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [TestMethod]
        public void TestAssemblyWork()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.Load();

                var unv = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;

                unv.ResetAllAlarm();
                unv.SetSignalTower(true, false, false);
                unv.SetBuzzer(1);
                unv.CoverFanCtrl(1, 150);
                unv.EMSAlarm(true, false, false, false);

            }
        }
    }
}
