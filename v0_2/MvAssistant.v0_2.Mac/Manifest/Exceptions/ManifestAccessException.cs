using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Mac.Manifest.Exceptions
{
    public class ManifestAccessException : MacException
    {
        public ManifestAccessException()
        {
        }

        public ManifestAccessException(string message)
            : base(message)
        {
        }

        public ManifestAccessException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
