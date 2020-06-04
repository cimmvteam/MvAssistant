using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.KjMachineDrawer.UDPCommand.HostToEquipment
{
   public  class SetTimeOut:BaseHostToEquipmentCommand
    {
        public SetTimeOut() : base(HostToEquipmentContent.SetTimeOut)
        {

        }
        
    }
    public class SetTimeOutParameter : IHostToEquipmentCommandParameter
    {
        public int Seconds { get; set; }
        public  string ToParameterText() =>BaseCommand.CommandSplitSign + Seconds;
        
    }
}
