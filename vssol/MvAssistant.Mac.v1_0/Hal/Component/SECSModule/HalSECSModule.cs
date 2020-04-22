using MaskAutoCleaner.Hal.Intf.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.Imp.Component.SECSModule
{

    [GuidAttribute("D94D1EB0-5AB1-4654-B0BE-BF202E85C295")]
    public class HalSECSModule :HalComponentBase, IHalSECSMODULE
    {
        bool ConnectTAP(string IPAddress, int port)
        {
            return true;
        }
        bool SendToTAP(string StreamFunction, object data)
        {
            return true;
        }
        void StartListenTAP()
        {

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

        bool IHalSECSMODULE.ConnectTAP(string IPAddress, int port)
        {
            throw new NotImplementedException();
        }

        bool IHalSECSMODULE.SendToTAP(string StreamFunction, object data)
        {
            throw new NotImplementedException();
        }

        void IHalSECSMODULE.StartListenTAP()
        {
            throw new NotImplementedException();
        }
    }
}
