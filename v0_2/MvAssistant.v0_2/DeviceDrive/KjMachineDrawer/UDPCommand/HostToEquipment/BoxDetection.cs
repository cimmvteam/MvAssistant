using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.KjMachineDrawer.UDPCommand.HostToEquipment
{
   public  class BoxDetection:BaseHostToEquipmentCommand
    {
        public BoxDetection() : base(HostToEquipmentContent.BoxDetection)
        {

        }
    }

    public class BoxDetectionParameter : IHostToEquipmentCommandParameter
    {
        public string ToParameterText() { return string.Empty; }
       
    }
}
