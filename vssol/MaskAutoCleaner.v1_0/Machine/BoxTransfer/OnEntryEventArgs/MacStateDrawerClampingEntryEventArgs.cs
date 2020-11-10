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
    /// <summary>EnumMacBoxTransferState.DrawerClamping  的 OnEntry Event Args</summary>
    /// <remarks>
    /// <para>2020/09/23, King Add</para>
    /// </remarks>
    public class MacStateDrawerClampingEntryEventArgs: MacStateEntryEventArgs
    {
        /// <summary>移動的目標點</summary>
        public BoxrobotTransferLocation DrawerLocation { get; private set; }
        public BoxType BoxType { get; private set; } 
        private MacStateDrawerClampingEntryEventArgs() : base()
        {

        }
        public MacStateDrawerClampingEntryEventArgs(BoxrobotTransferLocation drawerLocation,BoxType boxType, object parameter) : base(parameter)
        {
            DrawerLocation = drawerLocation;
            BoxType = boxType;
        }
        public MacStateDrawerClampingEntryEventArgs(BoxrobotTransferLocation drawerLocation,BoxType boxType) : this(drawerLocation,boxType, null)
        {

        }
        public MacStateDrawerClampingEntryEventArgs(object parameter) : this(BoxrobotTransferLocation.Dontcare, 0,parameter)
        {

        }

    }
}
