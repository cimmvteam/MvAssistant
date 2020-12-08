using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Manifest
{
  
    public class MacEnumDeviceDrawerRange
    {
        public MacEnumDevice StartID { get; }
        public MacEnumDevice EndID { get; }
        public MacEnumDeviceDrawerRange()
        {
            StartID = MacEnumDevice.cabinet_drawer_01_01;
            EndID = MacEnumDevice.cabinet_drawer_07_05;
        }
    }
}
