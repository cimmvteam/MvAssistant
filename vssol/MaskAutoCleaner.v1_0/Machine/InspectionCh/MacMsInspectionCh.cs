using MaskAutoCleaner.v1_0.StateMachineBeta;
using MaskAutoCleaner.v1_0.StateMachineExceptions.InspectionChStateMachineException;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.InspectionCh
{
    public class MacMsInspectionCh : MacMachineStateBase
    {
        private IMacHalInspectionCh HalInspectionCh { get { return this.halAssembly as IMacHalInspectionCh; } }

        private MacState _currentState = null;

        public void ResetState()
        { this.States[EnumMacMsInspectionChState.Start.ToString()].DoEntry(new MacStateEntryEventArgs(null)); }

        private void SetCurrentState(MacState state)
        { _currentState = state; }

        public MacState CurrentState { get { return _currentState; } }

        public MacMsInspectionCh() { LoadStateMachine(); }

        MacInspectionChUnitStateTimeOutController timeoutObj = new MacInspectionChUnitStateTimeOutController();

        public void Initial()
        {
            this.States[EnumMacMsInspectionChState.Start.ToString()].DoEntry(new MacStateEntryEventArgs(null));
        }

        public void WaitForInputPellicle()
        {

            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMsInspectionChTransition.ReceiveTriggerToWaitForInputPellicle.ToString()];
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

            transition = Transitions[EnumMacMsInspectionChTransition.ReceiveTriggerToInspectPellicle.ToString()];
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
        public void ReleasePellicle()
        {

            MacTransition transition = null;
            TriggerMember triggerMember = null;

            transition = Transitions[EnumMacMsInspectionChTransition.ReceiveTriggerToIdleAfterReleasePellicle.ToString()];
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

        public void WaitForInputGlass()
        {

            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMsInspectionChTransition.ReceiveTriggerToWaitForInputGlass.ToString()];
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
            
            transition = Transitions[EnumMacMsInspectionChTransition.ReceiveTriggerToInspectGlass.ToString()];
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
        public void ReleaseGlass()
        {

            MacTransition transition = null;
            TriggerMember triggerMember = null;

            transition = Transitions[EnumMacMsInspectionChTransition.ReceiveTriggerToIdleAfterReleaseGlass.ToString()];
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
            MacState sStart = NewState(EnumMacMsInspectionChState.Start);
            MacState sInitial = NewState(EnumMacMsInspectionChState.Initial);

            MacState sIdle = NewState(EnumMacMsInspectionChState.Idle);
            MacState sWaitingForInputPellicle = NewState(EnumMacMsInspectionChState.WaitingForInputMask);
            MacState sPellicleOnStage = NewState(EnumMacMsInspectionChState.MaskOnStage);
            MacState sDefensingPellicle = NewState(EnumMacMsInspectionChState.DefensingMask);
            MacState sInspectingPellicle = NewState(EnumMacMsInspectionChState.InspectingMask);
            MacState sPellicleOnStageInspected = NewState(EnumMacMsInspectionChState.MaskOnStageInspected);
            MacState sWaitingForReleasePellicle = NewState(EnumMacMsInspectionChState.WaitingForReleaseMask);

            MacState sWaitingForInputGlass = NewState(EnumMacMsInspectionChState.WaitingForInputGlass);
            MacState sGlassOnStage = NewState(EnumMacMsInspectionChState.GlassOnStage);
            MacState sDefensingGlass = NewState(EnumMacMsInspectionChState.DefensingGlass);
            MacState sInspectingGlass = NewState(EnumMacMsInspectionChState.InspectingGlass);
            MacState sGlassOnStageInspected = NewState(EnumMacMsInspectionChState.GlassOnStageInspected);
            MacState sWaitingForReleaseGlass = NewState(EnumMacMsInspectionChState.WaitingForReleaseGlass);
            #endregion State

            #region Transition
            MacTransition tStart_Initial = NewTransition(sStart, sInitial, EnumMacMsInspectionChTransition.PowerON);
            MacTransition tInitial_Idle = NewTransition(sStart, sIdle, EnumMacMsInspectionChTransition.Initial);
            MacTransition tIdle_NULL = NewTransition(sIdle, null, EnumMacMsInspectionChTransition.StandbyAtIdle);

            MacTransition tIdle_WaitingForInputPellicle = NewTransition(sIdle, sWaitingForInputPellicle, EnumMacMsInspectionChTransition.ReceiveTriggerToWaitForInputPellicle);
            MacTransition tWaitingForInputPellicle_NULL = NewTransition(sWaitingForInputPellicle, null, EnumMacMsInspectionChTransition.StandbyAtWaitForInputPellicle);
            MacTransition tWaitingForInputPellicle_PellicleOnStage = NewTransition(sWaitingForInputPellicle, sPellicleOnStage, EnumMacMsInspectionChTransition.ReceiveTriggerToInspectPellicle);
            MacTransition tPellicleOnStage_DefensingPellicle = NewTransition(sPellicleOnStage, sDefensingPellicle, EnumMacMsInspectionChTransition.DefensePellicle);
            MacTransition tDefensingPellicle_InspectingPellicle = NewTransition(sDefensingPellicle, sInspectingPellicle, EnumMacMsInspectionChTransition.InspectPellicle);
            MacTransition tInspectingPellicle_PellicleOnStageInspected = NewTransition(sInspectingPellicle, sPellicleOnStageInspected, EnumMacMsInspectionChTransition.StandbyAtStageWithPellicleInspected);
            MacTransition tPellicleOnStageInspected_WaitingForReleasePellicle = NewTransition(sPellicleOnStageInspected, sWaitingForReleasePellicle, EnumMacMsInspectionChTransition.WaitForReleasePellicle);
            MacTransition tWaitingForReleasePellicle_NULL = NewTransition(sWaitingForReleasePellicle, null, EnumMacMsInspectionChTransition.StandbyAtWaitForReleasePellicle);
            MacTransition tWaitingForReleasePellicle_Idle = NewTransition(sWaitingForReleasePellicle, sIdle, EnumMacMsInspectionChTransition.ReceiveTriggerToIdleAfterReleasePellicle);

            MacTransition tIdle_WaitingForInputGlass = NewTransition(sIdle, sWaitingForInputGlass, EnumMacMsInspectionChTransition.ReceiveTriggerToWaitForInputGlass);
            MacTransition tWaitingForInputGlass_NULL = NewTransition(sWaitingForInputGlass, null, EnumMacMsInspectionChTransition.StandbyAtWaitForInputGlass);
            MacTransition tWaitingForInputGlass_GlassOnStage = NewTransition(sWaitingForInputGlass, sGlassOnStage, EnumMacMsInspectionChTransition.ReceiveTriggerToInspectGlass);
            MacTransition tGlassOnStage_DefensingGlass = NewTransition(sGlassOnStage, sDefensingGlass, EnumMacMsInspectionChTransition.DefenseGlass);
            MacTransition tDefensingGlass_InspectingGlass = NewTransition(sDefensingGlass, sInspectingGlass, EnumMacMsInspectionChTransition.InspectGlass);
            MacTransition tInspectingGlass_GlassOnStageInspected = NewTransition(sInspectingGlass, sGlassOnStageInspected, EnumMacMsInspectionChTransition.StandbyAtStageWithGlassInspected);
            MacTransition tGlassOnStageInspected_WaitingForReleaseGlass = NewTransition(sGlassOnStageInspected, sWaitingForReleaseGlass, EnumMacMsInspectionChTransition.WaitForReleaseGlass);
            MacTransition tWaitingForReleaseGlass_NULL = NewTransition(sWaitingForReleaseGlass, null, EnumMacMsInspectionChTransition.StandbyAtWaitForReleaseGlass);
            MacTransition tWaitingForReleaseGlass_Idle = NewTransition(sWaitingForReleaseGlass, sIdle, EnumMacMsInspectionChTransition.ReceiveTriggerToIdleAfterReleaseGlass);
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
                    throw new InspectionChException(ex.Message);
                }

                var transition = tStart_Initial;
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
            sInitial.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);
                try
                {
                    HalInspectionCh.Initial();
                }
                catch (Exception ex)
                {
                    throw new InspectionChInitialFailException(ex.Message);
                }

                var transition = tInitial_Idle;
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
            sInitial.OnExit += (sender, e) =>
            { };
            sIdle.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);
                try
                {
                }
                catch (Exception ex)
                {
                    throw new InspectionChPLCExecuteFailException(ex.Message);
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


            sWaitingForInputPellicle.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);
                try
                {
                }
                catch (Exception ex)
                {
                    throw new InspectionChException(ex.Message);
                }

                var transition = tWaitingForInputPellicle_NULL;
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
            sWaitingForInputPellicle.OnExit += (sender, e) =>
            { };
            sPellicleOnStage.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);
                try
                {
                }
                catch (Exception ex)
                {
                    throw new InspectionChException(ex.Message);
                }

                var transition = tPellicleOnStage_DefensingPellicle;
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
            sPellicleOnStage.OnExit += (sender, e) =>
            { };
            sDefensingPellicle.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);
                try
                {
                    //上方相機
                    HalInspectionCh.Camera_TopDfs_CapToSave("D:/Image/IC/TopDfs", "bmp");
                    Thread.Sleep(500);

                    //側邊相機
                    for (int i = 0; i < 360; i += 90)
                    {
                        HalInspectionCh.WPosition(i);
                        HalInspectionCh.Camera_SideDfs_CapToSave("D:/Image/IC/SideDfs", "bmp");
                        Thread.Sleep(500);
                    }
                }
                catch (Exception ex)
                {
                    throw new InspectionChDefenseFailException(ex.Message);
                }

                var transition = tDefensingPellicle_InspectingPellicle;
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
            sDefensingPellicle.OnExit += (sender, e) =>
            { };
            sInspectingPellicle.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);
                try
                {
                    HalInspectionCh.ZPosition(-29.6);
                    //上方相機
                    for (int i = 158; i <= 296; i += 23)
                    {
                        for (int j = 123; j <= 261; j += 23)
                        {
                            HalInspectionCh.XYPosition(i, j);
                            HalInspectionCh.Camera_TopInsp_CapToSave("D:/Image/IC/TopInsp", "bmp");
                            Thread.Sleep(500);
                        }
                    }

                    //側邊相機
                    HalInspectionCh.XYPosition(246, 208);
                    for (int i = 0; i < 360; i += 90)
                    {
                        HalInspectionCh.WPosition(i);
                        HalInspectionCh.Camera_SideInsp_CapToSave("D:/Image/IC/SideInsp", "bmp");
                        Thread.Sleep(500);
                    }
                }
                catch (Exception ex)
                {
                    throw new InspectionChInspectFailException(ex.Message);
                }

                var transition = tInspectingPellicle_PellicleOnStageInspected;
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
            sPellicleOnStageInspected.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);
                try
                {
                    HalInspectionCh.XYPosition(0, 0);
                    HalInspectionCh.ZPosition(0);
                }
                catch (Exception ex)
                {
                    throw new InspectionChPLCExecuteFailException(ex.Message);
                }

                var transition = tPellicleOnStageInspected_WaitingForReleasePellicle;
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
            sPellicleOnStageInspected.OnExit += (sender, e) =>
            { };
            sWaitingForReleasePellicle.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);
                try
                {
                }
                catch (Exception ex)
                {
                    throw new InspectionChException(ex.Message);
                }

                var transition = tWaitingForReleasePellicle_NULL;
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
            sWaitingForReleasePellicle.OnExit += (sender, e) =>
            { };


            sWaitingForInputGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);
                try
                {
                }
                catch (Exception ex)
                {
                    throw new InspectionChException(ex.Message);
                }

                var transition = tWaitingForInputGlass_NULL;
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
            sWaitingForInputGlass.OnExit += (sender, e) =>
            { };
            sGlassOnStage.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);
                try
                {
                }
                catch (Exception ex)
                {
                    throw new InspectionChException(ex.Message);
                }

                var transition = tGlassOnStage_DefensingGlass;
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
            sGlassOnStage.OnExit += (sender, e) =>
            { };
            sDefensingGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);
                try
                {
                    //上方相機
                    HalInspectionCh.Camera_TopDfs_CapToSave("D:/Image/IC/TopDfs", "bmp");
                    Thread.Sleep(500);

                    //側邊相機
                    for (int i = 0; i < 360; i += 90)
                    {
                        HalInspectionCh.WPosition(i);
                        HalInspectionCh.Camera_SideDfs_CapToSave("D:/Image/IC/SideDfs", "bmp");
                        Thread.Sleep(500);
                    }
                }
                catch (Exception ex)
                {
                    throw new InspectionChDefenseFailException(ex.Message);
                }

                var transition = tDefensingGlass_InspectingGlass;
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
            sDefensingGlass.OnExit += (sender, e) =>
            { };
            sInspectingGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);
                try
                {
                    HalInspectionCh.ZPosition(-29.6);
                    //上方相機
                    for (int i = 158; i <= 296; i += 23)
                    {
                        for (int j = 123; j <= 261; j += 23)
                        {
                            HalInspectionCh.XYPosition(i, j);
                            HalInspectionCh.Camera_TopInsp_CapToSave("D:/Image/IC/TopInsp", "bmp");
                            Thread.Sleep(500);
                        }
                    }

                    //側邊相機
                    HalInspectionCh.XYPosition(246, 208);
                    for (int i = 0; i < 360; i += 90)
                    {
                        HalInspectionCh.WPosition(i);
                        HalInspectionCh.Camera_SideInsp_CapToSave("D:/Image/IC/SideInsp", "bmp");
                        Thread.Sleep(500);
                    }
                }
                catch (Exception ex)
                {
                    throw new InspectionChInspectFailException(ex.Message);
                }

                var transition = tInspectingGlass_GlassOnStageInspected;
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
            sGlassOnStageInspected.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);
                try
                {
                    HalInspectionCh.XYPosition(0, 0);
                    HalInspectionCh.ZPosition(0);
                }
                catch (Exception ex)
                {
                    throw new InspectionChPLCExecuteFailException(ex.Message);
                }

                var transition = tGlassOnStageInspected_WaitingForReleaseGlass;
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
            sGlassOnStageInspected.OnExit += (sender, e) =>
            { };
            sWaitingForReleaseGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);
                try
                {
                }
                catch (Exception ex)
                {
                    throw new InspectionChException(ex.Message);
                }

                var transition = tWaitingForReleaseGlass_NULL;
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
            sWaitingForReleaseGlass.OnExit += (sender, e) =>
            { };
            #endregion State Register OnEntry OnExit
        }

        public class MacInspectionChUnitStateTimeOutController
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
