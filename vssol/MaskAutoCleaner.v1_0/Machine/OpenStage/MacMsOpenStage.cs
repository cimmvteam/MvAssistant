using MaskAutoCleaner.v1_0.StateMachineBeta;
using MaskAutoCleaner.v1_0.StateMachineExceptions.OpenStageStateMachineException;
using MaskAutoCleaner.v1_0.StateMachineExceptions.UniversalStateMachineException;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.OpenStage
{
    public class MacMsOpenStage : MacMachineStateBase
    {
        public EnumMacMsOpenStageState CurrentWorkState { get; set; }

        private IMacHalOpenStage HalOpenStage { get { return this.halAssembly as IMacHalOpenStage; } }
        private IMacHalUniversal HalUniversal { get { return this.halAssembly as IMacHalUniversal; } }

        private MacState _currentState = null;

        public void ResetState()
        { this.States[EnumMacMsOpenStageState.Start.ToString()].DoEntry(new MacStateEntryEventArgs(null)); }

        private void SetCurrentState(MacState state)
        { _currentState = state; }

        public MacState CurrentState { get { return _currentState; } }

        public MacMsOpenStage() { LoadStateMachine(); }

        MacOpenStageUnitStateTimeOutController timeoutObj = new MacOpenStageUnitStateTimeOutController();

        public void SystemBootup()
        {
            this.States[EnumMacMsOpenStageState.Start.ToString()].DoEntry(new MacStateEntryEventArgs(null));
        }
        public void Initial()
        {
            this.States[EnumMacMsOpenStageState.Initial.ToString()].DoEntry(new MacStateEntryEventArgs(null));
        }
        public void InputBox()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMsOpenStageTransition.ReceiveTriggerToWaitForInputBox.ToString()];
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
        public void CalibrationClosedBox()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMsOpenStageTransition.ReceiveTriggerToCalibrationBox.ToString()];
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
        public void OpenBox()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMsOpenStageTransition.ReceiveTriggerToOpenBox.ToString()];
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
        public void CloseBoxWithMask()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMsOpenStageTransition.ReceiveTriggerToCloseBoxWithMask.ToString()];
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
        public void ReturnCloseBoxWithMask()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMsOpenStageTransition.ReceiveTriggerToReturnCloseBoxWithMask.ToString()];
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
        public void ReleaseBoxWithMask()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMsOpenStageTransition.ReceiveTriggerToReleaseBoxWithMask.ToString()];
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
        public void ReturnToIdleAfterReleaseBoxWithMask()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMsOpenStageTransition.ReceiveTriggerToIdleAfterReleaseBoxWithMask.ToString()];
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


        public void InputBoxWithMask()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMsOpenStageTransition.ReceiveTriggerToWaitForInputBoxWithMask.ToString()];
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
        public void CalibrationClosedBoxWithMask()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMsOpenStageTransition.ReceiveTriggerToCalibrationBoxWithMask.ToString()];
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
        public void OpenBoxWithMask()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMsOpenStageTransition.ReceiveTriggerToOpenBoxWithMask.ToString()];
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
        public void CloseBox()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMsOpenStageTransition.ReceiveTriggerToCloseBox.ToString()];
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
        public void ReturnCloseBox()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMsOpenStageTransition.ReceiveTriggerToReturnCloseBox.ToString()];
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
        public void ReleaseBox()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMsOpenStageTransition.ReceiveTriggerToReleaseBox.ToString()];
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
        public void ReturnToIdleAfterReleaseBox()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMsOpenStageTransition.ReceiveTriggerToIdleAfterReleaseBox.ToString()];
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
            MacState sStart = NewState(EnumMacMsOpenStageState.Start);
            MacState sInitial = NewState(EnumMacMsOpenStageState.Initial);

            MacState sIdle = NewState(EnumMacMsOpenStageState.Idle);
            MacState sWaitingForInputBox = NewState(EnumMacMsOpenStageState.WaitingForInputBox);
            MacState sClosedBox = NewState(EnumMacMsOpenStageState.ClosedBox);
            MacState sWaitingForUnlock = NewState(EnumMacMsOpenStageState.WaitingForUnlock);
            MacState sOpeningBox = NewState(EnumMacMsOpenStageState.OpeningBox);
            MacState sOpenedBox = NewState(EnumMacMsOpenStageState.OpenedBox);
            MacState sWaitingForInputMask = NewState(EnumMacMsOpenStageState.WaitingForInputMask);
            MacState sOpenedBoxWithMaskForClose = NewState(EnumMacMsOpenStageState.OpenedBoxWithMaskForClose);
            MacState sClosingBoxWithMask = NewState(EnumMacMsOpenStageState.ClosingBoxWithMask);
            MacState sWaitingForLockWithMask = NewState(EnumMacMsOpenStageState.WaitingForLockWithMask);
            MacState sClosedBoxWithMaskForRelease = NewState(EnumMacMsOpenStageState.ClosedBoxWithMaskForRelease);
            MacState sWaitingForReleaseBoxWithMask = NewState(EnumMacMsOpenStageState.WaitingForReleaseBoxWithMask);

            MacState sWaitingForInputBoxWithMask = NewState(EnumMacMsOpenStageState.WaitingForInputBoxWithMask);
            MacState sClosedBoxWithMask = NewState(EnumMacMsOpenStageState.ClosedBoxWithMask);
            MacState sWaitingForUnlockWithMask = NewState(EnumMacMsOpenStageState.WaitingForUnlickWithMask);
            MacState sOpeningBoxWithMask = NewState(EnumMacMsOpenStageState.OpeningBoxWithMask);
            MacState sOpenedBoxWithMask = NewState(EnumMacMsOpenStageState.OpenedBoxWithMask);
            MacState sWaitingForReleaseMask = NewState(EnumMacMsOpenStageState.WaitingForReleaseMask);
            MacState sOpenedBoxForClose = NewState(EnumMacMsOpenStageState.OpenedBoxForClose);
            MacState sClosingBox = NewState(EnumMacMsOpenStageState.ClosingBox);
            MacState sWaitingForLock = NewState(EnumMacMsOpenStageState.WaitingForLock);
            MacState sClosedBoxForRelease = NewState(EnumMacMsOpenStageState.ClosedBoxForRelease);
            MacState sWaitingForReleaseBox = NewState(EnumMacMsOpenStageState.WaitingForReleaseBox);

            #endregion State

            #region Transition
            MacTransition tStart_Initial = NewTransition(sStart, sInitial, EnumMacMsOpenStageTransition.PowerON);
            MacTransition tInitial_Idle = NewTransition(sStart, sIdle, EnumMacMsOpenStageTransition.Initial);
            MacTransition tIdle_NULL = NewTransition(sIdle, null, EnumMacMsOpenStageTransition.StandbyAtIdle);

            MacTransition tIdle_WaitingForInputBox = NewTransition(sIdle, sWaitingForInputBox, EnumMacMsOpenStageTransition.ReceiveTriggerToWaitForInputBox);
            MacTransition tWaitingForInputBox_NULL = NewTransition(sWaitingForInputBox, null, EnumMacMsOpenStageTransition.StandbyAtWaitForInputBox);
            MacTransition tWaitingForInputBox_ClosedBox = NewTransition(sWaitingForInputBox, sClosedBox, EnumMacMsOpenStageTransition.ReceiveTriggerToCalibrationBox);
            MacTransition tClosedBox_WaitingForUnlock = NewTransition(sClosedBox, sWaitingForUnlock, EnumMacMsOpenStageTransition.WaitForUnlock);
            MacTransition tWaitingForUnlock_NULL = NewTransition(sWaitingForUnlock, null, EnumMacMsOpenStageTransition.StandbyAtWaitForUnlock);
            MacTransition tWaitingForUnlock_OpeningBox = NewTransition(sWaitingForUnlock, sOpeningBox, EnumMacMsOpenStageTransition.ReceiveTriggerToOpenBox);
            MacTransition tOpeningBox_OpenedBox = NewTransition(sOpeningBox, sOpenedBox, EnumMacMsOpenStageTransition.OpenedBox);
            MacTransition tOpenedBox_WaitingForInputMask = NewTransition(sOpenedBox, sWaitingForInputMask, EnumMacMsOpenStageTransition.WaitForInputMask);
            MacTransition tWaitingForInputMask_NULL = NewTransition(sWaitingForInputMask, null, EnumMacMsOpenStageTransition.StandbyAtWaitForInputMask);
            MacTransition tWaitingForInputMask_OpenedBoxWithMaskForClose = NewTransition(sWaitingForInputMask, sOpenedBoxWithMaskForClose, EnumMacMsOpenStageTransition.ReceiveTriggerToCloseBoxWithMask);
            MacTransition tWaitingForInputMask_OpenedBoxForClose = NewTransition(sWaitingForInputMask, sOpenedBoxForClose, EnumMacMsOpenStageTransition.ReceiveTriggerToReturnCloseBox);
            MacTransition tOpenedBoxWithMaskForClose_ClosingBoxWithMask = NewTransition(sOpenedBoxWithMaskForClose, sClosingBoxWithMask, EnumMacMsOpenStageTransition.CloseBoxWithMask);
            MacTransition tClosingBoxWithMask_WaitingForLockWithMask = NewTransition(sClosingBoxWithMask, sWaitingForLockWithMask, EnumMacMsOpenStageTransition.WaitForLockWithMask);
            MacTransition tWaitingForLockWithMask_NULL = NewTransition(sWaitingForLockWithMask, null, EnumMacMsOpenStageTransition.StandbyAtWaitForLockWithMask);
            MacTransition tWaitingForLockWithMask_ClosedBoxWithMaskForRelease = NewTransition(sWaitingForLockWithMask, sClosedBoxWithMaskForRelease, EnumMacMsOpenStageTransition.ReceiveTriggerToReleaseBoxWithMask);
            MacTransition tClosedBoxWithMaskForRelease_WaitingForReleaseBoxWithMask = NewTransition(sClosedBoxWithMaskForRelease, sWaitingForReleaseBoxWithMask, EnumMacMsOpenStageTransition.WaitForReleaseBoxWithMask);
            MacTransition tWaitingForReleaseBoxWithMask_NULL = NewTransition(sWaitingForReleaseBoxWithMask, null, EnumMacMsOpenStageTransition.StandbyAtWaitForReleaseBoxWithMask);
            MacTransition tWaitingForReleaseBoxWithMask_Idle = NewTransition(sWaitingForReleaseBoxWithMask, sIdle, EnumMacMsOpenStageTransition.ReceiveTriggerToIdleAfterReleaseBoxWithMask);



            MacTransition tIdle_WaitingForInputBoxWithMask = NewTransition(sIdle, sWaitingForInputBoxWithMask, EnumMacMsOpenStageTransition.ReceiveTriggerToWaitForInputBoxWithMask);
            MacTransition tWaitingForInputBoxWithMask_NULL = NewTransition(sWaitingForInputBoxWithMask, null, EnumMacMsOpenStageTransition.StandbyAtWaitForInputBoxWithMask);
            MacTransition tWaitingForInputBoxWithMask_ClosedBoxWithMask = NewTransition(sWaitingForInputBoxWithMask, sClosedBoxWithMask, EnumMacMsOpenStageTransition.ReceiveTriggerToCalibrationBoxWithMask);
            MacTransition tClosedBoxWithMask_WaitingForUnlockWithMask = NewTransition(sClosedBoxWithMask, sWaitingForUnlockWithMask, EnumMacMsOpenStageTransition.WaitForUnlockWithMask);
            MacTransition tWaitingForUnlockWithMask_NULL = NewTransition(sWaitingForUnlockWithMask, null, EnumMacMsOpenStageTransition.StandbyAtWaitForUnlockWithMask);
            MacTransition tWaitingForUnlockWithMask_OpeningBoxWithMask = NewTransition(sWaitingForUnlockWithMask, sOpeningBoxWithMask, EnumMacMsOpenStageTransition.ReceiveTriggerToOpenBoxWithMask);
            MacTransition tOpeningBoxWithMask_OpenedBoxWithMask = NewTransition(sOpeningBoxWithMask, sOpenedBoxWithMask, EnumMacMsOpenStageTransition.OpenedBoxWithMask);
            MacTransition tOpenedBoxWithMask_WaitingForReleaseMask = NewTransition(sOpenedBoxWithMask, sWaitingForReleaseMask, EnumMacMsOpenStageTransition.WaitForReleaseMask);
            MacTransition tWaitingForReleaseMask_NULL = NewTransition(sWaitingForReleaseMask, null, EnumMacMsOpenStageTransition.StandbyAtWaitForReleaseMask);
            MacTransition tWaitingForReleaseMask_OpenedBoxForClose = NewTransition(sWaitingForReleaseMask, sOpenedBoxForClose, EnumMacMsOpenStageTransition.ReceiveTriggerToCloseBox);
            MacTransition tWaitingForReleaseMask_OpenedBoxWithMaskForClose = NewTransition(sWaitingForReleaseMask, sOpenedBoxWithMaskForClose, EnumMacMsOpenStageTransition.ReceiveTriggerToReturnCloseBoxWithMask);
            MacTransition tOpenedBoxForClose_ClosingBox = NewTransition(sOpenedBoxForClose, sClosingBox, EnumMacMsOpenStageTransition.CloseBox);
            MacTransition tClosingBox_WaitingForLock = NewTransition(sClosingBox, sWaitingForLock, EnumMacMsOpenStageTransition.WaitForLock);
            MacTransition tWaitingForLock_NULL = NewTransition(sWaitingForLock, null, EnumMacMsOpenStageTransition.StandbyAtWaitForLock);
            MacTransition tWaitingForLock_ClosedBoxForRelease = NewTransition(sWaitingForLock, sClosedBoxForRelease, EnumMacMsOpenStageTransition.ReceiveTriggerToReleaseBox);
            MacTransition tClosedBoxForRelease_WaitingForReleaseBox = NewTransition(sClosedBoxForRelease, sWaitingForReleaseBox, EnumMacMsOpenStageTransition.WaitForReleaseBox);
            MacTransition tWaitingForReleaseBox_NULL = NewTransition(sWaitingForReleaseBox, null, EnumMacMsOpenStageTransition.StandbyAtWaitForReleaseBox);
            MacTransition tWaitingForReleaseBox_Idle = NewTransition(sWaitingForReleaseBox, sIdle, EnumMacMsOpenStageTransition.ReceiveTriggerToIdleAfterReleaseBox);
            #endregion Transition

            #region State Register OnEntry OnExit
            sStart.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                }
                catch (Exception ex)
                {
                    throw new OpenStageException(ex.Message);
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

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalOpenStage.Initial();
                }
                catch (Exception ex)
                {
                    throw new OpenStagePLCExecuteFailException(ex.Message);
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

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (HalOpenStage.ReadWeightOnStage() > 285)
                        throw new OpenStageGuardException("The stage is not cleared !");
                }
                catch (OpenStageGuardException ex)
                { throw ex; }
                catch (Exception ex)
                {
                    throw new OpenStagePLCExecuteFailException(ex.Message);
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
            sWaitingForInputBox.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                }
                catch (Exception ex)
                {
                    throw new OpenStageException(ex.Message);
                }

                var transition = tWaitingForInputBox_NULL;
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
            sWaitingForInputBox.OnExit += (sender, e) =>
            { };
            sClosedBox.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var BoxType = (uint)e.Parameter;
                    CheckBoxWeight(BoxType, false);
                    if (HalOpenStage.ReadCoverSensor().Item2 == false)
                        throw new OpenStageGuardException("Box status was not closed");
                    HalOpenStage.SetBoxType(BoxType);
                    HalOpenStage.SortClamp();
                    Thread.Sleep(1000);
                    HalOpenStage.SortUnclamp();
                    HalOpenStage.SortClamp();
                    Thread.Sleep(1000);
                    HalOpenStage.Vacuum(true);
                    HalOpenStage.SortUnclamp();
                    HalOpenStage.Lock();
                }
                catch (OpenStageGuardException ex)
                { throw ex; }
                catch (Exception ex)
                {
                    throw new OpenStagePLCExecuteFailException(ex.Message);
                }

                var transition = tClosedBox_WaitingForUnlock;
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
            sClosedBox.OnExit += (sender, e) =>
            { };
            sWaitingForUnlock.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                }
                catch (Exception ex)
                {
                    throw new OpenStageException(ex.Message);
                }

                var transition = tWaitingForUnlock_NULL;
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
            sWaitingForUnlock.OnExit += (sender, e) =>
            { };
            sOpeningBox.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalOpenStage.Close();
                    HalOpenStage.Clamp();
                    HalOpenStage.Open();
                }
                catch (Exception ex)
                {
                    throw new OpenStagePLCExecuteFailException(ex.Message);
                }

                var transition = tOpeningBox_OpenedBox;
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
            sOpeningBox.OnExit += (sender, e) =>
            { };
            sOpenedBox.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (HalOpenStage.ReadCoverSensor().Item1 == false)
                        throw new OpenStageGuardException("Box status was not opened");
                }
                catch (OpenStageGuardException ex)
                { throw ex; }
                catch (Exception ex)
                {
                    throw new OpenStagePLCExecuteFailException(ex.Message);
                }

                var transition = tOpenedBox_WaitingForInputMask;
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
            sOpenedBox.OnExit += (sender, e) =>
            { };
            sWaitingForInputMask.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                }
                catch (Exception ex)
                {
                    throw new OpenStageException(ex.Message);
                }

                var transition = tWaitingForInputMask_NULL;
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
            sWaitingForInputMask.OnExit += (sender, e) =>
            { };
            sOpenedBoxWithMaskForClose.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    CheckBoxWeight(null, true);
                }
                catch (OpenStageGuardException ex)
                { throw ex; }
                catch (Exception ex)
                {
                    throw new OpenStagePLCExecuteFailException(ex.Message);
                }

                var transition = tOpenedBoxWithMaskForClose_ClosingBoxWithMask;
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
            sOpenedBoxWithMaskForClose.OnExit += (sender, e) =>
            { };
            sClosingBoxWithMask.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalOpenStage.Close();
                    HalOpenStage.Unclamp();
                    HalOpenStage.Lock();
                }
                catch (Exception ex)
                {
                    throw new OpenStagePLCExecuteFailException(ex.Message);
                }

                var transition = tClosingBoxWithMask_WaitingForLockWithMask;
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
            sClosingBoxWithMask.OnExit += (sender, e) =>
            { };
            sWaitingForLockWithMask.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                }
                catch (Exception ex)
                {
                    throw new OpenStageException(ex.Message);
                }

                var transition = tWaitingForLockWithMask_NULL;
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
            sWaitingForLockWithMask.OnExit += (sender, e) =>
            { };
            sClosedBoxWithMaskForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (HalOpenStage.ReadCoverSensor().Item2 == false)
                        throw new OpenStageGuardException("Box status was not closed");
                    HalOpenStage.Vacuum(false);
                }
                catch (OpenStageGuardException ex)
                { throw ex; }
                catch (Exception ex)
                {
                    throw new OpenStagePLCExecuteFailException(ex.Message);
                }

                var transition = tClosedBoxWithMaskForRelease_WaitingForReleaseBoxWithMask;
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
            sClosedBoxWithMaskForRelease.OnExit += (sender, e) =>
            { };
            sWaitingForReleaseBoxWithMask.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                }
                catch (Exception ex)
                {
                    throw new OpenStageException(ex.Message);
                }

                var transition = tWaitingForReleaseBoxWithMask_NULL;
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
            sWaitingForReleaseBoxWithMask.OnExit += (sender, e) =>
            { };



            sWaitingForInputBoxWithMask.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                }
                catch (Exception ex)
                {
                    throw new OpenStageException(ex.Message);
                }

                var transition = tWaitingForInputBoxWithMask_NULL;
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
            sWaitingForInputBoxWithMask.OnExit += (sender, e) =>
            { };
            sClosedBoxWithMask.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var BoxType = (uint)e.Parameter;
                    CheckBoxWeight(BoxType, true);
                    if (HalOpenStage.ReadCoverSensor().Item2 == false)
                        throw new OpenStageGuardException("Box status was not closed");
                    HalOpenStage.SetBoxType(BoxType);
                    HalOpenStage.SortClamp();
                    Thread.Sleep(1000);
                    HalOpenStage.SortUnclamp();
                    HalOpenStage.SortClamp();
                    Thread.Sleep(1000);
                    HalOpenStage.Vacuum(true);
                    HalOpenStage.SortUnclamp();
                    HalOpenStage.Lock();
                }
                catch (OpenStageGuardException ex)
                { throw ex; }
                catch (Exception ex)
                {
                    throw new OpenStagePLCExecuteFailException(ex.Message);
                }

                var transition = tClosedBoxWithMask_WaitingForUnlockWithMask;
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
            sClosedBoxWithMask.OnExit += (sender, e) =>
            { };
            sWaitingForUnlockWithMask.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                }
                catch (Exception ex)
                {
                    throw new OpenStageException(ex.Message);
                }

                var transition = tWaitingForUnlockWithMask_NULL;
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
            sWaitingForUnlockWithMask.OnExit += (sender, e) =>
            { };
            sOpeningBoxWithMask.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalOpenStage.Close();
                    HalOpenStage.Clamp();
                    HalOpenStage.Open();
                }
                catch (Exception ex)
                {
                    throw new OpenStagePLCExecuteFailException(ex.Message);
                }

                var transition = tOpeningBoxWithMask_OpenedBoxWithMask;
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
            sOpeningBoxWithMask.OnExit += (sender, e) =>
            { };
            sOpenedBoxWithMask.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (HalOpenStage.ReadCoverSensor().Item1 == false)
                        throw new OpenStageGuardException("Box status was not opened");
                }
                catch (OpenStageGuardException ex)
                { throw ex; }
                catch (Exception ex)
                {
                    throw new OpenStagePLCExecuteFailException(ex.Message);
                }

                var transition = tOpenedBoxWithMask_WaitingForReleaseMask;
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
            sOpenedBoxWithMask.OnExit += (sender, e) =>
            { };
            sWaitingForReleaseMask.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                }
                catch (Exception ex)
                {
                    throw new OpenStageException(ex.Message);
                }

                var transition = tWaitingForReleaseMask_NULL;
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
            sWaitingForReleaseMask.OnExit += (sender, e) =>
            { };
            sOpenedBoxForClose.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    CheckBoxWeight(null, false);
                }
                catch (OpenStageGuardException ex)
                { throw ex; }
                catch (Exception ex)
                {
                    throw new OpenStagePLCExecuteFailException(ex.Message);
                }

                var transition = tOpenedBoxForClose_ClosingBox;
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
            sOpenedBoxForClose.OnExit += (sender, e) =>
            { };
            sClosingBox.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalOpenStage.Close();
                    HalOpenStage.Unclamp();
                    HalOpenStage.Lock();
                }
                catch (Exception ex)
                {
                    throw new OpenStagePLCExecuteFailException(ex.Message);
                }

                var transition = tClosingBox_WaitingForLock;
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
            sClosingBox.OnExit += (sender, e) =>
            { };
            sWaitingForLock.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                }
                catch (Exception ex)
                {
                    throw new OpenStageException(ex.Message);
                }

                var transition = tWaitingForLock_NULL;
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
            sWaitingForLock.OnExit += (sender, e) =>
            { };
            sClosedBoxForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (HalOpenStage.ReadCoverSensor().Item2 == false)
                        throw new OpenStageGuardException("Box status was not closed");
                    HalOpenStage.Vacuum(false);
                }
                catch (OpenStageGuardException ex)
                { throw ex; }
                catch (Exception ex)
                {
                    throw new OpenStagePLCExecuteFailException(ex.Message);
                }

                var transition = tClosedBoxForRelease_WaitingForReleaseBox;
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
            sClosedBoxForRelease.OnExit += (sender, e) =>
            { };
            sWaitingForReleaseBox.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                }
                catch (Exception ex)
                {
                    throw new OpenStageException(ex.Message);
                }

                var transition = tWaitingForReleaseBox_NULL;
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
            sWaitingForReleaseBox.OnExit += (sender, e) =>
            { };
            #endregion State Register OnEntry OnExit
        }

        private bool CheckBoxWeight(uint? BoxType, bool WithMask)
        {
            int IronBoxMinWeight = 775, IronBoxMaxWeight = 778, IronBoxWithMaskMinWeight = 1102, IronBoxWithMaskMaxWeight = 1104;
            int CrystalBoxMinWeight = 589, CrystalBoxMaxWeight = 590, CrystalBoxWithMaskMinWeight = 918, CrystalBoxWithMaskMaxWeight = 920;
            var BoxWeight = HalOpenStage.ReadWeightOnStage();
            if (BoxType == 1 && !WithMask)
            {
                if (BoxWeight < IronBoxMinWeight || BoxWeight > IronBoxMaxWeight)
                    throw new OpenStageGuardException("Wrong weight of iron box, box weight = " + BoxWeight.ToString());
            }
            else if (BoxType == 1 && WithMask)
            {
                if (BoxWeight < IronBoxWithMaskMinWeight || BoxWeight > IronBoxWithMaskMaxWeight)
                    throw new OpenStageGuardException("Wrong weight of iron box with mask, box weight = " + BoxWeight.ToString());
            }
            else if (BoxType == 2 && !WithMask)
            {
                if (BoxWeight < CrystalBoxMinWeight || BoxWeight > CrystalBoxMaxWeight)
                    throw new OpenStageGuardException("Wrong weight of crystal box, box weight = " + BoxWeight.ToString());
            }
            else if (BoxType == 2 && WithMask)
            {
                if (BoxWeight < CrystalBoxWithMaskMinWeight || BoxWeight > CrystalBoxWithMaskMaxWeight)
                    throw new OpenStageGuardException("Wrong weight of crystal box with mask, box weight = " + BoxWeight.ToString());
            }
            else if (BoxType == null && !WithMask)
            {
                if (BoxWeight < IronBoxMinWeight || BoxWeight > IronBoxMaxWeight || BoxWeight < CrystalBoxMinWeight || BoxWeight > CrystalBoxMaxWeight)
                    throw new OpenStageGuardException("Wrong weight of box, box weight = " + BoxWeight.ToString());
            }
            else if (BoxType == null && WithMask)
            {
                if (BoxWeight < IronBoxWithMaskMinWeight || BoxWeight > IronBoxWithMaskMaxWeight || BoxWeight < CrystalBoxWithMaskMinWeight || BoxWeight > CrystalBoxWithMaskMaxWeight)
                    throw new OpenStageGuardException("Wrong weight of box with mask, box weight = " + BoxWeight.ToString());
            }
            else
                throw new OpenStageGuardException("When checking the box weight, the parameters provided do not meet the conditions !");
            return true;
        }

        private bool CheckEquipmentStatus()
        {
            string Result = null;
            if (HalUniversal.ReadPowerON() == false) Result += "Equipment is power off now, ";
            if (HalUniversal.ReadBCP_Maintenance()) Result += "Key lock in the electric control box is turn to maintenance, ";
            if (HalUniversal.ReadCB_Maintenance()) Result += "Outside key lock between cabinet_1 and cabinet_2 is turn to maintenance, ";
            if (HalUniversal.ReadBCP_EMO().Item1) Result += "EMO_1 has been trigger, ";
            if (HalUniversal.ReadBCP_EMO().Item2) Result += "EMO_2 has been trigger, ";
            if (HalUniversal.ReadBCP_EMO().Item3) Result += "EMO_3 has been trigger, ";
            if (HalUniversal.ReadBCP_EMO().Item4) Result += "EMO_4 has been trigger, ";
            if (HalUniversal.ReadBCP_EMO().Item5) Result += "EMO_5 has been trigger, ";
            if (HalUniversal.ReadCB_EMO().Item1) Result += "EMO_6 has been trigger, ";
            if (HalUniversal.ReadCB_EMO().Item2) Result += "EMO_7 has been trigger, ";
            if (HalUniversal.ReadCB_EMO().Item3) Result += "EMO_8 has been trigger, ";
            if (HalUniversal.ReadLP1_EMO()) Result += "Load Port_1 EMO has been trigger, ";
            if (HalUniversal.ReadLP2_EMO()) Result += "Load Port_2 EMO has been trigger, ";
            if (HalUniversal.ReadBCP_Door()) Result += "The door of electric control box has been open, ";
            if (HalUniversal.ReadLP1_Door()) Result += "The door of Load Port_1 has been open, ";
            if (HalUniversal.ReadLP2_Door()) Result += "The door of Load Pord_2 has been open, ";
            if (HalUniversal.ReadBCP_Smoke()) Result += "Smoke detected in the electric control box, ";

            if (Result == null)
                return true;
            else
                throw new UniversalEquipmentException(Result);
        }

        private bool CheckAssemblyAlarmSignal()
        {
            //var CB_Alarm = HalUniversal.ReadAlarm_Cabinet();
            //var CC_Alarm = HalUniversal.ReadAlarm_CleanCh();
            //var CF_Alarm = HalUniversal.ReadAlarm_CoverFan();
            //var BT_Alarm = HalUniversal.ReadAlarm_BTRobot();
            //var MTClampInsp_Alarm = HalUniversal.ReadAlarm_MTClampInsp();
            //var MT_Alarm = HalUniversal.ReadAlarm_MTRobot();
            //var IC_Alarm = HalUniversal.ReadAlarm_InspCh();
            //var LP_Alarm = HalUniversal.ReadAlarm_LoadPort();
            var OS_Alarm = HalUniversal.ReadAlarm_OpenStage();

            //if (CB_Alarm != "") throw new CabinetPLCAlarmException(CB_Alarm);
            //if (CC_Alarm != "") throw new CleanChPLCAlarmException(CC_Alarm);
            //if (CF_Alarm != "") throw new UniversalCoverFanPLCAlarmException(CF_Alarm);
            //if (BT_Alarm != "") throw new BoxTransferPLCAlarmException(BT_Alarm);
            //if (MTClampInsp_Alarm != "") throw new MTClampInspectDeformPLCAlarmException(MTClampInsp_Alarm);
            //if (MT_Alarm != "") throw new MaskTransferPLCAlarmException(MT_Alarm);
            //if (IC_Alarm != "") throw new InspectionChPLCAlarmException(IC_Alarm);
            //if (LP_Alarm != "") throw new LoadportPLCAlarmException(LP_Alarm);
            if (OS_Alarm != "") throw new OpenStagePLCAlarmException(OS_Alarm);

            return true;
        }

        private bool CheckAssemblyWarningSignal()
        {
            //var CB_Warning = HalUniversal.ReadWarning_Cabinet();
            //var CC_Warning = HalUniversal.ReadWarning_CleanCh();
            //var CF_Warning = HalUniversal.ReadWarning_CoverFan();
            //var BT_Warning = HalUniversal.ReadWarning_BTRobot();
            //var MTClampInsp_Warning = HalUniversal.ReadWarning_MTClampInsp();
            //var MT_Warning = HalUniversal.ReadWarning_MTRobot();
            //var IC_Warning = HalUniversal.ReadWarning_InspCh();
            //var LP_Warning = HalUniversal.ReadWarning_LoadPort();
            var OS_Warning = HalUniversal.ReadWarning_OpenStage();

            //if (CB_Warning != "") throw new CabinetPLCWarningException(CB_Warning);
            //if (CC_Warning != "") throw new CleanChPLCWarningException(CC_Warning);
            //if (CF_Warning != "") throw new UniversalCoverFanPLCWarningException(CF_Warning);
            //if (BT_Warning != "") throw new BoxTransferPLCWarningException(BT_Warning);
            //if (MTClampInsp_Warning != "") throw new MTClampInspectDeformPLCWarningException(MTClampInsp_Warning);
            //if (MT_Warning != "") throw new MaskTransferPLCWarningException(MT_Warning);
            //if (IC_Warning != "") throw new InspectionChPLCWarningException(IC_Warning);
            //if (LP_Warning != "") throw new LoadportPLCWarningException(LP_Warning);
            if (OS_Warning != "") throw new OpenStagePLCWarningException(OS_Warning);

            return true;
        }

        public class MacOpenStageUnitStateTimeOutController
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
