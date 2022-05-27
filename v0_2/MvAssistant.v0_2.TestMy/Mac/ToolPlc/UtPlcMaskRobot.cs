using System;
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
                plc.MaskTransfer.SetSpeedVar(10,null);
                plc.MaskTransfer.SetStaticElecLimit(20, 10);
                plc.MaskTransfer.SetSixAxisSensorUpperLimit(10, 20, 30, 10, 10, 10);
                plc.MaskTransfer.SetClampTactileLim(15,10);
                plc.MaskTransfer.SetLevelLimit(15, 10, 5);
            }
        }

        [TestMethod]
        public void TestPlcReadSetting()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                Console.WriteLine(plc.MaskTransfer.ReadSpeedVar());
                Console.WriteLine(plc.MaskTransfer.ReadStaticElecLimit());
                Console.WriteLine(plc.MaskTransfer.ReadSixAxisSensorUpperLimit());
                Console.WriteLine(plc.MaskTransfer.ReadClampTactileLimit());
                Console.WriteLine(plc.MaskTransfer.ReadLevelLimit());
            }
        }

        [TestMethod]
        public void TestPlcHardwareStatus()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                Console.WriteLine(plc.MaskTransfer.ReadStaticElec());
                Console.WriteLine(plc.MaskTransfer.ReadClampGripPos());
                Console.WriteLine(plc.MaskTransfer.ReadCCDSpinDegree());
                Console.WriteLine(plc.MaskTransfer.ReadSixAxisSensor());
                Console.WriteLine(plc.MaskTransfer.ReadClampTactile_FrontSide());
                Console.WriteLine(plc.MaskTransfer.ReadClampTactile_BehindSide());
                Console.WriteLine(plc.MaskTransfer.ReadClampTactile_LeftSide());
                Console.WriteLine(plc.MaskTransfer.ReadClampTactile_RightSide());
                Console.WriteLine(plc.MaskTransfer.ReadLevel());
            }
        }

        [TestMethod]
        public void TestPlcComponentStatus()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                Console.WriteLine(plc.MaskTransfer.ReadMTStatus());
            }
        }

        [TestMethod]
        public void TestPlcHardwareAction()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                plc.MaskTransfer.RobotMoving(true);
                Console.WriteLine(plc.MaskTransfer.Initial());
                Console.WriteLine(plc.MaskTransfer.Clamp(0));
                Console.WriteLine(plc.MaskTransfer.Unclamp());
                Console.WriteLine(plc.MaskTransfer.ReadHandInspection()); //OK
                plc.MaskTransfer.CCDSpin(10);
                plc.MaskTransfer.RobotMoving(false);
            }
        }
    }
}
