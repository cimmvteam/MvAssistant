using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Mac.Manifest.Exceptions
{
    public class ManifestSemanticException : MacException
    {
        public ManifestSemanticException()
        {
        }

        public ManifestSemanticException(string message)
            : base(message)
        {
        }

        public ManifestSemanticException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
