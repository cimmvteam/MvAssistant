using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.KjMachineDrawer.UDPCommand.HostToEquipment
{
    /// <summary>控制端到 Device(Drawer) 指令的基礎類別</summary>
    public abstract class BaseHostToEquipmentCommand : BaseCommand
    {
        public HostToEquipmentContent CommandCategory { get; private set; }
        private BaseHostToEquipmentCommand()
        {

        }
        public BaseHostToEquipmentCommand(HostToEquipmentContent commandCategory) : this()
        {
            CommandCategory = commandCategory;
        }
        
        protected override string GetRawCommandText()
        {
            var stringCode = CommandPrefixText + CommandCategory.GetStringCode() + BaseCommand.CommandSplitSign + CommandCategory.ToString() + CommandTextReplaceSignPair + BaseCommand.CommandPostfixText;
            return stringCode;
        }
        public  string GetCommandText<T>(T parameter) where T : IHostToEquipmentCommandParameter
        {
            var commandText = GetRawCommandText().Replace(CommandTextReplaceSignPair, parameter.ToParameterText());
            return commandText;
        }



    }

    public interface IHostToEquipmentCommandParameter
    {
           string ToParameterText(); 
    }

}
