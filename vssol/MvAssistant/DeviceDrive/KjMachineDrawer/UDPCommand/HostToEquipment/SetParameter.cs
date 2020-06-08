using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.KjMachineDrawer.UDPCommand.HostToEquipment
{
   public  class SetParameter:BaseHostToEquipmentCommand
    {
        public SetParameter() : base(HostToEquipmentContent.SetParameter)
        {

        }
    }

    public class SetParameterParameter : IHostToEquipmentCommandParameter
    {
        public SetParameterType SetParameterType { get; set; }
        public string ExtendText { get; set; }

        public string ToParameterText()
        {
            var sign = BaseHostToEquipmentCommand.CommandSplitSign;
            var rtnV = sign + ((int)SetParameterType).ToString() + sign + ExtendText;
           return rtnV;
        }


    }

    public enum SetParameterType
    {
        Home_position=3,
        Out_side_position=4,
        In_side_position=5,
        IP_address=6,
        SubMask=7,
        Gateway_address=8
    }
}
