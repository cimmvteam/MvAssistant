using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.DeviceDrive.GudengLoadPort.TCPCommand.HostToLoadPort
{
    public class ManualClamperLock:BaseHostToLoadPortCommand
    {
        public ManualClamperLock() : base(LoadPortRequestContent.ManualClamperLock)
        {

        }
    }
}
