using MvAssistant.Mac.v1_0.Hal.Component;
using MvAssistant.Mac.v1_0.Hal.Component.Camera;
using MvAssistant.Mac.v1_0.Hal.Component.Force6Axis;
using MvAssistant.Mac.v1_0.Hal.Component.Gripper;
using MvAssistant.Mac.v1_0.Hal.Component.Robot;
using System.Runtime.InteropServices;

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{
    [GuidAttribute("442DC2E7-1076-4B1F-8F73-7B865ED08771")]
    public interface IMacHalBoxTransfer : IMacHalAssembly
    {
        IHalCamera Camera_BoxSlot_Direction { get; set; }
        IHalLight CameraCircleLight { get; set; }
        IHalForce6Axis Force6Axis { get; set; }
        IHalGripper Gripper { get; set; }
        IHalLaser Laser_BoxSlot_Z { get; set; }
        IHalRobot Robot { get; set; }
        IHalRobotSkin RobotSkin { get; set; }



    }
}
