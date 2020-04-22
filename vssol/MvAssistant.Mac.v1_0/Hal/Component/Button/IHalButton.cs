using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.Intf.Component.Button
{
    [GuidAttribute("04FA7158-6504-471D-B059-64EFF0A53D90")]
    public interface IHalButton : IHalComponent
    {
        bool IsPressedOpen();
        bool IsPressedClose();
        bool IsProcessComplete();
    }
}
