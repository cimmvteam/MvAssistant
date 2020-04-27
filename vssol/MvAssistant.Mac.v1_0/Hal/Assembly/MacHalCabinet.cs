using MvAssistant.Mac.v1_0.Hal.CompPlc;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Runtime.InteropServices;

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{
    [GuidAttribute("DBCB4F3E-0405-450E-80D5-F2D1401975F1")]
    public class MacHalCabinet : MacHalAssemblyBase, IMacHalCabinet
    {


        #region Device Components

        public IMacHalPlcCabinet Plc { get { return (IMacHalPlcCabinet)this.GetMachine(MacEnumDevice.cabinet_plc); } }


        #endregion Device Components




    }
}
