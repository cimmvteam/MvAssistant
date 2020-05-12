using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaskCleaner.StateMachine
{
    public interface IStepAnalyzable
    {
        event EventHandler OnStepFinished;
        //void DoOnStepFinished(string s);
    }
}
