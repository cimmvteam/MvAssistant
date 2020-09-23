using MaskAutoCleaner.v1_0.StateMachineBeta;
using MvAssistant.Mac.v1_0.JSon.RobotTransferFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.BoxTransfer.OnEntryEventArgs
{
    /// <summary>EnumMacMsBoxTransferState.MovingToDrawer  的 OnEntry Event Args</summary>
    /// <remarks>
    /// <para>2020/09/23, King Add</para>
    /// </remarks>
    public class MacStateMovingToDrawerEntryEventArgs: MacStateEntryEventArgs
    {
        /// <summary>移動的目標點</summary>
        public BoxrobotTransferLocation TargetLocation { get; private set; }
      
        private MacStateMovingToDrawerEntryEventArgs() : base()
        {

        }
        public MacStateMovingToDrawerEntryEventArgs(BoxrobotTransferLocation targetLocation, object parameter ) :base(parameter)
        {
            TargetLocation = targetLocation;
           
        }
        public MacStateMovingToDrawerEntryEventArgs(BoxrobotTransferLocation targetLocation) : this(targetLocation, null)
        {

        }
        public MacStateMovingToDrawerEntryEventArgs(object parameter) : this(BoxrobotTransferLocation.Dontcare,parameter)
        {

        }

    }
}
