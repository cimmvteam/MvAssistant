//using Microsoft.Analytics.Interfaces;
//using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MvAssistant.v0_3.DeviceDrive.KjMachineDrawer.ReplyCode
{
   public class ReplyMessage
    {
        /// <summary>
        /// 
        /// </summary>
        public string StringCode { get; set; }
        public string StringFunc { get; set; }
        public int? Value { get; set; }
    }
}