using MvAssistant.v0_3.DeviceDrive.KjMachineDrawer.ReplyCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.KjMachineDrawer.DrawerEventArgs
{
    /// <summary>ReplyTrayMotion 事件處理程序的 Event Args </summary>
    public class OnReplyTrayMotionEventArgs : EventArgs
    {
        public ReplyResultCode ReplyResultCode { get; private set; }
        private OnReplyTrayMotionEventArgs() { }
        public OnReplyTrayMotionEventArgs(ReplyResultCode replyResultCode) : this() { ReplyResultCode = replyResultCode; }
    }
}
