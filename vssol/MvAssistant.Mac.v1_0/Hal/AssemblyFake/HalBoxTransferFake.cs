using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Hal.Component;
using MvAssistant.Mac.v1_0.Hal.Component.Camera;
using MvAssistant.Mac.v1_0.Hal.Component.Force6Axis;
using MvAssistant.Mac.v1_0.Hal.Component.Gripper;
using MvAssistant.Mac.v1_0.Hal.Component.Robot;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.AssemblyFake
{
    [GuidAttribute("E84D4D03-CCE7-4CB0-A192-CDD81C8C249F")]
    [MachineManifest(EnumDevice.boxtransfer_assembly)]
    public class HalBoxTransferFake : HalFakeBase, IHalBoxTransfer
    {
        #region Device Components (請看範例說明)
        /// 範例說明
        /// private IHalCamera topCamera;
        /// private IHalCamera sideCamera;
        /// 
        /// [MachineManifest(DeviceEnum.loadport_ccd_top_1)]
        /// public IHalCamera TopCamera
        /// {
        ///     set { topCamera = value; }
        ///     get { return topCamera; }
        /// }
        /// 
        /// [MachineManifest(DeviceEnum.loadport_ccd_side_1)]
        /// public IHalCamera SideCamera
        /// {
        ///     set { sideCamera = value; }
        ///     get { return sideCamera; }
        /// }
        /// 


        public IHalCamera Camera_BoxSlot_Direction { get; set; }
        public IHalLight CameraCircleLight { get; set; }
        public IHalForce6Axis Force6Axis { get; set; }
        public IHalGripper Gripper { get; set; }
        public IHalLaser Laser_BoxSlot_Z { get; set; }
        public IHalRobot Robot { get; set; }
        public IHalRobotSkin RobotSkin { get; set; }
        public IHalVibration Vibration { get; set; }

        #endregion Device Components

        #region HAL Interface Functions
        public int HalStop()
        {
            throw new NotImplementedException();
        }
        #endregion HAL Interface Functions
    }
}
