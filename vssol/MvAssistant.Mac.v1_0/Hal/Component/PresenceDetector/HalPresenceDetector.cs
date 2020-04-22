using MaskAutoCleaner.Hal.Intf.Component;
using MaskAutoCleaner.Hal.Intf.Component.PresenceDetector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.Imp.Component.PresenceDetector
{
     [GuidAttribute("2560E105-5260-4BD7-B44A-C11E68FDA732")]
    public class HalPresenceDetector : HalComponentBase, IHalPresenceDetector
    {
        private bool isPresent;

        //For Fake Assumptions
        public bool IsPresent
        {
            get { return isPresent; }
            set { isPresent = value; }
        }

        public bool HalIsPresent()
        {
            return isPresent;
        }

        public string ID { get; set; }

        public string DeviceConnStr { get; set; }

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
