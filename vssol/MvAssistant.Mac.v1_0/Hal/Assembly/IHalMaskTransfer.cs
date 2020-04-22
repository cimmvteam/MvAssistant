using MvAssistant.Mac.v1_0.Hal.Component;
using MvAssistant.Mac.v1_0.Hal.Component.Camera;
using MvAssistant.Mac.v1_0.Hal.Component.Force6Axis;
using MvAssistant.Mac.v1_0.Hal.Component.Gripper;
using MvAssistant.Mac.v1_0.Hal.Component.Inclinometer;
using MvAssistant.Mac.v1_0.Hal.Component.Infrared;
using MvAssistant.Mac.v1_0.Hal.Component.Robot;
using MvAssistant.Mac.v1_0.Hal.Component.Stage;
using System.Runtime.InteropServices;

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{
    [GuidAttribute("6412D4A0-41F3-4659-B12B-7A8BF9399BAE")]
    public interface IHalMaskTransfer : IHalAssembly
    {
        IHalLight CameraBarcodeLight { get; set; }
        IHalCamera CameraBarcodeReader { get; set; }
        IHalCamera CameraPellicleDeform { get; set; }
        IHalForce6Axis Force6Axis { get; set; }
        IHalInclinometer Gradienter { get; set; }
        IHalInfraredPhotointerrupter InfraLight { get; set; }
        IHalGripper Gripper01 { get; set; }
        IHalGripper Gripper02 { get; set; }
        IHalGripper Gripper03 { get; set; }
        IHalGripper Gripper04 { get; set; }
        IHalRobot Robot { get; set; }
        IHalPellicleDeformStage StagePellicleDeform { get; set; }
        IHalStaticElectricityDetector StaticElectricityDetector { get; set; }
        IHalTactile Tactile1 { get; set; }
        IHalTactile Tactile2 { get; set; }
        IHalTactile Tactile3 { get; set; }
        IHalTactile Tactile4 { get; set; }
    }
}
