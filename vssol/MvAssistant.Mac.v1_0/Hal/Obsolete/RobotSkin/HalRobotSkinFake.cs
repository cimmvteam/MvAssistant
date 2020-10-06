using MvAssistant.Mac.v1_0.Hal.Component;
using System;
using System.Runtime.InteropServices;

namespace MvAssistant.Mac.v1_0.Hal.ComponentFake.RobotSkin
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
