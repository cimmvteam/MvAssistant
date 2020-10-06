using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.GudengLoadPort.LoadPortEventArgs
{
    public class OnReceviceRtnFromServerEventArgs : EventArgs
    {
        public string RtnContent { get; private set; }
        public OnReceviceRtnFromServerEventArgs(string rtnContent)
        {
            RtnContent = rtnContent;
        }
    }
}
