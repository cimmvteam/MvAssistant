using MaskAutoCleaner.Hal.Intf.Component;
using MaskAutoCleaner.Hal.Intf.Component.Force6Axis;
using MaskAutoCleaner.Hal.Intf.Component.Gripper;
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
    [GuidAttribute("442DC2E7-1076-4B1F-8F73-7B865ED08771")]
    public interface IHalBoxTransfer : IHalAssembly
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
