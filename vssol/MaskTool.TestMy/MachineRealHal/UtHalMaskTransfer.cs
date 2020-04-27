using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Manifest;

namespace MvAssistant.Mac.TestMy.MachineRealHal
{
    [TestClass]
    public class UtHalMaskTransfer
    {
        [TestMethod]
        public void TestRobotMoveToLoadPort()
        {
            var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real");
            halContext.Load();


            var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;

            mt.Robot.HalMoveAsyn();





        }
    }
}
