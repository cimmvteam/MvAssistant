using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.OpenStage
{
    public enum EnumMacOpenStageState
    {
        Start,
        Initial,

        Idle,

        WaitingForInputBox,
        WaitingForReleaseBox,

        ClosedBox,
        ClosedBoxForRelease,

        WaitingForUnlock,
        OpeningBox,
        WaitingForLock,
        ClosingBox,

        OpenedBox,
        OpenedBoxForClose,

        WaitingForInputMask,
        WaitingForReleaseMask,

        OpenedBoxWithMask,
        OpenedBoxWithMaskForClose,

        ClosingBoxWithMask,
        WaitingForLockWithMask,
        OpeningBoxWithMask,
        WaitingForUnlockWithMask,
        
        ClosedBoxWithMask,
        ClosedBoxWithMaskForRelease,

        WaitingForReleaseBoxWithMask,
        WaitingForInputBoxWithMask
    }
}
