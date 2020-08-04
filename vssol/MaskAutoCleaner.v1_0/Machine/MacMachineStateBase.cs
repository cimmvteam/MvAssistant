using MaskAutoCleaner.v1_0.StateMachineBeta;
using MvAssistant.Mac.v1_0.Hal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine
{
    /// <summary>ta
    /// Machine State Object Base
    /// Design Pattern - State Pattern
    /// </summary>
    public abstract class MacMachineStateBase : MacStateMachine
    {
        //不需實作IMvContextFlow, 因為只有初始化StateMachine, 沒有其它作業
        //不需實作IDisposable, 因為沒有



        protected MacHalAssemblyBase halAssembly;

        public virtual void Load()
        {
            this.LoadStateMachine();
        }

        public abstract void LoadStateMachine();

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
