using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Hal.Component;
using MvAssistant.Mac.v1_0.Hal.Component.Camera;
using MvAssistant.Mac.v1_0.Hal.Component.Force6Axis;
using MvAssistant.Mac.v1_0.Hal.Component.Gripper;
using MvAssistant.Mac.v1_0.Hal.Component.Inclinometer;
using MvAssistant.Mac.v1_0.Hal.Component.Infrared;
using MvAssistant.Mac.v1_0.Hal.Component.Robot;
using MvAssistant.Mac.v1_0.Hal.Component.Stage;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Runtime.InteropServices;

namespace MvAssistant.Mac.v1_0.Hal.AssemblyFake
{
    [MacMachineManifest(MacEnumDevice.masktransfer_assembly)]
    [GuidAttribute("329A464A-B82E-4882-B00B-F34E7C1E2358")]
    public class MacHalMaskTransferFake : HalFakeBase, IMacHalMaskTransfer
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

        //[MachineManifest(DeviceEnum.masktransfer_assembly)]
        [MacMachineManifest(MacEnumDevice.masktransfer_robot_1)]
        public IHalRobot Robot { get; set; }
        [MacMachineManifest(MacEnumDevice.masktransfer_force_6axis_sensor_1)]
        public IHalForce6Axis Force6Axis { get; set; }
        [MacMachineManifest(MacEnumDevice.masktransfer_inclinometer01)]
        public IHalInclinometer Gradienter { get; set; }
        [MacMachineManifest(MacEnumDevice.masktransfer_ccd_pellicle_deform_1)]
        public IHalCamera CameraPellicleDeform { get; set; }
        [MacMachineManifest(MacEnumDevice.masktransfer_ccd_barcode_reader_1)]
        public IHalCamera CameraBarcodeReader { get; set; }
        [MacMachineManifest(MacEnumDevice.masktransfer_light_barcode_1)]
        public IHalLight CameraBarcodeLight { get; set; }
        [MacMachineManifest(MacEnumDevice.masktransfer_stage_pellicle_deform_1)]
        public IHalPellicleDeformStage StagePellicleDeform { get; set; }
        [MacMachineManifest(MacEnumDevice.masktransfer_tactile_1)]
        public IHalTactile Tactile1 { get; set; }
        [MacMachineManifest(MacEnumDevice.masktransfer_tactile_2)]
        public IHalTactile Tactile2 { get; set; }
        [MacMachineManifest(MacEnumDevice.masktransfer_tactile_3)]
        public IHalTactile Tactile3 { get; set; }
        [MacMachineManifest(MacEnumDevice.masktransfer_tactile_4)]
        public IHalTactile Tactile4 { get; set; }
        [MacMachineManifest(MacEnumDevice.masktransfer_gripper_01)]
        public IHalGripper Gripper01 { get; set; }
        [MacMachineManifest(MacEnumDevice.masktransfer_gripper_02)]
        public IHalGripper Gripper02 { get; set; }
        [MacMachineManifest(MacEnumDevice.masktransfer_gripper_03)]
        public IHalGripper Gripper03 { get; set; }
        [MacMachineManifest(MacEnumDevice.masktransfer_gripper_04)]
        public IHalGripper Gripper04 { get; set; }
        [MacMachineManifest(MacEnumDevice.masktransfer_light_interrupt_1)]
        public IHalInfraredPhotointerrupter InfraLight { get; set; }
        [MacMachineManifest(MacEnumDevice.masktransfer_robot_skin_1)]
        public IHalRobotSkin RobotSkin { get; set; }
        [MacMachineManifest(MacEnumDevice.masktransfer_static_electricity_detector_1)]
        public IHalStaticElectricityDetector StaticElectricityDetector { get; set; }

        #endregion Device Components

        #region HAL Interface Functions
        public int HalStop()
        {
            throw new NotImplementedException();
        }
        #endregion HAL Interface Functions
    }
}
