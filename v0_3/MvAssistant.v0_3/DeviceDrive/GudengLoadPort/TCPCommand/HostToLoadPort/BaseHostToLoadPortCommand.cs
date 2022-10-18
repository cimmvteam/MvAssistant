using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.GudengLoadPort.TCPCommand.HostToLoadPort
{
    public abstract class BaseHostToLoadPortCommand : BaseTCPCommand
    {
        public LoadPortRequestContent CommandCategory { get; private set; }
        private BaseHostToLoadPortCommand()
        {

        }
        public BaseHostToLoadPortCommand(LoadPortRequestContent commandCategory) : this()
        {
            CommandCategory = commandCategory;
        }
        
        protected override string GetRawCommandText()
        {
            var stringCode = CommandPrefixText + CommandCategory.GetStringCode() + BaseTCPCommand.CommandSplitSign + CommandCategory.ToString() + CommandTextReplaceSignPair + BaseTCPCommand.CommandPostfixText;
            return stringCode;
        }
        public  string GetCommandText<T>(T parameter) where T : IHostToLoadPortCommandParameter
        {
            if (parameter == null)
            {
                return GetRawCommandText().Replace(BaseTCPCommand.CommandTextReplaceSignPair, "");
            }
            else
            {
                return GetRawCommandText().Replace(BaseTCPCommand.CommandTextReplaceSignPair, parameter.ToParameterText());
            }
        }



    }

    public interface IHostToLoadPortCommandParameter
    {
        string ToParameterText();
    }

}
