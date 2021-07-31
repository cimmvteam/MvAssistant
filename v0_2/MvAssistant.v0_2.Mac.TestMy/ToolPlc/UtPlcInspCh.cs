using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.v0_2;
using MvAssistant.v0_2.Mac.Hal.CompPlc;

namespace MvAssistant.v0_2.Mac.TestMy.ToolPlc
{
    [TestClass]
    public class UtPlcInspCh
    {
        //[TestMethod]
        //public void TestPlcInspCh()//測試 OK
        //{
        //    using (var plc = new MacHalPlcContext())
        //    {
        //        plc.Connect("192.168.0.200", 2);
        //        //bool[] AlarmArray = new bool[256];

        //        Console.WriteLine(plc.InspCh.XYPosition(20, 10));//X:300~-10,Y:250~-10
        //        Console.WriteLine(plc.InspCh.ZPosition(-10));//1~-85
        //        Console.WriteLine(plc.InspCh.WPosition(20));//0~359
        //        Console.WriteLine(plc.InspCh.Initial());
        //        plc.InspCh.SetSpeed(10, 10, 10);
        //        Console.WriteLine(plc.InspCh.ReadRobotIntrude(true));
        //        Console.WriteLine(plc.InspCh.ReadXYPosition());
        //        Console.WriteLine(plc.InspCh.ReadZPosition());
        //        Console.WriteLine(plc.InspCh.ReadWPosition());
        //        plc.InspCh.SetRobotAboutLimit(-10, 10);
        //        Console.WriteLine(plc.InspCh.ReadRobotAboutLimitSetting());
        //        Console.WriteLine(plc.InspCh.ReadRobotPosAbout());
        //        plc.InspCh.SetRobotUpDownLimit(10, 0);
        //        Console.WriteLine(plc.InspCh.ReadRobotUpDownLimitSetting());
        //        Console.WriteLine(plc.InspCh.ReadRobotPosUpDown());
        //        Console.WriteLine(plc.InspCh.ReadInspChStatus());

        //        //AlarmArray = plc.InspCh.ReadAlarmArray();
        //        //Console.WriteLine(plc.InspCh.ReadAlarmArray());
        //    }
        //}
        [TestMethod]
        public void TestPlcVariableSetting()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                plc.InspCh.SetSpeedVar(10, 10, 10);
                plc.InspCh.SetRobotPosLeftRightLimit(-10, 10);
                plc.InspCh.SetRobotPosUpDownLimit(10, 0);
            }
        }

        [TestMethod]
        public void TestPlcReadSetting()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                Console.WriteLine(plc.InspCh.ReadSpeedVar());
                Console.WriteLine(plc.InspCh.ReadRobotPosLeftRightLimit());
                Console.WriteLine(plc.InspCh.ReadRobotPosUpDownLimit());
            }
        }

        [TestMethod]
        public void TestPlcHardwareStatus()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                Console.WriteLine(plc.InspCh.ReadXYPosition());
                Console.WriteLine(plc.InspCh.ReadZPosition());
                Console.WriteLine(plc.InspCh.ReadWPosition());
                Console.WriteLine(plc.InspCh.ReadRobotPosLeftRight());
                Console.WriteLine(plc.InspCh.ReadRobotPosUpDown());
            }
        }

        [TestMethod]
        public void TestPlcComponentStatus()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                Console.WriteLine(plc.InspCh.ReadInspChStatus());
            }
        }

        [TestMethod]
        public void TestPlcHardwareAction()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                Console.WriteLine(plc.InspCh.Initial());
                Console.WriteLine(plc.InspCh.XYPosition(20, 10));//X:300~-10,Y:250~-10
                Console.WriteLine(plc.InspCh.ZPosition(-30));//1~-85
                Console.WriteLine(plc.InspCh.WPosition(20));//0~359
                Console.WriteLine(plc.InspCh.SetRobotIntrude(true));
            }
        }
    }
}
