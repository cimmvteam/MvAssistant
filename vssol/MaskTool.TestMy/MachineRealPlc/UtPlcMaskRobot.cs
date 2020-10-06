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
        [TestMethod]
        public void TestPlcVariableSetting()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                plc.MaskRobot.SetSpeed(10,null);
                plc.MaskRobot.SetStaticElecLimit(20, 10);
                plc.MaskRobot.SetSixAxisSensorUpperLimit(10, 20, 30, 10, 10, 10);
                plc.MaskRobot.SetClampTactileLim(15,10);
                plc.MaskRobot.SetLevelLimit(15, 10, 5);
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
                Console.WriteLine(plc.MaskRobot.ReadSixAxisSensorUpperLimitSetting());
                Console.WriteLine(plc.MaskRobot.ReadClampTactileLimSetting());
                Console.WriteLine(plc.MaskRobot.ReadLevelLimitSetting());
            }
        }

        [TestMethod]
        public void TestPlcHardwareStatus()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                Console.WriteLine(plc.MaskRobot.ReadStaticElec());
                Console.WriteLine(plc.MaskRobot.ReadClampGripPos());
                Console.WriteLine(plc.MaskRobot.ReadCCDSpinDegree());
                Console.WriteLine(plc.MaskRobot.ReadSixAxisSensor());
                Console.WriteLine(plc.MaskRobot.ReadClampTactile_FrontSide());
                Console.WriteLine(plc.MaskRobot.ReadClampTactile_BehindSide());
                Console.WriteLine(plc.MaskRobot.ReadClampTactile_LeftSide());
                Console.WriteLine(plc.MaskRobot.ReadClampTactile_RightSide());
                Console.WriteLine(plc.MaskRobot.ReadLevel());
            }
        }

        [TestMethod]
        public void TestPlcComponentStatus()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                Console.WriteLine(plc.MaskRobot.ReadMTRobotStatus());
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
