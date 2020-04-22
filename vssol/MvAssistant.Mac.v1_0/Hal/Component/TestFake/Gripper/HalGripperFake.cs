using MaskAutoCleaner.Hal.Intf.Component;
using MaskAutoCleaner.Hal.Intf.Component.Gripper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.ImpFake.Component.Gripper
{
    [GuidAttribute("6A40F98F-D78B-44B8-B71C-20303BABF9D2")]
    public class HalGripperFake : HalFakeBase, IHalGripper
    {
        public bool HalSetSpeed(int speed)
        {
            return true;
        }

        public void Move(object moveinfo, out bool arriveFlag)
        {
            arriveFlag = true;
        }

        public void HalStop()
        {
        }


        public void HalMove(HalGripperCmd cmd)
        {
        }

        public float HalGetPosition()
        {
            return 0.0f;
        }

        public bool HalIsCompleted()
        {
            return true;
        }


        public bool HalZeroReset()
        {
            return true;
        }
    }
}
