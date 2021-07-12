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
    /// <summary>EnumMacBoxTransferState.DrawerClamping  的 OnEntry Event Args</summary>
    /// <remarks>
    /// <para>2020/09/23, King Add</para>
    /// </remarks>
    public class MacStateDrawerClampingEntryEventArgs: MacStateEntryEventArgs
    {
        /// <summary>移動的目標點</summary>
        public BoxrobotTransferLocation DrawerLocation { get; private set; }
        public EnumMacMaskBoxType BoxType { get; private set; } 
        private MacStateDrawerClampingEntryEventArgs() : base()
        {

        }
        public MacStateDrawerClampingEntryEventArgs(BoxrobotTransferLocation drawerLocation,EnumMacMaskBoxType boxType, object parameter) : base(parameter)
        {
            DrawerLocation = drawerLocation;
            BoxType = boxType;
        }
        public MacStateDrawerClampingEntryEventArgs(BoxrobotTransferLocation drawerLocation,EnumMacMaskBoxType boxType) : this(drawerLocation,boxType, null)
        {

        }
        public MacStateDrawerClampingEntryEventArgs(object parameter) : this(BoxrobotTransferLocation.Dontcare, EnumMacMaskBoxType.DontCare, parameter)
        {

        }

    }
}
