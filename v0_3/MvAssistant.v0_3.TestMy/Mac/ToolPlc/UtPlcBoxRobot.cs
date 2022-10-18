using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.v0_3;
using MvAssistant.v0_3.Mac.Hal.CompPlc;

namespace MvAssistant.v0_3.Mac.TestMy.ToolPlc
{
    [TestClass]
    public class UtPlcBoxRobot
    {
        public bool boolTestStop = false;

        //[TestMethod]
        //public void TestPlcBoxRobot() //測試 OK
        //{
        //    using (var plc = new MacHalPlcContext())
        //    {
        //        plc.Connect("192.168.0.200", 2);
        //        Console.WriteLine(plc.BoxRobot.Clamp(1));
        //        Console.WriteLine(plc.BoxRobot.Unclamp());
        //        Console.WriteLine(plc.BoxRobot.Initial());
        //        plc.BoxRobot.SetSpeed(10); //Speed：1~100mm/s
        //        Console.WriteLine(plc.BoxRobot.ReadHandPos());
        //        Console.WriteLine(plc.BoxRobot.ReadBoxDetect());
        //        plc.BoxRobot.SetHandSpaceLimit(10, 20);
        //        Console.WriteLine(plc.BoxRobot.ReadHandSpaceLimitSetting());
        //        Console.WriteLine(plc.BoxRobot.ReadHandPosByLSR());
        //        plc.BoxRobot.SetClampToCabinetSpaceLimit(10);
        //        Console.WriteLine(plc.BoxRobot.ReadClampToCabinetSpaceLimitSetting());
        //        Console.WriteLine(plc.BoxRobot.ReadClampDistance());
        //        plc.BoxRobot.SetLevelSensorLimit(10, 15);
        //        Console.WriteLine(plc.BoxRobot.ReadLevelSensorLimitSetting());
        //        Console.WriteLine(plc.BoxRobot.ReadLevelSensor());
        //        Console.WriteLine(plc.BoxRobot.SetLevelReset());
        //        plc.BoxRobot.SetSixAxisSensorLimit(10, 20, 30, 10, 10, 10);
        //        Console.WriteLine(plc.BoxRobot.ReadSixAxisSensorLimitSetting());
        //        Console.WriteLine(plc.BoxRobot.ReadSixAxisSensor());
        //        Console.WriteLine(plc.BoxRobot.ReadHandVacuum());
        //        Console.WriteLine(plc.BoxRobot.ReadBTRobotStatus());
        //        plc.BoxRobot.RobotMoving(true);
        //    }
        //}

        [TestMethod]
        public void TestPlcVariableSetting()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                plc.BoxTransfer.SetSpeedVar(10); //Speed：1~100mm/s
                plc.BoxTransfer.SetHandSpaceLimit(10, 20);
                plc.BoxTransfer.SetClampToCabinetSpaceLimit(10);
                plc.BoxTransfer.SetLevelSensorLimit(10, 15);
                plc.BoxTransfer.SetSixAxisSensorUpperLimit(10, 20, 30, 10, 10, 10);
                Console.WriteLine(plc.BoxTransfer.LevelReset());
            }
        }

        [TestMethod]
        public void TestPlcReadSetting()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                Console.WriteLine(plc.BoxTransfer.ReadSpeedVar());
                Console.WriteLine(plc.BoxTransfer.ReadHandSpaceLimitSetting());
                Console.WriteLine(plc.BoxTransfer.ReadClampToCabinetSpaceLimitSetting());
                Console.WriteLine(plc.BoxTransfer.ReadLevelSensorLimitSetting());
                Console.WriteLine(plc.BoxTransfer.ReadSixAxisSensorUpperLimit());
            }
        }

        [TestMethod]
        public void TestPlcHardwareStatus()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                Console.WriteLine(plc.BoxTransfer.ReadHandPos());
                Console.WriteLine(plc.BoxTransfer.ReadBoxDetect());
                Console.WriteLine(plc.BoxTransfer.ReadHandPosByLSR());
                Console.WriteLine(plc.BoxTransfer.ReadClampDistance());
                Console.WriteLine(plc.BoxTransfer.ReadLevelSensor());
                Console.WriteLine(plc.BoxTransfer.ReadSixAxisSensor());
                Console.WriteLine(plc.BoxTransfer.ReadHandVacuum());
            }
        }

        [TestMethod]
        public void TestPlcComponentStatus()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                Console.WriteLine(plc.BoxTransfer.ReadBTStatus());
            }
        }

        [TestMethod]
        public void TestPlcHardwareAction()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                plc.BoxTransfer.RobotMoving(true);
                Console.WriteLine(plc.BoxTransfer.Initial());
                Console.WriteLine(plc.BoxTransfer.Clamp(1));
                Console.WriteLine(plc.BoxTransfer.Unclamp());
                plc.BoxTransfer.RobotMoving(false);
            }
        }
    }
}
