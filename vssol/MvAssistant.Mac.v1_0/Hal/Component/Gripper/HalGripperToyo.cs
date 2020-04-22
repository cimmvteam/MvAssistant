using MvAssistant.Mac.v1_0.Hal.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MvAssistant.Mac.v1_0.Hal.Component.Gripper
{
    [GuidAttribute("1C6D3549-81D5-4082-AA85-33EAEFD50FA9")]
    public class HalGripperToyo : HalComponentBase, IHalComponent
    {

        public void HalZeroCalibration()
        {
            throw new NotImplementedException();
        }

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
    }
}
