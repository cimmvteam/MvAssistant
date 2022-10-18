using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.GudengLoadPort.TCPCommand.HostToLoadPort
{
    public class AskReticleExistStatus:BaseHostToLoadPortCommand
    {
        public AskReticleExistStatus() : base(LoadPortRequestContent.AskReticleExistStatus)
        {

        }
    }
}
