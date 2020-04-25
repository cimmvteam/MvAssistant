using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Manifest.Exceptions
{
    public class GenCheckSumException : Exception
    {
        public GenCheckSumException()
        {
        }

        public GenCheckSumException(string message)
            : base(message)
        {
        }

        public GenCheckSumException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
