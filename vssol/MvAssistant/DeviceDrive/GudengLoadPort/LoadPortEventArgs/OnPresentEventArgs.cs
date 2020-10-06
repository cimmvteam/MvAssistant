using MvAssistant.DeviceDrive.GudengLoadPort.ReplyCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.GudengLoadPort.LoadPortEventArgs
{
    public class OnPresentEventArgs : EventArgs
    {
        public EventPresentCode ReturnCode { get; private set; }
        private OnPresentEventArgs() { }
        public OnPresentEventArgs(EventPresentCode rtnCode) : this() { ReturnCode = rtnCode; }
    }
}
