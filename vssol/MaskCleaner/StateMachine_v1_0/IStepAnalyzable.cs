﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaskCleaner.StateMachine_v1_0
{
    public interface IStepAnalyzable
    {
        event EventHandler OnStepFinished;
        //void DoOnStepFinished(string s);
    }
}