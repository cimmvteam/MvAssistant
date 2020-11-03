using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaskAutoCleaner.v1_0.StateMachineBeta;
using MvAssistant.Mac.v1_0;

namespace MaskAutoCleaner.v1_0.Machine.BoxTransfer.OnEntryEventArgs
{
    public class MacStateMoveToLockEntryEventArgs : MacStateEntryEventArgs
    {
        public BoxType BoxType { get; private set; }
        private MacStateMoveToLockEntryEventArgs() : base()
        {

        }
        public MacStateMoveToLockEntryEventArgs(BoxType boxType, object parameter) : base(parameter)
        {
            
            BoxType = boxType;
        }
        public MacStateMoveToLockEntryEventArgs(BoxType boxType) : this( boxType, null)
        {

        }
        public MacStateMoveToLockEntryEventArgs(object parameter) : this( 0, parameter)
        {

        }
    }
}
