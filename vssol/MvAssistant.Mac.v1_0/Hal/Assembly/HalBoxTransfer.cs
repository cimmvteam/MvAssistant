using MaskAutoCleaner.Hal.Intf.Assembly;
using MaskAutoCleaner.Hal.Intf.Component;
using MaskAutoCleaner.Hal.Intf.Component.Force6Axis;
using MaskAutoCleaner.Hal.Intf.Component.Gripper;
using MaskAutoCleaner.Hal.Intf.Component.Robot;
using MaskAutoCleaner.Hal.Intf.Assembly;
using MaskAutoCleaner.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.Imp.Assembly
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
