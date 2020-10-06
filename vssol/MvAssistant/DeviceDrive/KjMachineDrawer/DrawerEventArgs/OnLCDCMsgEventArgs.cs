using MvAssistant.DeviceDrive.KjMachineDrawer.ReplyCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.KjMachineDrawer.DrawerEventArgs
{
    public class OnLCDCMsgEventArgs:EventArgs
    {
        public ReplyResultCode ReplyResultCode { get; private set; }
        private OnLCDCMsgEventArgs() { }
        public OnLCDCMsgEventArgs(ReplyResultCode replyResultCode) : this() { ReplyResultCode = replyResultCode; }
    }
}
