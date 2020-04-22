using MaskAutoCleaner.Hal.Intf.Component;
using MaskAutoCleaner.Hal.Intf.Component.Force6Axis;
using MaskAutoCleaner.Hal.Intf.Component.Gripper;
using MaskAutoCleaner.Hal.Intf.Component.Inclinometer;
using MaskAutoCleaner.Hal.Intf.Component.Infrared;
using MaskAutoCleaner.Hal.Intf.Component.Robot;
using MaskAutoCleaner.Hal.Intf.Component.Stage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.Intf.Assembly
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
