using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Manifest;

namespace MvAssistant.Mac.TestMy.MachineRealHal
{
    [TestClass]
    public class UtHalCabinet
    {
        [TestMethod]
        public void TestSetParameter()
        {
           var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real");
            halContext.Load();
            
            var cbn = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalCabinet;

            cbn.SetPressureDiffLimit(50, 60);
            cbn.SetExhaustFlow(20, 35);
        }

        [TestMethod]
        public void TestReadParameter()
        {
            var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real");
            halContext.Load();

            var cbn = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalCabinet;

            cbn.ReadPressureDiffLimitSetting();
            cbn.ReadExhaustFlowSetting();
        }

        [TestMethod]
        public void TestReadComponentValue()
        {
            var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real");
            halContext.Load();

            var cbn = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalCabinet;

            cbn.ReadPressureDiff();
            cbn.ReadLightCurtain();
        }

        [TestMethod]
        public void TestAssemblyWork()
        {
            var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real");
            halContext.Load();

            var cbn = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalCabinet;

        }
    }
}
