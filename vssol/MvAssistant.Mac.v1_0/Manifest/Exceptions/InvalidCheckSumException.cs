using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Manifest.Exceptions
{
    public class InvalidCheckSumException : Exception
    {
        public InvalidCheckSumException()
        {
        }

        public InvalidCheckSumException(string message)
            : base(message)
        {
        }

        public InvalidCheckSumException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
