using MaskAutoCleaner.v1_0.StateMachineExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineBeta
{
    public class MacTransition
    {
        public string Name;
        public MacTransition() { }
        public MacTransition(string name, MacState from, MacState to)
        {
            this.Name = name;
            this.StateFrom = from;
            this.StateTo = to;
        }

        public MacState StateFrom { get; protected set; }
        public MacState StateTo { get; protected set; }

        public TriggerMemberBase TriggerMembers{ get; private set; }
        public void SetTriggerMembers(TriggerMemberBase triggerMembers)
        {
            TriggerMembers = triggerMembers;
        }
    }

    /// <summary>Base Class of TriggerMember & TriggerMemberAsync</summary>
    public abstract class TriggerMemberBase
    {
          public Action<object> Action { get; set; }
          public object ActionParameter { get; set; }
          public Action<MacState,Exception> ExceptionHandler { get; set; }
          public MacStateEntryEventArgs NextStateEntryEventArgs { get;  set; }
          public MacStateExitEventArgs ThisStateExitEventArgs { get;  set; }   
        
    }

    /// <summary>Transition Member for Trigger </summary>
    public class TriggerMember : TriggerMemberBase
    {
           public Func<bool> Guard { get; set; }
           public StateMachineExceptionBase NotGuardException { get; set; }
    }
    /// <summary>Transition Member for TriggerAsync</summary>
    public class TriggerMemberAsync : TriggerMemberBase
    {
        public Func<DateTime, bool> Guard { get; set; }
    }    
}
