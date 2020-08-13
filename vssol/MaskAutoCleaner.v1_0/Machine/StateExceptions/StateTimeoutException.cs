using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.StateExceptions
{
   public class StateTimeoutException:StateException
    {
        public StateTimeoutException() : base()
        {

        }
        public StateTimeoutException(string message) : base(message)
        {

        }
    }
}
