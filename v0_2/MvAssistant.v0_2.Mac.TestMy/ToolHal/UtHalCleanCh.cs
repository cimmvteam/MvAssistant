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
                halContext.MvaCfLoad();

                var cc = halContext.HalDevices[MacEnumDevice.clean_assembly.ToString()] as MacHalCleanCh;
                var uni = halContext.HalDevices[MacEnumDevice.eqp_assembly.ToString()] as MacHalEqp;
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
                halContext.MvaCfLoad();

                var cc = halContext.HalDevices[MacEnumDevice.clean_assembly.ToString()] as MacHalCleanCh;
                var uni = halContext.HalDevices[MacEnumDevice.eqp_assembly.ToString()] as MacHalEqp;
                uni.HalConnect();
                cc.HalConnect();

                cc.SetParticleCntLimit(20, 30, 40);
                cc.SetRobotAboutLimit(10, 50);
                cc.SetRobotUpDownLimit(50, 10);
                cc.SetManometerPressureDiffLimit(40);
                cc.SetBlowPressureCtrl(90);
            }
        }

        [TestMethod]
        public void TestReadParameter()
        {
            using (var halContext = new MacHalContext(ManifestPath))
            {
                halContext.MvaCfLoad();

                var cc = halContext.HalDevices[MacEnumDevice.clean_assembly.ToString()] as MacHalCleanCh;
                var uni = halContext.HalDevices[MacEnumDevice.eqp_assembly.ToString()] as MacHalEqp;
                uni.HalConnect();
                cc.HalConnect();

                cc.ReadParticleCntLimitSetting();
                cc.ReadRobotAboutLimitSetting();
                cc.ReadRobotUpDownLimitSetting();
                cc.ReadManometerPressureDiffLimitSetting();
                cc.ReadBlowPressureCtrlSetting();
            }
        }

        [TestMethod]
        public void TestReadComponentValue()
        {
            using (var halContext = new MacHalContext(ManifestPath))
            {
                halContext.MvaCfInit();
                halContext.MvaCfLoad();

                var cc = halContext.HalDevices[MacEnumDevice.clean_assembly.ToString()] as MacHalCleanCh;
                var uni = halContext.HalDevices[MacEnumDevice.eqp_assembly.ToString()] as MacHalEqp;
                uni.HalConnect();
                cc.HalConnect();

                cc.ReadParticleCount();
                cc.ReadMaskLevel();
                cc.ReadRobotPosAbout();
                cc.ReadRobotPosUpDown();
                cc.ReadPressureDiff();
                cc.ReadBlowPressure();
                cc.ReadManometerPressure();
                cc.ReadLightCurtain();
            }
        }

        [TestMethod]
        public void TestAssemblyWork()
        {
            using (var halContext = new MacHalContext(ManifestPath))
            {
                halContext.MvaCfLoad();

                var cc = halContext.HalDevices[MacEnumDevice.clean_assembly.ToString()] as MacHalCleanCh;
                var uni = halContext.HalDevices[MacEnumDevice.eqp_assembly.ToString()] as MacHalEqp;
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
                halContext.MvaCfLoad();
                var cc = halContext.HalDevices[MacEnumDevice.clean_assembly.ToString()] as MacHalCleanCh;
                var uni = halContext.HalDevices[MacEnumDevice.eqp_assembly.ToString()] as MacHalEqp;
                uni.HalConnect();
                cc.HalConnect();

                cc.LightSideInsp.TurnOn(255);

            }
        }
    }
}
