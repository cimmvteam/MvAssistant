using MaskAutoCleaner.Hal.Intf.Component;
using MaskAutoCleaner.Hal.Intf.Component.Identifier;
using MaskAutoCleaner.Hal.Intf.Component.Motor;
using MaskAutoCleaner.Hal.Intf.Component.Stage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.Intf.Assembly
{    
    [GuidAttribute("8E7C81C2-3074-43AA-867E-E3F3700668E0")]
    public interface IHalLoadPort : IHalAssembly
    {
        IHalClamper Clamper { get; set; }
        IHalE84 E84 { get; set; }
        IHalLoadPortStage Lpstage { get; set; }
        IHalPlunger Plunger { get; set; }
        IHalRfidReader RfidReader { get; set; }
        IHalCamera Sideccd { get; set; }
        IHalCamera TopCcd { get; set; }
    }
}
