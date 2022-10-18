using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.KjMachineDrawer.UDPCommand.HostToEquipment
{
   public  class SetMotionSpeed:BaseHostToEquipmentCommand
    {
        public SetMotionSpeed() : base(HostToEquipmentContent.SetMotionSpeed)
        {
        }

    }
    public class SetMotionSpeedParameter : IHostToEquipmentCommandParameter
    {
        public int Speed { get; set; }
         // vs 2013
        //public  string ToParameterText() => BaseCommand.CommandSplitSign + Speed;
        public string ToParameterText()
        {
         return    BaseCommand.CommandSplitSign + Speed;
        }        
            
        
    }
}
