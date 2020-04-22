
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Hal.Component;
using MvAssistant.Manifest;
using System;
using System.Runtime.InteropServices;

namespace MvAssistant.Mac.v1_0.Hal.AssemblyFake
{
    [MachineManifest(DeviceEnum.universal_assembly)]
    [Guid("87EC20B6-2C41-45A1-BB07-CB2D6A7630EF")]
    public class HalUniversalFake : HalFakeBase, IHalUniversal
    {

        #region Device Components


        [MachineManifest(DeviceEnum.universal_plc_01)]
        public IHalPlc plc_01 { get; set; }
        [MachineManifest(DeviceEnum.universal_plc_02)]
        public IHalPlc plc_02 { get; set; }

        #endregion Device Components

        #region Hal Interface
        public int HalStop()
        {
            return 0;
        }


        public int HalClose()
        {
            return 0;
        }

        public int HalConnect()
        {
            return 0;
        }

        public bool HalIsConnected()
        {
            return true;
        }
        #endregion


    }
}
