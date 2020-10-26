using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaskAutoCleaner.v1_0
{
    public class MacException : Exception
    {


        public MacException(string message) : base(message)
        {

        }

        public MacException(string message, Exception innerException) : base(message, innerException)
        {

        }

    }
}
