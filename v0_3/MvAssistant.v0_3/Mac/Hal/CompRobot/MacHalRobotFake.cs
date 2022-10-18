
using MvAssistant.v0_3.Mac.JSon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace MvAssistant.v0_3.Mac.Hal.CompRobot
{
    [Guid("D164A8D9-031A-4EBB-8876-679F0874C8E7")]
    public class MacHalRobotFake : MacHalFakeComponentBase, IMacHalRobot
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

        public int HalMoveStraightAsyn(MacHalRobotMotion motion)
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

        public MacHalRobotMotion HalGetPose()
        {
            var motion = new MacHalRobotMotion();

            return motion;
        }


        public float HalDistanceWithCurr(MacHalRobotPose pose)
        {
            return 0;
        }


        public int HalSysRecover()
        {
            return 0;
        }

        public List<MacHalRobotMotion> ReadMovePath(string PathFileLocation)
        {
            //var PosInfo = JSonHelper.GetInstanceFromJsonFile<List<PositionInfo>>(PathFileLocation);
            var PosInfo = JSonHelper.GetPositionPathPositionsFromJson(PathFileLocation);
            var PosList = PosInfo.Select(m => m.GetPosition()).ToList();
            return PosList;
        }

        public int ExePosMove(List<MacHalRobotMotion> PathPosition)
        {
            return 0;
        }

        public int BacktrackPosMove(List<MacHalRobotMotion> PathPosition)
        {
            return 0;
        }
    }
}
