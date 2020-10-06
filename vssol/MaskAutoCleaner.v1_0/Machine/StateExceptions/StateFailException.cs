using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.StateExceptions
{
    [Obsolete]
    public class StateFailException:StateException
    {
        public StateFailException() : base()
        {

        }
        public StateFailException(string message) : base(message)
        {

        }
    }
}
