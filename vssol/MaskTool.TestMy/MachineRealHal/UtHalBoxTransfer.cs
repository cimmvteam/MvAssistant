using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Manifest;

namespace MvAssistant.Mac.TestMy.MachineRealHal
{
    [TestClass]
    public class UtHalBoxTransfer
    {
        [TestMethod]
        public void TestSetParameter()
        {
            var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real");
            halContext.Load();

            var bt = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalBoxTransfer;

            bt.SetSpeed(20);
            bt.SetHandSpaceLimit(10,20);
            bt.SetClampToCabinetSpaceLimit(50);
            bt.SetLevelSensorLimit(5, 6);
            bt.SetSixAxisSensorLimit(1, 2, 3, 4, 5, 6);
        }

        [TestMethod]
        public void TestReadParameter()
        {
            var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real");
            halContext.Load();

            var bt = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalBoxTransfer;

            bt.ReadSpeedSetting();
            bt.ReadHandSpaceLimitSetting();
            bt.ReadClampToCabinetSpaceLimitSetting();
            bt.ReadLevelSensorLimitSetting();
            bt.ReadSixAxisSensorLimitSetting();
        }

        [TestMethod]
        public void TestReadComponentValue()
        {
            var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real");
            halContext.Load();

            var bt = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalBoxTransfer;

            bt.ReadHandPos();
            bt.ReadBoxDetect();
            bt.ReadHandPosByLSR();
            bt.ReadClampDistance();
            bt.ReadLevelSensor();
            bt.ReadSixAxisSensor();
            bt.ReadHandVacuum();
        }

        [TestMethod]
        public void TestAssemblyWork()
        {
            var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real");
            halContext.Load();

            var bt = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalBoxTransfer;

            bt.Clamp(1);
            bt.Unclamp();
            bt.LevelReset();
            bt.ReadBTRobotStatus();
            bt.RobotMoving(false);
            bt.Initial();
        }
    }
}
