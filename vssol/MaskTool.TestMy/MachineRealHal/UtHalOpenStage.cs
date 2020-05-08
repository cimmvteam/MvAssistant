using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Manifest;

namespace MvAssistant.Mac.TestMy.MachineRealHal
{
    [TestClass]
    public class UtHalOpenStage
    {
        [TestMethod]
        public void TestOpenStage()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.Load();

                var os = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalOpenStage;

                
            }
        }
    }
}
