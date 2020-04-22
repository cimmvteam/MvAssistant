using MvAssistant.Mac.v1_0.Hal.Component;
using MvAssistant.Mac.v1_0.Hal.Component.Camera;
using MvAssistant.Mac.v1_0.Hal.Component.Force6Axis;
using MvAssistant.Mac.v1_0.Hal.Component.Gripper;
using MvAssistant.Mac.v1_0.Hal.Component.Inclinometer;
using MvAssistant.Mac.v1_0.Hal.Component.Infrared;
using MvAssistant.Mac.v1_0.Hal.Component.Robot;
using MvAssistant.Mac.v1_0.Hal.Component.Stage;
using MvAssistant.Manifest;
using System;
using System.Runtime.InteropServices;

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{
    [MachineManifest(DeviceEnum.masktransfer_assembly)]
    [GuidAttribute("BE7EADB1-6821-4CDC-980C-8673F2B50225")]
    public class HalMaskTransfer : HalAssemblyBase, IHalMaskTransfer
    {
        #region Device Components


        [MachineManifest(DeviceEnum.masktransfer_robot_1)]
        public IHalRobot Robot { get; set; }
        [MachineManifest(DeviceEnum.masktransfer_force_6axis_sensor_1)]
        public IHalForce6Axis Force6Axis { get; set; }
        [MachineManifest(DeviceEnum.masktransfer_inclinometer01)]
        public IHalInclinometer Gradienter { get; set; }
        [MachineManifest(DeviceEnum.masktransfer_ccd_pellicle_deform_1)]
        public IHalCamera CameraPellicleDeform { get; set; }
        [MachineManifest(DeviceEnum.masktransfer_ccd_barcode_reader_1)]
        public IHalCamera CameraBarcodeReader { get; set; }
        [MachineManifest(DeviceEnum.masktransfer_light_barcode_1)]
        public IHalLight CameraBarcodeLight { get; set; }
        [MachineManifest(DeviceEnum.masktransfer_stage_pellicle_deform_1)]
        public IHalPellicleDeformStage StagePellicleDeform { get; set; }
        [MachineManifest(DeviceEnum.masktransfer_tactile_1)]
        public IHalTactile Tactile1 { get; set; }
        [MachineManifest(DeviceEnum.masktransfer_tactile_2)]
        public IHalTactile Tactile2 { get; set; }
        [MachineManifest(DeviceEnum.masktransfer_tactile_3)]
        public IHalTactile Tactile3 { get; set; }
        [MachineManifest(DeviceEnum.masktransfer_tactile_4)]
        public IHalTactile Tactile4 { get; set; }
        [MachineManifest(DeviceEnum.masktransfer_gripper_01)]
        public IHalGripper Gripper01 { get; set; }
        [MachineManifest(DeviceEnum.masktransfer_gripper_02)]
        public IHalGripper Gripper02 { get; set; }
        [MachineManifest(DeviceEnum.masktransfer_gripper_03)]
        public IHalGripper Gripper03 { get; set; }
        [MachineManifest(DeviceEnum.masktransfer_gripper_04)]
        public IHalGripper Gripper04 { get; set; }
        [MachineManifest(DeviceEnum.masktransfer_light_interrupt_1)]
        public IHalInfraredPhotointerrupter InfraLight { get; set; }
        [MachineManifest(DeviceEnum.masktransfer_static_electricity_detector_1)]
        public IHalStaticElectricityDetector StaticElectricityDetector { get; set; }


        #endregion Device Components

        #region HAL Interface Functions
        int IHalAssembly.HalStop()
        {
            throw new NotImplementedException();
        }

        int IHal.HalConnect()
        {
            throw new NotImplementedException();
        }

        int IHal.HalClose()
        {
            throw new NotImplementedException();
        }

        bool IHal.HalIsConnected()
        {
            throw new NotImplementedException();
        }
        #endregion HAL Interface Functions

       



    }
}
