using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.KjMachineDrawer.UDPCommand.HostToEquipment
{
    public class WriteNetSetting:BaseHostToEquipmentCommand
    {
        public WriteNetSetting() : base(HostToEquipmentContent.WriteNetSetting)
        {

        }
    }

    public class WriteNetSettingParameter : IHostToEquipmentCommandParameter
    {
        // vs 2013
        //public string ToParameterText() => string.Empty;
        public string ToParameterText() { return string.Empty; }
    }
}
