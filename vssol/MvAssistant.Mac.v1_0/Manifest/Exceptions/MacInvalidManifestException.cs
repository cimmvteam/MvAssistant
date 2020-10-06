using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Manifest.Exceptions
{
    public class MacInvalidManifestException : MacException
    {
        public MacInvalidManifestException()
        {
        }

        public MacInvalidManifestException(string message)
            : base(message)
        {
        }

        public MacInvalidManifestException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
