using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.BoxTransfer
{
    public enum EnumMacMsBoxTransferTransition
    {
        PowerON,
        Initial,

        StandbyAtCB1Home,
        StandbyAtCB1HomeClamped,

        ChangeDirectionToCB2HomeFromCB1Home,
        ChangeDirectionToCB1HomeFromCB2Home,
        ChangeDirectionToCB2HomeClampedFromCB1HomeClamped,
        ChangeDirectionToCB1HomeClampedFromCB2HomeClamped,

        #region Move To Open Stage
        MoveToOpenStage,
        ClampInOpenStage,
        MoveToCB1HomeClampedFromOpenStage,
        StandbyAtCB1HomeClampedFromOpenStage,

        MoveToOpenStageForRelease,
        ReleaseInOpenStage,
        MoveToCB1HomeFromOpenStage,
        StandbyAtCB1HomeFromOpenStage,
        #endregion Move To Open Stage
    }
}
