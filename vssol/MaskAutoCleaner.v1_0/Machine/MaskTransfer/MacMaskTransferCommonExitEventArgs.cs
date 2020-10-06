using MaskAutoCleaner.v1_0.StateMachineBeta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.MaskTransfer
{
    public class MacMaskTransferCommonExitEventArgs : MacStateExitEventArgs
    {
        public MacMaskTransferCommonResult Result { get; private set; }

        private MacMaskTransferCommonExitEventArgs() { }

        public MacMaskTransferCommonExitEventArgs(MacMaskTransferCommonResult result) : this()
        {
            Result = result;
        }
    }

    public enum MacMaskTransferCommonResult
    {
        Wait,
        Complete,
        Fail,
        TimeOut,

        ReadyToMoveToLoadPortA,
        ReadyToMoveToLoadPortAForRelease,
        ReadyToMoveToLoadPortB,
        ReadyToMoveToLoadPortBForRelease,
        ReadyToMoveToInspectionCh,
        ReadyToMoveToInspectionChForRelease,
        ReadyToMoveToInspectionChGlass,
        ReadyToMoveToInspectionChGlassForRelease,
        ReadyToMoveToOpenStage,
        ReadyToMoveToOpenStageForRelease,
        ReadyToMoveToBarcodeReaderClamped,
    }

}
