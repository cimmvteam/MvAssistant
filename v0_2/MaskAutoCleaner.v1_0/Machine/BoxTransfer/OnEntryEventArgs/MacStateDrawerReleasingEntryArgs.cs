using MaskAutoCleaner.v1_0.StateMachineBeta;
using MvAssistant.v0_2.Mac;
using MvAssistant.v0_2.Mac.JSon.RobotTransferFile;
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
        public MacMaskBoxType BoxType { get; private set; }

        private MacStateDrawerReleasingEntryArgs() : base()
        {

        }
        public MacStateDrawerReleasingEntryArgs(BoxrobotTransferLocation drawerLocation,MacMaskBoxType boxType, object parameter) : base(parameter)
        {
            DrawerLocation = drawerLocation;
            BoxType = boxType;

        }
        public MacStateDrawerReleasingEntryArgs(BoxrobotTransferLocation drawerLocation, MacMaskBoxType boxType) : this(drawerLocation, boxType,null)
        {

        }
        public MacStateDrawerReleasingEntryArgs(object parameter) : this(BoxrobotTransferLocation.Dontcare,MacMaskBoxType.DontCare ,parameter)
        {

        }

    }
}
