using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.Intf.Component.Robot
{
    [GuidAttribute("E197224B-15F4-4582-A915-B2763CF979DC")]
    public enum HalRobotEnumMotionType
    {
        None,
        Offset,
        Position,
        Joint,
    }
}
