using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.v0_2.Mac.Hal;
using MvAssistant.v0_2.Mac.Hal.Assembly;
using MvAssistant.v0_2.Mac.Manifest;

namespace MvAssistant.v0_2.Mac.TestMy.ToolHal
{
    [TestClass]
    public class UtHalCleanCh
    {
        const string ManifestPath = "UserData/Manifest/Manifest.xml.real";

        [TestMethod]
        public void TestCamera()
        {
            using (var halContext = new MacHalContext(ManifestPath))
            {
                halContext.MvCfLoad();

                var cc = halContext.HalDevices[MacEnumDevice.clean_assembly.ToString()] as MacHalCleanCh;
                var uni = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                uni.HalConnect();
                cc.HalConnect();

                cc.Camera_Insp_CapToSave("D:/Image/CC/Insp", "jpg");
            }
        }

        [TestMethod]
        public void TestSetParameter()
        {
            using (var halContext = new MacHalContext(ManifestPath))
            {
                halContext.MvCfLoad();

                var cc = halContext.HalDevices[MacEnumDevice.clean_assembly.ToString()] as MacHalCleanCh;
                var uni = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                uni.HalConnect();
                cc.HalConnect();

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
            using (var halContext = new MacHalContext(ManifestPath))
            {
                halContext.MvCfLoad();

                var cc = halContext.HalDevices[MacEnumDevice.clean_assembly.ToString()] as MacHalCleanCh;
                var uni = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                uni.HalConnect();
                cc.HalConnect();

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
            using (var halContext = new MacHalContext(ManifestPath))
            {
                halContext.MvCfInit();
                halContext.MvCfLoad();

                var cc = halContext.HalDevices[MacEnumDevice.clean_assembly.ToString()] as MacHalCleanCh;
                var uni = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                uni.HalConnect();
                cc.HalConnect();

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
            using (var halContext = new MacHalContext(ManifestPath))
            {
                halContext.MvCfLoad();

                var cc = halContext.HalDevices[MacEnumDevice.clean_assembly.ToString()] as MacHalCleanCh;
                var uni = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                uni.HalConnect();
                cc.HalConnect();

                cc.GasValveBlow(50);
            }
        }



        [TestMethod]
        public void TestAssemblyWorkLight()
        {
            using (var halContext = new MacHalContext(ManifestPath))
            {
                halContext.MvCfLoad();
                var cc = halContext.HalDevices[MacEnumDevice.clean_assembly.ToString()] as MacHalCleanCh;
                var uni = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                uni.HalConnect();
                cc.HalConnect();

                cc.LightSideInsp.TurnOn(255);

            }
        }
    }
}
