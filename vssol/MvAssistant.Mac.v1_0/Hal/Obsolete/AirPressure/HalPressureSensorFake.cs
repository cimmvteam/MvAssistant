using MvAssistant.Mac.v1_0.Hal.Component.AirPressure;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.ComponentFake.AirPressure
{
    [GuidAttribute("AE827661-FCA6-4AEA-8FEB-1742774F6CCD")]
    public class HalPressureSensorFake : HalFakeBase, IHalPressureSensor
    {
        #region for TestScript
        private float pressureValue;

        public float PressureValue
        {
            get { return pressureValue; }
            set { pressureValue = value; }
        }
        #endregion

        public void SetPressureValue(float value)
        {
            return;
        }

        public float GetPressureValue()
        {
            return pressureValue;
        }

        public void HalZeroCalibration()
        {
            return;
        }
    }
}
