using MaskAutoCleaner.Hal.Intf.Assembly;
using MaskAutoCleaner.Hal.Intf.Component;
using MaskAutoCleaner.Hal.Intf.Component.Motor;
using MaskAutoCleaner.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.Imp.Assembly
{
    [MachineManifest(DeviceEnum.universal_assembly)]
    [Guid("FAFCEF2B-6356-4438-890F-30F865CAA742")]
    public class HalUniversal : HalAssemblyBase, IHalUniversal
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
