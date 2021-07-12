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
    /// <summary>EnumMacBoxTransferState.MovingToDrawer  的 OnEntry Event Args</summary>
    /// <remarks>
    /// <para>2020/09/23, King Add</para>
    /// </remarks>
    public class MacStateMovingToDrawerEntryEventArgs: MacStateEntryEventArgs
    {
        /// <summary>移動的目標點</summary>
        public BoxrobotTransferLocation DrawerLocation { get; private set; }
        public EnumMacMaskBoxType BoxType { get; private set; }
        private MacStateMovingToDrawerEntryEventArgs() : base()
        {

        }
        public MacStateMovingToDrawerEntryEventArgs(BoxrobotTransferLocation drawerLocation, EnumMacMaskBoxType boxType,object parameter ) :base(parameter)
        {
            DrawerLocation = drawerLocation;
            BoxType = boxType;


        }
        public MacStateMovingToDrawerEntryEventArgs(BoxrobotTransferLocation drawerLocation,EnumMacMaskBoxType boxType) : this(drawerLocation, boxType, null)
        {
            
        }
        public MacStateMovingToDrawerEntryEventArgs(object parameter) : this(BoxrobotTransferLocation.Dontcare,EnumMacMaskBoxType.DontCare,parameter)
        {

        }

    }
}
