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



        public int HalMoveAsyn()
        { return 0; }


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
        /// 調整手臂到其他進入Assembly的點位
        /// </summary>
        /// <param name="PosToAssembly">PosHome, PosToInspCh, PosToCleanCh</param>
        public void ChangeDirection(HalRobotMotion PosToAssembly)
        {
            var ChgLicence = ChgDirLicence();
            if (ChgLicence.Item1 == true)
            {
                //如果目前位置不在InspCh且要移動的目的地也不是InspCh，則需要先經過InspCh點位再移動到目的地
                if (ChgLicence.Item2 != "Inspection Chamber" && PosToAssembly != PosToInspCh())
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
                throw new Exception("Mask robot can not turn to " + ChgLicence.Item2);
        }

        public void MtClamp()
        {


            //TODO: Safety , caputre image and process to recognize position


            this.Plc.Clamp(0);
        }

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

        /// <summary>
        /// 取得移動手臂方向的許可，回傳是否可移動及目前位置
        /// </summary>
        /// <returns>可否移動 ; 目前位置</returns>
        public Tuple<bool, string> ChgDirLicence()
        {
            bool Licence = false;
            string PosName = "";
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
            return new Tuple<bool, string>(Licence, PosName);
        }

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
