using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.KjMachineDrawer.UDPCommand.HostToEquipment
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
        public  string ToParameterText() => BaseCommand.CommandSplitSign + Speed;
        
            
        
    }
}
