using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.KjMachineDrawer
{
   public  class OnReciveMessageEventArgs:EventArgs
    {
        public string Message { get; set; }
        public string IP { set; get; }
    }
}
