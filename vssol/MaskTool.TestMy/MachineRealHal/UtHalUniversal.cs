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

                var unv = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalUniversal;

                
            }
        }

        [TestMethod]
        public void TestReadParameter()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.Load();

                var unv = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalUniversal;


            }
        }

        [TestMethod]
        public void TestReadComponentValue()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.Load();

                var unv = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalUniversal;

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

                unv.Alarm_General();
                unv.Alarm_Cabinet();
                unv.Alarm_CleanCh();
                unv.Alarm_BTRobot();
                unv.Alarm_MTRobot();
                unv.Alarm_OpenStage();
                unv.Alarm_InspCh();
                unv.Alarm_LoadPort();
                unv.Alarm_CoverFan();
                unv.Alarm_MTClampInsp();

                unv.Warning_General();
                unv.Warning_Cabinet();
                unv.Warning_CleanCh();
                unv.Warning_BTRobot();
                unv.Warning_MTRobot();
                unv.Warning_OpenStage();
                unv.Warning_InspCh();
                unv.Warning_LoadPort();
                unv.Warning_CoverFan();
                unv.Warning_MTClampInsp();
            }
        }

        [TestMethod]
        public void TestAssemblyWork()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.Load();

                var unv = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalUniversal;

                unv.ResetAll();
                unv.SetSignalTower(true,false,false);
                unv.SetBuzzer(1);
                unv.CoverFanCtrl(1, 150);
                unv.EMSAlarm(true, false, false, false);

            }
        }
    }
}
