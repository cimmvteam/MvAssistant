using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.v0_2;
using MvAssistant.v0_2.Mac.Hal.CompPlc;

namespace MvAssistant.v0_2.Mac.TestMy.ToolPlc
{
    [TestClass]
    public class UtPlcLoadPort
    {
        //[TestMethod]
        //public void TestPlcLoadPort()//測試 OK
        //{
        //    using (var plc = new MacHalPlcContext())
        //    {
        //        plc.Connect("192.168.0.200", 2);

        //        plc.LoadPort.SetPressureDiffLimit(1, 2);
        //        Console.WriteLine(plc.LoadPort.ReadPressureDiff());
        //    }
        //}

        [TestMethod]
        public void TestPlcVariableSetting()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                plc.LoadPort.SetPressureDiffLimit(1, 2);
            }
        }

        [TestMethod]
        public void TestPlcReadSetting()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                Console.WriteLine(plc.LoadPort.ReadChamberPressureDiffLimit());
            }
        }

        [TestMethod]
        public void TestPlcHardwareStatus()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                Console.WriteLine(plc.LoadPort.ReadPressureDiff());
            }
        }
    }
}
