using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.KjMachineDrawer.DrawerEventArgs
{
    /// <summary>ReplyPosition 事件程序的 Event Args</summary>
    public class OnReplyPositionEventArgs : EventArgs
    {
        public string IHOStatus { get; private set; }
        private OnReplyPositionEventArgs() { }
        public OnReplyPositionEventArgs(string ihoStatus) : this() { IHOStatus = ihoStatus; }
        public bool I
        {
            get
            {
                var i = IHOStatus.Substring(0, 1);
                var rtnV = false;
                if (i == "1") { rtnV = true; }
                return rtnV;
            }

        }
        public bool H
        {
            get
            {
                var h = IHOStatus.Substring(1, 1);
                var rtnV = false;
                if (h == "1") { rtnV = true; }
                return rtnV;
            }
        }
        public bool O
        {
            get
            {
                var o = IHOStatus.Substring(2, 1);
                var rtnV = false;
                if (o == "1") { rtnV = true; }
                return rtnV;
            }
        }
    }
}
