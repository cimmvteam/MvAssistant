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
        public EnumMacOpenStageState CurrentWorkState { get; set; }

        private IMacHalOpenStage HalOpenStage { get { return this.halAssembly as IMacHalOpenStage; } }
        private IMacHalUniversal HalUniversal { get { return this.Mediater.GetCtrlMachine(EnumMachineID.MID_UNI_A_ASB.ToString()).HalAssembly as IMacHalUniversal; } }

        private MacState _currentState = null;

        public void ResetState()
        { this.States[EnumMacOpenStageState.Start.ToString()].DoEntry(new MacStateEntryEventArgs(null)); }


        public MacMsOpenStage() { LoadStateMachine(); }

        MacOpenStageUnitStateTimeOutController timeoutObj = new MacOpenStageUnitStateTimeOutController();


        #region State Machine Command

        /// <summary> 狀態機啟動 </summary>
        public override void SystemBootup()
        {
            this.States[EnumMacOpenStageState.Start.ToString()].DoEntry(new MacStateEntryEventArgs(null));
        }
        /// <summary> Open Stage 初始化 </summary>
        public void Initial()
        {
            this.States[EnumMacOpenStageState.Initial.ToString()].DoEntry(new MacStateEntryEventArgs(null));
        }
        /// <summary> 等待放入 Box(內無Mask) </summary>
        public void InputBox()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacOpenStageTransition.TriggerToWaitForInputBox.ToString()];
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
        /// <summary> 放入Box後，校正 Box 的位置(內無Mask)，等待 Unlock </summary>
        public void CalibrationClosedBox()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacOpenStageTransition.TriggerToCalibrationBox.ToString()];
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
        /// <summary> Unlock 後，開啟 Box(內無Mask) </summary>
        public void OpenBox()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacOpenStageTransition.TriggerToOpenBox.ToString()];
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
        /// <summary> 原先要放入 Mask ，但沒有放入 Mask ，關上 Box(內無Mask)，等待 Lock </summary>
        public void ReturnCloseBox()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacOpenStageTransition.TriggerToReturnCloseBox.ToString()];
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
        /// <summary> 關上 Box(內有Mask)，等待 Lock </summary>
        public void CloseBoxWithMask()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacOpenStageTransition.TriggerToCloseBoxWithMask.ToString()];
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
        /// <summary> Lock 後，停止吸真空固定 Box ，等待取走Box(內有Mask) </summary>
        public void ReleaseBoxWithMask()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacOpenStageTransition.TriggerToReleaseBoxWithMask.ToString()];
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
        /// <summary> 取走 Box(內有Mask)後，將狀態改為Idle </summary>
        public void ReturnToIdleAfterReleaseBoxWithMask()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacOpenStageTransition.TriggerToIdleAfterReleaseBoxWithMask.ToString()];
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


        /// <summary> 等待放入 Box(內有Mask) </summary>
        public void InputBoxWithMask()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacOpenStageTransition.TriggerToWaitForInputBoxWithMask.ToString()];
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
        /// <summary> 放入Box後，校正 Box 的位置(內有Mask)，等待 Unlock </summary>
        public void CalibrationClosedBoxWithMask()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacOpenStageTransition.TriggerToCalibrationBoxWithMask.ToString()];
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
        /// <summary> Unlock 後，開啟 Box(內有Mask) </summary>
        public void OpenBoxWithMask()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacOpenStageTransition.TriggerToOpenBoxWithMask.ToString()];
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
        /// <summary> 原先要取出 Mask ，但又將 Mask 放回 Box 內，關上 Box(內有Mask)，等待 Lock </summary>
        public void ReturnCloseBoxWithMask()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacOpenStageTransition.TriggerToReturnCloseBoxWithMask.ToString()];
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
        /// <summary> 關上 Box(內無Mask)，等待 Lock </summary>
        public void CloseBox()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacOpenStageTransition.TriggerToCloseBox.ToString()];
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
        /// <summary> Lock 後，停止吸真空固定 Box ，等待取走Box(內無Mask) </summary>
        public void ReleaseBox()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacOpenStageTransition.TriggerToReleaseBox.ToString()];
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
        /// <summary> 取走 Box(內無Mask)後，將狀態改為Idle </summary>
        public void ReturnToIdleAfterReleaseBox()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacOpenStageTransition.TriggerToIdleAfterReleaseBox.ToString()];
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

        #endregion

        public override void LoadStateMachine()
        {
            #region State
            MacState sStart = NewState(EnumMacOpenStageState.Start);
            MacState sInitial = NewState(EnumMacOpenStageState.Initial);

            MacState sIdle = NewState(EnumMacOpenStageState.Idle);
            MacState sWaitingForInputBox = NewState(EnumMacOpenStageState.WaitingForInputBox);
            MacState sClosedBox = NewState(EnumMacOpenStageState.ClosedBox);
            MacState sWaitingForUnlock = NewState(EnumMacOpenStageState.WaitingForUnlock);
            MacState sOpeningBox = NewState(EnumMacOpenStageState.OpeningBox);
            MacState sWaitingForInputMask = NewState(EnumMacOpenStageState.WaitingForInputMask);
            MacState sClosingBoxWithMask = NewState(EnumMacOpenStageState.ClosingBoxWithMask);
            MacState sWaitingForLockWithMask = NewState(EnumMacOpenStageState.WaitingForLockWithMask);
            MacState sClosedBoxWithMaskForRelease = NewState(EnumMacOpenStageState.ClosedBoxWithMaskForRelease);
            MacState sWaitingForReleaseBoxWithMask = NewState(EnumMacOpenStageState.WaitingForReleaseBoxWithMask);

            MacState sWaitingForInputBoxWithMask = NewState(EnumMacOpenStageState.WaitingForInputBoxWithMask);
            MacState sClosedBoxWithMask = NewState(EnumMacOpenStageState.ClosedBoxWithMask);
            MacState sWaitingForUnlockWithMask = NewState(EnumMacOpenStageState.WaitingForUnlickWithMask);
            MacState sOpeningBoxWithMask = NewState(EnumMacOpenStageState.OpeningBoxWithMask);
            MacState sWaitingForReleaseMask = NewState(EnumMacOpenStageState.WaitingForReleaseMask);
            MacState sClosingBox = NewState(EnumMacOpenStageState.ClosingBox);
            MacState sWaitingForLock = NewState(EnumMacOpenStageState.WaitingForLock);
            MacState sClosedBoxForRelease = NewState(EnumMacOpenStageState.ClosedBoxForRelease);
            MacState sWaitingForReleaseBox = NewState(EnumMacOpenStageState.WaitingForReleaseBox);

            #endregion State

            #region Transition
            MacTransition tStart_Initial = NewTransition(sStart, sInitial, EnumMacOpenStageTransition.SystemBootUp);
            MacTransition tInitial_Idle = NewTransition(sStart, sIdle, EnumMacOpenStageTransition.Initial);
            MacTransition tIdle_NULL = NewTransition(sIdle, null, EnumMacOpenStageTransition.StandbyAtIdle);

            MacTransition tIdle_WaitingForInputBox = NewTransition(sIdle, sWaitingForInputBox, EnumMacOpenStageTransition.TriggerToWaitForInputBox);
            MacTransition tWaitingForInputBox_NULL = NewTransition(sWaitingForInputBox, null, EnumMacOpenStageTransition.WaitForInputBox);
            MacTransition tWaitingForInputBox_ClosedBox = NewTransition(sWaitingForInputBox, sClosedBox, EnumMacOpenStageTransition.TriggerToCalibrationBox);
            MacTransition tClosedBox_WaitingForUnlock = NewTransition(sClosedBox, sWaitingForUnlock, EnumMacOpenStageTransition.CalibrationBox);
            MacTransition tWaitingForUnlock_NULL = NewTransition(sWaitingForUnlock, null, EnumMacOpenStageTransition.WaitForUnlock);
            MacTransition tWaitingForUnlock_OpeningBox = NewTransition(sWaitingForUnlock, sOpeningBox, EnumMacOpenStageTransition.TriggerToOpenBox);
            MacTransition tOpeningBox_WaitingForInputMask = NewTransition(sOpeningBox, sWaitingForInputMask, EnumMacOpenStageTransition.OpeningBox);
            MacTransition tWaitingForInputMask_NULL = NewTransition(sWaitingForInputMask, null, EnumMacOpenStageTransition.WaitForInputMask);
            MacTransition tWaitingForInputMask_ClosingBoxWithMask = NewTransition(sWaitingForInputMask, sClosingBoxWithMask, EnumMacOpenStageTransition.TriggerToCloseBoxWithMask);
            MacTransition tWaitingForInputMask_ClosingBox = NewTransition(sWaitingForInputMask, sClosingBox, EnumMacOpenStageTransition.TriggerToReturnCloseBox);
            MacTransition tClosingBoxWithMask_WaitingForLockWithMask = NewTransition(sClosingBoxWithMask, sWaitingForLockWithMask, EnumMacOpenStageTransition.CloseBoxWithMask);
            MacTransition tWaitingForLockWithMask_NULL = NewTransition(sWaitingForLockWithMask, null, EnumMacOpenStageTransition.WaitForLockWithMask);
            MacTransition tWaitingForLockWithMask_ClosedBoxWithMaskForRelease = NewTransition(sWaitingForLockWithMask, sClosedBoxWithMaskForRelease, EnumMacOpenStageTransition.TriggerToReleaseBoxWithMask);
            MacTransition tClosedBoxWithMaskForRelease_WaitingForReleaseBoxWithMask = NewTransition(sClosedBoxWithMaskForRelease, sWaitingForReleaseBoxWithMask, EnumMacOpenStageTransition.ReleaseVacuumForBoxWithMask);
            MacTransition tWaitingForReleaseBoxWithMask_NULL = NewTransition(sWaitingForReleaseBoxWithMask, null, EnumMacOpenStageTransition.WaitForReleaseBoxWithMask);
            MacTransition tWaitingForReleaseBoxWithMask_Idle = NewTransition(sWaitingForReleaseBoxWithMask, sIdle, EnumMacOpenStageTransition.TriggerToIdleAfterReleaseBoxWithMask);


            MacTransition tIdle_WaitingForInputBoxWithMask = NewTransition(sIdle, sWaitingForInputBoxWithMask, EnumMacOpenStageTransition.TriggerToWaitForInputBoxWithMask);
            MacTransition tWaitingForInputBoxWithMask_NULL = NewTransition(sWaitingForInputBoxWithMask, null, EnumMacOpenStageTransition.WaitForInputBoxWithMask);
            MacTransition tWaitingForInputBoxWithMask_ClosedBoxWithMask = NewTransition(sWaitingForInputBoxWithMask, sClosedBoxWithMask, EnumMacOpenStageTransition.TriggerToCalibrationBoxWithMask);
            MacTransition tClosedBoxWithMask_WaitingForUnlockWithMask = NewTransition(sClosedBoxWithMask, sWaitingForUnlockWithMask, EnumMacOpenStageTransition.CalibrationBoxWithMask);
            MacTransition tWaitingForUnlockWithMask_NULL = NewTransition(sWaitingForUnlockWithMask, null, EnumMacOpenStageTransition.WaitForUnlockWithMask);
            MacTransition tWaitingForUnlockWithMask_OpeningBoxWithMask = NewTransition(sWaitingForUnlockWithMask, sOpeningBoxWithMask, EnumMacOpenStageTransition.TriggerToOpenBoxWithMask);
            MacTransition tOpeningBoxWithMask_WaitingForReleaseMask = NewTransition(sOpeningBoxWithMask, sWaitingForReleaseMask, EnumMacOpenStageTransition.OpeningBoxWithMask);
            MacTransition tWaitingForReleaseMask_NULL = NewTransition(sWaitingForReleaseMask, null, EnumMacOpenStageTransition.WaitForReleaseMask);
            MacTransition tWaitingForReleaseMask_ClosingBox = NewTransition(sWaitingForReleaseMask, sClosingBox, EnumMacOpenStageTransition.TriggerToCloseBox);
            MacTransition tWaitingForReleaseMask_ClosingBoxWithMask = NewTransition(sWaitingForReleaseMask, sClosingBoxWithMask, EnumMacOpenStageTransition.TriggerToReturnCloseBoxWithMask);
            MacTransition tClosingBox_WaitingForLock = NewTransition(sClosingBox, sWaitingForLock, EnumMacOpenStageTransition.CloseBox);
            MacTransition tWaitingForLock_NULL = NewTransition(sWaitingForLock, null, EnumMacOpenStageTransition.WaitForLock);
            MacTransition tWaitingForLock_ClosedBoxForRelease = NewTransition(sWaitingForLock, sClosedBoxForRelease, EnumMacOpenStageTransition.TriggerToReleaseBox);
            MacTransition tClosedBoxForRelease_WaitingForReleaseBox = NewTransition(sClosedBoxForRelease, sWaitingForReleaseBox, EnumMacOpenStageTransition.ReleaseVacuumForBox);
            MacTransition tWaitingForReleaseBox_NULL = NewTransition(sWaitingForReleaseBox, null, EnumMacOpenStageTransition.WaitForReleaseBox);
            MacTransition tWaitingForReleaseBox_Idle = NewTransition(sWaitingForReleaseBox, sIdle, EnumMacOpenStageTransition.TriggerToIdleAfterReleaseBox);
            #endregion Transition

            #region State Register OnEntry OnExit
            sStart.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tStart_Initial;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
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
            sStart.OnExit += (sender, e) => { };

            sInitial.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tInitial_Idle;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalOpenStage.Initial();
                        }
                        catch (Exception ex) { throw new OpenStagePLCExecuteFailException(ex.Message); }
                    },
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
            sInitial.OnExit += (sender, e) => { };

            sIdle.OnEntry += (sender, e) =>
            {
                if (HalOpenStage.ReadWeightOnStage() > 285)
                    throw new OpenStageGuardException("The stage is not cleared !");
                SetCurrentState((MacState)sender);

                var transition = tIdle_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
                    Action = (parameter) => { },
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
            sIdle.OnExit += (sender, e) => { };

            sWaitingForInputBox.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tWaitingForInputBox_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
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
            sWaitingForInputBox.OnExit += (sender, e) => { };

            sClosedBox.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tClosedBox_WaitingForUnlock;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();

                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            CheckBoxWeight((uint)parameter, false);
                            if (HalOpenStage.ReadCoverSensor().Item2 == false)
                                throw new OpenStageGuardException("Box status was not closed");
                            HalOpenStage.SetBoxType((uint)parameter);
                            HalOpenStage.SortClamp();
                            Thread.Sleep(1000);
                            HalOpenStage.SortUnclamp();
                            HalOpenStage.SortClamp();
                            Thread.Sleep(1000);
                            HalOpenStage.Vacuum(true);
                            HalOpenStage.SortUnclamp();
                            HalOpenStage.Lock();
                        }
                        catch (OpenStageGuardException ex) { throw ex; }
                        catch (Exception ex) { throw new OpenStagePLCExecuteFailException(ex.Message); }
                    },
                    ActionParameter = e.Parameter,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sClosedBox.OnExit += (sender, e) => { };

            sWaitingForUnlock.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tWaitingForUnlock_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
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
            sWaitingForUnlock.OnExit += (sender, e) => { };

            sOpeningBox.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tOpeningBox_WaitingForInputMask;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
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
                    },
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
            sOpeningBox.OnExit += (sender, e) => { };

            sWaitingForInputMask.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tWaitingForInputMask_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
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
            sWaitingForInputMask.OnExit += (sender, e) => { };

            sClosingBoxWithMask.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tClosingBoxWithMask_WaitingForLockWithMask;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalOpenStage.Close();
                            HalOpenStage.Unclamp();
                            HalOpenStage.Lock();
                        }
                        catch (Exception ex) { throw new OpenStagePLCExecuteFailException(ex.Message); }
                    },
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
            sClosingBoxWithMask.OnExit += (sender, e) => { };

            sWaitingForLockWithMask.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tWaitingForLockWithMask_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
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
            sWaitingForLockWithMask.OnExit += (sender, e) => { };

            sClosedBoxWithMaskForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tClosedBoxWithMaskForRelease_WaitingForReleaseBoxWithMask;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        if (HalOpenStage.ReadCoverSensor().Item2 == false)
                            throw new OpenStageGuardException("Box status was not closed");
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalOpenStage.Vacuum(false);
                        }
                        catch (Exception ex) { throw new OpenStagePLCExecuteFailException(ex.Message); }
                    },
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
            sClosedBoxWithMaskForRelease.OnExit += (sender, e) => { };

            sWaitingForReleaseBoxWithMask.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tWaitingForReleaseBoxWithMask_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
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
            sWaitingForReleaseBoxWithMask.OnExit += (sender, e) => { };



            sWaitingForInputBoxWithMask.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tWaitingForInputBoxWithMask_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
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
            sWaitingForInputBoxWithMask.OnExit += (sender, e) => { };

            sClosedBoxWithMask.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tClosedBoxWithMask_WaitingForUnlockWithMask;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            CheckBoxWeight((uint)parameter, true);
                            if (HalOpenStage.ReadCoverSensor().Item2 == false)
                                throw new OpenStageGuardException("Box status was not closed");
                            HalOpenStage.SetBoxType((uint)parameter);
                            HalOpenStage.SortClamp();
                            Thread.Sleep(1000);
                            HalOpenStage.SortUnclamp();
                            HalOpenStage.SortClamp();
                            Thread.Sleep(1000);
                            HalOpenStage.Vacuum(true);
                            HalOpenStage.SortUnclamp();
                            HalOpenStage.Lock();
                        }
                        catch (OpenStageGuardException ex) { throw ex; }
                        catch (Exception ex) { throw new OpenStagePLCExecuteFailException(ex.Message); }
                    },
                    ActionParameter = e.Parameter,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sClosedBoxWithMask.OnExit += (sender, e) => { };

            sWaitingForUnlockWithMask.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tWaitingForUnlockWithMask_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
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
            sWaitingForUnlockWithMask.OnExit += (sender, e) => { };

            sOpeningBoxWithMask.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tOpeningBoxWithMask_WaitingForReleaseMask;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalOpenStage.Close();
                            HalOpenStage.Clamp();
                            HalOpenStage.Open();
                        }
                        catch (Exception ex) { throw new OpenStagePLCExecuteFailException(ex.Message); }
                    },
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
            sOpeningBoxWithMask.OnExit += (sender, e) => { };

            sWaitingForReleaseMask.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tWaitingForReleaseMask_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
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
            sWaitingForReleaseMask.OnExit += (sender, e) => { };

            sClosingBox.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tClosingBox_WaitingForLock;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalOpenStage.Close();
                            HalOpenStage.Unclamp();
                            HalOpenStage.Lock();
                        }
                        catch (Exception ex) { throw new OpenStagePLCExecuteFailException(ex.Message); }
                    },
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
            sClosingBox.OnExit += (sender, e) => { };

            sWaitingForLock.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tWaitingForLock_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
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
            sWaitingForLock.OnExit += (sender, e) => { };

            sClosedBoxForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tClosedBoxForRelease_WaitingForReleaseBox;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        if (HalOpenStage.ReadCoverSensor().Item2 == false)
                            throw new OpenStageGuardException("Box status was not closed");
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalOpenStage.Vacuum(false);
                        }
                        catch (Exception ex) { throw new OpenStagePLCExecuteFailException(ex.Message); }
                    },
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
            sClosedBoxForRelease.OnExit += (sender, e) => { };

            sWaitingForReleaseBox.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tWaitingForReleaseBox_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
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
            sWaitingForReleaseBox.OnExit += (sender, e) => { };
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
