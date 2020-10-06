using MvAssistant.Mac.v1_0.Hal.Component;
using MvAssistant.Mac.v1_0.Hal.Component.Gripper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MvAssistant.Mac.v1_0.Hal;

namespace MvAssistant.Mac.v1_0.Hal.Component.Gripper
{
    [GuidAttribute("B81CFCFA-8320-4111-9E19-03CB900C5E69")]
    public class HalPlcOmronCustom01 : IHalGripper
    {
        public int HalClose()
        {
            throw new NotImplementedException();
        }

        public int HalConnect()
        {
            throw new NotImplementedException();
        }

        public float HalGetPosition()
        {
            throw new NotImplementedException();
        }

        public bool HalIsCompleted()
        {
            throw new NotImplementedException();
        }

        public bool HalIsConnected()
        {
            throw new NotImplementedException();
        }

        public void HalMove(HalGripperCmd cmd)
        {
            throw new NotImplementedException();
        }

        public void HalStop()
        {
            throw new NotImplementedException();
        }

        public bool HalZeroReset()
        {
            throw new NotImplementedException();
        }

        int IHal.HalStop()
        {
            throw new NotImplementedException();
        }
    }
}
