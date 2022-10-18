using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.v0_3.Mac.Hal;
using MvAssistant.v0_3.Mac.Hal.Assembly;
using MvAssistant.v0_3.Mac.Manifest;

namespace MvAssistant.v0_3.Mac.TestMy.ToolHal.Hirata_ScenarioTest
{
    [TestClass]
    public class Ut012_LP
    {
        [TestMethod]
        public void TestMethod1()//OK
        {
            try
            {
                using (var halContext = new MacHalContext("UserData/Manifest/Manifest.xml.real"))
                {
                    halContext.MvaCfBootup();
                    halContext.MvaCfLoad();

                    var unv = halContext.HalDevices[EnumMacDeviceId.eqp_assembly.ToString()] as MacHalEqp;
                    var lpa = halContext.HalDevices[EnumMacDeviceId.loadportA_assembly.ToString()] as MacHalLoadPort;
                    var lpb = halContext.HalDevices[EnumMacDeviceId.loadportB_assembly.ToString()] as MacHalLoadPort;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    lpb.HalConnect();
                    lpa.HalConnect();

                    //1. POD內有光罩, 且POD在Load Port A為開啟狀態

                    //2~5  Load Poat A Undock
                    lpa.Undock();

                    //6. POD內有光罩, 且POD在Load Port B為開啟狀態

                    //7~10  Load Poat B Undock
                    lpb.Undock();
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
