using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant;
using MvAssistant.Mac.v1_0.Hal.CompPlc;

namespace MaskTool.TestMy.MachineRealPlc
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
        //        Console.WriteLine(plc.BoxRobot.ReadSpeedSetting());
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
                plc.BoxRobot.SetSpeed(10); //Speed：1~100mm/s
                plc.BoxRobot.SetHandSpaceLimit(10, 20);
                plc.BoxRobot.SetClampToCabinetSpaceLimit(10);
                plc.BoxRobot.SetLevelSensorLimit(10, 15);
                plc.BoxRobot.SetSixAxisSensorLimit(10, 20, 30, 10, 10, 10);
                Console.WriteLine(plc.BoxRobot.SetLevelReset());
            }
        }

        [TestMethod]
        public void TestPlcReadSetting()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                Console.WriteLine(plc.BoxRobot.ReadSpeedSetting());
                Console.WriteLine(plc.BoxRobot.ReadHandSpaceLimitSetting());
                Console.WriteLine(plc.BoxRobot.ReadClampToCabinetSpaceLimitSetting());
                Console.WriteLine(plc.BoxRobot.ReadLevelSensorLimitSetting());
                Console.WriteLine(plc.BoxRobot.ReadSixAxisSensorLimitSetting());
            }
        }

        [TestMethod]
        public void TestPlcHardwareStatus()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                Console.WriteLine(plc.BoxRobot.ReadHandPos());
                Console.WriteLine(plc.BoxRobot.ReadBoxDetect());
                Console.WriteLine(plc.BoxRobot.ReadHandPosByLSR());
                Console.WriteLine(plc.BoxRobot.ReadClampDistance());
                Console.WriteLine(plc.BoxRobot.ReadLevelSensor());
                Console.WriteLine(plc.BoxRobot.ReadSixAxisSensor());
                Console.WriteLine(plc.BoxRobot.ReadHandVacuum());
            }
        }

        [TestMethod]
        public void TestPlcComponentStatus()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                Console.WriteLine(plc.BoxRobot.ReadBTRobotStatus());
            }
        }

        [TestMethod]
        public void TestPlcHardwareAction()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                plc.BoxRobot.RobotMoving(true);
                Console.WriteLine(plc.BoxRobot.Initial());
                Console.WriteLine(plc.BoxRobot.Clamp(1));
                Console.WriteLine(plc.BoxRobot.Unclamp());
                plc.BoxRobot.RobotMoving(false);
            }
        }
    }
}
