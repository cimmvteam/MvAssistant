using MvAssistant.v0_3.DeviceDrive.GudengLoadPort.TCPCommand.HostToLoadPort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.GudengLoadPort
{
    public class ReturnFromServer
    {
        public string StringCode { get; set; }
        public string StringContent { get; set; }
        public string ReturnCode { get; set; }
        public string ReturnValue { get; set; }
        //public string LastRequestCommandText { get; set; }
        public ReturnFromServer(string content)
        {
            content = content.Replace(BaseHostToLoadPortCommand.CommandPrefixText, "").Replace(BaseHostToLoadPortCommand.CommandPostfixText, "");
            var contentAry = content.Split(new string[] { BaseHostToLoadPortCommand.CommandSplitSign }, StringSplitOptions.RemoveEmptyEntries);
            StringCode = contentAry[0];
            StringContent = contentAry[1];
            if (contentAry.Length >= 3)
            {
                ReturnCode = contentAry[2];
                if (contentAry.Length > 3)
                {
                    ReturnValue = contentAry[3];
                }
            }
            else
            {
                ReturnCode = null;
            }
        }
    }
}
