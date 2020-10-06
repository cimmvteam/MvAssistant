using MvAssistant.Mac.v1_0.Hal.Component.AirPressure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.ComponentFake.AirPressure
{
    [GuidAttribute("5AEA0ECC-5B89-45C0-88E5-59C678BA3532")]
    public class HalDiffPressureFake : HalFakeBase, IHalDiffPressure
    {
        #region for TestScript
        private float diffPressureValue;

        public float DiffPressureValue
        {
            get { return diffPressureValue; }
            set { diffPressureValue = value; }
        }
        #endregion
        
        public float GetPressureValue()
        {
            return diffPressureValue;
        }

        public void HalZeroCalibration()
        {
            return;
        }
    }
}
