using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Manifest.Exceptions
{
    public class WrongDriverException : MacException
    {
        public WrongDriverException()
        {
        }

        public WrongDriverException(string message)
            : base(message)
        {
        }

        public WrongDriverException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
