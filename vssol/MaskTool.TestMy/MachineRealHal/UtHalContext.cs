using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0.Hal;

namespace MvAssistant.Mac.TestMy.MachineRealHal
{
    [TestClass]
    public class UtHalContext
    {
        [TestMethod]
        public void TestMethod1()
        {


            var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real");
            halContext.Load();

            halContext.Connect();




            








        }
    }
}
