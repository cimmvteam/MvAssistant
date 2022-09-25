using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Mac.Hal.CompRobot
{
    [GuidAttribute("E197224B-15F4-4582-A915-B2763CF979DC")]
    public enum MacHalRobotEnumMotionType
    {
        None,
        Offset,
        Position,
        Joint,
    }
}
