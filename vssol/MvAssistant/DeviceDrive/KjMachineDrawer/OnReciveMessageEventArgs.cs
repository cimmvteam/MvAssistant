using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.KjMachineDrawer
{
   public  class OnReciveMessageEventArgs:EventArgs
    {
        /// <summary>回傳的訊息</summary>
        public string Message { get; set; }
        /// <summary>回傳資料的IP </summary>
        public string IP { set; get; }
    }
}
