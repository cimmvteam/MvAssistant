using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Manifest;

namespace MvAssistant.Mac.TestMy.MachineRealHal
{
    [TestClass]
    public class UtHalCleanCh
    {
        [TestMethod]
        public void TestSetParameter()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.Load();

                var cc = halContext.HalDevices[MacEnumDevice.clean_assembly.ToString()] as MacHalCleanCh;

                cc.SetParticleCntLimit(20, 30, 40);
                cc.SetRobotAboutLimit(10, 50);
                cc.SetRobotUpDownLimit(50, 10);
                cc.SetPressureDiffLimit(40);
                cc.SetPressureCtrl(90);
            }
        }

        [TestMethod]
        public void TestReadParameter()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.Load();

                var cc = halContext.HalDevices[MacEnumDevice.clean_assembly.ToString()] as MacHalCleanCh;

                cc.ReadParticleCntLimitSetting();
                cc.ReadRobotAboutLimitSetting();
                cc.ReadRobotUpDownLimitSetting();
                cc.ReadPressureDiffLimitSetting();
                cc.ReadPressureCtrlSetting();
            }
        }

        [TestMethod]
        public void TestReadComponentValue()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.Load();

                var cc = halContext.HalDevices[MacEnumDevice.clean_assembly.ToString()] as MacHalCleanCh;

                cc.ReadParticleCount();
                cc.ReadMaskLevel();
                cc.ReadRobotPosAbout();
                cc.ReadRobotPosUpDown();
                cc.ReadPressureDiff();
                cc.ReadBlowPressure();
                cc.ReadPressure();
                cc.ReadLightCurtain();
            }
        }

        [TestMethod]
        public void TestAssemblyWork()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.Load();

                var cc = halContext.HalDevices[MacEnumDevice.clean_assembly.ToString()] as MacHalCleanCh;

                cc.GasValveBlow(50);
            }
        }
    }
}
