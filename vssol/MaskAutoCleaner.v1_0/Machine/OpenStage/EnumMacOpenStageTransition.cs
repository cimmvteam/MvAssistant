using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.OpenStage
{
    public enum EnumMacOpenStageTransition
    {
        PowerON,
        Initial,
        StandbyAtIdle,

        ReceiveTriggerToWaitForInputBox,
        StandbyAtWaitForInputBox,
        ReceiveTriggerToCalibrationBox,
        WaitForUnlock,
        StandbyAtWaitForUnlock,
        ReceiveTriggerToOpenBox,
        OpenedBox,
        WaitForInputMask,
        StandbyAtWaitForInputMask,
        ReceiveTriggerToCloseBoxWithMask,
        ReceiveTriggerToReturnCloseBox,
        CloseBoxWithMask,
        WaitForLockWithMask,
        StandbyAtWaitForLockWithMask,
        ReceiveTriggerToReleaseBoxWithMask,
        WaitForReleaseBoxWithMask,
        StandbyAtWaitForReleaseBoxWithMask,
        ReceiveTriggerToIdleAfterReleaseBoxWithMask,

        ReceiveTriggerToWaitForInputBoxWithMask,
        StandbyAtWaitForInputBoxWithMask,
        ReceiveTriggerToCalibrationBoxWithMask,
        WaitForUnlockWithMask,
        StandbyAtWaitForUnlockWithMask,
        ReceiveTriggerToOpenBoxWithMask,
        OpenedBoxWithMask,
        WaitForReleaseMask,
        StandbyAtWaitForReleaseMask,
        ReceiveTriggerToCloseBox,
        ReceiveTriggerToReturnCloseBoxWithMask,
        CloseBox,
        WaitForLock,
        StandbyAtWaitForLock,
        ReceiveTriggerToReleaseBox,
        WaitForReleaseBox,
        StandbyAtWaitForReleaseBox,
        ReceiveTriggerToIdleAfterReleaseBox,
    }
}
