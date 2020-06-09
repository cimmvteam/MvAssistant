using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.GudengLoadPort
{
   public enum EventLoadportStatusCode
    {
        DockReady=0,
        UndockReady=1,
        Busy=2,
        Alarm=3,
        WaitInitial=4
    }

}
