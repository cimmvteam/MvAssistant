using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.v0_3.DeviceDrive.KjMachineDrawer;
using MvAssistant.v0_3.Mac.Hal;
using MvAssistant.v0_3.Mac.Hal.Assembly;
using MvAssistant.v0_3.Mac.Manifest;
// vs 2013
//using static MvAssistant.v0_3.DeviceDrive.KjMachineDrawer.MvKjMachineDrawerLdd;
using System.Net;
using System.Threading;
using MvAssistant.v0_3.DeviceDrive.KjMachineDrawer.ReplyCode;
using MvAssistant.v0_3.DeviceDrive.KjMachineDrawer.DrawerEventArgs;

namespace MvAssistant.v0_3.Mac.TestMy.ToolHal
{
    [TestClass]
    public class UtHalCabinet
    {
      
        [TestMethod]
        public void TestSetParameter()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvaCfLoad();

                var cbn = halContext.HalDevices[EnumMacDeviceId.cabinet_assembly.ToString()] as MacHalCabinet;
                var uni = halContext.HalDevices[EnumMacDeviceId.eqp_assembly.ToString()] as MacHalEqp;
                uni.HalConnect();
                cbn.HalConnect();

                cbn.SetChamberPressureDiffLimit(50, 60);
                cbn.SetExhaustFlowVar(20, 35);
            }
        }

        [TestMethod]
        public void TestReadParameter()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvaCfLoad();

                var cbn = halContext.HalDevices[EnumMacDeviceId.cabinet_assembly.ToString()] as MacHalCabinet;
                var uni = halContext.HalDevices[EnumMacDeviceId.eqp_assembly.ToString()] as MacHalEqp;
                uni.HalConnect();
                cbn.HalConnect();

                cbn.ReadChamberPressureDiffLimit();
                cbn.ReadExhaustFlowVar();
            }
        }

        [TestMethod]
        public void TestReadComponentValue()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvaCfLoad();

                var cbn = halContext.HalDevices[EnumMacDeviceId.cabinet_assembly.ToString()] as MacHalCabinet;
                var uni = halContext.HalDevices[EnumMacDeviceId.eqp_assembly.ToString()] as MacHalEqp;
                uni.HalConnect();
                cbn.HalConnect();

                cbn.ReadChamberPressureDiff();
                cbn.ReadLightCurtain();
            }
        }

        [TestMethod]
        public void TestAssemblyWork()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.MvaCfLoad();

                var cbn = halContext.HalDevices[EnumMacDeviceId.cabinet_assembly.ToString()] as MacHalCabinet;
                var uni = halContext.HalDevices[EnumMacDeviceId.eqp_assembly.ToString()] as MacHalEqp;
                uni.HalConnect();
                cbn.HalConnect();
            }
        }
    }
    
}
