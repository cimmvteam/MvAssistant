using MaskAutoCleaner.Hal.Intf.Component;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.ImpFake.Component.Plunger
{
    [GuidAttribute("084AC61C-8071-436E-8735-25EA6690F983")]
    public class HalPlungerFake : HalFakeBase, IHalPlunger
    {
        private bool isPressed = false;

        public int millSecond = 20;
        public bool IsPressed
        {
            get { return isPressed; }
            set {
                System.Threading.Thread.Sleep(millSecond);//模擬POD在LOAD PORT LOAD上去的時間
                isPressed = value;
            }
        }

        public bool HalIsPressed()
        {
            return isPressed;
        }
    }
}
