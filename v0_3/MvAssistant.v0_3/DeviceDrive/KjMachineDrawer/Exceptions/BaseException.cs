using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.KjMachineDrawer.Exceptions
{
    public abstract class BaseException:Exception
    {
        public BaseException(string message) : base(message)
        {

        }
    }
}
