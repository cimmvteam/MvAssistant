using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.KjMachineDrawer.Exceptions
{
   public  class TimeOutSecondOutOfRangeException:BaseException
    {
        public TimeOutSecondOutOfRangeException() : base("設定的逾時秒數太大或太小")
        { 
        }
        public TimeOutSecondOutOfRangeException(string message) : base(message)
        {

        }
    }
}
