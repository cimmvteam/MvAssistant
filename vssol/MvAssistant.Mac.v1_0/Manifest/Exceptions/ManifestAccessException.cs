using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Manifest.Exceptions
{
    public class ManifestAccessException : Exception
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
