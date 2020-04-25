using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvAssistant.Mac.v1_0.Hal.Component;
using System.Runtime.InteropServices;
using MvAssistant.Mac.v1_0.Hal.Component.Inclinometer;

namespace MvAssistant.Mac.v1_0.Hal.Component.Inclinometer
{
    [Guid("F0AEF882-8298-4DAE-9D7C-99AFEDEAF7F3")]
    public class InclinometerOmronPlc : MacHalPlcOmronBase, IHalInclinometer
    {

        #region Override HAL
        public override int HalClose()
        {
            return 0;
        }

        public override int HalConnect()
        {
            this.PlcSetup();
            return 0;
        }

        public override bool HalIsConnected() { return this.GetAngle() != null; }

        #endregion


        #region Override IHalInclinometer

        public object GetAngle() { return this.PlcGetValue(this.DevSettings["Variable"]); }

        public void HalZeroCalibration()
        {

        }

        #endregion
    }
}
