using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.KjMachineDrawer.UDPCommand.HostToEquipment
{
   public  class LCDMsg:BaseHostToEquipmentCommand
    {
        public LCDMsg() : base(HostToEquipmentContent.LCDMsg)
        {

        }
    }
    public class LSDMsgParameter : IHostToEquipmentCommandParameter
    {
        public string Message { get; set; }

        public string ToParameterText()
        {
            return   BaseHostToEquipmentCommand.CommandSplitSign + Message;
        }

        
    }
}
