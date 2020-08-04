using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineBeta
{
   public class StateGuardRtns
    {
        public MacStateEntryEventArgs EntryEventArgs { get; set; }
        public MacStateExitEventArgs ExitEventArgs { get; set; }
        public MacTransition Transition { get; set; }
    }
}
