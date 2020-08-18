using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineBeta
{
    /// <summary>Base Class of TriggerMember & TriggerMemberAsync</summary>
    public abstract class TriggerMemberBase
    {
        public Action<object> Action { get; set; }
        public object ActionParameter { get; set; }
        public Action<MacState, Exception> ExceptionHandler { get; set; }
        public MacStateEntryEventArgs NextStateEntryEventArgs { get; set; }
        public MacStateExitEventArgs ThisStateExitEventArgs { get; set; }

    }
}
