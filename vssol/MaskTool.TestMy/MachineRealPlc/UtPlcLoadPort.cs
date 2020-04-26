using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant;
using MvAssistant.Mac.v1_0.CompPlc;

namespace MaskTool.TestMy.MachineRealPlc
{
    [TestClass]
    public class UtPlcLoadPort
    {
        //[TestMethod]
        //public void TestPlcLoadPort()//測試 OK
        //{
        //    using (var plc = new MvPlcContext())
        //    {
        //        plc.Connect("192.168.0.200", 2);

        //        plc.LoadPort.SetPressureDiffLimit(1, 2);
        //        Console.WriteLine(plc.LoadPort.ReadPressureDiffLimitSrtting());
        //        Console.WriteLine(plc.LoadPort.ReadPressureDiff());
        //    }
        //}

        [TestMethod]
        public void TestPlcVariableSetting()
        {
            using (var plc = new MvPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                plc.LoadPort.SetPressureDiffLimit(1, 2);
            }
        }

        [TestMethod]
        public void TestPlcReadSetting()
        {
            using (var plc = new MvPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                Console.WriteLine(plc.LoadPort.ReadPressureDiffLimitSrtting());
            }
        }

        [TestMethod]
        public void TestPlcHardwareStatus()
        {
            using (var plc = new MvPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                Console.WriteLine(plc.LoadPort.ReadPressureDiff());
            }
        }
    }
}
