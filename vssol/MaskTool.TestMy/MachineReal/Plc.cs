using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.MaskTool_v0_1.Plc;

namespace MaskTool.TestMy.MachineReal
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestPlcConnect()
        {

            using(var plc = new MvPlcContext())
            {
                plc.Connect();


            }

        }
    }
}
