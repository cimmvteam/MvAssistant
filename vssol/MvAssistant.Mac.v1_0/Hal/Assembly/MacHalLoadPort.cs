using MvAssistant.Mac.v1_0.Hal.Component;
using MvAssistant.Mac.v1_0.Hal.Component.Camera;
using MvAssistant.Mac.v1_0.Hal.Component.E84;
using MvAssistant.Mac.v1_0.Hal.Component.Identifier;
using MvAssistant.Mac.v1_0.Hal.Component.Motor;
using MvAssistant.Mac.v1_0.Hal.Component.Stage;
using MvAssistant.Mac.v1_0.Hal.CompPlc;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Runtime.InteropServices;

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{
    [GuidAttribute("35E28A32-12E1-413A-8783-8A8018D512F1")]
    public class MacHalLoadPort : MacHalAssemblyBase, IMacHalLoadPort
    {
        #region Device Components


        public IMacHalPlcLoadPort Plc { get { return (IMacHalPlcLoadPort)this.GetMachine(MacEnumDevice.loadport_plc); } }

        public IHalRfidReader RfidReader { get { return (IHalRfidReader)this.GetMachine(MacEnumDevice.loadport_rfid_reader_1); } }
        public IHalPlunger Plunger { get { return (IHalPlunger)this.GetMachine(MacEnumDevice.loadport_plunger_1); } }
        public IHalCamera TopCcd { get { return (IHalCamera)this.GetMachine(MacEnumDevice.loadport_ccd_top_1); } }
        public IHalCamera Sideccd { get { return (IHalCamera)this.GetMachine(MacEnumDevice.loadport_ccd_side_1); } }
        public IHalE84 E84 { get { return (IHalE84)this.GetMachine(MacEnumDevice.loadport_e84_1); } }
        public IHalClamper Clamper { get { return (IHalClamper)this.GetMachine(MacEnumDevice.loadport_clamper_1); } }
        public IHalLoadPortStage Lpstage { get { return (IHalLoadPortStage)this.GetMachine(MacEnumDevice.loadport_stage_1); } }



        #endregion 

 

    }
}
