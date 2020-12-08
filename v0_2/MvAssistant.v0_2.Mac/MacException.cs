using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Mac
{
    public class MacException : Exception
    {
        public MacException()
        {
        }

        public MacException(string message)
            : base(message)
        {
        }

        public MacException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
