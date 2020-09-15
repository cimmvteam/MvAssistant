using MaskAutoCleaner.v1_0.StateMachineBeta;
using MaskAutoCleaner.v1_0.StateMachineExceptions.CleanChStateMachineException;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.CleanCh
{
    public class MacMsCleanCh : MacMachineStateBase
    {
        private IMacHalCleanCh HalCleanCh { get { return this.halAssembly as IMacHalCleanCh; } }

        private MacState _currentState = null;

        public void ResetState()
        { this.States[EnumMacMsCleanChState.Start.ToString()].DoEntry(new MacStateEntryEventArgs(null)); }

        private void SetCurrentState(MacState state)
        { _currentState = state; }

        public MacState CurrentState { get { return _currentState; } }

        public MacMsCleanCh() { LoadStateMachine(); }

        public void StartStateMachine()
        {
            this.States[EnumMacMsCleanChState.Start.ToString()].DoEntry(new MacStateEntryEventArgs(null));
        }

        public void CleanPellicle()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMsCleanChTransition.ReceiveTriggerToCleanPellicle.ToString()];
            triggerMember = new TriggerMember
            {
                Guard = () =>
                {
                    return true;
                },
                Action = null,
                ActionParameter = null,
                ExceptionHandler = (thisState, ex) =>
                {   // TODO: do something
                    },
                NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                ThisStateExitEventArgs = new MacStateExitEventArgs(),
            };
            transition.SetTriggerMembers(triggerMember);
            Trigger(transition);
        }
        public void InspectPellicle()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMsCleanChTransition.ReceiveTriggerToInspectPellicle.ToString()];
            triggerMember = new TriggerMember
            {
                Guard = () =>
                {
                    return true;
                },
                Action = null,
                ActionParameter = null,
                ExceptionHandler = (thisState, ex) =>
                {   // TODO: do something
                },
                NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                ThisStateExitEventArgs = new MacStateExitEventArgs(),
            };
            transition.SetTriggerMembers(triggerMember);
            Trigger(transition);
        }

        public void CleanGlass()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMsCleanChTransition.ReceiveTriggerToCleanGlass.ToString()];
            triggerMember = new TriggerMember
            {
                Guard = () =>
                {
                    return true;
                },
                Action = null,
                ActionParameter = null,
                ExceptionHandler = (thisState, ex) =>
                {   // TODO: do something
                },
                NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                ThisStateExitEventArgs = new MacStateExitEventArgs(),
            };
            transition.SetTriggerMembers(triggerMember);
            Trigger(transition);
        }
        public void InspectGlass()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMsCleanChTransition.ReceiveTriggerToInspectGlass.ToString()];
            triggerMember = new TriggerMember
            {
                Guard = () =>
                {
                    return true;
                },
                Action = null,
                ActionParameter = null,
                ExceptionHandler = (thisState, ex) =>
                {   // TODO: do something
                },
                NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                ThisStateExitEventArgs = new MacStateExitEventArgs(),
            };
            transition.SetTriggerMembers(triggerMember);
            Trigger(transition);
        }

        public override void LoadStateMachine()
        {
            #region State
            MacState sStart = NewState(EnumMacMsCleanChState.Start);
            //MacState sInitial = NewState(EnumMacMsCleanChState.Initial);

            MacState sIdle = NewState(EnumMacMsCleanChState.Idle);
            MacState sCleaningPellicle = NewState(EnumMacMsCleanChState.CleaningMask);
            MacState sInspectingPellicle = NewState(EnumMacMsCleanChState.InspectingMask);
            MacState sCleaningGlass = NewState(EnumMacMsCleanChState.CleaningGlass);
            MacState sInspectingGlass = NewState(EnumMacMsCleanChState.InspectingGlass);
            #endregion State

            #region Transition
            MacTransition tStart_Idle = NewTransition(sStart, sIdle, EnumMacMsCleanChTransition.PowerON);
            MacTransition tIdle_NULL = NewTransition(sIdle, null, EnumMacMsCleanChTransition.StandbyAtIdle);

            MacTransition tIdle_CleaningPellicle = NewTransition(sIdle, sCleaningPellicle, EnumMacMsCleanChTransition.ReceiveTriggerToCleanPellicle);
            MacTransition tCleaningPellicle_Idle = NewTransition(sCleaningPellicle, sIdle, EnumMacMsCleanChTransition.ReturnToIdleAfterCleanPellicle);
            MacTransition tIdle_InspectingPellicle = NewTransition(sIdle, sInspectingPellicle, EnumMacMsCleanChTransition.ReceiveTriggerToInspectPellicle);
            MacTransition tInspectingPellicle_Idle = NewTransition(sInspectingPellicle, sIdle, EnumMacMsCleanChTransition.ReturnToIdleAfterInspectPellicle);
            MacTransition tIdle_CleaningGlass = NewTransition(sIdle, sCleaningGlass, EnumMacMsCleanChTransition.ReceiveTriggerToCleanGlass);
            MacTransition tCleaningGlass_Idle = NewTransition(sCleaningGlass, sIdle, EnumMacMsCleanChTransition.ReturnToIdleAfterCleanGlass);
            MacTransition tIdle_InspectingGlass = NewTransition(sIdle, sInspectingGlass, EnumMacMsCleanChTransition.ReceiveTriggerToInspectGlass);
            MacTransition tInspectingGlass_Idle = NewTransition(sInspectingGlass, sIdle, EnumMacMsCleanChTransition.ReturnToIdleAfterInspectGlass);
            #endregion Transition

            #region State Register OnEntry OnExit
            sStart.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);
                try
                {
                }
                catch (Exception ex)
                {
                    throw new CleanChException(ex.Message);
                }

                var transition = tStart_Idle;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sStart.OnExit += (sender, e) =>
            { };
            sIdle.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);
                try
                {
                }
                catch (Exception ex)
                {
                    throw new CleanChException(ex.Message);
                }

                var transition = tIdle_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sIdle.OnExit += (sender, e) =>
            { };

            sCleaningPellicle.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);
                try
                {
                    // TODO:清理的詳細動作
                }
                catch (Exception ex)
                {
                    throw new CleanChCleanFailException(ex.Message);
                }

                var transition = tCleaningPellicle_Idle;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sCleaningPellicle.OnExit += (sender, e) =>
            { };
            sInspectingPellicle.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);
                try
                {
                    // TODO:檢查的詳細動作
                }
                catch (Exception ex)
                {
                    throw new CleanChInspectFailException(ex.Message);
                }

                var transition = tInspectingPellicle_Idle;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sInspectingPellicle.OnExit += (sender, e) =>
            { };
            sCleaningGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);
                try
                {
                    // TODO:清理的詳細動作
                }
                catch (Exception ex)
                {
                    throw new CleanChCleanFailException(ex.Message);
                }

                var transition = tCleaningGlass_Idle;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sCleaningGlass.OnExit += (sender, e) =>
            { };
            sInspectingGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);
                try
                {
                    // TODO:檢查的詳細動作
                }
                catch (Exception ex)
                {
                    throw new CleanChInspectFailException(ex.Message);
                }

                var transition = tInspectingGlass_Idle;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sInspectingGlass.OnExit += (sender, e) =>
            { };
            #endregion State Register OnEntry OnExit
        }

        public class MacCleanChUnitStateTimeOutController
        {
            const int defTimeOutSec = 20;
            public bool IsTimeOut(DateTime startTime, int targetDiffSecs)
            {
                var thisTime = DateTime.Now;
                var diff = thisTime.Subtract(startTime).TotalSeconds;
                if (diff >= targetDiffSecs)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public bool IsTimeOut(DateTime startTime)
            {
                return IsTimeOut(startTime, defTimeOutSec);
            }
        }
    }
}
