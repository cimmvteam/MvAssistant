﻿using MvAssistant.DeviceDrive.GudengLoadPort.ReplyCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.GudengLoadPort.LoadPortEventArgs
{
    public class OnStagePositionEventArgs : EventArgs
    {
        public EventStagePositionCode ReturnCode { get; private set; }
        private OnStagePositionEventArgs() { }
        public OnStagePositionEventArgs(EventStagePositionCode rtnCode) : this() { ReturnCode = rtnCode; }
    }
}
