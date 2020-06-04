using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.KjMachineDrawer.UDPCommand.HostToEquipment
{
    public class INI : BaseHostToEquipmentCommand
    {
        public INI() : base(HostToEquipmentContent.INI)
        {

        }
    }
    public class INIParameter : IHostToEquipmentCommandParameter
    {
        public  string ToParameterText() => "";
    }
}
