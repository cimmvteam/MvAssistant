﻿using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.v0_2;
using MvAssistant.v0_2.Mac.Hal.CompPlc;

namespace MvAssistant.v0_2.Mac.TestMy.ToolPlc
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
                plc.MaskRobot.SetSpeedVar(10,null);
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
                Console.WriteLine(plc.MaskRobot.ReadSpeedVar());
                Console.WriteLine(plc.MaskRobot.ReadStaticElecLimit());
                Console.WriteLine(plc.MaskRobot.ReadSixAxisSensorUpperLimit());
                Console.WriteLine(plc.MaskRobot.ReadClampTactileLimit());
                Console.WriteLine(plc.MaskRobot.ReadLevelLimit());
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
