using MvAssistant.DeviceDrive.GudengLoadPort.ReplyCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.GudengLoadPort.LoadPortEventArgs
{
    public class OnClamperUnlockCompleteEventArgs : EventArgs
    {
        public EventClamperUnlockCompleteCode ReturnCode { get; private set; }
        private OnClamperUnlockCompleteEventArgs() { }
        public OnClamperUnlockCompleteEventArgs(EventClamperUnlockCompleteCode rtnCode) : this() { ReturnCode = rtnCode; }
    }
}
