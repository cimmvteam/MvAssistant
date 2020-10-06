﻿using MvAssistant.DeviceDrive.GudengLoadPort.ReplyCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.GudengLoadPort.LoadPortEventArgs
{
    public class OnLoadportStatusEventArgs : EventArgs
    {
        public EventLoadportStatusCode ReturnCode { get; private set; }
        private OnLoadportStatusEventArgs() { }
        public OnLoadportStatusEventArgs(EventLoadportStatusCode rtnCode) : this() { ReturnCode = rtnCode; }

    }
}
