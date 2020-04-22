using MaskAutoCleaner.Hal.Intf.Component.AirPressure;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.ImpFake.Component.AirPressure
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
