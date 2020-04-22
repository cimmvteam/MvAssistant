using MvAssistant.Mac.v1_0.Hal.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.AutoSwitch
{
    [GuidAttribute("A1C4C775-2DBE-4280-B13D-593A8BD56E45")]
    public class HalAutoSwitch : HalComponentBase, IHalAutoSwitch
    {
        private bool autoSwitchResult = false;
        public bool AutoSwitchResult
        {
            get { return autoSwitchResult; }
            set { autoSwitchResult = value; }
        }


        public bool GetValue()
        {
            return autoSwitchResult;
        }

        public void HalZeroCalibration()
        {
            return;
        }

        public int HalConnect()
        {
            return 1;
        }

        public int HalClose()
        {
            return 1; 
        }

        public bool HalIsConnected()
        {
            return true; 
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
    }
}
