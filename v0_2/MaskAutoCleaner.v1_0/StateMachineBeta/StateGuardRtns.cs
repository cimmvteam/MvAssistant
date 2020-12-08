using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineBeta
{
    [Obsolete]
    public class StateGuardRtns
    {
        public MacStateEntryEventArgs NextStateEntryEventArgs { get; set; }
        public MacStateExitEventArgs ThisStateExitEventArgs { get; set; }
        public MacTransition Transition { get; set; }
        public MacState ThisState { get { return Transition.StateFrom; } }
        public MacState NextState { get { return Transition.StateTo; } }
    }
}
