using MvAssistant.Mac.v1_0.Hal.Component.AirPressure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.AirPressure
{

    [GuidAttribute("6AF656AC-2333-4BF2-9D34-8847632EF160")]
    public class HalPressureCtrl : MacHalComponentBase, IHalPressureCtrl
    {     
        public double ReadPresureValue()
        {
            throw new NotImplementedException();
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

        float IHalPressureCtrl.ReadPresureValue()
        {
            throw new NotImplementedException();
        }

        public void SetPressureValue(double value)
        {
            return;
        }


        public void SetFlowTimeValue(int value)
        {
            return;
        }
    }
}
