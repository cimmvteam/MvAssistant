using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaskAutoCleaner.v1_0.StateMachineBeta
{
    public class MacStateExitEventArgs : EventArgs
    {
         public MacTransition Transition { get; private set; }
         public MacStateExitEventArgs()
         {
             Transition = null;
         }
        public MacStateExitEventArgs(MacTransition transition) :this()
        {
            Transition = transition;
        }
    }
}
