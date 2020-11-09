using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Manifest;

namespace MvAssistant.Mac.TestMy.MachineRealHal
{
    [TestClass]
    public class UtHalInspectionCh
    {
        [TestMethod]
        public void TestLight()
        {

            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvCfLoad();


                var ic = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalInspectionCh;
                var uni = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                uni.HalConnect();
                ic.HalConnect();

                ic.LightForSideBarDfsSetValue(200);
                ic.LightForSideBarInspSetValue(200);
                ic.LightForTopCrlDefenseSetValue(200);
                ic.LightForTopCrlInspSetValue(200);
                ic.LightForLeftSpotInspSetValue(200);
                ic.LightForRightSpotInspSetValue(200);
            }
        }

        [TestMethod]
        public void TestCamera()
        {

            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvCfLoad();


                var ic = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalInspectionCh;
                var uni = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                uni.HalConnect();
                ic.HalConnect();

                for (double i = -43; i > -46; i-=0.01)
                {
                    ic.ZPosition(i);
                    Thread.Sleep(1000);
                    ic.Camera_TopInsp_CapToSave("D:/Image/IC/TopInsp", "bmp");
                    Thread.Sleep(2000);
                }
                //ic.Camera_SideDfs_CapToSave("D:/Image/IC/SideDfs", "jpg");
                //ic.Camera_SideInsp_CapToSave("D:/Image/IC/SideInsp", "jpg");
                //ic.Camera_TopDfs_CapToSave("D:/Image/IC/TopDfs", "jpg");
            }
        }

        [TestMethod]
        public void TestSetParameter()
        {

            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvCfLoad();

                var ic = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalInspectionCh;
                var uni = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                uni.HalConnect();
                ic.HalConnect();

                ic.SetSpeed(200, 100, 50);
                ic.SetRobotAboutLimit(10, 100);
                ic.SetRobotUpDownLimit(10, -20);
            }
        }

        [TestMethod]
        public void TestReadParameter()
        {

            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvCfLoad();

                var ic = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalInspectionCh;
                var uni = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                uni.HalConnect();
                ic.HalConnect();

                ic.ReadSpeedSetting();
                ic.ReadRobotAboutLimitSetting();
                ic.ReadRobotUpDownLimitSetting();
            }
        }

        [TestMethod]
        public void TestReadComponentValue()
        {

            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvCfLoad();
                
                var ic = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalInspectionCh;
                var uni = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                uni.HalConnect();
                ic.HalConnect();

                ic.ReadRobotIntrude(false);
                ic.ReadXYPosition();
                ic.ReadZPosition();
                ic.ReadWPosition();
                ic.ReadRobotPosAbout();
                ic.ReadRobotPosUpDown();
            }
        }

        [TestMethod]
        public void TestAssemblyWork()
        {

            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvCfLoad();
                
                var ic = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalInspectionCh;
                var uni = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                uni.HalConnect();
                ic.HalConnect();

                ic.ReadRobotIntrude(false);
                ic.Initial();
                ic.XYPosition(200, 100);
                ic.ZPosition(-30);
                ic.WPosition(51);
            }
        }

        [TestMethod]
        public void TestWork_Inspection()
        {

            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvCfLoad();
                
                var ic = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalInspectionCh;
                var uni = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                uni.HalConnect();
                ic.HalConnect();

                ic.HalConnect();
                //ic.Initial();

                ic.InspectionSide();




            }
        }


    }
}
