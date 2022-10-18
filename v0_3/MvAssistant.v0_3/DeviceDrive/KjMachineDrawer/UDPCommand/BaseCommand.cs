using MvAssistant.v0_3.DeviceDrive.KjMachineDrawer.UDPCommand.HostToEquipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.KjMachineDrawer.UDPCommand
{
    /// <summary>指令的基礎類別</summary>
    public abstract class BaseCommand
    {
        /// <summary>指令的前綴符號</summary>
        public const string CommandPrefixText = "~";
        /// <summary>指令的後綴符號</summary>
        public const string CommandPostfixText = "@";

        /// <summary>取得最原始的指令文字之後將這個符號取代為實際的指令</summary>
        public const string CommandTextReplaceSignPair = "[]";

        /// <summary>指令的分隔符號</summary>
        public const string CommandSplitSign = ",";
        // public static string TestStatic = "4";
        /// <summary>最原始的指令文字</summary>
        /// <returns></returns>
        protected abstract string GetRawCommandText();
       // public abstract string GetCommandText<T>(T parameterText) where T: IHostToEquipmentCommandParameter;
    }
}
