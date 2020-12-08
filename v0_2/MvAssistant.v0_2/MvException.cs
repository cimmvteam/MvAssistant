using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvAssistant.v0_2
{
    public class MvException : Exception
    {


        public MvException(string message) : base(message)
        {

        }

        public MvException(string message, Exception innerException) : base(message, innerException)
        {

        }

    }
}
