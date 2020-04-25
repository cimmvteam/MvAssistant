using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Manifest.Exceptions
{
    public class LackAssemblyElementException : Exception
    {
        public LackAssemblyElementException()
        {
        }

        public LackAssemblyElementException(string message)
            : base(message)
        {
        }

        public LackAssemblyElementException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
