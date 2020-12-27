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
        public BoxType BoxType { get; private set; }
        private MacStateMoveToUnLockEntryEventArgs() : base()
        {

        }
        public MacStateMoveToUnLockEntryEventArgs(BoxType boxType, object parameter) : base(parameter)
        {
            
            BoxType = boxType;
        }
        public MacStateMoveToUnLockEntryEventArgs(BoxType boxType) : this( boxType, null)
        {

        }
        public MacStateMoveToUnLockEntryEventArgs(object parameter) : this(BoxType.DontCare, parameter)
        {

        }
    }
}
