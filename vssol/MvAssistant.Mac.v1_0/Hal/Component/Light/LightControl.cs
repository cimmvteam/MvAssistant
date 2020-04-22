using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvAssistant.Mac.v1_0.Hal.Component;
using System.Runtime.InteropServices;

namespace MvAssistant.Mac.v1_0.Hal.Component.Light
{
    [GuidAttribute("9D3F03C3-32CA-4992-B236-D3CACDE1235E")]
    public class LightControl : HalComponentBase, IHalLight
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

        public void HalZeroCalibration()
        {
            throw new NotImplementedException();
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
    }
}
