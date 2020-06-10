using MaskAutoCleaner.StateMachine_v1_1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Machine
{
    /// <summary>
    /// Design Pattern - State Pattern
    /// 
    /// State Machine 基底
    /// </summary>
    public abstract class MacMachineSmBase : MacStateMachine
    {
     


        public MacState NewState(Enum name)
        {
            var state = new MacState(name.ToString());
            this.States[state.Name] = state;
            return state;
        }
        public MacTransition NewTransition(MacState from, MacState to, Enum name)
        {
            var transition = new MacTransition(name.ToString(), from, to);
            this.Transitions[transition.Name] = transition;
            return transition;
        }

    }
}
