using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaskAutoCleaner.v1_0.StateMachineBeta;
namespace MaskAutoCleaner.v1_0.Machine.BoxTransfer.OnEntryEventArgs
{
    public class MacStateOpenStageClampingEntryEventArgs : MacStateEntryEventArgs
    {
        public uint BoxType { get; private set; }
        private MacStateOpenStageClampingEntryEventArgs() : base()
        {

        }
        public MacStateOpenStageClampingEntryEventArgs( uint boxType, object parameter) : base(parameter)
        {
            
            BoxType = boxType;
        }
        public MacStateOpenStageClampingEntryEventArgs( uint boxType) : this( boxType, null)
        {

        }
        public MacStateOpenStageClampingEntryEventArgs(object parameter) : this( 0, parameter)
        {

        }
    }
}
