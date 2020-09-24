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
    /// <remarks>2020/09/24, King Add </remarks>
    public class MacStateMovingToDrawerForReleaseEntryEventArgs:MacStateEntryEventArgs
    {
        /// <summary>移動的起點</summary>
        public BoxrobotTransferLocation DrawerLocation { get; private set; }

        private MacStateMovingToDrawerForReleaseEntryEventArgs() : base()
        {

        }
        public MacStateMovingToDrawerForReleaseEntryEventArgs(BoxrobotTransferLocation drawerLocation, object parameter) : base(parameter)
        {
            DrawerLocation = drawerLocation;

        }
        public MacStateMovingToDrawerForReleaseEntryEventArgs(BoxrobotTransferLocation drawerLocation) : this(drawerLocation, null)
        {

        }
        public MacStateMovingToDrawerForReleaseEntryEventArgs(object parameter) : this(BoxrobotTransferLocation.Dontcare, parameter)
        {

        }

    }
}
