using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.DeviceDrive.OmronPlc;
using MvAssistant.Mac.v1_0.Hal.CompPlc;

namespace MaskTool.TestMy.Device
{
    [TestClass]
    public class UtDevicePlc
    {
        [TestMethod]
        public void TestPlcOn()
        {

            using(var plc = new MvOmronPlcLdd())
            {

                plc.NLPLC_Initial("192.168.0.200", 2);


                plc.Write(MacHalPlcEnumVariable.PC_TO_PLC_CheckClock.ToString(), true);
                Thread.Sleep(500);
                var test = plc.Read(MacHalPlcEnumVariable.PC_TO_PLC_CheckClock_Reply.ToString());


                Console.WriteLine(test);

            }

        }

        [TestMethod]
        public void TestPlcInsResult()
        {

            using (var plc = new MvOmronPlcLdd())
            {

                plc.NLPLC_Initial("192.168.0.200", 2);


                var test = plc.Read(MacHalPlcEnumVariable.PC_TO_IC_XPoint.ToString());
                plc.Write(MacHalPlcEnumVariable.PC_TO_IC_XPoint.ToString(), 10.0);
                test = plc.Read(MacHalPlcEnumVariable.PC_TO_IC_XPoint.ToString());

                Console.WriteLine(test);

            }

        }
    }
}
