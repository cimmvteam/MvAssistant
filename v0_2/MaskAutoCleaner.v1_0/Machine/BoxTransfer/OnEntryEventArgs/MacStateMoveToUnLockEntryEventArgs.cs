using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaskAutoCleaner.v1_0.StateMachineBeta;
using MvAssistant.v0_2.Mac;

namespace MaskAutoCleaner.v1_0.Machine.BoxTransfer.OnEntryEventArgs
{
    public class MacStateMoveToUnLockEntryEventArgs : MacStateEntryEventArgs
    {
        public MacMaskBoxType BoxType { get; private set; }
        private MacStateMoveToUnLockEntryEventArgs() : base()
        {

        }
        public MacStateMoveToUnLockEntryEventArgs(MacMaskBoxType boxType, object parameter) : base(parameter)
        {
            
            BoxType = boxType;
        }
        public MacStateMoveToUnLockEntryEventArgs(MacMaskBoxType boxType) : this( boxType, null)
        {

        }
        public MacStateMoveToUnLockEntryEventArgs(object parameter) : this(MacMaskBoxType.DontCare, parameter)
        {

        }
    }
}
