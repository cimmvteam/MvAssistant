using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.StateExceptions
{
    [Obsolete]
    public abstract class StateException:Exception
    {
        public StateException() : base()
        {

        }
        public StateException(string message) : base(message)
        {

        }
    }
}
