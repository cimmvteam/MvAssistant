using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.GudengLoadPort.LoadPortEventArgs
{
    public class OnRFIDEventArgs : EventArgs
    {
        public string RFID { get; private set; }
        private OnRFIDEventArgs() { }
        public OnRFIDEventArgs(string rfid) : this() { RFID = rfid; }
    }
}
