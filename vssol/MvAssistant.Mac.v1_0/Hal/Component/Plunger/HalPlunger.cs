using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvAssistant.Mac.v1_0.Hal.Component;
using System.Runtime.InteropServices;

namespace MvAssistant.Mac.v1_0.Hal.Component.Plunger
{
    [GuidAttribute("6D928A86-C8D2-42AA-AC9B-2B52B5CFA6AE")]
    public class HalPlunger : HalComponentBase, IHalPlunger
    {
        private bool isPressed;

        //For Fake Assumptions
        public bool IsPressed
        {
            get { return isPressed; }
            set { isPressed = value; }
        }

        public bool HalIsPressed()
        {
            return isPressed;
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
