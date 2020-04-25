using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Hal.Component;
using MvAssistant.Mac.v1_0.Hal.Component.Identifier;
using MvAssistant.Mac.v1_0.Hal.Component.Stage;
using MvAssistant.Mac.v1_0.Hal.Component.Motor;

using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using MvAssistant.Mac.v1_0.Hal.Component.Camera;
using MvAssistant.Mac.v1_0.Hal.Component.E84;

namespace MvAssistant.Mac.v1_0.Hal.AssemblyFake
{
    [MachineManifest(EnumDevice.loadport_assembly)]
    [GuidAttribute("BCD29C6A-D36A-4191-900B-32E74DC894DF")]
    public class HalLoadPortFake : HalFakeBase, IHalLoadPort
    {
        #region DeviceHAL
        private IHalRfidReader rfidReader = null;
        [MachineManifest(EnumDevice.loadport_rfid_reader_1)]
        public IHalRfidReader RfidReader { get { return rfidReader; } set { rfidReader = value; } }
        private IHalPlunger plunger = null;
        [MachineManifest(EnumDevice.loadport_plunger_1)]
        public IHalPlunger Plunger { get { return plunger; } set { plunger = value; } }
        private IHalCamera topccd = null;
        [MachineManifest(EnumDevice.loadport_ccd_top_1)]
        public IHalCamera TopCcd { get { return topccd; } set { topccd = value; } }
        private IHalCamera sideccd = null;
        [MachineManifest(EnumDevice.loadport_ccd_side_1)]
        public IHalCamera Sideccd { get { return sideccd; } set { sideccd = value; } }
        private IHalLaser mask_sidelaser1 = null;

        private IHalE84 e84 = null;
        [MachineManifest(EnumDevice.loadport_e84_1)]
        public IHalE84 E84 { get { return e84; } set { e84 = value; } }
        private IHalClamper clamper = null;
        [MachineManifest(EnumDevice.loadport_clamper_1)]
        public IHalClamper Clamper { get { return clamper; } set { clamper = value; } }
        private IHalLaser mask_sidelaser2 = null;
        private IHalLoadPortStage lpstage = null;
        [MachineManifest(EnumDevice.loadport_stage_1)]
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
