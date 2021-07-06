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
   public  class MacStateMovingToCB1HomeFromDrawerEntryEventArgs : MacStateEntryEventArgs
    {
        /// <summary>移動的目標點</summary>
        public BoxrobotTransferLocation DrawerLocation { get; private set; }
        public MacMaskBoxType BoxType { get; private set; }

        private MacStateMovingToCB1HomeFromDrawerEntryEventArgs() : base()
        {

        }
        public MacStateMovingToCB1HomeFromDrawerEntryEventArgs(BoxrobotTransferLocation drawerLocation,MacMaskBoxType boxType ,object parameter) : base(parameter)
        {
            DrawerLocation = drawerLocation;
            BoxType = boxType;

        }
        public MacStateMovingToCB1HomeFromDrawerEntryEventArgs(BoxrobotTransferLocation drawerLocation,MacMaskBoxType boxType) : this(drawerLocation,boxType ,null)
        {

        }
        public MacStateMovingToCB1HomeFromDrawerEntryEventArgs(object parameter) : this(BoxrobotTransferLocation.Dontcare, MacMaskBoxType.DontCare, parameter)
        {

        }

    }
    
}
