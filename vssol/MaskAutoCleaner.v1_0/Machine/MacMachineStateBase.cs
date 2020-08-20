using MaskAutoCleaner.v1_0.Machine.StateExceptions;
using MaskAutoCleaner.v1_0.StateMachineBeta;
using MaskAutoCleaner.v1_0.StateMachineExceptions;
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
        /// <param name="guard">guard (Func delegate) </param>
        /// <param name="action">action(Action delegate)</param>
        /// <param name="actionParameter">action Parameter (Object)</param>
        /// <param name="exceptionHandler">Exception Handler(Action delegate)</param>
        [Obsolete]
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
                            var State = rtn.ThisState;
                            var nextState = rtn.NextState;
                            State.DoExit(rtn.ThisStateExitEventArgs);
                            if (nextState != null)
                            {
                                nextState.DoEntry(rtn.NextStateEntryEventArgs);
                            }
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

        /// <summary></summary>
        /// <param name="guard">guard (Func delegate)</param>
        /// <param name="action">action (Action delegate)</param>
        /// <param name="actionParameter">action parameter(object)</param>
        /// <param name="exceptionHndler">Exception Handler (Action delegate)</param>
        [Obsolete]
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
                    var state = guardRtns.ThisState;
                    var nextState = guardRtns.NextState;
                    var stateExitArgs = guardRtns.ThisStateExitEventArgs;
                    var nextStateEntryArgs = guardRtns.NextStateEntryEventArgs;
                    state.DoExit(stateExitArgs);
                    if (nextState != null)
                    {
                        nextState.DoEntry(nextStateEntryArgs);
                    }
                }

            }
            catch (Exception ex)
            {
                if (exceptionHndler != null)
                {
                    exceptionHndler.Invoke(ex);
                }
            }
        }

        public void Trigger(MacTransition transition)
        {
            TriggerMember triggerMember = (TriggerMember)transition.TriggerMembers;
            var thisState = transition.StateFrom;
            thisState.ClearException();
            var nextState = transition.StateTo;
            var hasDoExit = false;
            try
            {
              
                if (triggerMember.Guard())
                {
                    if (triggerMember.Action != null)
                    {
                        triggerMember.Action(triggerMember.ActionParameter);
                    }
                    
                    thisState.DoExit(triggerMember.ThisStateExitEventArgs);
                    hasDoExit = true;
                }
                else
                {
                    if(triggerMember.NotGuardException != null)
                    {
                        throw triggerMember.NotGuardException;
                    }
                }
               
            }
            catch (Exception ex)
            {
                if(triggerMember.ExceptionHandler !=null)
                {
                    thisState.SetException(ex);
                    triggerMember.ExceptionHandler.Invoke(thisState,ex);
                }
            }
            if(hasDoExit)
            {
                if (nextState != null)
                {
                    nextState.DoEntry(triggerMember.NextStateEntryEventArgs);
                }
            }
        }
        public void TriggerAsync(MacTransition transition)
        {
            TriggerMemberAsync triggerMemberAsync = (TriggerMemberAsync)transition.TriggerMembers;
            DateTime startTime = DateTime.Now;
            var thisState = transition.StateFrom;
            var nextState = transition.StateTo;
            thisState.ClearException();
            var hasDoExit = false;
            Action Trigger = () =>
            {
                try
                {
                    while (true)
                    {
                        if (triggerMemberAsync.Guard(startTime))
                        {
                            break;
                        }
                        Thread.Sleep(10);
                    }
                    if (triggerMemberAsync.Action != null)
                    {
                        triggerMemberAsync.Action(triggerMemberAsync.ActionParameter);
                    }
                    thisState.DoExit(triggerMemberAsync.ThisStateExitEventArgs);
                    hasDoExit = true;
                   

                }
                catch (Exception ex)
                {
                    thisState.SetException(ex);
                    if (triggerMemberAsync.ExceptionHandler != null)
                    {
                        thisState.SetException(ex);
                        //triggerMemberAsync.ExceptionHandler.Invoke(thisState,ex);
                    }

                }
                if (hasDoExit)
                {
                    if (nextState !=null)
                    {
                        nextState.DoEntry(triggerMemberAsync.NextStateEntryEventArgs);
                    }
                }
            };
            new Task(Trigger).Start();
        }

        public void TriggerAsync(IList<MacTransition> transitions)
        {
            DateTime startTime = DateTime.Now;
            //var thisState = default(MacState);
            var triggerMemberAsync = default(TriggerMemberAsync);
            foreach (var transition in transitions) { transition.StateFrom.ClearException(); }
            var hasDoExit = false;
            var curentTransition = default(MacTransition);
            Action Trigger = () =>
            {
                try
                {
                    while (true)
                    {
                        foreach (var transition in transitions)
                        {
                            curentTransition = transition;
                            triggerMemberAsync = (TriggerMemberAsync)transition.TriggerMembers;
                            if (triggerMemberAsync.Guard(startTime))
                            {
                                break;
                            }
                            curentTransition = default(MacTransition);
                        }
                        if (curentTransition != default(MacTransition))
                        { break; }
                        Thread.Sleep(10);
                    }
                    curentTransition.StateFrom.DoExit(curentTransition.TriggerMembers.ThisStateExitEventArgs);
                    hasDoExit = true;
                }
                catch (Exception ex)
                {
                    if (curentTransition.TriggerMembers.ExceptionHandler != null)
                    {
                        curentTransition.TriggerMembers.ExceptionHandler.Invoke(curentTransition.StateFrom, ex);
                    }
                }
                if (hasDoExit)
                {
                    if (curentTransition.StateTo != null)
                    {
                        curentTransition.StateTo.DoEntry(triggerMemberAsync.NextStateEntryEventArgs);
                    }
                }
            };
            new Task(Trigger).Start();
        }
    }
}
