using MvAssistant.v0_3.DeviceDrive.GudengLoadPort.ReplyCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.GudengLoadPort.LoadPortEventArgs
{
    public class OnBarcode_IDEventArgs : EventArgs
    {
        public EventBarcodeIDCode ReturnCode { get; private set; }
        public string BarcodeID { get; private set; }
        private OnBarcode_IDEventArgs() { }
        public OnBarcode_IDEventArgs(EventBarcodeIDCode rtnCode, string barCodeID) : this() { ReturnCode = rtnCode; BarcodeID = barCodeID; }
    }
}
