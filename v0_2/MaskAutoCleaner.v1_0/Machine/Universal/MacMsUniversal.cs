using MaskAutoCleaner.v1_0.StateMachineBeta;
using MaskAutoCleaner.v1_0.StateMachineExceptions.UniversalStateMachineException;
using MvAssistant.v0_2.Mac.Hal.Assembly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Universal
{
    public class MacMsUniversal : MacMachineStateBase
    {
        private IMacHalUniversal HalUniversal { get { return this.halAssembly as IMacHalUniversal; } }

        public MacMsUniversal() { LoadStateMachine(); }
        public override void LoadStateMachine()
        {
            #region State
            MacState sStart = NewState(EnumMacUniversalState.Start);
            MacState sIdle = NewState(EnumMacUniversalState.Idle);
            #endregion State

            #region Transition
            MacTransition tStart_Idle = NewTransition(sStart, sIdle, EnumMacUniversalTransition.PowerON);
            MacTransition tIdle_NULL = NewTransition(sIdle, null, EnumMacUniversalTransition.ReceiveTriggerAtIdle);
            #endregion Transition

            #region State Register OnEntry OnExit
            sStart.OnEntry += (sender, e) =>
            {
                try
                {
                }
                catch (Exception ex)
                {
                    throw new UniversalException(ex.Message);
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
                try
                {
                }
                catch (Exception ex)
                {
                    throw new UniversalException(ex.Message);
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
            #endregion State Register OnEntry OnExit
        }


        #region Command

        public override void SystemBootup()
        {
            //throw new NotImplementedException();
        }

        #endregion

    }
}
