using MaskAutoCleaner.Hal.Intf.Assembly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaskAutoCleaner.Manifest;
using MaskAutoCleaner.Hal.Intf.Component;
using MaskAutoCleaner.Hal.Intf.Component.Identifier;
using MaskAutoCleaner.Hal.Intf.Component.Stage;
using MaskAutoCleaner.Hal.Intf.Component.Motor;
using System.Runtime.InteropServices;

namespace MaskAutoCleaner.Hal.Imp.Assembly
{
    [MachineManifest(DeviceEnum.loadport_assembly)]
    [GuidAttribute("35E28A32-12E1-413A-8783-8A8018D512F1")]
    public class HalLoadPort : HalAssemblyBase, IHalLoadPort
    {
        #region DeviceHAL



        [MachineManifest(DeviceEnum.loadport_rfid_reader_1)]
        public IHalRfidReader RfidReader { get; set; }
        [MachineManifest(DeviceEnum.loadport_plunger_1)]
        public IHalPlunger Plunger { get; set; }
        [MachineManifest(DeviceEnum.loadport_ccd_top_1)]
        public IHalCamera TopCcd { get; set; }
        [MachineManifest(DeviceEnum.loadport_ccd_side_1)]
        public IHalCamera Sideccd { get; set; }
        [MachineManifest(DeviceEnum.loadport_e84_1)]
        public IHalE84 E84 { get; set; }
        [MachineManifest(DeviceEnum.loadport_clamper_1)]
        public IHalClamper Clamper { get; set; }
        [MachineManifest(DeviceEnum.loadport_stage_1)]
        public IHalLoadPortStage Lpstage { get; set; }



        #endregion

        #region HAL Interface Functions
        int IHalAssembly.HalStop()
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
