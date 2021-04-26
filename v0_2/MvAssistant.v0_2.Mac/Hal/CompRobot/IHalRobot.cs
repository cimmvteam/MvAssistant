using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Mac.Hal.CompRobot
{
    [GuidAttribute("A3065B41-C1F5-41B0-937E-A13205DB0D46")]
    public interface IHalRobot : IMacHalComponent
    {

        int HalReset();
        int HalStartProgram(string name = null);
        int HalStopProgram();

        int HalSysRecover();


        /// <summary>
        /// 不會遇到奇異點, 但容易有大幅度旋轉與不可預期的空間干涉
        /// </summary>
        /// <returns></returns>
        int HalMoveAsyn();
     
        /// <summary>
        /// 直線運動
        /// </summary>
        /// <returns></returns>
        int HalMoveStraightAsyn(HalRobotMotion motion);

        bool HalMoveIsComplete();
        int HalMoveEnd();


        int HalAlarm();

        HalRobotMotion HalGetPose();

        float HalDistanceWithCurr(HalRobotPose pose);

        List<HalRobotMotion> ReadMovePath(string PathFileLocation);

        int ExePosMove(List<HalRobotMotion> PathPosition);

        int BacktrackPosMove(List<HalRobotMotion> PathPosition);
    }
}
