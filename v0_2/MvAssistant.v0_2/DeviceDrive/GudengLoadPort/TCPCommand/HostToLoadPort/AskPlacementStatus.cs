﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.DeviceDrive.GudengLoadPort.TCPCommand.HostToLoadPort
{
   public class AskPlacementStatus:BaseHostToLoadPortCommand
    {
        public AskPlacementStatus() : base(LoadPortRequestContent.AskPlacementStatus)
        {

        }
    }
}
