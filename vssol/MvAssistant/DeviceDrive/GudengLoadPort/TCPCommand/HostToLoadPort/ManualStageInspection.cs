using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.GudengLoadPort.TCPCommand.HostToLoadPort
{
   public class ManualStageInspection:BaseHostToLoadPortCommand
    {
        public ManualStageInspection() : base(LoadPortRequestContent.ManualStageInspection)
        {

        }
    }
}
