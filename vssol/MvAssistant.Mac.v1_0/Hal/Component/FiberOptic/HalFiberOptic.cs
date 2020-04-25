using MvAssistant.Mac.v1_0.Hal.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.FiberOptic
{
    [GuidAttribute("2A01477C-03BA-46C4-994E-8C8F81575583")]
    public class HalFiberOptic : MacHalComponentBase, IHalFiberOptic
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

        public int HalConnect()
        {
            return 1;
        }

        public int HalClose()
        {
            return 1;
        }

        public bool HalIsConnected()
        {
            return true;
        }

        public string ID
        {
            get;
            set;
        }

        public string DeviceConnStr
        {
            get;
            set;
        }
    }
}
