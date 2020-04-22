using MaskAutoCleaner.Hal.Intf.Component;
using MaskAutoCleaner.Hal.Intf.Component.Motor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.Intf.Assembly
{
    [Guid("85C61EE9-7989-48D9-B5DC-9A93E7146863")]
    public interface IHalUniversal : IHalAssembly
    {
        IHalPlc plc_01 { get; set; }
        IHalPlc plc_02 { get; set; }



    }
}
