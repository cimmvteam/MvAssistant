﻿using MvAssistant.v0_3.DeviceDrive.KjMachineDrawer.ReplyCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.KjMachineDrawer.DrawerEventArgs
{
    /// <summary> OnReplyBrightLED Evrnt Args</summary>
    public class OnReplyBrightLEDEventArgs : EventArgs
    {
        public ReplyResultCode ReplyResultCode { get; private set; }
        private OnReplyBrightLEDEventArgs() { }
        public OnReplyBrightLEDEventArgs(ReplyResultCode replyResultCode) : this() { ReplyResultCode = replyResultCode; }

    }
}
