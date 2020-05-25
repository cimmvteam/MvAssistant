using MaskAutoCleaner.Machine;
using MaskAutoCleaner.Hal.Intf.Component.Gripper;
using MaskAutoCleaner.Hal.Intf.Component.Robot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Machine.MaskTransfer
{

    [Serializable]
    public class MaskTransferCmdParam : Msg.MsgDeviceCmd
    {

        
        public HalRobotMotion Motion;
        public HalGripperCmd GripperCmd;


    }

}
