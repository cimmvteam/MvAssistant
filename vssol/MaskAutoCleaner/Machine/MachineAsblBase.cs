﻿using MaskAutoCleaner.Msg;
using MvAssistant.Mac.v1_0.Hal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Machine
{
    public abstract class MachineAsblBase : MachineBase
    {
        protected IMacHalAssembly halAssembly;
        protected MachineAsblSmBase smAssembly;



    }
}