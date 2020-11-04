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

        TriggerToWaitForInputBox,
        StandbyAtWaitForInputBox,
        TriggerToCalibrationBox,
        WaitForUnlock,
        StandbyAtWaitForUnlock,
        TriggerToOpenBox,
        OpenedBox,
        WaitForInputMask,
        StandbyAtWaitForInputMask,
        TriggerToCloseBoxWithMask,
        TriggerToReturnCloseBox,
        CloseBoxWithMask,
        WaitForLockWithMask,
        StandbyAtWaitForLockWithMask,
        TriggerToReleaseBoxWithMask,
        WaitForReleaseBoxWithMask,
        StandbyAtWaitForReleaseBoxWithMask,
        TriggerToIdleAfterReleaseBoxWithMask,

        TriggerToWaitForInputBoxWithMask,
        StandbyAtWaitForInputBoxWithMask,
        TriggerToCalibrationBoxWithMask,
        WaitForUnlockWithMask,
        StandbyAtWaitForUnlockWithMask,
        TriggerToOpenBoxWithMask,
        OpenedBoxWithMask,
        WaitForReleaseMask,
        StandbyAtWaitForReleaseMask,
        TriggerToCloseBox,
        TriggerToReturnCloseBoxWithMask,
        CloseBox,
        WaitForLock,
        StandbyAtWaitForLock,
        TriggerToReleaseBox,
        WaitForReleaseBox,
        StandbyAtWaitForReleaseBox,
        TriggerToIdleAfterReleaseBox,
    }
}
