using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.OpenStage
{
    public enum EnumMacMsOpenStageState
    {
        Start,
        Initial,

        Idle,

        WaitingForInputBox,
        WaitingForReleaseBox,

        ClosedBox,

        WaitingForUnlock,
        OpeningBox,
        WaitingForLock,
        ClosingBox,

        OpenedBox,

        WaitingForInputMask,
        WaitingForReleaseMask,

        OpenedBoxWithMask,

        ClosingBoxWithMask,
        WaitingForLockWithMask,
        OpeningBoxWithMask,
        WaitingForUnlickWithMask,
        
        ClosedBoxWithMask,

        WaitingForReleaseBoxWithMask,
        WaitingForInputBoxWithMask
    }
}
