using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.KjMachineDrawer.DrawerEventArgs
{
    /// <summary>ReplyBoxDetection事件程序的 Event Args </summary>
    public class OnReplyBoxDetectionEventArgs : EventArgs
    {
        public bool HasBox { get; private set; }
        private OnReplyBoxDetectionEventArgs() { }
        public OnReplyBoxDetectionEventArgs(bool hasBox) { HasBox = hasBox; }
    }
}
