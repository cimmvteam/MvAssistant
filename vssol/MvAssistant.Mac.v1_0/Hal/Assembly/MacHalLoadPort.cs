using MvAssistant.Mac.v1_0.Hal.Component;
using MvAssistant.Mac.v1_0.Hal.Component.Camera;
using MvAssistant.Mac.v1_0.Hal.Component.E84;
using MvAssistant.Mac.v1_0.Hal.Component.Identifier;
using MvAssistant.Mac.v1_0.Hal.Component.Motor;
using MvAssistant.Mac.v1_0.Hal.Component.Stage;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Runtime.InteropServices;

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{
    [MacMachineManifest(MacEnumDevice.loadport_assembly)]
    [GuidAttribute("35E28A32-12E1-413A-8783-8A8018D512F1")]
    public class MacHalLoadPort : MacHalAssemblyBase, IMacHalLoadPort
    {
        #region DeviceHAL



        [MacMachineManifest(MacEnumDevice.loadport_rfid_reader_1)]
        public IHalRfidReader RfidReader { get; set; }
        [MacMachineManifest(MacEnumDevice.loadport_plunger_1)]
        public IHalPlunger Plunger { get; set; }
        [MacMachineManifest(MacEnumDevice.loadport_ccd_top_1)]
        public IHalCamera TopCcd { get; set; }
        [MacMachineManifest(MacEnumDevice.loadport_ccd_side_1)]
        public IHalCamera Sideccd { get; set; }
        [MacMachineManifest(MacEnumDevice.loadport_e84_1)]
        public IHalE84 E84 { get; set; }
        [MacMachineManifest(MacEnumDevice.loadport_clamper_1)]
        public IHalClamper Clamper { get; set; }
        [MacMachineManifest(MacEnumDevice.loadport_stage_1)]
        public IHalLoadPortStage Lpstage { get; set; }



        #endregion

        #region HAL Interface Functions
        int IMacHalAssembly.HalStop()
        {
            throw new NotImplementedException();
        }

        int IHal.HalConnect()
        {
            throw new NotImplementedException();
        }

        int IHal.HalClose()
        {
            throw new NotImplementedException();
        }

        bool IHal.HalIsConnected()
        {
            throw new NotImplementedException();
        }
        #endregion HAL Interface Functions


    }
}
