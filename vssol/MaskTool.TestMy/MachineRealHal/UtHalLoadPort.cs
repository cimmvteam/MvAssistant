using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Manifest;

namespace MvAssistant.Mac.TestMy.MachineRealHal
{
    [TestClass]
    public class UtHalLoadPort
    {
        [TestMethod]
        public void TestSetParameter()
        {
            var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real");
            halContext.Load();

            var lp = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalLoadPort;
            lp.SetPressureDiffLimit(40,50);
        }

        [TestMethod]
        public void TestReadParameter()
        {
            var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real");
            halContext.Load();

            var lp = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalLoadPort;
            lp.ReadPressureDiffLimitSrtting();
        }

        [TestMethod]
        public void TestReadComponentValue()
        {
            var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real");
            halContext.Load();

            var lp = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalLoadPort;
            lp.ReadPressureDiff();
        }

        [TestMethod]
        public void TestAssemblyWork()
        {
            var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real");
            halContext.Load();

            var lp = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalLoadPort;
        }
    }
}
