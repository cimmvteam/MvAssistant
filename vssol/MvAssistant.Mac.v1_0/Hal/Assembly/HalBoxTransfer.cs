using MvAssistant.Mac.v1_0.Hal.Component;
using MvAssistant.Mac.v1_0.Hal.Component.Camera;
using MvAssistant.Mac.v1_0.Hal.Component.Force6Axis;
using MvAssistant.Mac.v1_0.Hal.Component.Gripper;
using MvAssistant.Mac.v1_0.Hal.Component.Robot;
using MvAssistant.Manifest;
using System;
using System.Runtime.InteropServices;

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{
    [MachineManifest(DeviceEnum.boxtransfer_assembly)]
    [GuidAttribute("53CD572B-CAB0-498A-8005-B56AB7ED745F")]
    public class HalBoxTransfer : HalAssemblyBase, IHalBoxTransfer
    {
        #region Device Components (請看範例說明)
        [MachineManifest(DeviceEnum.boxtransfer_robot_1)]
        public IHalRobot Robot { get; set; }

        [MachineManifest(DeviceEnum.boxtransfer_force_6axis_sensor_1)]
        public IHalForce6Axis Force6Axis { get; set; }

        [MachineManifest(DeviceEnum.boxtransfer_ccd_gripper_1)]
        public IHalCamera Camera_BoxSlot_Direction { get; set; }

        [MachineManifest(DeviceEnum.boxtransfer_ringlight_1)]
        public IHalLight CameraCircleLight { get; set; }

        [MachineManifest(DeviceEnum.boxtransfer_gripper_1)]
        public IHalGripper Gripper { get; set; }

        [MachineManifest(DeviceEnum.boxtransfer_robot_skin_1)]
        public IHalRobotSkin RobotSkin { get; set; }

        [MachineManifest(DeviceEnum.boxtransfer_laser_gripper_1)]
        public IHalLaser Laser_BoxSlot_Z { get; set; }

        #endregion Device Components

        #region HAL Interface Functions
        public string ID
        {
            get;
            set;
        }

        public string DeviceConnStr
        {
            get;
            set;
        }

        public int HalStop()
        {
            throw new NotImplementedException();
        }

        public int HalConnect()
        {
            throw new NotImplementedException();
        }

        public int HalClose()
        {
            throw new NotImplementedException();
        }

        public bool HalIsConnected()
        {
            throw new NotImplementedException();
        }
        #endregion HAL Interface Functions


    }
}
