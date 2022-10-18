using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.KjMachineDrawer.Exceptions
{
   public class MotionSpeedOutOfRangeException:BaseException
    {
        public MotionSpeedOutOfRangeException() : base("設定的速度太大或太小")
        {

        }
        public MotionSpeedOutOfRangeException(string message) : base(message)
        {

        }
    }
}
