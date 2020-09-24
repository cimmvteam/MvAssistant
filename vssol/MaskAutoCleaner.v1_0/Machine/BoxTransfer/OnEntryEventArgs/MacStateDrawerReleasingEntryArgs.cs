using MaskAutoCleaner.v1_0.StateMachineBeta;
using MvAssistant.Mac.v1_0.JSon.RobotTransferFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.BoxTransfer.OnEntryEventArgs
{

    /// <summary>2020/09/24, King Add </summary>
    public  class MacStateDrawerReleasingEntryArgs : MacStateEntryEventArgs
    {
        /// <summary>移動的起點</summary>
        public BoxrobotTransferLocation DrawerLocation { get; private set; }

        private MacStateDrawerReleasingEntryArgs() : base()
        {

        }
        public MacStateDrawerReleasingEntryArgs(BoxrobotTransferLocation drawerLocation, object parameter) : base(parameter)
        {
            DrawerLocation = drawerLocation;

        }
        public MacStateDrawerReleasingEntryArgs(BoxrobotTransferLocation drawerLocation) : this(drawerLocation, null)
        {

        }
        public MacStateDrawerReleasingEntryArgs(object parameter) : this(BoxrobotTransferLocation.Dontcare, parameter)
        {

        }

    }
}
