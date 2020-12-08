using MaskAutoCleaner.v1_0.StateMachineBeta;
using MvAssistant.Mac.v1_0;
using MvAssistant.Mac.v1_0.JSon.RobotTransferFile;
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
        public BoxType BoxType { get; private set; }

        private MacStateMovingToCB1HomeFromDrawerEntryEventArgs() : base()
        {

        }
        public MacStateMovingToCB1HomeFromDrawerEntryEventArgs(BoxrobotTransferLocation drawerLocation,BoxType boxType ,object parameter) : base(parameter)
        {
            DrawerLocation = drawerLocation;
            BoxType = boxType;

        }
        public MacStateMovingToCB1HomeFromDrawerEntryEventArgs(BoxrobotTransferLocation drawerLocation,BoxType boxType) : this(drawerLocation,boxType ,null)
        {

        }
        public MacStateMovingToCB1HomeFromDrawerEntryEventArgs(object parameter) : this(BoxrobotTransferLocation.Dontcare, BoxType.DontCare, parameter)
        {

        }

    }
    
}
