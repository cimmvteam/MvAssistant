using MvAssistant.Mac.v1_0.Hal.Component.AirPressure;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.ComponentFake.AirPressure
{
    [GuidAttribute("B9262D52-AED0-4657-865C-BB9221B173F5")]
    public class HalPressureCtrlFake : HalFakeBase, IHalPressureCtrl
    {
        #region for TestScript
        private float pressureValue;

        public float PressureValue
        {
            get { return pressureValue; }
            set { pressureValue = value; }
        }
        #endregion
        
        public float ReadPresureValue()
        {
            return pressureValue;
        }

        public void HalZeroCalibration()
        {
            return;
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
