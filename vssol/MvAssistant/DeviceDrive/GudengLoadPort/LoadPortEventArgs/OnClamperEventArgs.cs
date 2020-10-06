using MvAssistant.DeviceDrive.GudengLoadPort.ReplyCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.GudengLoadPort.LoadPortEventArgs
{
    public class OnClamperEventArgs : EventArgs
    {
        public EventClamperCode ReturnCode { get; private set; }
        private OnClamperEventArgs() { }
        public OnClamperEventArgs(EventClamperCode rtnCode) : this() { ReturnCode = rtnCode; }
    }
}
