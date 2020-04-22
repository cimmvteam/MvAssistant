using MvAssistant.Mac.v1_0;
using MvAssistant.Mac.v1_0.Hal.Component.Robot;
using MvAssistant.DeviceDrive.FanucRobot_v42_14;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.Robot
{

    [GuidAttribute("5EDFE42F-A27A-42B5-9702-11FC84208D74")]
    public class HalRobotFanuc : HalComponentBase, IHalRobot
    {
        public HalRobotFanuc()
        {
            Console.WriteLine("Create " + typeof(HalRobotFanuc).Name);
        }


        public MvFanucRobotLdd ldd;

        public static HalRobotEnumMotionType GetMotionType(int corJ, int OfsOrPos)
        {
            if (corJ == 0 && OfsOrPos == 0) return HalRobotEnumMotionType.Offset;
            if (corJ == 0 && OfsOrPos == 1) return HalRobotEnumMotionType.Position;
            if (corJ == 1 && OfsOrPos == 0) return HalRobotEnumMotionType.Joint;

            throw new ArgumentException("無對應的移動方式");
        }
        #region IHalRobot


        public override int HalConnect()
        {
            var ip = this.DevSettings["IP"];

            if (this.ldd == null)
                this.ldd = new MvFanucRobotLdd();
            this.ldd.RobotIp = ip;
            var success = this.ldd.ConnectIfNo();
            return success;
        }

        public override int HalClose()
        {
            var result = 0;
            if (this.ldd != null)
                result += this.ldd.Close();
            return result;
        }


        bool IHal.HalIsConnected()
        {
            return this.ldd.IsConnected();
        }

        public int HalSysRecover()
        {
            this.ldd.SystemRecoverAuto();
            return 0;
        }

        public int HalReset()
        {
            return this.ldd.AlarmReset() ? 0 : -1;
        }

        public int HalStartProgram(string name)
        {
            if (string.IsNullOrEmpty(name)) name = "PNS001";

            return this.ldd.ExecutePNS(name) ? 0 : -1;
        }

        public int HalStopProgram()
        {
            return this.ldd.StopProgram();
        }




        public int HalMoveAsyn()
        {
            throw new NotImplementedException();
        }

        public int HalMoveStraightAsyn(HalRobotMotion motion)
        {
            var corJ = 0;
            var OfsOrPos = 0;
            switch (motion.MotionType)
            {
                case HalRobotEnumMotionType.Offset:
                    corJ = 0; OfsOrPos = 0; break;
                case HalRobotEnumMotionType.Position:
                    corJ = 0; OfsOrPos = 1; break;
                case HalRobotEnumMotionType.Joint:
                    corJ = 1; OfsOrPos = 0; break;
                default:
                    corJ = 0; OfsOrPos = 0; break;
            }


            return this.ldd.Pns0101MoveStraightAsync(motion.ToPoseArray(), corJ, OfsOrPos, motion.IsTcpMove, motion.Speed);
        }




        bool IHalRobot.HalMoveIsComplete()
        {

            var flag = this.ldd.MoveIsComplete();
            return flag;
        }
        int IHalRobot.HalMoveEnd()
        {
            this.ldd.MoveCompeleteReply();
            return 0;
        }


        int IHalRobot.HalAlarm()
        {
            throw new NotImplementedException();
        }



        public HalRobotMotion HalGetPose()
        {
            var robotInfo = this.ldd.GetCurrRobotInfo();

            var motion = new HalRobotMotion()
            {
                MotionType = HalRobotEnumMotionType.None,//取得資料不會有移動類型
                UserFrame = robotInfo.userFrame,
                UserTool = robotInfo.userTool,
                X = robotInfo.x,
                Y = robotInfo.y,
                Z = robotInfo.z,
                W = robotInfo.w,
                P = robotInfo.p,
                R = robotInfo.r,
                J1 = robotInfo.j1,
                J2 = robotInfo.j2,
                J3 = robotInfo.j3,
                J4 = robotInfo.j4,
                J5 = robotInfo.j5,
                J6 = robotInfo.j6,
            };



            return motion;
        }


        public float HalDistanceWithCurr(HalRobotPose pose)
        {
            return this.HalGetPose().Distance(pose);
        }


        #endregion




        #region IDisposable


        #endregion







    }
}

