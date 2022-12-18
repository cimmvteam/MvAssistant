using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.GudengLoadPort.TCPCommand
{
    public abstract class BaseTCPCommand
    {
        public const string CommandPrefixText = "~";
        public const string CommandPostfixText = "@";
        public const string CommandTextReplaceSignPair = "[]";
        public const string CommandSplitSign = ",";
        protected abstract string GetRawCommandText();
    }
}
