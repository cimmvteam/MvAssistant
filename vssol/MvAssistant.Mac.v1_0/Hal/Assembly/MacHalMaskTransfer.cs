using MvAssistant.DeviceDrive.FanucRobot_v42_14;
using MvAssistant.Mac.v1_0.Hal.Component;
using MvAssistant.Mac.v1_0.Hal.Component.Camera;
using MvAssistant.Mac.v1_0.Hal.Component.Force6Axis;
using MvAssistant.Mac.v1_0.Hal.Component.Gripper;
using MvAssistant.Mac.v1_0.Hal.Component.Inclinometer;
using MvAssistant.Mac.v1_0.Hal.Component.Infrared;
using MvAssistant.Mac.v1_0.Hal.Component.Robot;
using MvAssistant.Mac.v1_0.Hal.Component.Stage;
using MvAssistant.Mac.v1_0.Hal.CompPlc;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{
    [GuidAttribute("BE7EADB1-6821-4CDC-980C-8673F2B50225")]
    public class MacHalMaskTransfer : MacHalAssemblyBase, IMacHalMaskTransfer
    {
        #region Device Components

        public IMacHalPlcMaskTransfer Plc { get { return (IMacHalPlcMaskTransfer)this.GetMachine(MacEnumDevice.masktransfer_plc); } }
        public IHalRobot Robot { get { return (IHalRobot)this.GetMachine(MacEnumDevice.masktransfer_robot_1); } }
        public IHalForce6Axis Force6Axis { get { return (IHalForce6Axis)this.GetMachine(MacEnumDevice.masktransfer_force_6axis_sensor_1); } }
        public IHalInclinometer Gradienter { get { return (IHalInclinometer)this.GetMachine(MacEnumDevice.masktransfer_inclinometer01); } }
        public IHalCamera CameraPellicleDeform { get { return (IHalCamera)this.GetMachine(MacEnumDevice.masktransfer_ccd_pellicle_deform_1); } }
        public IHalCamera CameraBarcodeReader { get { return (IHalCamera)this.GetMachine(MacEnumDevice.masktransfer_ccd_barcode_reader_1); } }
        public IHalLight CameraBarcodeLight { get { return (IHalLight)this.GetMachine(MacEnumDevice.masktransfer_light_barcode_1); } }
        public IHalPellicleDeformStage StagePellicleDeform { get { return (IHalPellicleDeformStage)this.GetMachine(MacEnumDevice.masktransfer_stage_pellicle_deform_1); } }
        public IHalTactile Tactile1 { get { return (IHalTactile)this.GetMachine(MacEnumDevice.masktransfer_tactile_1); } }
        public IHalTactile Tactile2 { get { return (IHalTactile)this.GetMachine(MacEnumDevice.masktransfer_tactile_2); } }
        public IHalTactile Tactile3 { get { return (IHalTactile)this.GetMachine(MacEnumDevice.masktransfer_tactile_3); } }
        public IHalTactile Tactile4 { get { return (IHalTactile)this.GetMachine(MacEnumDevice.masktransfer_tactile_4); } }
        public IHalGripper Gripper01 { get { return (IHalGripper)this.GetMachine(MacEnumDevice.masktransfer_gripper_01); } }
        public IHalGripper Gripper02 { get { return (IHalGripper)this.GetMachine(MacEnumDevice.masktransfer_gripper_02); } }
        public IHalGripper Gripper03 { get { return (IHalGripper)this.GetMachine(MacEnumDevice.masktransfer_gripper_03); } }
        public IHalGripper Gripper04 { get { return (IHalGripper)this.GetMachine(MacEnumDevice.masktransfer_gripper_04); } }
        public IHalInfraredPhotointerrupter InfraLight { get { return (IHalInfraredPhotointerrupter)this.GetMachine(MacEnumDevice.masktransfer_light_interrupt_1); } }
        public IHalStaticElectricityDetector StaticElectricityDetector { get { return (IHalStaticElectricityDetector)this.GetMachine(MacEnumDevice.masktransfer_static_electricity_detector_1); } }



        #endregion Device Components

        #region HalBase

        public override int HalConnect()
        {
            return base.HalConnect();
        }


        #endregion


        /// <summary>
        /// 給點位清單，依序移動
        /// </summary>
        /// <param name="PathPosition"></param>
        public void RobotMove(List<HalRobotMotion> PathPosition)
        {
            //List<HalRobotMotion> PathPosition = HomeToOpenStage();

            var targets = new List<HalRobotMotion>();
            targets.AddRange(PathPosition);
            float[] target = new float[6];

            for (int idx = 0; idx < targets.Count; idx++)
            {
                var motion = targets[idx];

                this.Robot.HalMoveStraightAsyn(motion);
            }

        }

        /// <summary>
        /// 給單一點位，進行移動
        /// </summary>
        /// <param name="PathPosition"></param>
        public void RobotMove(HalRobotMotion PathPosition)
        {
            this.Robot.HalMoveStraightAsyn(PathPosition);
        }

        /// <summary>
        /// 調整手臂到其他進入Assembly的點位
        /// </summary>
        /// <param name="PosToAssembly">PosHome, PosToInspCh, PosToCleanCh</param>
        public void ChangeDirection(HalRobotMotion PosToAssembly)
        {
            bool Licence = false;
            string PosName = "";
            #region 確認Robot是否在三個可以轉動方向的點位內，並確認目前在哪個方位
            var StasrtPosInfo = new MvFanucRobotLdd().GetCurrRobotInfo();
            {
                if (StasrtPosInfo.x <= PosHome().X + 5 && StasrtPosInfo.x >= PosHome().X - 5
                    && StasrtPosInfo.y <= PosHome().Y + 5 && StasrtPosInfo.y >= PosHome().Y - 5
                    && StasrtPosInfo.z <= PosHome().Z + 5 && StasrtPosInfo.z >= PosHome().Z - 5
                    && StasrtPosInfo.w <= PosHome().W + 5 && StasrtPosInfo.w >= PosHome().W - 5
                    && StasrtPosInfo.p <= PosHome().P + 5 && StasrtPosInfo.p >= PosHome().P - 5
                    && StasrtPosInfo.r <= PosHome().R + 5 && StasrtPosInfo.r >= PosHome().R - 5)
                {
                    Licence = true; PosName = "Home";
                }
                else if (StasrtPosInfo.j1 <= PosToInspCh().J1 + 5 && StasrtPosInfo.j1 >= PosToInspCh().J1 - 5
                    && StasrtPosInfo.j2 <= PosToInspCh().J2 + 5 && StasrtPosInfo.j2 >= PosToInspCh().J2 - 5
                    && StasrtPosInfo.j3 <= PosToInspCh().J3 + 5 && StasrtPosInfo.j3 >= PosToInspCh().J3 - 5
                    && StasrtPosInfo.j4 <= PosToInspCh().J4 + 5 && StasrtPosInfo.j4 >= PosToInspCh().J4 - 5
                    && StasrtPosInfo.j5 <= PosToInspCh().J5 + 5 && StasrtPosInfo.j5 >= PosToInspCh().J5 - 5
                    && StasrtPosInfo.j6 <= PosToInspCh().J6 + 5 && StasrtPosInfo.j6 >= PosToInspCh().J6 - 5)
                {
                    Licence = true; PosName = "Inspection Chamber";
                }
                else if (StasrtPosInfo.j1 <= PosToCleanCh().J1 + 5 && StasrtPosInfo.j1 >= PosToCleanCh().J1 - 5
                    && StasrtPosInfo.j2 <= PosToCleanCh().J2 + 5 && StasrtPosInfo.j2 >= PosToCleanCh().J2 - 5
                    && StasrtPosInfo.j3 <= PosToCleanCh().J3 + 5 && StasrtPosInfo.j3 >= PosToCleanCh().J3 - 5
                    && StasrtPosInfo.j4 <= PosToCleanCh().J4 + 5 && StasrtPosInfo.j4 >= PosToCleanCh().J4 - 5
                    && StasrtPosInfo.j5 <= PosToCleanCh().J5 + 5 && StasrtPosInfo.j5 >= PosToCleanCh().J5 - 5
                    && StasrtPosInfo.j6 <= PosToCleanCh().J6 + 5 && StasrtPosInfo.j6 >= PosToCleanCh().J6 - 5)
                {
                    Licence = true; PosName = "Clean Chamber";
                }
            }
            #endregion

            if (Licence == true)
            {
                //如果目前位置不在InspCh且要移動的目的地也不是InspCh，則需要先經過InspCh點位再移動到目的地
                if (PosName != "Inspection Chamber" && PosToAssembly != PosToInspCh())
                {
                    this.Robot.HalMoveStraightAsyn(PosToInspCh());
                    this.Robot.HalMoveStraightAsyn(PosToAssembly);
                }
                else
                {
                    this.Robot.HalMoveStraightAsyn(PosToAssembly);
                }
            }
            else
                throw new Exception("Mask robot can not change direction. Because robot is not in the safe range now");
        }

        public string MTClamp()
        {
            string result = "";
            //TODO: Safety , capture image and process to recognize position
            //TODO: Safety , check sensor value: six axis sensor, clamp sensor, level sensor
            result = Plc.Clamp(0);
            return result;
        }

        public string MTUnclamp()
        {
            string result = "";
            //TODO: Safety , capture image and process to recognize position
            //TODO: Safety , check sensor value: six axis sensor, clamp sensor, level sensor
            result = Plc.Unclamp();
            return result;
        }

        public string MTCCDSpin(int SpinDegree)
        {
            string result = "";
            result= Plc.CCDSpin(SpinDegree);
            return result;
        }

        public void MTInitial()
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
            var SetResult = Plc.ReadClampTactileLimSetting();
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
            var SetResult = Plc.ReadLevelLimitSetting();
            if ((Level_X != null && SetResult.Item1 != Level_X)
                || (Level_Y != null && SetResult.Item2 != Level_Y)
                || (Level_Z != null && SetResult.Item3 != Level_Z))
                throw new Exception("MT clamp level sensor limit setting error !");
        }

        /// <summary>
        /// 設定六軸力覺Sensor的壓力極限值
        /// </summary>
        /// <param name="Fx"></param>
        /// <param name="Fy"></param>
        /// <param name="Fz"></param>
        /// <param name="Mx"></param>
        /// <param name="My"></param>
        /// <param name="Mz"></param>
        public void SetSixAxisSensorLimit(uint? Fx, uint? Fy, uint? Fz, uint? Mx, uint? My, uint? Mz)
        {
            Plc.SetSixAxisSensorLimit(Fx, Fy, Fz, Mx, My, Mz);
            Thread.Sleep(100);
            var SetResult = Plc.ReadLevelLimitSetting();
            if ((Fx != null && SetResult.Item1 != Fx)
                || (Fy != null && SetResult.Item2 != Fy)
                || (Fz != null && SetResult.Item3 != Fz)
                || (Mx != null && SetResult.Item1 != Mx)
                || (My != null && SetResult.Item1 != My)
                || (Mz != null && SetResult.Item1 != Mz))
                throw new Exception("MT six axis sensor limit setting error !");
        }

        /// <summary>
        /// 夾爪速度設定，單位(mm/sec)，CCD旋轉速度設定，單位(0.01 deg/sec)
        /// </summary>
        /// <param name="ClampSpeed">(mm/sec)</param>
        /// <param name="CCDSpinSpeed">(0.01 deg/sec)</param>
        public void SetSpeed(double? ClampSpeed, int? CCDSpinSpeed)
        {
            if (ClampSpeed < 1.0 || ClampSpeed > 10.0)
                throw new Exception("MT clamp speed setting only between 1.0 ~ 10.0 (mm/sec) !");
            Plc.SetSpeed(ClampSpeed, CCDSpinSpeed);
            Thread.Sleep(100);
            var SetResult = Plc.ReadSpeedSetting();
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
            var SetResult = Plc.ReadStaticElecLimitSetting();
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
        public Tuple<int, int> ReadClampTactileLimSetting()
        { return Plc.ReadClampTactileLimSetting(); }

        /// <summary>
        /// 讀取三軸水平極限值設定，X軸、Y軸、Z軸
        /// </summary>
        /// <returns>X軸、Y軸、Z軸</returns>
        public Tuple<int, int, int> ReadLevelLimitSetting()
        { return Plc.ReadLevelLimitSetting(); }

        /// <summary>
        /// 讀取六軸力覺Sensor的壓力極限值設定，Fx、Fy、Fz、Mx、My、Mz
        /// </summary>
        /// <returns>Fx、Fy、Fz、Mx、My、Mz</returns>
        public Tuple<int, int, int, int, int, int> ReadSixAxisSensorLimitSetting()
        { return Plc.ReadSixAxisSensorLimitSetting(); }

        /// <summary>
        /// 讀取速度設定，夾爪速度、CCD旋轉速度
        /// </summary>
        /// <returns>夾爪速度、CCD旋轉速度</returns>
        public Tuple<double, int> ReadSpeedSetting()
        { return Plc.ReadSpeedSetting(); }

        /// <summary>
        /// 讀取靜電感測的區間限制設定值，上限、下限
        /// </summary>
        /// <returns>上限、下限</returns>
        public Tuple<double, double> ReadStaticElecLimitSetting()
        { return Plc.ReadStaticElecLimitSetting(); }
        #endregion

        #region Read Component Value
        /// <summary>
        /// 讀取CCD旋轉角度
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
        public Tuple<int, int, int, int, int, int> ReadSixAxisSensor()
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
                X = (float)-1.287,
                Y = (float)302.844,
                Z = (float)189.852,
                W = (float)45.266,
                P = (float)-88.801,
                R = (float)-135.369,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 200
            });

            //PR[56]-LoadPort前(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)-375.933,
                Y = (float)304.885,
                Z = (float)291.887,
                W = (float)12.666,
                P = (float)-89.281,
                R = (float)-14.134,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 200
            });

            //PR[57]-LoadPort上方(伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)-635.133,
                Y = (float)304.885,
                Z = (float)291.887,
                W = (float)12.665,
                P = (float)-89.281,
                R = (float)-14.133,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 200
            });

            //PR[58]-LoadPort上方(盒子上方)
            poss.Add(new HalRobotMotion()
            {
                X = (float)-635.132,
                Y = (float)304.885,
                Z = (float)76.866,
                W = (float)12.668,
                P = (float)-89.281,
                R = (float)-14.136,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 100
            });

            //PR[59]-LoadPort上方(盒子上夾放Mask位置)
            poss.Add(new HalRobotMotion()
            {
                X = (float)-635.132,
                Y = (float)304.885,
                Z = (float)64.194,
                W = (float)12.669,
                P = (float)-89.281,
                R = (float)-14.137,
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
                X = (float)-635.132,
                Y = (float)304.885,
                Z = (float)64.194,
                W = (float)12.669,
                P = (float)-89.281,
                R = (float)-14.137,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            //PR[58]-LoadPort上方(盒子上方)
            poss.Add(new HalRobotMotion()
            {
                X = (float)-635.132,
                Y = (float)304.885,
                Z = (float)76.866,
                W = (float)12.668,
                P = (float)-89.281,
                R = (float)-14.136,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            //PR[57]-LoadPort上方(伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)-635.133,
                Y = (float)304.885,
                Z = (float)291.887,
                W = (float)12.665,
                P = (float)-89.281,
                R = (float)-14.133,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 200
            });

            //PR[56]-LoadPort前(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)-375.933,
                Y = (float)304.885,
                Z = (float)291.887,
                W = (float)12.666,
                P = (float)-89.281,
                R = (float)-14.134,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 200
            });

            //PR[54]-Load Port upside
            poss.Add(new HalRobotMotion()
            {
                X = (float)-1.287,
                Y = (float)302.844,
                Z = (float)189.852,
                W = (float)45.266,
                P = (float)-88.801,
                R = (float)-135.369,
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
                J1 = (float)-1.477,
                J2 = (float)-28.739,
                J3 = (float)-32.678,
                J4 = (float)-0.884,
                J5 = (float)33.525,
                J6 = (float)1.596,
                MotionType = HalRobotEnumMotionType.Joint,
                Speed = 20
            });

            //PR[61]-InspCh前(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)259.586,
                Y = (float)233.236,
                Z = (float)224.456,
                W = (float)13.800,
                P = (float)-88.910,
                R = (float)165.909,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 200
            });

            //PR[62]-InspCh內(伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)693.303,
                Y = (float)233.236,
                Z = (float)224.456,
                W = (float)13.799,
                P = (float)-88.910,
                R = (float)165.910,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 200
            });

            //PR[63]-Stage上方(盒子上方)
            poss.Add(new HalRobotMotion()
            {
                X = (float)693.303,
                Y = (float)233.236,
                Z = (float)156.683,
                W = (float)13.799,
                P = (float)-88.910,
                R = (float)165.910,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 100
            });

            //PR[64]-Stage上方(雲台上夾放Mask位置)
            poss.Add(new HalRobotMotion()
            {
                X = (float)693.303,
                Y = (float)233.236,
                Z = (float)151.268,
                W = (float)13.797,
                P = (float)-88.910,
                R = (float)165.912,
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
                X = (float)693.303,
                Y = (float)233.236,
                Z = (float)151.268,
                W = (float)13.797,
                P = (float)-88.910,
                R = (float)165.912,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            //PR[63]-Stage上方(盒子上方)
            poss.Add(new HalRobotMotion()
            {
                X = (float)693.303,
                Y = (float)233.236,
                Z = (float)156.683,
                W = (float)13.799,
                P = (float)-88.910,
                R = (float)165.910,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            //PR[62]-InspCh內(伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)693.303,
                Y = (float)233.236,
                Z = (float)224.456,
                W = (float)13.799,
                P = (float)-88.910,
                R = (float)165.910,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 100
            });

            //PR[61]-InspCh前(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)259.586,
                Y = (float)233.236,
                Z = (float)224.456,
                W = (float)13.800,
                P = (float)-88.910,
                R = (float)165.909,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 200
            });

            //PR[60]-要進InspCh的位置
            poss.Add(new HalRobotMotion()
            {
                J1 = (float)-1.477,
                J2 = (float)-28.739,
                J3 = (float)-32.678,
                J4 = (float)-0.884,
                J5 = (float)33.525,
                J6 = (float)1.596,
                MotionType = HalRobotEnumMotionType.Joint,
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
                J1 = (float)-1.477,
                J2 = (float)-28.739,
                J3 = (float)-32.678,
                J4 = (float)-0.884,
                J5 = (float)33.525,
                J6 = (float)1.596,
                MotionType = HalRobotEnumMotionType.Joint,
                Speed = 20
            });

            //PR[70]-InspCh前(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)295.586,
                Y = (float)232.637,
                Z = (float)224.455,
                W = (float)54.131,
                P = (float)-88.593,
                R = (float)126.138,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 200
            });

            //PR[71]-InspCh前，夾爪旋轉90度(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)295.586,
                Y = (float)232.637,
                Z = (float)224.456,
                W = (float)90.295,
                P = (float)-2.003,
                R = (float)89.678,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            //PR[72]-InspCh前，夾爪旋轉180度(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)295.586,
                Y = (float)232.637,
                Z = (float)224.456,
                W = (float)-57.100,
                P = (float)89.403,
                R = (float)-57.382,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            //PR[73]-InspCh內(伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)693.027,
                Y = (float)232.637,
                Z = (float)224.456,
                W = (float)-57.096,
                P = (float)89.403,
                R = (float)-57.379,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 200
            });

            //PR[74]-Stage上方(盒子上方)
            poss.Add(new HalRobotMotion()
            {
                X = (float)693.027,
                Y = (float)232.637,
                Z = (float)131.201,
                W = (float)-57.091,
                P = (float)89.403,
                R = (float)-57.374,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 100
            });

            //PR[75]-Stage上方(雲台上夾放Mask位置)
            poss.Add(new HalRobotMotion()
            {
                X = (float)693.027,
                Y = (float)232.637,
                Z = (float)120.929,
                W = (float)-57.078,
                P = (float)89.403,
                R = (float)-57.361,
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
                X = (float)693.027,
                Y = (float)232.637,
                Z = (float)120.929,
                W = (float)-57.078,
                P = (float)89.403,
                R = (float)-57.361,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            //PR[74]-Stage上方(盒子上方)
            poss.Add(new HalRobotMotion()
            {
                X = (float)693.027,
                Y = (float)232.637,
                Z = (float)131.201,
                W = (float)-57.091,
                P = (float)89.403,
                R = (float)-57.374,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            //PR[73]-InspCh內(伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)693.027,
                Y = (float)232.637,
                Z = (float)224.456,
                W = (float)-57.096,
                P = (float)89.403,
                R = (float)-57.379,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 200
            });

            //PR[72]-InspCh前(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)295.586,
                Y = (float)232.637,
                Z = (float)224.456,
                W = (float)-57.100,
                P = (float)89.403,
                R = (float)-57.382,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 200
            });

            //PR[71]-InspCh前，夾爪旋轉90度(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)295.586,
                Y = (float)232.637,
                Z = (float)224.456,
                W = (float)90.295,
                P = (float)-2.003,
                R = (float)89.678,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            //PR[70]-InspCh前，夾爪旋轉180度(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)295.586,
                Y = (float)232.637,
                Z = (float)224.455,
                W = (float)54.131,
                P = (float)-88.593,
                R = (float)126.138,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            //PR[60]-要進InspCh的位置
            poss.Add(new HalRobotMotion()
            {
                J1 = (float)-1.477,
                J2 = (float)-28.739,
                J3 = (float)-32.678,
                J4 = (float)-0.884,
                J5 = (float)33.525,
                J6 = (float)1.596,
                MotionType = HalRobotEnumMotionType.Joint,
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
                J1 = (float)-89.667,
                J2 = (float)-28.739,
                J3 = (float)-32.678,
                J4 = (float)-0.884,
                J5 = (float)33.525,
                J6 = (float)1.596,
                MotionType = HalRobotEnumMotionType.Joint,
                Speed = 20

            });

            //PR[30]-CleanCh內(伸出手臂於Air Gun上方)
            poss.Add(new HalRobotMotion()
            {
                X = (float)0.976,
                Y = (float)-634.621,
                Z = (float)205.211,
                W = (float)45.253,
                P = (float)-88.801,
                R = (float)44.586,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 100
            });

            //PR[25]-CleanCh內(伸出手臂於Air Gun上方，以Mask左下方為清理起點)
            for (float y = 0; y < (float)150.0; y += (float)50.0)
                for (float x = 0; x < (float)150.0; x += (float)150.0)
                    poss.Add(new HalRobotMotion()
                    {
                        X = (float)58.192 + x,
                        Y = (float)-734.080 + y,
                        Z = (float)116.219,
                        W = (float)45.272,
                        P = (float)-88.801,
                        R = (float)44.566,
                        MotionType = HalRobotEnumMotionType.Position,
                        Speed = 20
                    });

            //PR[30]-CleanCh內(伸出手臂於Air Gun上方)
            poss.Add(new HalRobotMotion()
            {
                X = (float)0.976,
                Y = (float)-634.621,
                Z = (float)205.211,
                W = (float)45.253,
                P = (float)-88.801,
                R = (float)44.586,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 100
            });

            //PR[21]-要進CleanCh的位置(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                J1 = (float)-89.667,
                J2 = (float)-28.739,
                J3 = (float)-32.678,
                J4 = (float)-0.884,
                J5 = (float)33.525,
                J6 = (float)1.596,
                MotionType = HalRobotEnumMotionType.Joint,
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
                J1 = (float)-89.667,
                J2 = (float)-28.739,
                J3 = (float)-32.678,
                J4 = (float)-0.884,
                J5 = (float)33.525,
                J6 = (float)1.596,
                MotionType = HalRobotEnumMotionType.Joint,
                Speed = 20

            });

            //PR[30]-CleanCh內(伸出手臂於Air Gun上方)
            poss.Add(new HalRobotMotion()
            {
                X = (float)0.976,
                Y = (float)-634.621,
                Z = (float)205.211,
                W = (float)45.253,
                P = (float)-88.801,
                R = (float)44.586,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 100
            });

            //PR[27]-CleanCh內(伸出手臂於CCD上方，以Mask左下方為拍照起點)
            for (float y = 0; y < (float)150.0; y += (float)50.0)
                for (float x = 0; x < (float)150.0; x += (float)150.0)
                    poss.Add(new HalRobotMotion()
                    {
                        X = (float)-209.072 + x,
                        Y = (float)-734.080 + y,
                        Z = (float)205.211,
                        W = (float)45.268,
                        P = (float)-88.801,
                        R = (float)44.570,
                        MotionType = HalRobotEnumMotionType.Position,
                        Speed = 20
                    });

            //PR[30]-CleanCh內(伸出手臂於Air Gun上方)
            poss.Add(new HalRobotMotion()
            {
                X = (float)0.976,
                Y = (float)-634.621,
                Z = (float)205.211,
                W = (float)45.253,
                P = (float)-88.801,
                R = (float)44.586,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 100
            });

            //PR[21]-要進CleanCh的位置(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                J1 = (float)-89.667,
                J2 = (float)-28.739,
                J3 = (float)-32.678,
                J4 = (float)-0.884,
                J5 = (float)33.525,
                J6 = (float)1.596,
                MotionType = HalRobotEnumMotionType.Joint,
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
                J1 = (float)-89.667,
                J2 = (float)-28.739,
                J3 = (float)-32.678,
                J4 = (float)-0.884,
                J5 = (float)33.525,
                J6 = (float)1.596,
                MotionType = HalRobotEnumMotionType.Joint,
                Speed = 20

            });

            //PR[41]-要進CleanCh的位置，旋轉90度(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)0.976,
                Y = (float)-302.844,
                Z = (float)189.851,
                W = (float)89.843,
                P = (float)1.229,
                R = (float)-0.310,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            //PR[31]-要進CleanCh的位置，旋轉180度(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)0.976,
                Y = (float)-302.844,
                Z = (float)189.851,
                W = (float)-70.174,
                P = (float)89.091,
                R = (float)-160.021,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            //PR[32]-CleanCh內(伸出手臂於Air Gun上方)
            poss.Add(new HalRobotMotion()
            {
                X = (float)0.976,
                Y = (float)-605.053,
                Z = (float)229.019,
                W = (float)-70.178,
                P = (float)89.091,
                R = (float)-160.025,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 100
            });

            //PR[35]-CleanCh內(伸出手臂於Air Gun上方，以Mask左下方為清理起點)
            for (float y = 0; y < (float)150.0; y += (float)50.0)
                for (float x = 0; x < (float)150.0; x += (float)150.0)
                    poss.Add(new HalRobotMotion()
                    {
                        X = (float)47.708 + x,
                        Y = (float)-722.709 + y,
                        Z = (float)138.564,
                        W = (float)-74.088,
                        P = (float)89.110,
                        R = (float)-163.934,
                        MotionType = HalRobotEnumMotionType.Position,
                        Speed = 20
                    });

            //PR[32]-CleanCh內(伸出手臂於Air Gun上方)
            poss.Add(new HalRobotMotion()
            {
                X = (float)0.976,
                Y = (float)-605.053,
                Z = (float)229.019,
                W = (float)-70.178,
                P = (float)89.091,
                R = (float)-160.025,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 100
            });

            //PR[31]-要進CleanCh的位置(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)0.976,
                Y = (float)-302.844,
                Z = (float)189.851,
                W = (float)-70.174,
                P = (float)89.091,
                R = (float)-160.021,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 100
            });

            //PR[41]-要進CleanCh的位置，旋轉90度(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)0.976,
                Y = (float)-302.844,
                Z = (float)189.851,
                W = (float)89.843,
                P = (float)1.229,
                R = (float)-0.310,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            //PR[21]-要進CleanCh的位置，旋轉180度(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                J1 = (float)-89.667,
                J2 = (float)-28.739,
                J3 = (float)-32.678,
                J4 = (float)-0.884,
                J5 = (float)33.525,
                J6 = (float)1.596,
                MotionType = HalRobotEnumMotionType.Joint,
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
                J1 = (float)-89.667,
                J2 = (float)-28.739,
                J3 = (float)-32.678,
                J4 = (float)-0.884,
                J5 = (float)33.525,
                J6 = (float)1.596,
                MotionType = HalRobotEnumMotionType.Joint,
                Speed = 20

            });

            //PR[41]-要進CleanCh的位置，旋轉90度(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)0.976,
                Y = (float)-302.844,
                Z = (float)189.851,
                W = (float)89.843,
                P = (float)1.229,
                R = (float)-0.310,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            //PR[31]-要進CleanCh的位置，旋轉180度(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)0.976,
                Y = (float)-302.844,
                Z = (float)189.851,
                W = (float)-70.174,
                P = (float)89.091,
                R = (float)-160.021,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            //PR[32]-CleanCh內(伸出手臂於Air Gun上方)
            poss.Add(new HalRobotMotion()
            {
                X = (float)0.976,
                Y = (float)-605.053,
                Z = (float)229.019,
                W = (float)-70.178,
                P = (float)89.091,
                R = (float)-160.025,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 100
            });

            //PR[39]-CleanCh內(伸出手臂於CCD上方，以Mask左下方為拍照起點)
            for (float y = 0; y < (float)150.0; y += (float)50.0)
                for (float x = 0; x < (float)150.0; x += (float)150.0)
                    poss.Add(new HalRobotMotion()
                    {
                        X = (float)-214.026 + x,
                        Y = (float)-759.268 + y,
                        Z = (float)290.305,
                        W = (float)-70.183,
                        P = (float)89.091,
                        R = (float)-160.030,
                        MotionType = HalRobotEnumMotionType.Position,
                        Speed = 20
                    });

            //PR[32]-CleanCh內(伸出手臂於Air Gun上方)
            poss.Add(new HalRobotMotion()
            {
                X = (float)0.976,
                Y = (float)-605.053,
                Z = (float)229.019,
                W = (float)-70.178,
                P = (float)89.091,
                R = (float)-160.025,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 100
            });

            //PR[31]-要進CleanCh的位置(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)0.976,
                Y = (float)-302.844,
                Z = (float)189.851,
                W = (float)-70.174,
                P = (float)89.091,
                R = (float)-160.021,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 100
            });

            //PR[41]-要進CleanCh的位置，旋轉90度(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                X = (float)0.976,
                Y = (float)-302.844,
                Z = (float)189.851,
                W = (float)89.843,
                P = (float)1.229,
                R = (float)-0.310,
                MotionType = HalRobotEnumMotionType.Position,
                Speed = 20
            });

            //PR[21]-要進CleanCh的位置，旋轉180度(未伸出手臂)
            poss.Add(new HalRobotMotion()
            {
                J1 = (float)-89.667,
                J2 = (float)-28.739,
                J3 = (float)-32.678,
                J4 = (float)-0.884,
                J5 = (float)33.525,
                J6 = (float)1.596,
                MotionType = HalRobotEnumMotionType.Joint,
                Speed = 20
            });

            return poss;
        }

        public HalRobotMotion PosHome()
        {
            var posotion = new HalRobotMotion();

            posotion.X = (float)-1.287;
            posotion.Y = (float)302.844;
            posotion.Z = (float)189.852;
            posotion.W = (float)45.266;
            posotion.P = (float)-88.801;
            posotion.R = (float)-135.369;
            posotion.MotionType = HalRobotEnumMotionType.Position;
            posotion.Speed = 200;

            return posotion;
        }

        public HalRobotMotion PosToInspCh()
        {
            var posotion = new HalRobotMotion();

            posotion.J1 = (float)-1.477;
            posotion.J2 = (float)-28.739;
            posotion.J3 = (float)-32.678;
            posotion.J4 = (float)-0.884;
            posotion.J5 = (float)33.525;
            posotion.J6 = (float)1.596;
            posotion.MotionType = HalRobotEnumMotionType.Joint;
            posotion.Speed = 20;

            return posotion;
        }

        public HalRobotMotion PosToCleanCh()
        {
            var posotion = new HalRobotMotion();

            posotion.J1 = (float)-89.667;
            posotion.J2 = (float)-28.739;
            posotion.J3 = (float)-32.678;
            posotion.J4 = (float)-0.884;
            posotion.J5 = (float)33.525;
            posotion.J6 = (float)1.596;
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
