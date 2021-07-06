using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaskAutoCleaner.v1_0.StateMachineBeta;
using MvAssistant.v0_2.Mac;

namespace MaskAutoCleaner.v1_0.Machine.BoxTransfer.OnEntryEventArgs
{
    public class MacStateMoveToLockEntryEventArgs : MacStateEntryEventArgs
    {
        public MacMaskBoxType BoxType { get; private set; }
        private MacStateMoveToLockEntryEventArgs() : base()
        {

        }
        public MacStateMoveToLockEntryEventArgs(MacMaskBoxType boxType, object parameter) : base(parameter)
        {
            
            BoxType = boxType;
        }
        public MacStateMoveToLockEntryEventArgs(MacMaskBoxType boxType) : this( boxType, null)
        {

        }
        public MacStateMoveToLockEntryEventArgs(object parameter) : this( 0, parameter)
        {

        }
    }
}
