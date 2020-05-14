using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaskCleaner.StateMachine
{
    public abstract class DeviceStateMachine : StateMachine, IStepAnalyzable
    {
        public event EventHandler OnStepFinished; 
        protected void DoOnStepFinished(string s)
        {
            if (OnStepFinished != null)
            {
                OnStepFinished(this, EventArgs.Empty);
            }
        }
    }
}
