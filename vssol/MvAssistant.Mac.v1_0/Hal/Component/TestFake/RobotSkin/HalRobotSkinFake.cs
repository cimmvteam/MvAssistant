using MaskAutoCleaner.Hal.Intf.Component;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.ImpFake.Component.RobotSkin
{
    [GuidAttribute("7FCE1A11-BBBD-4C1E-B55F-8B7A1DF82141")]
    public class HalRobotFake : HalFakeBase, IHalRobotSkin
    {
        public float GetValue()
        {
            throw new NotImplementedException();
        }
    }
}
