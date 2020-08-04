using MaskAutoCleaner.v1_0.StateMachineBeta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.OpenStage
{
    public class MacMsOpenStage : MacMachineStateBase
    {
        public EnumMacMsOpenStageState CurrentWorkState { get; set; }
        public override void LoadStateMachine()
        {
            #region State
            MacState sStart = NewState(EnumMacMsOpenStageState.Start);
            MacState sInitial = NewState(EnumMacMsOpenStageState.Initial);

            MacState sIdle = NewState(EnumMacMsOpenStageState.Idle);
            MacState sWaitingForInputBox = NewState(EnumMacMsOpenStageState.WaitingForInputBox);
            MacState sWaitingForReleaseBox = NewState(EnumMacMsOpenStageState.WaitingForReleaseBox);
            MacState sClosedBox = NewState(EnumMacMsOpenStageState.ClosedBox);
            MacState sWaitingForUnlock = NewState(EnumMacMsOpenStageState.WaitingForUnlock);
            MacState sOpeningBox = NewState(EnumMacMsOpenStageState.OpeningBox);
            MacState sWaitingForLock = NewState(EnumMacMsOpenStageState.WaitingForLock);
            MacState sClosingBox = NewState(EnumMacMsOpenStageState.ClosingBox);
            MacState sOpenedBox = NewState(EnumMacMsOpenStageState.OpenedBox);
            MacState sWaitingForInputMask = NewState(EnumMacMsOpenStageState.WaitingForInputMask);
            MacState sWaitingForReleaseMask = NewState(EnumMacMsOpenStageState.WaitingForReleaseMask);
            MacState sOpenedBoxWithMask = NewState(EnumMacMsOpenStageState.OpenedBoxWithMask);
            MacState sClosingBoxWithMask = NewState(EnumMacMsOpenStageState.ClosingBoxWithMask);
            MacState sWaitingForLockWithMask = NewState(EnumMacMsOpenStageState.WaitingForLockWithMask);
            MacState sWaitingForUnlickWithMask = NewState(EnumMacMsOpenStageState.WaitingForUnlickWithMask);
            MacState sOpeningBoxWithMask = NewState(EnumMacMsOpenStageState.OpeningBoxWithMask);
            MacState sClosedBoxWithMask = NewState(EnumMacMsOpenStageState.ClosedBoxWithMask);
            MacState sWaitingForReleaseBoxWithMask = NewState(EnumMacMsOpenStageState.WaitingForReleaseBoxWithMask);
            MacState sWaitingForInputBoxWithMask = NewState(EnumMacMsOpenStageState.WaitingForInputBoxWithMask);
            #endregion State

            #region Transition
            MacTransition tStart_Initial = NewTransition(sStart, sInitial, EnumMacMsOpenStageTransition.PowerON);
            MacTransition tInitial_Idle = NewTransition(sStart, sIdle, EnumMacMsOpenStageTransition.Initial);

            MacTransition tIdle_WaitingForInputBox = NewTransition(sIdle, sWaitingForInputBox, EnumMacMsOpenStageTransition.WaitForInputBox);
            MacTransition tWaitingForInputBox_CloseedBox = NewTransition(sWaitingForInputBox, sClosedBox, EnumMacMsOpenStageTransition.StandbyAtClosedBoxFromIdle);
            MacTransition tCloseedBox_WaitingForUnlock = NewTransition(sClosedBox, sWaitingForUnlock, EnumMacMsOpenStageTransition.WaitForUnlock);
            MacTransition tWaitingForUnlock_OpeningBox = NewTransition(sWaitingForUnlock, sOpeningBox, EnumMacMsOpenStageTransition.OpenBox);
            MacTransition tOpeningBox_OpenedBox = NewTransition(sOpeningBox, sOpenedBox, EnumMacMsOpenStageTransition.StandbyAtOpenedBoxFromClosedBox);
            MacTransition tOpenedBox_WaitingForInputMask = NewTransition(sOpenedBox, sWaitingForInputMask, EnumMacMsOpenStageTransition.WaitForInputMask);
            MacTransition tWaitingForInputMask_OpenedBoxWithMask = NewTransition(sWaitingForInputMask, sOpenedBoxWithMask, EnumMacMsOpenStageTransition.StandbyAtOpenedBoxWithMaskFromOpenedBox);
            MacTransition tOpenedBoxWithMask_ClosingBoxWithMask = NewTransition(sOpenedBoxWithMask, sClosingBoxWithMask, EnumMacMsOpenStageTransition.CloseBoxWithMask);
            MacTransition tClosingBoxWithMask_WaitingForLockWithMask = NewTransition(sClosingBoxWithMask, sWaitingForLockWithMask, EnumMacMsOpenStageTransition.WaitForLockWithMask);
            MacTransition tWaitingForLockWithMask_ClosedBoxWithMask = NewTransition(sWaitingForLockWithMask, sClosedBoxWithMask, EnumMacMsOpenStageTransition.StandbyAtClosedBoxWithMaskFromOpenedBoxWithMask);
            MacTransition tClosedBoxWithMask_Idle = NewTransition(sClosedBoxWithMask, sWaitingForReleaseBoxWithMask, EnumMacMsOpenStageTransition.WaitForReleaseBoxWithMask);
            MacTransition tWaitingForReleaseBoxWithMask_Idle = NewTransition(sWaitingForReleaseBoxWithMask, sIdle, EnumMacMsOpenStageTransition.ReturnToIdleFromClosedBoxWithMask);

            MacTransition tIdle_WaitingForInputBoxWithMask = NewTransition(sIdle, sWaitingForInputBoxWithMask, EnumMacMsOpenStageTransition.WaitForInputBoxWithMask);
            MacTransition tWaitingForInputBoxWithMask_ClosedBoxWithMask = NewTransition(sWaitingForInputBoxWithMask, sClosedBoxWithMask, EnumMacMsOpenStageTransition.StandbyAtClosedBoxWithMaskFromIdle);
            MacTransition tClosedBoxWithMask_WaitingForUnlickWithMask = NewTransition(sClosedBoxWithMask, sWaitingForUnlickWithMask, EnumMacMsOpenStageTransition.WaitForUnlockWithMask);
            MacTransition tWaitingForUnlickWithMask_OpeningBoxWithMask = NewTransition(sWaitingForUnlickWithMask, sOpeningBoxWithMask, EnumMacMsOpenStageTransition.OpenBoxWithMask);
            MacTransition tOpeningBoxWithMask_OpenedBoxWithMask = NewTransition(sOpeningBoxWithMask, sOpenedBoxWithMask, EnumMacMsOpenStageTransition.StandbyAtOpenedBoxWithMaskFromClosedBoxWithMask);
            MacTransition tOpenedBoxWithMask_WaitingForReleaseMask = NewTransition(sOpenedBoxWithMask, sWaitingForReleaseMask, EnumMacMsOpenStageTransition.WaitForReleaseMask);
            MacTransition tWaitingForReleaseMask_OpenedBox = NewTransition(sWaitingForReleaseMask, sOpenedBox, EnumMacMsOpenStageTransition.StandbyAtOpenedBoxFromOpenedBoxWithMask);
            MacTransition tOpenedBox_ClosingBox = NewTransition(sOpenedBox, sClosingBox, EnumMacMsOpenStageTransition.CloseBox);
            MacTransition tClosingBox_WaitingForLock = NewTransition(sClosingBox, sWaitingForLock, EnumMacMsOpenStageTransition.WaitForLock);
            MacTransition tWaitingForLock_ClosedBox = NewTransition(sWaitingForLock, sClosedBox, EnumMacMsOpenStageTransition.StandbyAtClosedBoxFromOpenedBox);
            MacTransition tClosedBox_WaitingForReleaseBox = NewTransition(sClosedBox, sWaitingForReleaseBox, EnumMacMsOpenStageTransition.WaitForReleaseBox);
            MacTransition tWaitingForReleaseBox_Idle = NewTransition(sWaitingForReleaseBox, sIdle, EnumMacMsOpenStageTransition.ReturnToIdleFromClosedBox);
            #endregion Transition

            #region State Register OnEntry OnExit
            sStart.OnEntry += (sender, e) =>
            { };
            sStart.OnExit += (sender, e) =>
            { };
            sInitial.OnEntry += (sender, e) =>
            { };
            sInitial.OnExit += (sender, e) =>
            { };

            sIdle.OnEntry += (sender, e) =>
            { };
            sIdle.OnExit += (sender, e) =>
            { };
            sWaitingForInputBox.OnEntry += (sender, e) =>
            { };
            sWaitingForInputBox.OnExit += (sender, e) =>
            { };
            sWaitingForReleaseBox.OnEntry += (sender, e) =>
            { };
            sWaitingForReleaseBox.OnExit += (sender, e) =>
            { };
            sClosedBox.OnEntry += (sender, e) =>
            { };
            sClosedBox.OnExit += (sender, e) =>
            { };
            sWaitingForUnlock.OnEntry += (sender, e) =>
            { };
            sWaitingForUnlock.OnExit += (sender, e) =>
            { };
            sOpeningBox.OnEntry += (sender, e) =>
            { };
            sOpeningBox.OnExit += (sender, e) =>
            { };
            sWaitingForLock.OnEntry += (sender, e) =>
            { };
            sWaitingForLock.OnExit += (sender, e) =>
            { };
            sClosingBox.OnEntry += (sender, e) =>
            { };
            sClosingBox.OnExit += (sender, e) =>
            { };
            sOpenedBox.OnEntry += (sender, e) =>
            { };
            sOpenedBox.OnExit += (sender, e) =>
            { };
            sWaitingForInputMask.OnEntry += (sender, e) =>
            { };
            sWaitingForInputMask.OnExit += (sender, e) =>
            { };
            sWaitingForReleaseMask.OnEntry += (sender, e) =>
            { };
            sWaitingForReleaseMask.OnExit += (sender, e) =>
            { };
            sOpenedBoxWithMask.OnEntry += (sender, e) =>
            { };
            sOpenedBoxWithMask.OnExit += (sender, e) =>
            { };
            sClosingBoxWithMask.OnEntry += (sender, e) =>
            { };
            sClosingBoxWithMask.OnExit += (sender, e) =>
            { };
            sWaitingForLockWithMask.OnEntry += (sender, e) =>
            { };
            sWaitingForLockWithMask.OnExit += (sender, e) =>
            { };
            sWaitingForUnlickWithMask.OnEntry += (sender, e) =>
            { };
            sWaitingForUnlickWithMask.OnExit += (sender, e) =>
            { };
            sOpeningBoxWithMask.OnEntry += (sender, e) =>
            { };
            sOpeningBoxWithMask.OnExit += (sender, e) =>
            { };
            sClosedBoxWithMask.OnEntry += (sender, e) =>
            { };
            sClosedBoxWithMask.OnExit += (sender, e) =>
            { };
            sWaitingForReleaseBoxWithMask.OnEntry += (sender, e) =>
            { };
            sWaitingForReleaseBoxWithMask.OnExit += (sender, e) =>
            { };
            sWaitingForInputBoxWithMask.OnEntry += (sender, e) =>
            { };
            sWaitingForInputBoxWithMask.OnExit += (sender, e) =>
            { };
            #endregion State Register OnEntry OnExit
        }

        public class TimeOutController
        {
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
                return IsTimeOut(startTime, 20);
            }
        }
    }
}
