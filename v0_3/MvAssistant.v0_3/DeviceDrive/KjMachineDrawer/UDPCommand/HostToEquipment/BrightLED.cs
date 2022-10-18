using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.KjMachineDrawer.UDPCommand.HostToEquipment
{
    public class BrightLED : BaseHostToEquipmentCommand
    {
        public BrightLED() : base(HostToEquipmentContent.BrightLED)
        {

        }
    }

    public class BrightLEDParameter : IHostToEquipmentCommandParameter
    {
        public BrightLEDType BrightLEDType { get; set; }
        // vs 2013
        //public string ToParameterText() => BaseHostToEquipmentCommand.CommandSplitSign+((int)BrightLEDType).ToString();
        public string ToParameterText() 
        {
          return    BaseHostToEquipmentCommand.CommandSplitSign + ((int)BrightLEDType).ToString(); 
        }
    }
   
    public enum BrightLEDType
    {
        AllOff=0,
        GreenOn=1,
        RedOn=2,
        AllOn=3

    }
}
