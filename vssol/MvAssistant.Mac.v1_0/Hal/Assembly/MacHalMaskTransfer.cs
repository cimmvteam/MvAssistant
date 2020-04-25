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

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{
    [GuidAttribute("BE7EADB1-6821-4CDC-980C-8673F2B50225")]
    public class MacHalMaskTransfer : MacHalAssemblyBase, IMacHalMaskTransfer
    {
        #region Device Components


        public IHalRobot Robot { get{ return (IHalRobot)this.GetMachine(MacEnumDevice.masktransfer_robot_1); }  }
        public IHalForce6Axis Force6Axis { get{ return (IHalForce6Axis)this.GetMachine(MacEnumDevice.masktransfer_force_6axis_sensor_1); }  }
        public IHalInclinometer Gradienter { get{ return (IHalInclinometer)this.GetMachine(MacEnumDevice.masktransfer_inclinometer01); }  }
        public IHalCamera CameraPellicleDeform { get{ return (IHalCamera)this.GetMachine(MacEnumDevice.masktransfer_ccd_pellicle_deform_1); }  }
        public IHalCamera CameraBarcodeReader { get{ return (IHalCamera)this.GetMachine(MacEnumDevice.masktransfer_ccd_barcode_reader_1); }  }
        public IHalLight CameraBarcodeLight { get{ return (IHalLight)this.GetMachine(MacEnumDevice.masktransfer_light_barcode_1); }  }
        public IHalPellicleDeformStage StagePellicleDeform { get{ return (IHalPellicleDeformStage)this.GetMachine(MacEnumDevice.masktransfer_stage_pellicle_deform_1); }  }
        public IHalTactile Tactile1 { get{ return (IHalTactile)this.GetMachine(MacEnumDevice.masktransfer_tactile_1); }  }
        public IHalTactile Tactile2 { get{ return (IHalTactile)this.GetMachine(MacEnumDevice.masktransfer_tactile_2); }  }
        public IHalTactile Tactile3 { get{ return (IHalTactile)this.GetMachine(MacEnumDevice.masktransfer_tactile_3); }  }
        public IHalTactile Tactile4 { get{ return (IHalTactile)this.GetMachine(MacEnumDevice.masktransfer_tactile_4); }  }
        public IHalGripper Gripper01 { get{ return (IHalGripper)this.GetMachine(MacEnumDevice.masktransfer_gripper_01); }  }
        public IHalGripper Gripper02 { get{ return (IHalGripper)this.GetMachine(MacEnumDevice.masktransfer_gripper_02); }  }
        public IHalGripper Gripper03 { get{ return (IHalGripper)this.GetMachine(MacEnumDevice.masktransfer_gripper_03); }  }
        public IHalGripper Gripper04 { get{ return (IHalGripper)this.GetMachine(MacEnumDevice.masktransfer_gripper_04); }  }
        public IHalInfraredPhotointerrupter InfraLight { get{ return (IHalInfraredPhotointerrupter)this.GetMachine(MacEnumDevice.masktransfer_light_interrupt_1); }  }
        public IHalStaticElectricityDetector StaticElectricityDetector { get{ return (IHalStaticElectricityDetector)this.GetMachine(MacEnumDevice.masktransfer_static_electricity_detector_1); }  }


        #endregion Device Components





    }
}
