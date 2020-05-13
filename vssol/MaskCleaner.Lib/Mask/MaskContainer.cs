using MvLib.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Mask
{
    public class MaskContainer
    {
        public EnumMaskContainerType ContinerType;//可能永遠都不會知道是什麼Pod
        public MacMachineBase Mask;
        public string Position;
        public EnumMaskContainerStatus Status = EnumMaskContainerStatus.Unknown;
        public string PodBarcode { get; set; }//Load Port Dock 後讀取
        public string BoxBarcode { get; set; }//Drawer Dock 後讀取
        public string MaskBarcodeInAccount { get; set; }//TAP發來的MaskBarcode
    }
}
