using MvAssistant.Mac.v1_0.Hal.Component.Door;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.ComponentFake.Door
{
    [GuidAttribute("ADAA0C85-24C0-43ED-BC21-8BC907CD5CEE")]
    public class DoorFake : HalFakeBase, IHalDoor
    {
        private string doorStatus;
        private bool opendoor;
        private bool closedoor;
        #region for test script
        public string DoorStatus
        {
            get { return doorStatus; }
            set { doorStatus = value; }
        }
        public bool Opendoor
        {
            get { return opendoor; }
            set { opendoor = value; }
        }
        public bool Closedoor
        {
            get { return closedoor; }
            set { closedoor = value; }
        }
        #endregion

        public string CheckDoorStatus()
        {
            return doorStatus;
        }

        public bool OpenDoor()
        {
            return opendoor;
        }

        public bool CloseDoor()
        {
            return closedoor;
        }

        public void HalZeroCalibration()
        {
            throw new NotImplementedException();
        }
    }
}
