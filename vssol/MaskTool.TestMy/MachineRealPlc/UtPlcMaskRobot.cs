using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant;
using MvAssistant.Mac.v1_0.Hal.CompPlc;

namespace MaskTool.TestMy.MachineRealPlc
{
    [TestClass]
    public class UtPlcMaskRobot
    {
        //[TestMethod]
        //public void TestPlcMaskRobot()
        //{
        //    using (var plc = new MacHalPlcContext())
        //    {
        //        plc.Connect("192.168.0.200", 2);

        //        //Console.WriteLine(plc.MaskRobot.Clamp(0));
        //        //Console.WriteLine(plc.MaskRobot.Unclamp());
        //        //Console.WriteLine(plc.MaskRobot.Initial());
        //        //plc.MaskRobot.SetSpeed(10);
        //        //Console.WriteLine(plc.MaskRobot.ReadSpeedSetting());
        //        //Console.WriteLine(plc.MaskRobot.ReadClampGripPos());
        //        //plc.MaskRobot.CCDSpin(10);
        //        //Console.WriteLine(plc.MaskRobot.ReadCCDSpinDegree());
        //        //plc.MaskRobot.SetSixAxisSensorLimit(10, 20, 30, 10, 10, 10);
        //        //Console.WriteLine(plc.MaskRobot.ReadSixAxisSensorLimitSetting());
        //        //Console.WriteLine(plc.MaskRobot.ReadSixAxisSensor());
        //        //plc.MaskRobot.SetClampTactileLimit(5);
        //        //Console.WriteLine(plc.MaskRobot.ReadClampTactileLimitSetting());
        //        //plc.MaskRobot.SetStaticElecLimit(10, 20);
        //        //Console.WriteLine(plc.MaskRobot.ReadStaticElecLimitSetting());
        //        //Console.WriteLine(plc.MaskRobot.ReadStaticElec());
        //        //Console.WriteLine(plc.MaskRobot.ReadMTRobotStatus());
        //        Console.WriteLine(plc.MaskRobot.ReadHandInspection());//OK
        //        plc.BoxRobot.RobotMoving(true);
        //    }
        //}

        [TestMethod]
        public void TestPlcVariableSetting()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                plc.MaskRobot.SetSpeed(10,null);
                plc.MaskRobot.SetStaticElecLimit(10, 20);
                plc.MaskRobot.SetSixAxisSensorLimit(10, 20, 30, 10, 10, 10);
                plc.MaskRobot.SetClampTactileLimit(5);
            }
        }

        [TestMethod]
        public void TestPlcReadSetting()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                Console.WriteLine(plc.MaskRobot.ReadSpeedSetting());
                Console.WriteLine(plc.MaskRobot.ReadStaticElecLimitSetting());
                Console.WriteLine(plc.MaskRobot.ReadSixAxisSensorLimitSetting());
                Console.WriteLine(plc.MaskRobot.ReadClampTactileLimitSetting());
            }
        }

        [TestMethod]
        public void TestPlcHardwareStatus()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                Console.WriteLine(plc.MaskRobot.ReadStaticElec());
            }
        }

        [TestMethod]
        public void TestPlcComponentStatus()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                Console.WriteLine(plc.MaskRobot.ReadMTRobotStatus());
                Console.WriteLine(plc.MaskRobot.ReadClampGripPos());
                Console.WriteLine(plc.MaskRobot.ReadCCDSpinDegree());
                Console.WriteLine(plc.MaskRobot.ReadSixAxisSensor());
            }
        }

        [TestMethod]
        public void TestPlcHardwareAction()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                plc.MaskRobot.RobotMoving(true);
                Console.WriteLine(plc.MaskRobot.Initial());
                Console.WriteLine(plc.MaskRobot.Clamp(0));
                Console.WriteLine(plc.MaskRobot.Unclamp());
                Console.WriteLine(plc.MaskRobot.ReadHandInspection()); //OK
                plc.MaskRobot.CCDSpin(10);
                plc.MaskRobot.RobotMoving(false);
            }
        }
    }
}
