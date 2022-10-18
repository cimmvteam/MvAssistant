using MvAssistant.v0_3.DeviceDrive.GudengLoadPort.ReplyCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.GudengLoadPort.LoadPortEventArgs
{
    public class OnPlacementEventArgs : EventArgs
    {
        public EventPlacementCode ReturnCode { get; private set; }
        private OnPlacementEventArgs() { }
        public OnPlacementEventArgs(EventPlacementCode rtnCode) : this() { ReturnCode = rtnCode; }
    }
}
