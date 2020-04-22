using MaskAutoCleaner.Hal.Intf.Assembly;
using MaskAutoCleaner.Hal.Intf.Component;
using MaskAutoCleaner.Hal.Intf.Component.Identifier;
using MaskAutoCleaner.Hal.Intf.Component.Stage;
using MaskAutoCleaner.Hal.Intf.Component.Motor;

using MaskAutoCleaner.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace MaskAutoCleaner.Hal.ImpFake.Assembly
{
    [MachineManifest(DeviceEnum.loadport_assembly)]
    [GuidAttribute("BCD29C6A-D36A-4191-900B-32E74DC894DF")]
    public class HalLoadPortFake : HalFakeBase, IHalLoadPort
    {
        #region DeviceHAL
        private IHalRfidReader rfidReader = null;
        [MachineManifest(DeviceEnum.loadport_rfid_reader_1)]
        public IHalRfidReader RfidReader { get { return rfidReader; } set { rfidReader = value; } }
        private IHalPlunger plunger = null;
        [MachineManifest(DeviceEnum.loadport_plunger_1)]
        public IHalPlunger Plunger { get { return plunger; } set { plunger = value; } }
        private IHalCamera topccd = null;
        [MachineManifest(DeviceEnum.loadport_ccd_top_1)]
        public IHalCamera TopCcd { get { return topccd; } set { topccd = value; } }
        private IHalCamera sideccd = null;
        [MachineManifest(DeviceEnum.loadport_ccd_side_1)]
        public IHalCamera Sideccd { get { return sideccd; } set { sideccd = value; } }
        private IHalLaser mask_sidelaser1 = null;

        private IHalE84 e84 = null;
        [MachineManifest(DeviceEnum.loadport_e84_1)]
        public IHalE84 E84 { get { return e84; } set { e84 = value; } }
        private IHalClamper clamper = null;
        [MachineManifest(DeviceEnum.loadport_clamper_1)]
        public IHalClamper Clamper { get { return clamper; } set { clamper = value; } }
        private IHalLaser mask_sidelaser2 = null;
        private IHalLoadPortStage lpstage = null;
        [MachineManifest(DeviceEnum.loadport_stage_1)]
        public IHalLoadPortStage Lpstage { get { return lpstage; } set { lpstage = value; } }

        #endregion

        #region HAL Interface Functions
        public int HalStop()
        {
            throw new NotImplementedException();
        }
        #endregion HAL Interface Functions
    }
}
