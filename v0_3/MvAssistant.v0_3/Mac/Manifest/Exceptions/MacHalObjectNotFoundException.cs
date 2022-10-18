using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.Mac.Manifest.Exceptions
{
    public class MacHalObjectNotFoundException : MacException
    {
        public MacHalObjectNotFoundException()
        {
        }

        public MacHalObjectNotFoundException(string message)
            : base(message)
        {
        }

        public MacHalObjectNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
