using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.KjMachineDrawer.UDPCommand.HostToEquipment
{
   public  class TrayMotion:BaseHostToEquipmentCommand
    {
        public TrayMotion() : base(HostToEquipmentContent.TrayMotion)
        {

        }
    }

    public class TrayMotionParameter : IHostToEquipmentCommandParameter
    {
        public TrayMotionType TrayMotionType { get; set; }
         // vs 2013
        //public string ToParameterText() => BaseHostToEquipmentCommand.CommandSplitSign + ((int)TrayMotionType).ToString();
        public string ToParameterText() { return BaseHostToEquipmentCommand.CommandSplitSign + ((int)TrayMotionType).ToString(); }
    }
    

    public enum TrayMotionType
    {
        Home=0,
        Out=1,
        In=2,
    }
}
