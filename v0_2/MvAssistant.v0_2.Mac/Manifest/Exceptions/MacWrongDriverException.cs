using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Mac.Manifest.Exceptions
{
    public class MacWrongDriverException : MacException
    {
        public MacWrongDriverException()
        {
        }

        public MacWrongDriverException(string message)
            : base(message)
        {
        }

        public MacWrongDriverException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
