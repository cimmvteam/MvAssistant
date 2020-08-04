using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.StateExceptions
{
   public class TimeoutException:StateException
    {
        public TimeoutException() : base()
        {

        }
        public TimeoutException(string message) : base(message)
        {

        }
    }
}
