﻿using MaskAutoCleaner.Hal.Intf.Component;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.ImpFake.Component.ParticleCounter
{
    [GuidAttribute("CA5BA708-12CA-43AE-BC4C-17BFFB88348A")]
    public class HalParticleCounterFake : HalFakeBase, IHalParticleCounter
    {
        public float GetParticleValue()
        {
            return 100;
        }
    }
}
