using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.KjMachineDrawer.UDPCommand.HostToEquipment
{
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
        public override string GetCommandText<T>(T parameter) 
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
