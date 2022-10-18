using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.KjMachineDrawer.UDPCommand.HostToEquipment
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
        public string ParameterValue { get; set; }

        public string ToParameterText()
        {
            var sign = BaseHostToEquipmentCommand.CommandSplitSign;
            var rtnV = sign + ((int)SetParameterType).ToString() + sign + ParameterValue;
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
