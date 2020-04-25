﻿using MvAssistant.Mac.v1_0.Hal.Component;
using MvAssistant.Mac.v1_0.Hal.Component.Camera;
using MvAssistant.Mac.v1_0.Hal.Component.Force6Axis;
using MvAssistant.Mac.v1_0.Hal.Component.Gripper;
using MvAssistant.Mac.v1_0.Hal.Component.Robot;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Runtime.InteropServices;

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{
    [GuidAttribute("53CD572B-CAB0-498A-8005-B56AB7ED745F")]
    public class MacHalBoxTransfer : MacHalAssemblyBase, IMacHalBoxTransfer
    {
        #region Device Components



        public IHalRobot Robot { get { return (IHalRobot)this.GetMachine(MacEnumDevice.boxtransfer_robot_1); } }
        public IHalForce6Axis Force6Axis { get { return (IHalForce6Axis)this.GetMachine(MacEnumDevice.boxtransfer_force_6axis_sensor_1); } }
        public IHalCamera Camera_BoxSlot_Direction { get { return (IHalCamera)this.GetMachine(MacEnumDevice.boxtransfer_ccd_gripper_1); } }
        public IHalLight CameraCircleLight { get { return (IHalLight)this.GetMachine(MacEnumDevice.boxtransfer_ringlight_1); } }
        public IHalGripper Gripper { get { return (IHalGripper)this.GetMachine(MacEnumDevice.boxtransfer_gripper_1); } }
        public IHalRobotSkin RobotSkin { get { return (IHalRobotSkin)this.GetMachine(MacEnumDevice.boxtransfer_robot_skin_1); } }
        public IHalLaser Laser_BoxSlot_Z { get { return (IHalLaser)this.GetMachine(MacEnumDevice.boxtransfer_laser_gripper_1); } }


        #endregion Device Components




    }
}
