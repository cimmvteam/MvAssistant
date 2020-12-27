using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Msg
{
    public class MacMsgCommand : MacMsgBase
    {

        public string Command;




        #region Static - Operator
        public static implicit operator MacMsgCommand(string cmd) { return new MacMsgCommand() { Command = cmd }; }
        #endregion

        #region Static
        public static MacMsgCommand Create(string cmd) { return new MacMsgCommand() { Command = cmd }; }
        #endregion



    }
}
