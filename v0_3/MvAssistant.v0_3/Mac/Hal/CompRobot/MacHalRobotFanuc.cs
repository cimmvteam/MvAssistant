﻿using MvAssistant.v0_3.DeviceDrive.FanucRobot_v42_15;
using MvAssistant.v0_3.Mac.JSon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace MvAssistant.v0_3.Mac.Hal.CompRobot
{

    [GuidAttribute("5EDFE42F-A27A-42B5-9702-11FC84208D74")]
    public class MacHalRobotFanuc : MacHalComponentBase, IMacHalRobot
    {
        public MacHalRobotFanuc()
        {
            Console.WriteLine("Create " + typeof(MacHalRobotFanuc).Name);
        }


        public MvaFanucRobotLdd ldd;

        public static MacHalRobotEnumMotionType GetMotionType(int corJ, int OfsOrPos)
        {
            if (corJ == 0 && OfsOrPos == 0) return MacHalRobotEnumMotionType.Offset;
            if (corJ == 0 && OfsOrPos == 1) return MacHalRobotEnumMotionType.Position;
            if (corJ == 1 && OfsOrPos == 0) return MacHalRobotEnumMotionType.Joint;

            throw new ArgumentException("無對應的移動方式");
        }
        #region IHalRobot


        public override int HalConnect()
        {
            var ip = this.GetDevConnSetting("ip");

            if (this.ldd == null)
                this.ldd = new MvaFanucRobotLdd();
            this.ldd.RobotIp = ip;
            var success = this.ldd.ConnectTry();
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

        /// <summary> 讀路徑Jason檔，依照清單移動 </summary>
        /// <param name="PathFileLocation"></param>
        public List<MacHalRobotMotion> ReadMovePath(string PathFileLocation)
        {
            // var PosInfo = JSonHelper.GetInstanceFromJsonFile<List<PositionInfo>>(PathFileLocation);
            var PosInfo = JSonHelper.GetPositionPathPositionsFromJson(PathFileLocation);
            //   var PosList = PosInfo.Select(m => m.GetPosition()).ToList();
            var PosList = PosInfo.Select(m => m.Position).ToList();
            return PosList;
        }

        /// <summary> 給點位清單，依序移動 </summary>
        /// <param name="PathPosition"></param>
        public int ExePosMove(List<MacHalRobotMotion> PathPosition)
        {
            var targets = new List<MacHalRobotMotion>();
            targets.AddRange(PathPosition);
            for (int idx = 0; idx < targets.Count; idx++)
            {
                var motion = targets[idx];
                this.HalMoveStraightAsyn(motion);
                while (!this.ldd.MoveIsComplete())
                    Thread.Sleep(100);
                this.ldd.MoveCompeleteReply();
            }
            return 0;
        }

        /// <summary> 給點位清單，回朔移動路徑，從最後一個點位返回依序移動至清單起始點位 </summary>
        /// <param name="PathPosition"></param>
        public int BacktrackPosMove(List<MacHalRobotMotion> PathPosition)
        {
            var targets = new List<MacHalRobotMotion>();
            targets.AddRange(PathPosition);
            for (int idx = targets.Count - 1; idx >= 0; idx--)
            {
                var motion = targets[idx];
                this.HalMoveStraightAsyn(motion);
                while (!this.ldd.MoveIsComplete())
                    Thread.Sleep(100);
                this.ldd.MoveCompeleteReply();
            }
            return 0;
        }

        public int HalMoveAsyn()
        {
            throw new NotImplementedException();
        }

        public int HalMoveStraightAsyn(MacHalRobotMotion motion)
        {

            this.HalStopProgram();
            if (!this.ldd.ExecutePNS("PNS0101"))
                throw new Exception("Start PNS0101 Fail");



            var corJ = 0;
            var OfsOrPos = 0;
            float[] PosArray;
            switch (motion.MotionType)
            {
                case MacHalRobotEnumMotionType.Offset:
                    corJ = 0; OfsOrPos = 0; PosArray = motion.ToXyzwprArray(); break;
                case MacHalRobotEnumMotionType.Position:
                    corJ = 0; OfsOrPos = 1; PosArray = motion.ToXyzwprArray(); break;
                case MacHalRobotEnumMotionType.Joint:
                    corJ = 1; OfsOrPos = 0; PosArray = motion.ToJointArray(); break;//Joint模式讀取J1~J6座標
                default:
                    corJ = 0; OfsOrPos = 0; PosArray = motion.ToXyzwprArray(); break;
            }


            return this.ldd.Pns0101MoveStraightAsync(PosArray, corJ, OfsOrPos, motion.IsTcpMove, motion.Speed, motion.UserTool);
        }

        /// <summary>
        /// 執行連線及連續點位一次性寫入，點位數量不能超過30個，edit from HalMoveStraightAsyn(HalRobotMotion)
        /// </summary>
        /// <param name="motion"></param>
        /// <returns></returns>
        public void HalWriteContinuousMotionAsyn(List<MacHalRobotMotion> PathPosition)
        {
            var targets = new List<MacHalRobotMotion>();
            targets.AddRange(PathPosition);

            if (targets.Count > 30)
                throw new MvaException("Position quantity can not over than 30 !!");

            var Group1_Qty = ldd.ReadRegIntValue(21);//點位群組1的點位數量，存於PR[101]~PR[130]，當數量為0代表還沒存入點位
            var Group2_Qty = ldd.ReadRegIntValue(22);//點位群組2的點位數量，存於PR[131]~PR[160]，當數量為0代表還沒存入點位
            var Group3_Qty = ldd.ReadRegIntValue(23);//點位群組3的點位數量，存於PR[161]~PR[190]，當數量為0代表還沒存入點位
            int PositionRegisterStartNum;

            if (Group1_Qty == 0 && Group3_Qty != 0)//將點位寫入群組1，群組範圍PR[101]~PR[130]
            { PositionRegisterStartNum = 101; }
            else if (Group2_Qty == 0 && Group1_Qty != 0)//將點位寫入群組2，群組範圍PR[131]~PR[160]
            { PositionRegisterStartNum = 131; }
            else//將點位寫入群組3，群組範圍PR[161]~PR[190]
            { PositionRegisterStartNum = 161; }

            for (int idx = 0; idx < targets.Count; idx++)
            {
                var motion = targets[idx];
                var corJ = 0;
                float[] PosArray;
                switch (motion.MotionType)
                {
                    case MacHalRobotEnumMotionType.Offset:
                        corJ = 0; PosArray = motion.ToXyzwprArray(); break;
                    case MacHalRobotEnumMotionType.Position:
                        corJ = 0; PosArray = motion.ToXyzwprArray(); break;
                    case MacHalRobotEnumMotionType.Joint:
                        corJ = 1; PosArray = motion.ToJointArray(); break;//Joint模式讀取J1~J6座標
                    default:
                        corJ = 0; PosArray = motion.ToXyzwprArray(); break;
                }
                var Result = this.ldd.Pns0103PositionSaveToPosReg(PosArray, corJ, motion.Speed, PositionRegisterStartNum + idx);
                if (Result == -1)
                    throw new MvaException("Can not connected to Robot!!");
            }
        }


        bool IMacHalRobot.HalMoveIsComplete()
        {

            var flag = this.ldd.MoveIsComplete();
            return flag;
        }
        int IMacHalRobot.HalMoveEnd()
        {
            this.ldd.MoveCompeleteReply();
            return 0;
        }


        int IMacHalRobot.HalAlarm()
        {
            throw new NotImplementedException();
        }



        public MacHalRobotMotion HalGetPose()
        {
            var robotInfo = this.ldd.GetCurrRobotInfo();

            var motion = new MacHalRobotMotion()
            {
                MotionType = MacHalRobotEnumMotionType.None,//取得資料不會有移動類型
                UserFrame = robotInfo.UserFrame,
                UserTool = robotInfo.UserTool,
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


        public float HalDistanceWithCurr(MacHalRobotPose pose)
        {
            return this.HalGetPose().Distance(pose);
        }


        #endregion



        #region IDisposable


        #endregion







    }
}

