
using MvAssistant.Mac.v1_0.JSon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace MvAssistant.Mac.v1_0.Hal.CompRobot
{
    [Guid("D164A8D9-031A-4EBB-8876-679F0874C8E7")]
    public class HalRobotFake : MacHalFakeComponentBase, IHalRobot
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
            return 0;
        }

        public List<HalRobotMotion> ReadMovePath(string PathFileLocation)
        {
            var PosInfo = JSonHelper.GetInstanceFromJsonFile<List<PositionInfo>>(PathFileLocation);
            var PosList = PosInfo.Select(m => m.GetPosition()).ToList();
            return PosList;
        }

        public int ExePosMove(List<HalRobotMotion> PathPosition)
        {
            return 0;
        }
    }
}
