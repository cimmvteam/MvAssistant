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
        public Func<bool> Guard { get; set; }
        public StateMachineExceptionBase NotGuardException { get; set; }
    }
}
