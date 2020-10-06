using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Manifest.Exceptions
{
    public class MacInvalidCheckSumException : MacException
    {
        public MacInvalidCheckSumException()
        {
        }

        public MacInvalidCheckSumException(string message)
            : base(message)
        {
        }

        public MacInvalidCheckSumException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
