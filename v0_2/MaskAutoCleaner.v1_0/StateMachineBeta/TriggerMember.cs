using MaskAutoCleaner.v1_0.StateMachineExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineBeta
{
    /// <summary>Transition Member for Trigger </summary>
    public class TriggerMember : TriggerMemberBase
    {
        /// <summary>Trigger 的 Guard</summary>
        public Func<bool> Guard { get; set; }
        /// <summary>如果 Guard 得到結果為 false, 等效的例外類別</summary>
        public StateMachineExceptionBase NotGuardException { get; set; }
    }
}
