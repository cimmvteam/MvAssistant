using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.Gripper
{
    [GuidAttribute("82E204E9-C696-48DF-9A2E-9DADE1F811C0")]
    public class HalGripperCmd
    {
        public HalEnumGripperDirection Direction = HalEnumGripperDirection.None;
        public bool? Enable;
        public float? Offset;
        public float? Position;
        public int? SpeedLevel;
    }
}
