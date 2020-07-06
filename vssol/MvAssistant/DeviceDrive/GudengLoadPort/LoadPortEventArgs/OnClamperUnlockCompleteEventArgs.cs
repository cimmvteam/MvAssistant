using MvAssistant.DeviceDrive.GudengLoadPort.ReplyCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.GudengLoadPort.LoadPortEventArgs
{
    public class OnClamperLockCompleteEventArgs : EventArgs
    {
        public EventClamperLockCompleteCode ReturnCode { get; private set; }
        private OnClamperLockCompleteEventArgs() { }
        public OnClamperLockCompleteEventArgs(EventClamperLockCompleteCode rtnCode) : this() { ReturnCode = rtnCode; }
    }
}
