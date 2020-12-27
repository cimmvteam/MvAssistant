using MvAssistant.Mac.v1_0.Hal.Component;
using MvAssistant.Mac.v1_0.Hal.Component.Inclinometer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.ComponentFake.Inclinometer
{
    [GuidAttribute("F0A4F1A7-E0B9-4573-AFB8-357FE41697BD")]
    public class InclinometerFake : HalFakeBase, IHalInclinometer
    {
        #region for testScript
        public class GradientData
        {
            private double row;
            public double Row
            {
                get { return row; }
                set { row = value; }
            }
            private double pitch;
            public double Pitch
            {
                get { return pitch; }
                set { pitch = value; }
            }
        }
        #endregion
        GradientData data;

        public InclinometerFake()
        {
            data = new GradientData();
        }

        public object GetAngle()
        {
            data.Pitch=0;
            data.Row=0;
            return data;
        }

        public void HalZeroCalibration()
        {
            throw new NotImplementedException();
        }
    }
}
