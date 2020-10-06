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
    /// <para>2020/09/23 King Add</para>
    /// </remarks>
    public  class MacStateMovingToCB1HomeClampedFromDrawerEntryEventArgs : MacStateEntryEventArgs
   {
        /// <summary>移動的目標點</summary>
        public BoxrobotTransferLocation DrawerLocation { get; private set; }

        private MacStateMovingToCB1HomeClampedFromDrawerEntryEventArgs() : base()
        {

        }
        public MacStateMovingToCB1HomeClampedFromDrawerEntryEventArgs(BoxrobotTransferLocation targetLocation, object parameter) : base(parameter)
        {
            DrawerLocation = targetLocation;

        }
        public MacStateMovingToCB1HomeClampedFromDrawerEntryEventArgs(BoxrobotTransferLocation targetLocation) : this(targetLocation, null)
        {

        }
        public MacStateMovingToCB1HomeClampedFromDrawerEntryEventArgs(object parameter) : this(BoxrobotTransferLocation.Dontcare, parameter)
        {

        }
    }
}
