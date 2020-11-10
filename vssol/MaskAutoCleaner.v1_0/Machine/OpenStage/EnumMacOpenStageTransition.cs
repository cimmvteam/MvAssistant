using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.OpenStage
{
    public enum EnumMacOpenStageTransition
    {
        SystemBootUp,
        Initial,
        StandbyAtIdle,

        TriggerToWaitForInputBox,
        WaitForInputBox,
        TriggerToCalibrationBox,
        CalibrationBox,
        WaitForUnlock,
        TriggerToOpenBox,
        OpeningBox,
        WaitForInputMask,
        TriggerToCloseBoxWithMask,
        TriggerToReturnCloseBox,
        CloseBoxWithMask,
        WaitForLockWithMask,
        TriggerToReleaseBoxWithMask,
        ReleaseVacuumForBoxWithMask,
        WaitForReleaseBoxWithMask,
        TriggerToIdleAfterReleaseBoxWithMask,

        TriggerToWaitForInputBoxWithMask,
        WaitForInputBoxWithMask,
        TriggerToCalibrationBoxWithMask,
        CalibrationBoxWithMask,
        WaitForUnlockWithMask,
        TriggerToOpenBoxWithMask,
        OpeningBoxWithMask,
        WaitForReleaseMask,
        TriggerToCloseBox,
        TriggerToReturnCloseBoxWithMask,
        CloseBox,
        WaitForLock,
        TriggerToReleaseBox,
        ReleaseVacuumForBox,
        WaitForReleaseBox,
        TriggerToIdleAfterReleaseBox,
    }
}
