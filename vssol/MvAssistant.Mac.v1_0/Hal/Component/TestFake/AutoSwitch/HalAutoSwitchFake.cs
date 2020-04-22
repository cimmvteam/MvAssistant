using MaskAutoCleaner.Hal.Intf.Component;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.ImpFake.Component.AutoSwitch
{
    [GuidAttribute("C7FA7EB2-19AE-405B-83D3-ADA49773890C")]
    public class HalAutoSwitchFake : HalFakeBase, IHalAutoSwitch
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
    }
}
