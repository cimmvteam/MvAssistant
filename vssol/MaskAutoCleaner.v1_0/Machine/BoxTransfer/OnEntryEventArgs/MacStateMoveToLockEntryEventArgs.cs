using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaskAutoCleaner.v1_0.StateMachineBeta;
namespace MaskAutoCleaner.v1_0.Machine.BoxTransfer.OnEntryEventArgs
{
    public class MacStateMoveToLockEntryEventArgs : MacStateEntryEventArgs
    {
        public uint BoxType { get; private set; }
        private MacStateMoveToLockEntryEventArgs() : base()
        {

        }
        public MacStateMoveToLockEntryEventArgs( uint boxType, object parameter) : base(parameter)
        {
            
            BoxType = boxType;
        }
        public MacStateMoveToLockEntryEventArgs( uint boxType) : this( boxType, null)
        {

        }
        public MacStateMoveToLockEntryEventArgs(object parameter) : this( 0, parameter)
        {

        }
    }
}
