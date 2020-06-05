using MvAssistant.DeviceDrive.KjMachineDrawer.UDPCommand.HostToEquipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.KjMachineDrawer.UDPCommand
{
    public abstract class BaseCommand
    {
        public const string CommandPrefixText = "~";
        public const string CommandPostfixText = "@";
        public const string CommandTextReplaceSignPair = "[]";
        public const string CommandSplitSign = ",";
        public static string TestStatic = "4";
        protected abstract string GetRawCommandText();
        public abstract string GetCommandText<T>(T parameterText) where T: IHostToEquipmentCommandParameter;
    }
}
