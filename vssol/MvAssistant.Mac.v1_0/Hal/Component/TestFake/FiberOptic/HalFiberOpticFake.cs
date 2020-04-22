using MaskAutoCleaner.Hal.Intf.Component;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.ImpFake.Component.FiberOptic
{
    [GuidAttribute("71F10E5D-F0ED-45C4-B533-17B26FB5E4B8")]
    public class HalFiberOpticFake : HalFakeBase, IHalFiberOptic
    {
        private float fiberOpticValue;
        private bool boolStatus;

        public bool BoolStatus
        {
            get { return boolStatus; }
            set { boolStatus = value; }
        }

        public float FiberOpticValue
        {
            get { return fiberOpticValue; }
            set { fiberOpticValue = value; }
        }

        public float GetOpticalValue()
        {
            return FiberOpticValue;
        }

        public bool GetBooleanStatus()
        {
            return boolStatus;
        }

        public void HalZeroCalibration()
        {
            return;
        }
    }
}
