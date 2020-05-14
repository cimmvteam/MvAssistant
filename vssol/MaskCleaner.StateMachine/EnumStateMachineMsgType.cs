using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaskCleaner.StateMachine
{
    public enum EnumStateMachineMsgType
    {
        GuardFail, 
        CommandSuccessful,
        RecipeInsertQueue,
        StateMachineException,
        NoTransitionName,
        NotCurrentStateTransition,
        StateMachineFatel,
        MachineRunningControl,
        StateEventError,
        PluralTriggerSameTime
    }
}
