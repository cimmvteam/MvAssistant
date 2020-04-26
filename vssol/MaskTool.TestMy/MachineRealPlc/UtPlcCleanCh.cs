using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant;
using MvAssistant.Mac.v1_0.CompPlc;

namespace MaskTool.TestMy.MachineRealPlc
{
    [TestClass]
    public class UtPlcCleanCh
    {
       
        //[TestMethod]
        //public void TestPlcCleanCh()//測試 OK
        //{
        //    using (var plc = new MvPlcContext())
        //    {
        //        plc.Connect("192.168.0.200", 2);

        //        plc.CleanCh.SetParticleCntLimit(1, 2, 3);
        //        Console.WriteLine(plc.CleanCh.ReadParticleCntLimitSetting());
        //        Console.WriteLine(plc.CleanCh.ReadParticleCount());
        //        Console.WriteLine(plc.CleanCh.ReadMaskLevel());
        //        plc.CleanCh.SetRobotAboutLimit(10, -10);
        //        Console.WriteLine(plc.CleanCh.ReadRobotAboutLimitSetting());
        //        Console.WriteLine(plc.CleanCh.ReadRobotPosAbout());
        //        plc.CleanCh.SetRobotUpDownLimit(10, -10);
        //        Console.WriteLine(plc.CleanCh.ReadRobotUpDownLimitSetting());
        //        Console.WriteLine(plc.CleanCh.ReadRobotPosUpDown());
        //        plc.CleanCh.SetPressureDiffLimit(2);
        //        Console.WriteLine(plc.CleanCh.ReadPressureDiffLimitSetting());
        //        Console.WriteLine(plc.CleanCh.ReadPressureDiff());
        //        Console.WriteLine(plc.CleanCh.GasValveBlow(20));
        //        plc.CleanCh.SetPressureCtrl(10);
        //        Console.WriteLine(plc.CleanCh.ReadPressureCtrlSetting());
        //        Console.WriteLine(plc.CleanCh.ReadBlowPressure());
        //        Console.WriteLine(plc.CleanCh.ReadPressure());
        //        Console.WriteLine(plc.CleanCh.ReadAreaSensor());
        //    }
        //}

        [TestMethod]
        public void TestPlcVariableSetting()
        {
            using (var plc = new MvPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                plc.CleanCh.SetParticleCntLimit(1, 2, 3);
                plc.CleanCh.SetRobotAboutLimit(10, -10);
                plc.CleanCh.SetRobotUpDownLimit(10, -10);
                plc.CleanCh.SetPressureDiffLimit(2);
                plc.CleanCh.SetPressureCtrl(10);
            }
        }

        [TestMethod]
        public void TestPlcReadSetting()
        {
            using (var plc = new MvPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                Console.WriteLine(plc.CleanCh.ReadParticleCntLimitSetting());
                Console.WriteLine(plc.CleanCh.ReadRobotAboutLimitSetting());
                Console.WriteLine(plc.CleanCh.ReadRobotUpDownLimitSetting());
                Console.WriteLine(plc.CleanCh.ReadPressureDiffLimitSetting());
                Console.WriteLine(plc.CleanCh.ReadPressureCtrlSetting());
            }
        }

        [TestMethod]
        public void TestPlcHardwareStatus()
        {
            using (var plc = new MvPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                Console.WriteLine(plc.CleanCh.ReadParticleCount());
                Console.WriteLine(plc.CleanCh.ReadMaskLevel());
                Console.WriteLine(plc.CleanCh.ReadRobotPosAbout());
                Console.WriteLine(plc.CleanCh.ReadRobotPosUpDown());
                Console.WriteLine(plc.CleanCh.ReadPressureDiff());
                Console.WriteLine(plc.CleanCh.ReadBlowPressure());
                Console.WriteLine(plc.CleanCh.ReadPressure());
                Console.WriteLine(plc.CleanCh.ReadAreaSensor());
            }
        }

        [TestMethod]
        public void TestPlcHardwareAction()
        {
            using (var plc = new MvPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                Console.WriteLine(plc.CleanCh.GasValveBlow(20));
            }
        }
    }
}
