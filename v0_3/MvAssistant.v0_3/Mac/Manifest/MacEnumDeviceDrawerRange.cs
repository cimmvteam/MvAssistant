using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.Mac.Manifest
{
  
    public class MacEnumDeviceDrawerRange
    {
        public EnumMacDeviceId StartID { get; private set; }
        public EnumMacDeviceId EndID { get; private set; }
        public MacEnumDeviceDrawerRange()
        {
            StartID = EnumMacDeviceId.cabinet_drawer_01_01;
            EndID = EnumMacDeviceId.cabinet_drawer_07_05;
        }
    }
}
