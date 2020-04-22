using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Manifest.Exceptions
{
    public class AssemblyCreationException : Exception
    {
        public AssemblyCreationException()
        {
        }

        public AssemblyCreationException(string message)
            : base(message)
        {
        }

        public AssemblyCreationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
