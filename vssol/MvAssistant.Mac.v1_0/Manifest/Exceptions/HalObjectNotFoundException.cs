using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Manifest.Exceptions
{
    public class HalObjectNotFoundException : Exception
    {
        public HalObjectNotFoundException()
        {
        }

        public HalObjectNotFoundException(string message)
            : base(message)
        {
        }

        public HalObjectNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
