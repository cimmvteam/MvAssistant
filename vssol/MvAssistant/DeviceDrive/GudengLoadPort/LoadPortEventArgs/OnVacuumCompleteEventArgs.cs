using MvAssistant.DeviceDrive.GudengLoadPort.ReplyCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.GudengLoadPort.LoadPortEventArgs
{
    public class OnVacuumCompleteEventArgs : EventArgs
    {
        public EventVacuumCompleteCode ReturnCode { get; private set; }
        private OnVacuumCompleteEventArgs() { }
        public OnVacuumCompleteEventArgs(EventVacuumCompleteCode rtnCode) : this() { ReturnCode = rtnCode; }
    }
}
