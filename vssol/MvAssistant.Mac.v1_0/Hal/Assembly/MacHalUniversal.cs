using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Hal.Component;
using MvAssistant.Mac.v1_0.Hal.Component.Motor;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{
    [MacMachineManifest(MacEnumDevice.universal_assembly)]
    [Guid("FAFCEF2B-6356-4438-890F-30F865CAA742")]
    public class MacHalUniversal : MacHalAssemblyBase, IMacHalUniversal
    {


        #region Device Components


        [MacMachineManifest(MacEnumDevice.universal_plc_01)]
        public IHalPlc plc_01 { get; set; }
        [MacMachineManifest(MacEnumDevice.universal_plc_02)]
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
