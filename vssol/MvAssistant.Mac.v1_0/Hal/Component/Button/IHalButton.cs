using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.Button
{
    [GuidAttribute("04FA7158-6504-471D-B059-64EFF0A53D90")]
    public interface IHalButton : IMacHalComponent
    {
        bool IsPressedOpen();
        bool IsPressedClose();
        bool IsProcessComplete();
    }
}
