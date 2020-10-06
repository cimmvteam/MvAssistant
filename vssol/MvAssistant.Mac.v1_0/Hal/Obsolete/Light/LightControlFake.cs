using MvAssistant.Mac.v1_0.Hal.Component;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.ComponentFake.Light
{
    [GuidAttribute("0F2B229D-E234-4F5F-BFB2-C9404590A9C1")]
    public class LightControlFake : HalFakeBase, IHalLight
    {
        private int lightValue = 0;
        public int LightValue
        {
            get { return lightValue; }
            set { lightValue = value; }
        }


        public void SetLightValue(int value)
        {
            lightValue = value;
        }

        public int GetLightValve()
        {
            return lightValue;
        }
    }
}
