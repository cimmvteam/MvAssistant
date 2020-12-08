using MvAssistant.Mac.v1_0.Hal.Component;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.ComponentFake.Tactile
{
    [GuidAttribute("6FD734AD-7A88-4C92-A365-8142F941706C")]
    public class HalTactileFake : HalFakeBase, IHalTactile
    {
        #region for TestScript
        private float pressValue;

        public float PressValue
        {
            get { return pressValue; }
            set { pressValue = value; }
        }
        #endregion
        public float GetPressValue()
        {
            return 0;
        }


        public float GetCurrent()
        {
            return 0;
        }
    }
}
