using MaskAutoCleaner.Hal.Intf.Component.Force6Axis;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.ImpFake.Component.Force6Axis
{
    [GuidAttribute("1FDB963D-6828-4960-B0ED-4BC072E309E3")]
    public class HalForce6AxisFake : HalFakeBase, IHalForce6Axis
    {
        #region for TestScript
        private float laserValue;

        public float LaserValue
        {
            get { return laserValue; }
            set { laserValue = value; }
        }
        #endregion


        public event EventHandler<IHalForce6AxisEventArgs> evtDataReceive;
        void OnDataReceive(IHalForce6AxisEventArgs ea)
        {
            if (this.evtDataReceive == null) return;
            this.evtDataReceive(this, ea);
        }



        public HalForce6AxisVector GetVector() { return new HalForce6AxisVector(); }


        public void HalZeroCalibration()
        {
            throw new NotImplementedException();
        }


    }
}
