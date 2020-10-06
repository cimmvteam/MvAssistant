using MvAssistant.Mac.v1_0.Hal.Component.Robot;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.ComponentFake.Robot
{
    [GuidAttribute("AC9FF1CB-B377-4F1C-9924-CC3678A0D0D9")]
    public class HalRobotFake : HalFakeBase, IHalRobot
    {
        public int HalReset()
        {
            return 0;
        }

        public int HalStartProgram(string name = "PNS001")
        {
            return 0;
        }

        public int HalStopProgram()
        {
            return 0;
        }

        public int HalMoveAsyn()
        {
            return 0;
        }

        public int HalMoveStraightAsyn(HalRobotMotion motion)
        {
            return 0;
        }

        public bool HalMoveIsComplete()
        {
            return true;
        }

        public int HalMoveEnd()
        {
            return 0;
        }

        public int HalAlarm()
        {
            return 0;
        }

        public HalRobotMotion HalGetPose()
        {
            return new HalRobotMotion();
        }


        public float HalDistanceWithCurr(HalRobotPose pose)
        {
            return 0;
        }


        public int HalSysRecover()
        {
            throw new NotImplementedException();
        }

        public List<HalRobotMotion> ReadMovePath(string PathFileLocation)
        {
            throw new NotImplementedException();
        }

        public int ExePosMove(List<HalRobotMotion> PathPosition)
        {
            throw new NotImplementedException();
        }
    }
}
