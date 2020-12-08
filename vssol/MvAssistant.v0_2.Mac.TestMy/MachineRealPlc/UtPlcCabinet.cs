using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant;
using MvAssistant.Mac.v1_0.Hal.CompPlc;

namespace MaskTool.TestMy.MachineRealPlc
{
    [TestClass]
    public class UtPlcCabinet
    {

        //[TestMethod]
        //public void TestPlcCabinet()//測試 OK
        //{
        //    using (var plc = new MacHalPlcContext())
        //    {
        //        plc.Connect("192.168.0.200", 2);
        //        plc.Cabinet.SetPressureDiffLimit(1, 2);
        //        Console.WriteLine(plc.Cabinet.ReadPressureDiffLimitSetting());
        //        Console.WriteLine(plc.Cabinet.ReadPressureDiff());
        //        plc.Cabinet.SetExhaustFlow(3, 4);
        //        Console.WriteLine(plc.Cabinet.ReadExhaustFlowSetting());
        //        Console.WriteLine(plc.Cabinet.ReadExhaustFlow());//硬體應有另一個實際值，待測
        //        Console.WriteLine(plc.Cabinet.ReadAreaSensor());
        //    }
        //}

        [TestMethod]
        public void TestPlcVariableSetting()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                plc.Cabinet.SetPressureDiffLimit(1, 2);
                plc.Cabinet.SetExhaustFlow(3, 4);
            }
        }

        [TestMethod]
        public void TestPlcReadSetting()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                Console.WriteLine(plc.Cabinet.ReadPressureDiffLimitSetting());
                Console.WriteLine(plc.Cabinet.ReadExhaustFlowSetting());
            }
        }

        [TestMethod]
        public void TestPlcHardwareStatus()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                Console.WriteLine(plc.Cabinet.ReadPressureDiff());
            }
        }

        [TestMethod]
        public void TestPlcComponentStatus()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                Console.WriteLine(plc.Cabinet.ReadLightCurtain());
            }
        }
    }
}
