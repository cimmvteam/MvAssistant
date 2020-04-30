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


        public void MoveToLoadPortFromHome()
        {


            List<MvFanucRobotInfo> PathPosition = LPUpsideToOSPutMask();

            var targets = new List<MvFanucRobotInfo>();
            targets.AddRange(PathPosition);
            float[] target = new float[6];

            for (int idx = 0; idx < targets.Count; idx++)
            {
                var pose = targets[idx];

                var motion = new HalRobotMotion();

                motion.X = pose.x;
                motion.Y = pose.y;
                motion.X = pose.z;
                motion.X = pose.w;
                motion.X = pose.p;
                motion.X = pose.r;

                motion.Speed = pose.speed;
                motion.MotionType = HalRobotEnumMotionType.Position;

       
                this.Robot.HalMoveStraightAsyn(motion);
            }

        }

        public void MtClamp()
        {


            //TODO: Safety , caputre image and process to recognize position


            this.Plc.Clamp(0);
        }


        public List<MvFanucRobotInfo> LPUpsideToOSPutMask()
        {
            var poss = new List<MvFanucRobotInfo>();

            //PR[54]-Load Port upside
            poss.Add(new MvFanucRobotInfo()
            {
                x = (float)-1.287,
                y = (float)302.844,
                z = (float)189.852,
                w = (float)45.266,
                p = (float)-88.801,
                r = (float)-135.369,
                MotionType = 1,
                speed = 200
            });

            //PR[56]-LoadPort前(未伸出手臂)
            poss.Add(new MvFanucRobotInfo()
            {
                x = (float)-422.038,
                y = (float)305.272,
                z = (float)181.435,
                w = (float)7.339,
                p = (float)-88.870,
                r = (float)-8.811,
                MotionType = 1,
                speed = 100
            });

            //PR[57]-LoadPort上方(伸出手臂)
            poss.Add(new MvFanucRobotInfo()
            {
                x = (float)-637.878,
                y = (float)305.272,
                z = (float)181.435,
                w = (float)7.339,
                p = (float)-88.870,
                r = (float)-8.810,
                MotionType = 1,
                speed = 20
            });

            return poss;
        }


    }
}
