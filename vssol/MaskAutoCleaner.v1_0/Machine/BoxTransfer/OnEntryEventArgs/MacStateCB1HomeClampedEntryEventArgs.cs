using MaskAutoCleaner.v1_0.StateMachineBeta;
using MvAssistant.Mac.v1_0.JSon.RobotTransferFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.BoxTransfer.OnEntryEventArgs
{

    /// <summary></summary>
    /// <remarks>
    /// <para>
    /// 2020/09/23 King Add
    /// </para>
    /// </remarks>
    public class MacStateCB1HomeClampedEntryEventArgs : MacStateEntryEventArgs
    {
        /// <summary>移動的目標點</summary>
        public BoxrobotTransferLocation TargetLocation { get; private set; }
      
        private MacStateCB1HomeClampedEntryEventArgs() : base()
        {

        }
        public MacStateCB1HomeClampedEntryEventArgs(BoxrobotTransferLocation targetLocation,  object parameter) : base(parameter)
        {
            TargetLocation = targetLocation;
            
        }
        public MacStateCB1HomeClampedEntryEventArgs(BoxrobotTransferLocation targetLocation) : this(targetLocation, null)
        {

        }
        public MacStateCB1HomeClampedEntryEventArgs(object parameter) : this(BoxrobotTransferLocation.Dontcare, parameter)
        {

        }

    }
}
