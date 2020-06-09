using System;
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
        public void TestSetParameter()
        {

            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvCfLoad();


                var ic = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalInspectionCh;

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

                ic.Initial();
                ic.XYPosition(200, 100);
                ic.ZPosition(-50);
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

                ic.HalConnect();
                //ic.Initial();

                ic.InspectionSide();




            }
        }


    }
}
