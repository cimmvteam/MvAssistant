using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.KjMachineDrawer.Exceptions
{
   public  class OutOfListenPortsException:BaseException
    {
        public OutOfListenPortsException(string messsage):base(messsage)
        {

        }
        public OutOfListenPortsException() : base("監聽的通訊埠已經使用完畢")
        {

        }
    }
}
