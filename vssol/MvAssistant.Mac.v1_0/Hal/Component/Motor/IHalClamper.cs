﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.Intf.Component.Motor
{
    [GuidAttribute("D6B8D055-BDB2-4925-8CAD-BFEDEB3A5C38")]
    public interface IHalClamper : IHalComponent
    {
        bool HalIsShrinked();
        void HalShrinked();
        void HalReleased();
    }
}
