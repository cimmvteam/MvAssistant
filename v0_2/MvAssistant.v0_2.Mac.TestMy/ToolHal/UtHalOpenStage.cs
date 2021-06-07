using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.v0_2.Mac.Hal;
using MvAssistant.v0_2.Mac.Hal.Assembly;
using MvAssistant.v0_2.Mac.Manifest;
using System.Drawing;

namespace MvAssistant.v0_2.Mac.TestMy.ToolHal
{
    [TestClass]
    public class UtHalOpenStage
    {
        [TestMethod]
        public void TestCamera()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvaCfLoad();

                var os = halContext.HalDevices[MacEnumDevice.openstage_assembly.ToString()] as MacHalOpenStage;
                os.HalConnect();
                
                os.Camera_Top_CapToSave("D:/Image/OS/Top", "jpg");
                os.Camera_Side_CapToSave("D:/Image/OS/Side", "jpg");
                os.Camera_Left_CapToSave("D:/Image/OS/NearLP", "jpg");
                os.Camera_Right_CapToSave("D:/Image/OS/NearCC", "jpg");
            }
        }

        [TestMethod]
        public void TestSetParameter()
        {
            using (var halContext = new MacHalContext("UserData/Manifest/Manifest.xml.real"))
            {
                halContext.MvaCfInit();
                halContext.MvaCfLoad();

                var os = halContext.HalDevices[MacEnumDevice.openstage_assembly.ToString()] as MacHalOpenStage;

                os.SetBoxType(1);
                os.SetSpeed(50);
                os.SetParticleCntLimit(10,20,30);
            }
        }

        [TestMethod]
        public void TestReadParameter()
        {
            using (var halContext = new MacHalContext("UserData/Manifest/Manifest.xml.real"))
            {
                halContext.MvaCfInit();
                halContext.MvaCfLoad();

                var os = halContext.HalDevices[MacEnumDevice.openstage_assembly.ToString()] as MacHalOpenStage;

                os.ReadBoxTypeSetting();
                os.ReadSpeedSetting();
                os.ReadParticleCntLimitSetting();
            }
        }

        [TestMethod]
        public void TestReadComponentValue()
        {
            try
            {
                using (var halContext = new MacHalContext("UserData/Manifest/Manifest.xml.real"))
                {
                    halContext.MvaCfInit();
                    halContext.MvaCfLoad();

                    var unv = halContext.HalDevices[MacEnumDevice.eqp_assembly.ToString()] as MacHalEqp;
                    var os = halContext.HalDevices[MacEnumDevice.openstage_assembly.ToString()] as MacHalOpenStage;
                    unv.HalConnect();
                    os.HalConnect();

                    os.SetRobotIntrude(true, false);
                    os.SetRobotIntrude(false, true);
                    os.SetRobotIntrude(false, false);
                    os.ReadClampStatus();
                    os.ReadSortClampPosition();
                    os.ReadSliderPosition();
                    os.ReadCoverPos();
                    os.ReadCoverSensor();
                    os.ReadBoxDeform();
                    os.ReadWeightOnStage();
                    os.ReadBoxExist();
                    os.ReadOpenStageStatus();
                    os.ReadRobotIntruded();
                    os.ReadParticleCount();
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
                halContext.MvaCfLoad();

                var os = halContext.HalDevices[MacEnumDevice.openstage_assembly.ToString()] as MacHalOpenStage;
                var uni = halContext.HalDevices[MacEnumDevice.eqp_assembly.ToString()] as MacHalEqp;
                uni.HalConnect();
                os.HalConnect();

                os.Initial();
                os.SortClamp();
                os.Vacuum(true);
                os.SortUnclamp();
                os.Close();
                os.Clamp();
                os.Open();
                os.Close();
                os.Unclamp();
                os.Lock();
                os.Vacuum(false);
            }
        }
    }
}
