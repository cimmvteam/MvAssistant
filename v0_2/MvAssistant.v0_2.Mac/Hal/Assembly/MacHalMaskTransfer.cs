using MvAssistant.v0_2.Mac.Hal.CompPlc;
using MvAssistant.v0_2.Mac.Hal.CompRobot;
using MvAssistant.v0_2.Mac.Manifest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace MvAssistant.v0_2.Mac.Hal.Assembly
{
    [GuidAttribute("BE7EADB1-6821-4CDC-980C-8673F2B50225")]
    public class MacHalMaskTransfer : MacHalAssemblyBase, IMacHalMaskTransfer
    {
        #region Device Components

        public IMacHalPlcMaskTransfer Plc { get { return (IMacHalPlcMaskTransfer)this.GetHalDevice(MacEnumDevice.masktransfer_plc); } }
        public IHalRobot Robot { get { return (IHalRobot)this.GetHalDevice(MacEnumDevice.masktransfer_robot_1); } }

        #endregion Device Components

        /// <summary> 讀取檔案的路徑點位 </summary>
        /// <param name="PathFileLocation"></param>
        /// <returns></returns>
        public List<HalRobotMotion> ReadFilePosition(string PathFileLocation)
        { return Robot.ReadMovePath(PathFileLocation); }

        /// <summary> 帶入點位資訊，移動Robot </summary>
        /// <param name="MovePosition"></param>
        /// <returns></returns>
        public int ExePathMove(List<HalRobotMotion> MovePosition)
        { return Robot.ExePosMove(MovePosition); }

        /// <summary> 給點位清單，依序移動 </summary>
        /// <param name="PathPosition"></param>
        public int ExePathMove(string PathFileLocation)
        {
            var PathPosition = Robot.ReadMovePath(PathFileLocation);
            return Robot.ExePosMove(PathPosition);
        }

        /// <summary> 給點位清單，回朔移動路徑，從最後一個點位返回依序移動至清單起始點位 </summary>
        /// <param name="PathFileLocation"></param>
        /// <returns></returns>
        public int BacktrackPathMove(string PathFileLocation)
        {
            var PathPosition = Robot.ReadMovePath(PathFileLocation);
            return Robot.BacktrackPosMove(PathPosition);
        }

        /// <summary>
        /// 調整手臂到其他進入Assembly的點位
        /// </summary>
        /// <param name="EndPosFileLocation">Jason檔的儲存目錄</param>
        /// <param name="EndPosition"></param>
        public void ChangeDirection(string EndPosFileLocation)
        {
            bool Licence = false;
            string StartPosName = "";
            string EndPosName = "";
            string EndPosFileDirectory = Path.GetDirectoryName(EndPosFileLocation);
            string EndPosFileName = Path.GetFileName(EndPosFileLocation);
            var LPHome = Robot.ReadMovePath(EndPosFileDirectory + "\\LoadPortHome.json")[0];
            var ICHome = Robot.ReadMovePath(EndPosFileDirectory + "\\InspChHome.json")[0];
            var CCHome = Robot.ReadMovePath(EndPosFileDirectory + "\\CleanChHome.json")[0];
            List<HalRobotMotion> PosList = new List<HalRobotMotion>();

            #region 確認Robot是否在三個可以轉動方向的點位內，並確認目前在哪個方位
            var StasrtPosInfo = (this.Robot as HalRobotFanuc).ldd.GetCurrRobotInfo();
            {
                if (StasrtPosInfo.j1 <= LPHome.J1 + 5 && StasrtPosInfo.j1 >= LPHome.J1 - 5
                    && StasrtPosInfo.j2 <= LPHome.J2 + 5 && StasrtPosInfo.j2 >= LPHome.J2 - 5
                    && StasrtPosInfo.j3 <= LPHome.J3 + 5 && StasrtPosInfo.j3 >= LPHome.J3 - 5
                    && StasrtPosInfo.j4 <= LPHome.J4 + 5 && StasrtPosInfo.j4 >= LPHome.J4 - 5
                    && StasrtPosInfo.j5 <= LPHome.J5 + 5 && StasrtPosInfo.j5 >= LPHome.J5 - 5
                    && StasrtPosInfo.j6 <= LPHome.J6 + 5 && StasrtPosInfo.j6 >= LPHome.J6 - 5)
                {
                    Licence = true; StartPosName = "Load Port";
                }
                else if (StasrtPosInfo.j1 <= ICHome.J1 + 5 && StasrtPosInfo.j1 >= ICHome.J1 - 5
                    && StasrtPosInfo.j2 <= ICHome.J2 + 5 && StasrtPosInfo.j2 >= ICHome.J2 - 5
                    && StasrtPosInfo.j3 <= ICHome.J3 + 5 && StasrtPosInfo.j3 >= ICHome.J3 - 5
                    && StasrtPosInfo.j4 <= ICHome.J4 + 5 && StasrtPosInfo.j4 >= ICHome.J4 - 5
                    && StasrtPosInfo.j5 <= ICHome.J5 + 5 && StasrtPosInfo.j5 >= ICHome.J5 - 5
                    && StasrtPosInfo.j6 <= ICHome.J6 + 5 && StasrtPosInfo.j6 >= ICHome.J6 - 5)
                {
                    Licence = true; StartPosName = "Inspection Chamber";
                }
                else if (StasrtPosInfo.j1 <= CCHome.J1 + 5 && StasrtPosInfo.j1 >= CCHome.J1 - 5
                    && StasrtPosInfo.j2 <= CCHome.J2 + 5 && StasrtPosInfo.j2 >= CCHome.J2 - 5
                    && StasrtPosInfo.j3 <= CCHome.J3 + 5 && StasrtPosInfo.j3 >= CCHome.J3 - 5
                    && StasrtPosInfo.j4 <= CCHome.J4 + 5 && StasrtPosInfo.j4 >= CCHome.J4 - 5
                    && StasrtPosInfo.j5 <= CCHome.J5 + 5 && StasrtPosInfo.j5 >= CCHome.J5 - 5
                    && StasrtPosInfo.j6 <= CCHome.J6 + 5 && StasrtPosInfo.j6 >= CCHome.J6 - 5)
                {
                    Licence = true; StartPosName = "Clean Chamber";
                }
            }
            #endregion

            #region 確認終點
            if (EndPosFileName == "LoadPortHome.json")
            {
                EndPosName = "Load Port";
                PosList.Add(LPHome);
            }
            else if (EndPosFileName == "InspChHome.json")
            {
                EndPosName = "Inspection Chamber";
                PosList.Add(ICHome);
            }
            else if (EndPosFileName == "CleanChHome.json")
            {
                EndPosName = "Clean Chamber";
                PosList.Add(CCHome);
            }
            #endregion

            if (Licence == true)
            {
                if (StartPosName != EndPosName && EndPosName != "")
                {
                    //如果目前位置不在InspCh且要移動的目的地也不是InspCh，則需要先經過InspCh點位再移動到目的地
                    if (StartPosName != "Inspection Chamber" && EndPosName != "Inspection Chamber")
                    {
                        PosList.Insert(0, ICHome);
                        Robot.ExePosMove(PosList);
                    }
                    else
                    {
                        Robot.ExePosMove(PosList);
                    }
                }
                else if (EndPosName == StartPosName)
                    return;//throw new Exception("End position as same as current position !!");
                else
                    throw new Exception("Unknown end position !!");
            }
            else
                throw new Exception("Mask robot can not change direction. Because robot is not in the safe range now");
        }

        /// <summary> 檢查當前位置與目標位置是否一致，點位允許誤差 ±50mm </summary>
        /// <param name="PosFileLocation"></param>
        /// <returns></returns>
        public bool CheckPosition(string PosFileLocation)
        {
            var TargetPos = Robot.ReadMovePath(PosFileLocation)[0];

            var CurrentPosInfo = (this.Robot as HalRobotFanuc).ldd.GetCurrRobotInfo();
            {
                if (CurrentPosInfo.x <= TargetPos.X + 50 && CurrentPosInfo.x >= TargetPos.X - 50
                    && CurrentPosInfo.y <= TargetPos.Y + 50 && CurrentPosInfo.y >= TargetPos.Y - 50
                    && CurrentPosInfo.z <= TargetPos.Z + 50 && CurrentPosInfo.z >= TargetPos.Z - 50
                    && CurrentPosInfo.w <= TargetPos.W + 50 && CurrentPosInfo.w >= TargetPos.W - 50
                    && CurrentPosInfo.p <= TargetPos.P + 50 && CurrentPosInfo.p >= TargetPos.P - 50
                    && CurrentPosInfo.r <= TargetPos.R + 50 && CurrentPosInfo.r >= TargetPos.R - 50)
                {
                    return true;
                }
                else
                    return false;
            }
        }

        public int RobotReset()
        {
            if (Robot.HalReset() == -1)
                throw new Exception("Mask Transfer reset failed.");
            return 0;
        }

        public int RobotRecover()
        {
            return Robot.HalSysRecover();
        }

        public int RobotStopProgram()
        { return Robot.HalStopProgram(); }

        public string Clamp(uint MaskType)
        {
            string result = "";
            result = Plc.Clamp(MaskType);
            return result;
        }

        public string Unclamp()
        {
            string result = "";
            result = Plc.Unclamp();
            return result;
        }

        /// <summary>
        /// CCD旋轉(單位 0.01 Degree)
        /// </summary>
        /// <param name="SpinDegree"></param>
        /// <returns></returns>
        public string CCDSpin(int SpinDegree)
        {
            string result = "";
            result = Plc.CCDSpin(SpinDegree);
            return result;
        }

        public void Initial()
        {
            //TODO: Safety , robot how to initial?
            Plc.Initial();
        }

        public string ReadMTRobotStatus()
        { return Plc.ReadMTRobotStatus(); }

        /// <summary>
        /// 當手臂作動或停止時，需要下指令讓PLC知道目前Robot是移動或靜止狀態
        /// </summary>
        /// <param name="isMoving">手臂是否要移動</param>
        public void RobotMoving(bool isMoving)
        { Plc.RobotMoving(isMoving); }

        #region Set Parameter
        /// <summary>
        /// 設定夾爪觸覺極限
        /// </summary>
        /// <param name="TactileMaxLimit">上限</param>
        /// <param name="TactileMinLimit">下限</param>
        public void SetClampTactileLim(int? TactileMaxLimit, int? TactileMinLimit)
        {
            if (TactileMaxLimit != null && TactileMinLimit != null && TactileMinLimit > TactileMaxLimit)
                throw new Exception("MT clamp tactile sensor maximum limit cannot be lower than the minimum limit !");
            Plc.SetClampTactileLim(TactileMaxLimit, TactileMinLimit);
            Thread.Sleep(100);
            var SetResult = Plc.ReadClampTactileLimit();
            if ((TactileMaxLimit != null && SetResult.Item1 != TactileMaxLimit)
                || (TactileMinLimit != null && SetResult.Item2 != TactileMinLimit))
                throw new Exception("MT clamp tactile sensor limit setting error !");
        }

        /// <summary>
        /// 設定三軸水平極限值
        /// </summary>
        /// <param name="Level_X"></param>
        /// <param name="Level_Y"></param>
        /// <param name="Level_Z"></param>
        public void SetLevelLimit(int? Level_X, int? Level_Y, int? Level_Z)
        {
            Plc.SetLevelLimit(Level_X, Level_Y, Level_Z);
            Thread.Sleep(100);
            var SetResult = Plc.ReadLevelLimit();
            if ((Level_X != null && SetResult.Item1 != Level_X)
                || (Level_Y != null && SetResult.Item2 != Level_Y)
                || (Level_Z != null && SetResult.Item3 != Level_Z))
                throw new Exception("MT clamp level sensor limit setting error !");
        }

        /// <summary>
        /// 設定六軸力覺Sensor的壓力值上限
        /// </summary>
        /// <param name="Fx"></param>
        /// <param name="Fy"></param>
        /// <param name="Fz"></param>
        /// <param name="Mx"></param>
        /// <param name="My"></param>
        /// <param name="Mz"></param>
        public void SetSixAxisSensorUpperLimit(double? Fx, double? Fy, double? Fz, double? Mx, double? My, double? Mz)
        {
            Plc.SetSixAxisSensorUpperLimit(Fx, Fy, Fz, Mx, My, Mz);
            Thread.Sleep(100);
            var SetResult = Plc.ReadSixAxisSensorUpperLimit();
            if ((Fx != null && SetResult.Item1 != Fx)
                || (Fy != null && SetResult.Item2 != Fy)
                || (Fz != null && SetResult.Item3 != Fz)
                || (Mx != null && SetResult.Item4 != Mx)
                || (My != null && SetResult.Item5 != My)
                || (Mz != null && SetResult.Item6 != Mz))
                throw new Exception("MT six axis sensor upper limit setting error !");
        }

        /// <summary>
        /// 設定六軸力覺Sensor的壓力值下限
        /// </summary>
        /// <param name="Fx"></param>
        /// <param name="Fy"></param>
        /// <param name="Fz"></param>
        /// <param name="Mx"></param>
        /// <param name="My"></param>
        /// <param name="Mz"></param>
        public void SetSixAxisSensorLowerLimit(double? Fx, double? Fy, double? Fz, double? Mx, double? My, double? Mz)
        {
            Plc.SetSixAxisSensorLowerLimit(Fx, Fy, Fz, Mx, My, Mz);
            Thread.Sleep(100);
            var SetResult = Plc.ReadSixAxisSensorLowerLimit();
            if ((Fx != null && SetResult.Item1 != Fx)
                || (Fy != null && SetResult.Item2 != Fy)
                || (Fz != null && SetResult.Item3 != Fz)
                || (Mx != null && SetResult.Item4 != Mx)
                || (My != null && SetResult.Item5 != My)
                || (Mz != null && SetResult.Item6 != Mz))
                throw new Exception("MT six axis sensor lower limit setting error !");
        }

        /// <summary>
        /// 夾速度設定，單位(mm/sec)，CCD旋轉速度設定，單位(0.01 deg/sec)
        /// </summary>
        /// <param name="ClampSpeed">(mm/sec)</param>
        /// <param name="CCDSpinSpeed">(0.01 deg/sec)</param>
        public void SetSpeed(double? ClampSpeed, long? CCDSpinSpeed)
        {
            if (ClampSpeed < 1.0 || ClampSpeed > 10.0)
                throw new Exception("MT clamp speed setting only between 1.0 ~ 10.0 (mm/sec) !");
            Plc.SetSpeedVar(ClampSpeed, CCDSpinSpeed);
            Thread.Sleep(100);
            var SetResult = Plc.ReadSpeedVar();
            if (ClampSpeed != null && SetResult.Item1 != ClampSpeed)
                throw new Exception("MT clamp speed setting error !");
            else if (CCDSpinSpeed != null && SetResult.Item2 != CCDSpinSpeed)
                throw new Exception("MT CCD spin speed setting error !");
        }

        /// <summary>
        /// 設定靜電感測的區間限制
        /// </summary>
        /// <param name="Maximum">上限</param>
        /// <param name="Minimum">下限</param>
        public void SetStaticElecLimit(double? Maximum, double? Minimum)
        {
            if (Maximum != null && Minimum != null && Minimum > Maximum)
                throw new Exception("MT static electricity sensor maximum limit cannot be lower than the minimum limit !");
            Plc.SetStaticElecLimit(Maximum, Minimum);
            Thread.Sleep(100);
            var SetResult = Plc.ReadStaticElecLimit();
            if ((Maximum != null && SetResult.Item1 != Maximum)
                || (Minimum != null && SetResult.Item2 != Minimum))
                throw new Exception("MT static electricity sensor limit setting error !");
        }
        #endregion

        #region Read Parameter
        /// <summary>
        /// 讀取夾爪觸覺極限設定值，上限、下限
        /// </summary>
        /// <returns>Maximum、Minimum</returns>
        public Tuple<int, int> ReadClampTactileLimit()
        { return Plc.ReadClampTactileLimit(); }

        /// <summary>
        /// 讀取三軸水平極限值設定，X軸、Y軸、Z軸
        /// </summary>
        /// <returns>X軸、Y軸、Z軸</returns>
        public Tuple<int, int, int> ReadLevelLimit()
        { return Plc.ReadLevelLimit(); }

        /// <summary>
        /// 讀取六軸力覺Sensor的壓力值上限設定，Fx、Fy、Fz、Mx、My、Mz
        /// </summary>
        /// <returns>Fx、Fy、Fz、Mx、My、Mz</returns>
        public Tuple<double, double, double, double, double, double> ReadSixAxisSensorUpperLimit()
        { return Plc.ReadSixAxisSensorUpperLimit(); }

        /// <summary>
        /// 讀取六軸力覺Sensor的壓力值下限設定，Fx、Fy、Fz、Mx、My、Mz
        /// </summary>
        /// <returns></returns>
        public Tuple<double, double, double, double, double, double> ReadSixAxisSensorLowerLimit()
        { return Plc.ReadSixAxisSensorLowerLimit(); }

        /// <summary>
        /// 讀取速度設定，夾爪速度、CCD旋轉速度
        /// </summary>
        /// <returns>夾爪速度、CCD旋轉速度</returns>
        public Tuple<double, long> ReadSpeedVar()
        { return Plc.ReadSpeedVar(); }

        /// <summary>
        /// 讀取靜電感測的區間限制設定值，上限、下限
        /// </summary>
        /// <returns>上限、下限</returns>
        public Tuple<double, double> ReadStaticElecLimit()
        { return Plc.ReadStaticElecLimit(); }
        #endregion

        #region Read Component Value
        /// <summary>
        /// 讀取CCD旋轉角度(單位 0.01 Degree)
        /// </summary>
        /// <returns></returns>
        public long ReadCCDSpinDegree()
        { return Plc.ReadCCDSpinDegree(); }

        /// <summary>
        /// 讀取夾爪鉗四邊伸出長度的位置，夾爪前端、夾爪後端、夾爪左邊、夾爪右邊
        /// </summary>
        /// <returns>夾爪前端、夾爪後端、夾爪左邊、夾爪右邊</returns>
        public Tuple<double, double, double, double> ReadClampGripPos()
        { return Plc.ReadClampGripPos(); }

        /// <summary>
        /// 讀取夾爪前端觸覺數值(前端Sensor會有三個數值)
        /// </summary>
        /// <returns></returns>
        public Tuple<int, int, int> ReadClampTactile_FrontSide()
        { return Plc.ReadClampTactile_FrontSide(); }

        /// <summary>
        /// 讀取夾爪後端觸覺數值(後端Sensor會有三個數值)
        /// </summary>
        /// <returns></returns>
        public Tuple<int, int, int> ReadClampTactile_BehindSide()
        { return Plc.ReadClampTactile_BehindSide(); }

        /// <summary>
        /// 讀取夾爪左側觸覺數值(左側Sensor會有六個數值)
        /// </summary>
        /// <returns></returns>
        public Tuple<int, int, int, int, int, int> ReadClampTactile_LeftSide()
        { return Plc.ReadClampTactile_LeftSide(); }

        /// <summary>
        /// 讀取夾爪右側觸覺數值(右側Sensor會有六個數值)
        /// </summary>
        /// <returns></returns>
        public Tuple<int, int, int, int, int, int> ReadClampTactile_RightSide()
        { return Plc.ReadClampTactile_RightSide(); }

        /// <summary>
        /// 讀取夾爪變形檢測數值(需要先將手臂伸到檢測平台)
        /// </summary>
        /// <returns></returns>
        public Tuple<double, double, double, double, double, double> ReadHandInspection()
        { return Plc.ReadHandInspection(); }

        /// <summary>
        /// 讀取三軸水平數值，X軸、Y軸、Z軸
        /// </summary>
        /// <returns></returns>
        public Tuple<int, int, int> ReadLevel()
        { return Plc.ReadLevel(); }

        /// <summary>
        /// 讀取六軸Sensor數值，Fx、Fy、Fz、Mx、My、Mz
        /// </summary>
        /// <returns>Fx、Fy、Fz、Mx、My、Mz</returns>
        public Tuple<double, double, double, double, double, double> ReadSixAxisSensor()
        { return Plc.ReadSixAxisSensor(); }

        /// <summary>
        /// 讀取靜電測量值
        /// </summary>
        /// <returns></returns>
        public double ReadStaticElec()
        { return Plc.ReadStaticElec(); }
        #endregion

        #region 路徑、點位資訊
        public List<HalRobotMotion> HomeToOpenStage()
        {
            var poss = new List<HalRobotMotion>();

            //PR[54]-Load Port upside
            poss.Add(new HalRobotMotion()
            {
                X = (float)0.784,
                Y = (float)302.846,
                Z = (float)229.596,
                W = (float)45.267,
                P = (float)-88.801,
                R = (float)-135.761,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 200
            });

            //PR[56]-LoadPort前(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)-379.331,
                Y = (float)248.502,
                Z = (float)333.276,
                W = (float)-11.711,
                P = (float)-88.971,
                R = (float)11.523,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 200
            });

            //PR[57]-LoadPort上方(伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)-610.020,
                Y = (float)248.503,
                Z = (float)333.276,
                W = (float)-11.712,
                P = (float)-88.971,
                R = (float)11.523,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 200
            });

            //PR[58]-LoadPort上方(盒子上方)
            poss.Add(new HalRobotMotion()
            {
                X = (float)-610.020,
                Y = (float)248.503,
                Z = (float)70.845,
                W = (float)-11.712,
                P = (float)-88.971,
                R = (float)11.523,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 100
            });

            //PR[59]-LoadPort上方(盒子上夾放Mask位置)
            poss.Add(new HalRobotMotion()
            {
                X = (float)-610.020,
                Y = (float)248.503,
                Z = (float)64.855,
                W = (float)-11.710,
                P = (float)-88.971,
                R = (float)11.521,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            return poss;
        }

        public List<HalRobotMotion> OpenStageToHome()
        {
            var poss = new List<HalRobotMotion>();

            //PR[59]-LoadPort上方(盒子上夾放Mask位置)
            poss.Add(new HalRobotMotion()
            {
                X = (float)-610.020,
                Y = (float)248.503,
                Z = (float)64.855,
                W = (float)-11.710,
                P = (float)-88.971,
                R = (float)11.521,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            //PR[58]-LoadPort上方(盒子上方)
            poss.Add(new HalRobotMotion()
            {
                X = (float)-610.020,
                Y = (float)248.503,
                Z = (float)70.845,
                W = (float)-11.712,
                P = (float)-88.971,
                R = (float)11.523,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            //PR[57]-LoadPort上方(伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)-610.020,
                Y = (float)248.503,
                Z = (float)333.276,
                W = (float)-11.712,
                P = (float)-88.971,
                R = (float)11.523,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 200
            });

            //PR[56]-LoadPort前(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)-379.331,
                Y = (float)248.502,
                Z = (float)333.276,
                W = (float)-11.711,
                P = (float)-88.971,
                R = (float)11.523,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 200
            });

            //PR[54]-Load Port upside
            poss.Add(new HalRobotMotion()
            {
                X = (float)0.784,
                Y = (float)302.846,
                Z = (float)229.596,
                W = (float)45.267,
                P = (float)-88.801,
                R = (float)-135.761,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 200
            });

            return poss;
        }

        public List<HalRobotMotion> FrontSideIntoInspCh()
        {
            var poss = new List<HalRobotMotion>();

            //PR[60]-要進InspCh的位置
            poss.Add(new HalRobotMotion()
            {
                X = (float)302.84552,
                Y = (float)-0.784070134,
                Z = (float)229.5958,
                W = (float)45.2667427,
                P = (float)-88.80105,
                R = (float)134.238617,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            //PR[61]-InspCh前(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)347.455,
                Y = (float)83.942,
                Z = (float)229.596,
                W = (float)54.857,
                P = (float)-88.535,
                R = (float)123.174,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 200
            });

            //PR[62]-InspCh內(伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)705.727,
                Y = (float)83.942,
                Z = (float)229.596,
                W = (float)54.874,
                P = (float)-88.536,
                R = (float)123.157,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 200
            });

            //PR[63]-Stage上方(盒子上方)
            poss.Add(new HalRobotMotion()
            {
                X = (float)705.727,
                Y = (float)83.942,
                Z = (float)147.870,
                W = (float)54.875,
                P = (float)-88.536,
                R = (float)123.156,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 100
            });

            //PR[64]-Stage上方(雲台上夾放Mask位置)
            poss.Add(new HalRobotMotion()
            {
                X = (float)705.727,
                Y = (float)83.942,
                Z = (float)136.927,
                W = (float)54.875,
                P = (float)-88.536,
                R = (float)123.156,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            return poss;
        }

        public List<HalRobotMotion> FrontSideLeaveInspCh()
        {
            var poss = new List<HalRobotMotion>();

            //PR[64]-Stage上方(雲台上夾放Mask位置)
            poss.Add(new HalRobotMotion()
            {
                X = (float)705.727,
                Y = (float)83.942,
                Z = (float)136.927,
                W = (float)54.875,
                P = (float)-88.536,
                R = (float)123.156,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            //PR[63]-Stage上方(盒子上方)
            poss.Add(new HalRobotMotion()
            {
                X = (float)705.727,
                Y = (float)83.942,
                Z = (float)147.870,
                W = (float)54.875,
                P = (float)-88.536,
                R = (float)123.156,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            //PR[62]-InspCh內(伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)705.727,
                Y = (float)83.942,
                Z = (float)229.596,
                W = (float)54.874,
                P = (float)-88.536,
                R = (float)123.157,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 100
            });

            //PR[61]-InspCh前(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)347.455,
                Y = (float)83.942,
                Z = (float)229.596,
                W = (float)54.857,
                P = (float)-88.535,
                R = (float)123.174,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 200
            });

            //PR[60]-要進InspCh的位置
            poss.Add(new HalRobotMotion()
            {
                X = (float)302.84552,
                Y = (float)-0.784070134,
                Z = (float)229.5958,
                W = (float)45.2667427,
                P = (float)-88.80105,
                R = (float)134.238617,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            return poss;
        }

        public List<HalRobotMotion> BackSideIntoInspCh()
        {
            var poss = new List<HalRobotMotion>();

            //PR[60]-要進InspCh的位置
            poss.Add(new HalRobotMotion()
            {
                X = (float)302.84552,
                Y = (float)-0.784070134,
                Z = (float)229.5958,
                W = (float)45.2667427,
                P = (float)-88.80105,
                R = (float)134.238617,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            //PR[70]-InspCh前(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)347.440735,
                Y = (float)83.9533844,
                Z = (float)229.5585,
                W = (float)55.07626,
                P = (float)-88.53644,
                R = (float)122.957176,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 200
            });

            //PR[71]-InspCh前，夾爪旋轉90度(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)347.4437,
                Y = (float)83.93376,
                Z = (float)229.553055,
                W = (float)87.9169,
                P = (float)-7.902644,
                R = (float)90.8078,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            //PR[72]-InspCh前，夾爪旋轉180度(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)347.4421,
                Y = (float)60.6153,
                Z = (float)229.5587,
                W = (float)-37.94723,
                P = (float)88.0081,
                R = (float)-35.9757957,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            //PR[73]-InspCh內(伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)713.397461,
                Y = (float)60.6267357,
                Z = (float)229.521408,
                W = (float)-37.95548,
                P = (float)88.01182,
                R = (float)-35.983757,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 200
            });

            //PR[74]-Stage上方(盒子上方)
            poss.Add(new HalRobotMotion()
            {
                X = (float)713.3917,
                Y = (float)60.627636,
                Z = (float)104.926239,
                W = (float)-37.9381828,
                P = (float)88.0114441,
                R = (float)-35.9664726,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 100
            });

            //PR[75]-Stage上方(雲台上夾放Mask位置)
            poss.Add(new HalRobotMotion()
            {
                X = (float)713.3919,
                Y = (float)60.6279678,
                Z = (float)95.1371841,
                W = (float)-37.966507,
                P = (float)88.01119,
                R = (float)-35.9946861,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            return poss;
        }

        public List<HalRobotMotion> BackSideLeaveInspCh()
        {
            var poss = new List<HalRobotMotion>();

            //PR[75]-Stage上方(雲台上夾放Mask位置)
            poss.Add(new HalRobotMotion()
            {
                X = (float)713.3919,
                Y = (float)60.6279678,
                Z = (float)95.1371841,
                W = (float)-37.966507,
                P = (float)88.01119,
                R = (float)-35.9946861,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            //PR[74]-Stage上方(盒子上方)
            poss.Add(new HalRobotMotion()
            {
                X = (float)713.3917,
                Y = (float)60.627636,
                Z = (float)104.926239,
                W = (float)-37.9381828,
                P = (float)88.0114441,
                R = (float)-35.9664726,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            //PR[73]-InspCh內(伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)713.397461,
                Y = (float)60.6267357,
                Z = (float)229.521408,
                W = (float)-37.95548,
                P = (float)88.01182,
                R = (float)-35.983757,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 200
            });

            //PR[72]-InspCh前(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)347.4421,
                Y = (float)60.6153,
                Z = (float)229.5587,
                W = (float)-37.94723,
                P = (float)88.0081,
                R = (float)-35.9757957,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 200
            });

            //PR[71]-InspCh前，夾爪旋轉90度(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)347.4437,
                Y = (float)83.93376,
                Z = (float)229.553055,
                W = (float)87.9169,
                P = (float)-7.902644,
                R = (float)90.8078,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            //PR[70]-InspCh前，夾爪旋轉180度(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)347.440735,
                Y = (float)83.9533844,
                Z = (float)229.5585,
                W = (float)55.07626,
                P = (float)-88.53644,
                R = (float)122.957176,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            //PR[60]-要進InspCh的位置
            poss.Add(new HalRobotMotion()
            {
                X = (float)302.84552,
                Y = (float)-0.784070134,
                Z = (float)229.5958,
                W = (float)45.2667427,
                P = (float)-88.80105,
                R = (float)134.238617,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            return poss;
        }

        /// <summary>
        /// 此路徑正取Mask，對Mask背面進行清理
        /// </summary>
        /// <returns></returns>
        public List<HalRobotMotion> BackSideClean()
        {
            var poss = new List<HalRobotMotion>();

            //PR[21]-要進CleanCh的位置(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)-0.7841019,
                Y = (float)-302.845428,
                Z = (float)229.595718,
                W = (float)45.2669678,
                P = (float)-88.8010559,
                R = (float)44.2383842,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20

            });

            //PR[30]-CleanCh內(伸出手臂於Air Gun上方)
            poss.Add(new HalRobotMotion()
            {
                X = (float)0.976,
                Y = (float)-634.621,
                Z = (float)229.596,
                W = (float)45.253,
                P = (float)-88.801,
                R = (float)44.586,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 100
            });

            //PR[25]-CleanCh內(伸出手臂於Air Gun上方，以Mask左下方為清理起點)
            for (float y = 0; y <= (float)150.0; y += (float)50.0)
                for (float x = 0; x <= (float)150.0; x += (float)150.0)
                    poss.Add(new HalRobotMotion()
                    {
                        X = (float)121.264 + x,
                        Y = (float)-701.341 + y,
                        Z = (float)116.219,
                        W = (float)45.243,
                        P = (float)-88.800,
                        R = (float)44.595,
                        MotionType = HalRobotEnumMotionType.Position,
                        Speed = 20
                    });

            //PR[30]-CleanCh內(伸出手臂於Air Gun上方)
            poss.Add(new HalRobotMotion()
            {
                X = (float)0.976,
                Y = (float)-634.621,
                Z = (float)229.596,
                W = (float)45.253,
                P = (float)-88.801,
                R = (float)44.586,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 100
            });

            //PR[21]-要進CleanCh的位置(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)-0.7841019,
                Y = (float)-302.845428,
                Z = (float)229.595718,
                W = (float)45.2669678,
                P = (float)-88.8010559,
                R = (float)44.2383842,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            return poss;
        }

        /// <summary>
        /// 此路徑正取Mask，對Mask背面進行拍照
        /// </summary>
        /// <returns></returns>
        public List<HalRobotMotion> BackSideCCDTakeImage()
        {
            var poss = new List<HalRobotMotion>();

            //PR[21]-要進CleanCh的位置(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)-0.7841019,
                Y = (float)-302.845428,
                Z = (float)229.595718,
                W = (float)45.2669678,
                P = (float)-88.8010559,
                R = (float)44.2383842,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20

            });

            //PR[30]-CleanCh內(伸出手臂於Air Gun上方)
            poss.Add(new HalRobotMotion()
            {
                X = (float)0.976,
                Y = (float)-634.621,
                Z = (float)229.596,
                W = (float)45.253,
                P = (float)-88.801,
                R = (float)44.586,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 100
            });

            //PR[27]-CleanCh內(伸出手臂於CCD上方，以Mask左下方為拍照起點)，新PR[29]點位
            for (float y = 0; y <= (float)150.0; y += (float)50.0)
                for (float x = 0; x <= (float)150.0; x += (float)150.0)
                    poss.Add(new HalRobotMotion()
                    {
                        X = (float)-114.608 + x,
                        Y = (float)-730.624 + y,
                        Z = (float)205.211,
                        W = (float)45.266,
                        P = (float)-88.801,
                        R = (float)44.572,
                        MotionType = HalRobotEnumMotionType.Position,
                        Speed = 20
                    });

            //PR[30]-CleanCh內(伸出手臂於Air Gun上方)
            poss.Add(new HalRobotMotion()
            {
                X = (float)0.976,
                Y = (float)-634.621,
                Z = (float)229.596,
                W = (float)45.253,
                P = (float)-88.801,
                R = (float)44.586,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 100
            });

            //PR[21]-要進CleanCh的位置(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)-0.7841019,
                Y = (float)-302.845428,
                Z = (float)229.595718,
                W = (float)45.2669678,
                P = (float)-88.8010559,
                R = (float)44.2383842,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            return poss;
        }

        /// <summary>
        /// 此路徑反取Mask，對Mask正面進行清理
        /// </summary>
        /// <returns></returns>
        public List<HalRobotMotion> FrontSideClean()
        {
            var poss = new List<HalRobotMotion>();

            //PR[21]-要進CleanCh的位置(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)-0.7841019,
                Y = (float)-302.845428,
                Z = (float)229.595718,
                W = (float)45.2669678,
                P = (float)-88.8010559,
                R = (float)44.2383842,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20

            });

            //PR[31]-要進CleanCh的位置，旋轉90度(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)-0.784,
                Y = (float)-302.845,
                Z = (float)229.596,
                W = (float)89.486,
                P = (float)-0.897,
                R = (float)0.836,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            //PR[32]-要進CleanCh的位置，旋轉180度(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)-0.783,
                Y = (float)-302.845,
                Z = (float)229.595,
                W = (float)-81.019,
                P = (float)87.027,
                R = (float)-170.573,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 100
            });

            //PR[41]-CleanCh內(伸出手臂於Air Gun上方)
            poss.Add(new HalRobotMotion()
            {
                X = (float)78.704,
                Y = (float)-676.093,
                Z = (float)229.595,
                W = (float)-81.019,
                P = (float)87.027,
                R = (float)-170.573,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            //PR[35]-CleanCh內(伸出手臂於Air Gun上方，以Mask左下方為清理起點)，新PR[36]點位
            for (float y = 0; y <= (float)150.0; y += (float)50.0)
                for (float x = 0; x <= (float)150.0; x += (float)150.0)
                    poss.Add(new HalRobotMotion()
                    {
                        X = (float)109.512 + x,
                        Y = (float)-707.848 + y,
                        Z = (float)138.564,
                        W = (float)-74.089,
                        P = (float)89.110,
                        R = (float)-163.935,
                        MotionType = HalRobotEnumMotionType.Position,
                        Speed = 20
                    });

            //PR[41]-CleanCh內(伸出手臂於Air Gun上方)
            poss.Add(new HalRobotMotion()
            {
                X = (float)78.704,
                Y = (float)-676.093,
                Z = (float)229.595,
                W = (float)-81.019,
                P = (float)87.027,
                R = (float)-170.573,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            //PR[32]-要進CleanCh的位置(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)-0.783,
                Y = (float)-302.845,
                Z = (float)229.595,
                W = (float)-81.019,
                P = (float)87.027,
                R = (float)-170.573,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 100
            });

            //PR[31]-要進CleanCh的位置，旋轉90度(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)-0.784,
                Y = (float)-302.845,
                Z = (float)229.596,
                W = (float)89.486,
                P = (float)-0.897,
                R = (float)0.836,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 100
            });

            //PR[21]-要進CleanCh的位置，旋轉180度(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)-0.7841019,
                Y = (float)-302.845428,
                Z = (float)229.595718,
                W = (float)45.2669678,
                P = (float)-88.8010559,
                R = (float)44.2383842,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            return poss;
        }

        /// <summary>
        /// 此路徑反取Mask，對Mask正面進行拍照
        /// </summary>
        /// <returns></returns>
        public List<HalRobotMotion> FrontSideCCDTakeImage()
        {
            var poss = new List<HalRobotMotion>();

            //PR[21]-要進CleanCh的位置(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)-0.7841019,
                Y = (float)-302.845428,
                Z = (float)229.595718,
                W = (float)45.2669678,
                P = (float)-88.8010559,
                R = (float)44.2383842,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20

            });

            //PR[31]-要進CleanCh的位置，旋轉90度(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)-0.784,
                Y = (float)-302.845,
                Z = (float)229.596,
                W = (float)89.486,
                P = (float)-0.897,
                R = (float)0.836,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            //PR[32]-要進CleanCh的位置，旋轉180度(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)-0.783,
                Y = (float)-302.845,
                Z = (float)229.595,
                W = (float)-81.019,
                P = (float)87.027,
                R = (float)-170.573,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 100
            });

            //PR[41]-CleanCh內(伸出手臂於Air Gun上方)
            poss.Add(new HalRobotMotion()
            {
                X = (float)78.704,
                Y = (float)-676.093,
                Z = (float)229.595,
                W = (float)-81.019,
                P = (float)87.027,
                R = (float)-170.573,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });



            //PR[39]-CleanCh內(伸出手臂於CCD上方，以Mask左下方為拍照起點)，新PR[40]點位
            for (float y = 0; y <= (float)150.0; y += (float)50.0)
                for (float x = 0; x <= (float)150.0; x += (float)150.0)
                    poss.Add(new HalRobotMotion()
                    {
                        X = (float)-138.934 + x,
                        Y = (float)-725.532 + y,
                        Z = (float)227.905,
                        W = (float)-70.183,
                        P = (float)89.091,
                        R = (float)-160.030,
                        MotionType = HalRobotEnumMotionType.Position,
                        Speed = 20
                    });

            //PR[41]-CleanCh內(伸出手臂於Air Gun上方)
            poss.Add(new HalRobotMotion()
            {
                X = (float)78.704,
                Y = (float)-676.093,
                Z = (float)229.595,
                W = (float)-81.019,
                P = (float)87.027,
                R = (float)-170.573,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            //PR[32]-要進CleanCh的位置(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)-0.783,
                Y = (float)-302.845,
                Z = (float)229.595,
                W = (float)-81.019,
                P = (float)87.027,
                R = (float)-170.573,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 100
            });

            //PR[31]-要進CleanCh的位置，旋轉90度(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)-0.784,
                Y = (float)-302.845,
                Z = (float)229.596,
                W = (float)89.486,
                P = (float)-0.897,
                R = (float)0.836,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 100
            });

            //PR[21]-要進CleanCh的位置，旋轉180度(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)-0.7841019,
                Y = (float)-302.845428,
                Z = (float)229.595718,
                W = (float)45.2669678,
                P = (float)-88.8010559,
                R = (float)44.2383842,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            return poss;
        }

        public HalRobotMotion PosHome()
        {
            var posotion = new HalRobotMotion();
            //PR[54]-Load Port upside
            posotion.J1 = (float)89.99975;
            posotion.J2 = (float)-32.3472061;
            posotion.J3 = (float)-24.6673031;
            posotion.J4 = (float)-1.13160455;
            posotion.J5 = (float)25.5155029;
            posotion.J6 = (float)1.88025844;
            posotion.MotionType = HalRobotEnumMotionType.Joint;
            posotion.Speed = 20;

            return posotion;
        }

        public HalRobotMotion PosToInspCh()
        {
            var posotion = new HalRobotMotion();
            //PR[60]-要進InspCh的位置
            posotion.J1 = (float)0.000;
            posotion.J2 = (float)-32.347;
            posotion.J3 = (float)-24.667;
            posotion.J4 = (float)-1.134;
            posotion.J5 = (float)25.515;
            posotion.J6 = (float)1.882;
            posotion.MotionType = HalRobotEnumMotionType.Joint;
            posotion.Speed = 20;

            return posotion;
        }

        public HalRobotMotion PosToCleanCh()
        {
            var posotion = new HalRobotMotion();
            //PR[21]-要進CleanCh的位置(未伸出手臂)
            posotion.J1 = (float)-90.000;
            posotion.J2 = (float)-32.347;
            posotion.J3 = (float)-24.667;
            posotion.J4 = (float)-1.134;
            posotion.J5 = (float)25.515;
            posotion.J6 = (float)1.882;
            posotion.MotionType = HalRobotEnumMotionType.Joint;
            posotion.Speed = 20;

            return posotion;
        }
        #endregion

        //public List<HalRobotMotion> TurnJoint()
        //{
        //    var poss = new List<HalRobotMotion>();

        //    poss.Add(new HalRobotMotion()
        //    {
        //        J1 = (float)-1.287,
        //        J2 = (float)302.844,
        //        J3 = (float)189.852,
        //        J4 = (float)45.266,
        //        J5 = (float)-88.801,
        //        J6 = (float)-135.369,
        //        MotionType = HalRobotEnumMotionType.Joint,
        //        Speed = 20
        //    });

        //    poss.Add(new HalRobotMotion()
        //    {
        //        J1 = (float)-422.038,
        //        J2 = (float)305.272,
        //        J3 = (float)181.435,
        //        J4 = (float)7.339,
        //        J5 = (float)-88.870,
        //        J6 = (float)-8.811,
        //        MotionType = HalRobotEnumMotionType.Joint,
        //        Speed = 100
        //    });

        //    poss.Add(new HalRobotMotion()
        //    {
        //        J1 = (float)-637.878,
        //        J2 = (float)305.272,
        //        J3 = (float)181.435,
        //        J4 = (float)7.339,
        //        J5 = (float)-88.870,
        //        J6 = (float)-8.810,
        //        MotionType = HalRobotEnumMotionType.Joint,
        //        Speed = 200
        //    });

        //    return poss;
        //}

    }
}
