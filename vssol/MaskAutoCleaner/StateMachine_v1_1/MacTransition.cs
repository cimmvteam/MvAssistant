﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.StateMachine_v1_1
{
    public class MacTransition
    {
        public string Name;
        public MacTransition() { }
        public MacTransition(string name, MacState from, MacState to)
        {
            this.Name = name;
            this.StateFrom = from;
            this.StateTo = to;
        }

        public MacState StateFrom { get; protected set; }
        public MacState StateTo { get; protected set; }

    }
}