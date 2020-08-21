using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineBeta
{
    [Obsolete]
    public class MacStateExitWithTransitionEventArgs: MacStateExitEventArgs
    {
        public MacTransition Transition { get; private set; }
        private MacStateExitWithTransitionEventArgs():base()
        {

        }
        public MacStateExitWithTransitionEventArgs(MacTransition transition) : this()
        {
            Transition = transition;
        }
    }
}
