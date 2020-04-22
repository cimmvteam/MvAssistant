using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.Intf.Component
{
    [GuidAttribute("75630424-B2DD-44ED-8724-C9AC3CABA306")]
    public interface IHalPlunger : IHalComponent
    {
        bool HalIsPressed();
    }
}
