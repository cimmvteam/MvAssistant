using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.KjMachineDrawer.UDPCommand.HostToEquipment
{
    public class WriteNetSetting:BaseHostToEquipmentCommand
    {
        public WriteNetSetting() : base(HostToEquipmentContent.WriteNetSetting)
        {

        }
    }

    public class WriteNetSettingParameter : IHostToEquipmentCommandParameter
    {
        public string ToParameterText() => string.Empty;
    }
}
