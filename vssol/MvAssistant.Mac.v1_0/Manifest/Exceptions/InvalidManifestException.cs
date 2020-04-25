using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Manifest.Exceptions
{
    public class InvalidManifestException : Exception
    {
        public InvalidManifestException()
        {
        }

        public InvalidManifestException(string message)
            : base(message)
        {
        }

        public InvalidManifestException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
