using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.Gripper 
{
    [GuidAttribute("760F8CC6-ADCC-4EAF-B1ED-F6ED4FAD0525")]
    public interface IHalGripper : IMacHalComponent
    {
        void HalMove(HalGripperCmd cmd);
        float HalGetPosition();
        bool HalIsCompleted();
        void HalStop();
        bool HalZeroReset();
        







    }
}
