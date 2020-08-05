using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineBeta
{
   public class StateGuardRtns
    {
        public MacStateEntryEventArgs NextStateEntryEventArgs { get; set; }
        public MacStateExitEventArgs ThisStateExitEventArgs { get; set; }
        public MacTransition Transition { get; set; }
    }
}
