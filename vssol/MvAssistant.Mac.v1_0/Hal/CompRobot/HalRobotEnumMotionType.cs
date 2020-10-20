using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.CompRobot
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
