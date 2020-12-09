using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Mac.Manifest
{
  
    public class MacEnumDeviceDrawerRange
    {
        public MacEnumDevice StartID { get; private set; }
        public MacEnumDevice EndID { get; private set; }
        public MacEnumDeviceDrawerRange()
        {
            StartID = MacEnumDevice.cabinet_drawer_01_01;
            EndID = MacEnumDevice.cabinet_drawer_07_05;
        }
    }
}
