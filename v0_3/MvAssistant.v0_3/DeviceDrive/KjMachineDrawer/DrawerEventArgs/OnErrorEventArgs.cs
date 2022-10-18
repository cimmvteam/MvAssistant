using MvAssistant.v0_3.DeviceDrive.KjMachineDrawer.ReplyCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.KjMachineDrawer.DrawerEventArgs
{
    /// <summary>Error 事件程序的 Event Args</summary>
    public class OnErrorEventArgs : EventArgs
    {
        public ReplyErrorCode ReplyErrorCode { get; private set; }
        private OnErrorEventArgs() { }
        public OnErrorEventArgs(ReplyErrorCode replyErrorCode) : this()
        {
            ReplyErrorCode = replyErrorCode;
        }
    }
}
