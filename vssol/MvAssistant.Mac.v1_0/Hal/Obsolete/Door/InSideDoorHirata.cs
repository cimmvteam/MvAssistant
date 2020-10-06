using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvAssistant.Mac.v1_0.Hal.Component.Door;
using System.Runtime.InteropServices;

namespace MvAssistant.Mac.v1_0.Hal.Component.Door
{
    [GuidAttribute("0B36F57C-364D-45FD-B5E5-74C869B27A5E")]
    public class InSideDoorHirata : MacHalComponentBase, IHalDoor
    {


        public string CheckDoorStatus()
        {
            throw new NotImplementedException();
        }

        public bool OpenDoor()
        {
            throw new NotImplementedException();
        }

        public bool CloseDoor()
        {
            throw new NotImplementedException();
        }

        public string ID
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string DeviceConnStr
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
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
