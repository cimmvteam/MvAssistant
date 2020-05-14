using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Hal.Component;
using MvAssistant.Mac.v1_0.Hal.Component.Motor;
using MvAssistant.Mac.v1_0.Hal.CompPlc;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{
    [Guid("FAFCEF2B-6356-4438-890F-30F865CAA742")]
    public class MacHalUniversal : MacHalAssemblyBase, IMacHalUniversal
    {


        #region Device Components


        public IMacHalPlcUniversal plc_01 { get { return (IMacHalPlcUniversal)this.GetMachine(MacEnumDevice.universal_plc_01); } }

        #endregion Device Components



        public override int HalClose()
        {
            //return base.HalClose();
            return 0;

        }


    }
}
