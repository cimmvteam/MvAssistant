using MvAssistant.DeviceDrive.KjMachineDrawer.ReplyCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.KjMachineDrawer.DrawerEventArgs
{
    /// <summary>ReplySetTimeOut 事件程序</summary>
    public class OnReplySetTimeOutEventArgs : EventArgs
    {
        public ReplyResultCode ReplyResultCode { get; private set; }
        private OnReplySetTimeOutEventArgs() { }
        public OnReplySetTimeOutEventArgs(ReplyResultCode replyResultCode) : this() { ReplyResultCode = replyResultCode; }
    }
}
