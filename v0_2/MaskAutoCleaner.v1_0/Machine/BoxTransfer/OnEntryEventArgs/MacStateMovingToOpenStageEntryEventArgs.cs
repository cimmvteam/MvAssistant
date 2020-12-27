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
    /// <summary>
    /// <remarks>
    /// 
    /// </remarks>
    public class MacStateMovingToOpenStageEntryEventArgs : MacStateEntryEventArgs
    {
        
        //public BoxrobotTransferLocation DrawerLocation { get; private set; }
        public BoxType BoxType { get; private set; } 
        private MacStateMovingToOpenStageEntryEventArgs() : base()
        {

        }
        public MacStateMovingToOpenStageEntryEventArgs(/*BoxrobotTransferLocation drawerLocation,*/BoxType boxType, object parameter) : base(parameter)
        {
           // DrawerLocation = drawerLocation;
            BoxType = boxType;
        }
        public MacStateMovingToOpenStageEntryEventArgs(/**BoxrobotTransferLocation drawerLocation,*/BoxType boxType) : this(/*drawerLocation,*/boxType, null)
        {

        }
        public MacStateMovingToOpenStageEntryEventArgs(object parameter) : this(/*BoxrobotTransferLocation.Dontcare,**/ BoxType.DontCare, parameter)
        {

        }

    }
}
