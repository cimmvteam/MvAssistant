using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Mac.Manifest.Exceptions
{
    public class MacLackAssemblyElementException : MacException
    {
        public MacLackAssemblyElementException()
        {
        }

        public MacLackAssemblyElementException(string message)
            : base(message)
        {
        }

        public MacLackAssemblyElementException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
