using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Manifest;

namespace MvAssistant.Mac.TestMy.MachineRealHal
{
    [TestClass]
    public class UtHalInspectionCh
    {
        [TestMethod]
        public void TestStageXyShift()
        {

            var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real");
            halContext.Load();


            var ic = halContext.HalDevices[MacEnumDevice.inspection_assembly.ToString()] as MacHalInspectionCh;


            ic.Inspection_stage_1.HalConnect();

        }
    }
}
