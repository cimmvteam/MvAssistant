using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.KjMachineDrawer.UDPCommand.HostToEquipment
{
    public class INI : BaseHostToEquipmentCommand
    {
        public INI() : base(HostToEquipmentContent.INI)
        {

        }
    }
    public class INIParameter : IHostToEquipmentCommandParameter
    {
        // vs 2013
        //public  string ToParameterText() => "";
        public  string ToParameterText() { return "";}
    }
}
