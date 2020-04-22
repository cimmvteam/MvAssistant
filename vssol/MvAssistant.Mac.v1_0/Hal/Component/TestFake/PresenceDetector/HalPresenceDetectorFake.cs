using MaskAutoCleaner.Hal.Intf.Component;
using MaskAutoCleaner.Hal.Intf.Component.PresenceDetector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.ImpFake.Component.PresenceDetector
{
    [GuidAttribute("A927284D-B2D5-4DC3-B078-ACD28F870096")]
    public class HalPresenceDetectorFake : HalFakeBase, IHalPresenceDetector
    {
        public int millSecond = 20;
        private bool isPresent =false;

        //For Fake Assumptions
        public bool IsPresent
        {
            get { return isPresent; }
            set
            {
                System.Threading.Thread.Sleep(millSecond);//模擬POD在LOAD PORT LOAD上去的時間
                isPresent = value;
            }
        }

        public bool HalIsPresent()
        {
            return isPresent;
        }

    }
}
