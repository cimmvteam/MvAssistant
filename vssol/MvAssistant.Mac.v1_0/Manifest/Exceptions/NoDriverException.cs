using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Manifest.Exceptions
{
    public class NoDriverException : Exception
    {
        public NoDriverException()
        {
        }

        public NoDriverException(string message)
            : base(message)
        {
        }

        public NoDriverException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
