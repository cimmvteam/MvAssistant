using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.KjMachineDrawer.UDPCommand.HostToEquipment
{
    public class PositionRead : BaseHostToEquipmentCommand
    {
        public PositionRead():base(HostToEquipmentContent.PositionRead)
        {
        }
    }

    public class PositionReadParameter : IHostToEquipmentCommandParameter
    {
        public string ToParameterText() => string.Empty;
    }
}
