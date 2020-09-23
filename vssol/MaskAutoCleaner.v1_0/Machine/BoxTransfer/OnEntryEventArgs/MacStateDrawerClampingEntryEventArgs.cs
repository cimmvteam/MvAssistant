using MaskAutoCleaner.v1_0.StateMachineBeta;
using MvAssistant.Mac.v1_0.JSon.RobotTransferFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.BoxTransfer.OnEntryEventArgs
{
    /// <summary>EnumMacMsBoxTransferState.DrawerClamping  的 OnEntry Event Args</summary>
    /// <remarks>
    /// <para>2020/09/23, King Add</para>
    /// </remarks>
    public class MacStateDrawerClampingEntryEventArgs: MacStateEntryEventArgs
    {
        /// <summary>移動的目標點</summary>
        public BoxrobotTransferLocation TargetLocation { get; private set; }
        public uint BoxType { get; private set; } 
        private MacStateDrawerClampingEntryEventArgs() : base()
        {

        }
        public MacStateDrawerClampingEntryEventArgs(BoxrobotTransferLocation targetLocation,uint boxType, object parameter) : base(parameter)
        {
            TargetLocation = targetLocation;
            BoxType = boxType;
        }
        public MacStateDrawerClampingEntryEventArgs(BoxrobotTransferLocation targetLocation,uint boxType) : this(targetLocation,boxType, null)
        {

        }
        public MacStateDrawerClampingEntryEventArgs(object parameter) : this(BoxrobotTransferLocation.Dontcare, 0,parameter)
        {

        }

    }
}
