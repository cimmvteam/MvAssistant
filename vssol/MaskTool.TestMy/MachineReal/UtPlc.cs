using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant;
using MvAssistant.MaskTool_v0_1.Plc;

namespace MaskTool.TestMy.MachineReal
{
    [TestClass]
    public class UtPlc
    {
        [TestMethod]
        public void TestPlcConnect()
        {

            using (var plc = new MvPlcContext())
            {
                plc.StartAsyn();

                if (!SpinWait.SpinUntil(() => plc.IsConnected, 5000))
                    throw new MvException("PLC connection fail");

                Console.WriteLine("PLC connection success");


            }

        }
<<<<<<< HEAD
        public void TestHandshake()
        {

        }
=======




>>>>>>> b54f04460879b9de87c7ac34cd9e08eb48c3a7b4
    }
}
