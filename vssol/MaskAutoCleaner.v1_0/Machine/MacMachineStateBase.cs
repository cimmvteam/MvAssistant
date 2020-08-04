using MaskAutoCleaner.v1_0.Machine.StateExceptions;
using MaskAutoCleaner.v1_0.StateMachineBeta;
using MvAssistant.Mac.v1_0.Hal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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


        /// <summary></summary>
        /// <param name="guard">guard</param>
        /// <param name="action">action</param>
        /// <param name="actionParameter">action Parameter</param>
        /// <param name="exceptionHandler">Exception Handler</param>
        public void TriggerAsync(Func<DateTime, StateGuardRtns> guard, Action<object> action,object actionParameter,Action<Exception> exceptionHandler)
        {
            Action trigger = () =>
            {
                try
                {
                    DateTime startTime = DateTime.Now;
                    while (true)
                    {
                        StateGuardRtns rtn = guard(startTime);
                        if (rtn != null)
                        {
                            if (action != null)
                            {
                                
                                action.Invoke(actionParameter);
                            }
                            var State = rtn.Transition.StateFrom;
                            var nextState = rtn.Transition.StateTo;
                            State.DoExit(rtn.ExitEventArgs);
                            nextState.DoEntry(rtn.EntryEventArgs);
                            break;
                        }
                        Thread.Sleep(10);
                    }
                }
               catch(Exception ex)
                {
                    if(exceptionHandler != null)
                    {
                        exceptionHandler.Invoke(ex);
                    }
                }
            };
            new Task(trigger).Start();

        }

        public void Trigger(Func<StateGuardRtns> guard, Action<object> action, object actionParameter, Action<Exception> exceptionHndler)
        {
            try
            {
                var guardRtns = guard();
                if (guardRtns != null)
                {
                    if (action != null)
                    {
                        action(actionParameter);
                    }
                    var state = guardRtns.Transition.StateFrom;
                    var nextState = guardRtns.Transition.StateTo;
                    var stateExitArgs = guardRtns.ExitEventArgs;
                    var nextStateEntryArgs = guardRtns.EntryEventArgs;
                    state.DoExit(stateExitArgs);
                    nextState.DoEntry(nextStateEntryArgs);
                }
            }
            catch(Exception ex)
            {
                if(exceptionHndler != null)
                {
                    exceptionHndler.Invoke(ex);
                }
            }
        }

       
    }
}
