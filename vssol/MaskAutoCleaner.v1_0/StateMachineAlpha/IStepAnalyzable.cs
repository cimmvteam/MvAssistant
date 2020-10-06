using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaskAutoCleaner.v1_0.StateMachineAlpha
{
    public interface IStepAnalyzable
    {
        event EventHandler OnStepFinished;
        //void DoOnStepFinished(string s);
    }
}
