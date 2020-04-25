using MvAssistant.Mac.v1_0.Hal.Component.Stage;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.ComponentFake.Stage
{
    [GuidAttribute("131E8C3A-6AEE-4FC3-BAFA-0D424B2B62E0")]
    public class HalLoadPortStageFake : HalFakeBase, IHalLoadPortStage
    {
        public bool SetMoveSpeed(int speed)
        {
            return true;
        }

        public void Move(double moveinfo, out bool arriveFlag)
        {
            Thread.Sleep(2000);
            arriveFlag = true;
        }

        public void EmergenceActive()
        {
            throw new NotImplementedException();
        }
    }
}
