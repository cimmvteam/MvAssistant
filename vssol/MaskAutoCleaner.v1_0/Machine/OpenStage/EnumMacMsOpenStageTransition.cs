using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.OpenStage
{
    public enum EnumMacMsOpenStageTransition
    {
        PowerON,
        Initial,

        WaitForInputBox,
        StandbyAtClosedBoxFromIdle,
        WaitForUnlock,
        OpenBox,
        StandbyAtOpenedBoxFromClosedBox,
        WaitForInputMask,
        StandbyAtOpenedBoxWithMaskFromOpenedBox,
        CloseBoxWithMask,
        WaitForLockWithMask,
        StandbyAtClosedBoxWithMaskFromOpenedBoxWithMask,
        WaitForReleaseBoxWithMask,
        ReturnToIdleFromClosedBoxWithMask,

        WaitForInputBoxWithMask,
        StandbyAtClosedBoxWithMaskFromIdle,
        WaitForUnlockWithMask,
        OpenBoxWithMask,
        StandbyAtOpenedBoxWithMaskFromClosedBoxWithMask,
        WaitForReleaseMask,
        StandbyAtOpenedBoxFromOpenedBoxWithMask,
        CloseBox,
        WaitForLock,
        StandbyAtClosedBoxFromOpenedBox,
        WaitForReleaseBox,
        ReturnToIdleFromClosedBox,
    }
}
