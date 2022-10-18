using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.Mac.Manifest.Exceptions
{
    public class MacNoPropertyException : MacException
    {
        public MacNoPropertyException()
        {
        }

        public MacNoPropertyException(string message)
            : base(message)
        {
        }

        public MacNoPropertyException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
