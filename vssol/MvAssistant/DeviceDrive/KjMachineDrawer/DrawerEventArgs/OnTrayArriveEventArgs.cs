using MvAssistant.DeviceDrive.KjMachineDrawer.ReplyCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.KjMachineDrawer.DrawerEventArgs
{
    /// <summary>TrayArrive 事件程序的Event Args</summary>
    public class OnTrayArriveEventArgs : EventArgs
    {
        public TrayArriveType TrayArriveType { get; private set; }
        private OnTrayArriveEventArgs() { }
        public OnTrayArriveEventArgs(TrayArriveType trayArriveType) : this() { TrayArriveType = trayArriveType; }
    }
}
