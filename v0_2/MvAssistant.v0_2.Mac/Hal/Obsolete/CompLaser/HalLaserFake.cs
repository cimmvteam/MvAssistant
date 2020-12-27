using MvAssistant.Mac.v1_0.Hal.Component;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.ComponentFake.Laser
{
    [GuidAttribute("143F00A9-E8A1-46D3-AF4E-89589F4885C3")]
    public class HalLaserFake : HalFakeBase, IHalLaser
    {
        #region for TestScript
        private float laserValue = 999;

        public float LaserValue
        {
            get { return laserValue; }
            set { laserValue = value; }
        }
        #endregion

        public bool SetAddress(string varName)
        {
            return true;
        }

        public float Read()
        {
            return laserValue;
        }
    }
}
