using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.GudengLoadPort.TCPCommand.HostToLoadPort
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
        public  string GetCommandText()
        {
            return GetRawCommandText();
        }



    }

   

}
