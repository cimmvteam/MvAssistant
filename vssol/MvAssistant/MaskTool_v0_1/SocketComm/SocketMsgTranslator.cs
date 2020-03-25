using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvAssistant.MaskTool_v0_1.SocketComm
{
    public class SocketMsgTranslator
    {
        public string[] Decode(string rcvMsg)
        {
            string tmpRcvMsg = rcvMsg;
            int Tilde_index = tmpRcvMsg.IndexOf("~");
            int AT_index= tmpRcvMsg.IndexOf("@");
            tmpRcvMsg = tmpRcvMsg.Substring(Tilde_index+1,AT_index-Tilde_index-1);
            string[] msg = tmpRcvMsg.Split(',');
            return msg;
        }

        public string Encode(string[] sendMsg)
        {
            string msg = "~";
            foreach(string s in sendMsg)
            {
                msg += ",";
                msg += s;
            }
            msg += "@";
            return msg;
        }
    }
}
