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
        /// <summary>Trigger 的Action</summary>
        public Action<object> Action { get; set; }
        /// <summary>Action 的參數,不需參數則設為 null</summary>
        public object ActionParameter { get; set; }
        /// <summary>例外的處理函式</summary>
        public Action<MacState, Exception> ExceptionHandler { get; set; }
        /// <summary>Entry的參數</summary>
        public MacStateEntryEventArgs NextStateEntryEventArgs { get; set; }
        /// <summary>Exit 的參數</summary>
        public MacStateExitEventArgs ThisStateExitEventArgs { get; set; }
    }
}
