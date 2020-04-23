using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant;
using MvAssistant.MaskTool_v0_1.Plc;

namespace MaskTool.TestMy.MachineReal
{
    [TestClass]
    public class UtPlcMaskRobot
    {
        //[TestMethod]
        //public void TestPlcMaskRobot()
        //{
        //    using (var plc = new MvPlcContext())
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
        //        //Console.WriteLine(plc.MaskRobot.ReadSixAxisSensor());
        //        //plc.MaskRobot.SetStaticElecLimit(10, 20);
        //        //Console.WriteLine(plc.MaskRobot.ReadStaticElecLimitSetting());
        //        //Console.WriteLine(plc.MaskRobot.ReadStaticElec());
        //        //Console.WriteLine(plc.MaskRobot.ReadMTRobotStatus());
        //        Console.WriteLine(plc.MaskRobot.ReadHandInspection());//OK
        //    }
        //}

        [TestMethod]
        public void TestPlcVariableSetting()
        {
            using (var plc = new MvPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                plc.MaskRobot.SetSpeed(10);
                plc.MaskRobot.SetStaticElecLimit(10, 20);
            }
        }

        [TestMethod]
        public void TestPlcReadSetting()
        {
            using (var plc = new MvPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                Console.WriteLine(plc.MaskRobot.ReadSpeedSetting());
                Console.WriteLine(plc.MaskRobot.ReadStaticElecLimitSetting());
            }
        }

        [TestMethod]
        public void TestPlcHardwareStatus()
        {
            using (var plc = new MvPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                Console.WriteLine(plc.MaskRobot.ReadStaticElec());
            }
        }

        [TestMethod]
        public void TestPlcComponentStatus()
        {
            using (var plc = new MvPlcContext())
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
            using (var plc = new MvPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                Console.WriteLine(plc.MaskRobot.Initial());
                Console.WriteLine(plc.MaskRobot.Clamp(0));
                Console.WriteLine(plc.MaskRobot.Unclamp());
                Console.WriteLine(plc.MaskRobot.ReadHandInspection()); //OK
                plc.MaskRobot.CCDSpin(10);




            }
        }
    }
}
