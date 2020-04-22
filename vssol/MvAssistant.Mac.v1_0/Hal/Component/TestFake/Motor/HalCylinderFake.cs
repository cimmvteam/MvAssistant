﻿using MaskAutoCleaner.Hal.Intf.Component.Motor;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.ImpFake.Component.Motor
{
    [GuidAttribute("35953732-4775-4418-9887-54C053ADFD0D")]
    public class HalCylinderFake : HalFakeBase, IHalCylinder
    {
        public bool TurnOn()
        {
            return true;
        }

        public bool TurnOff()
        {
            return true;
        }
    }
}
