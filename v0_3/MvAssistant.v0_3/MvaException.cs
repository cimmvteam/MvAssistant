using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvAssistant.v0_3
{
    public class MvaException : Exception
    {


        public MvaException(string message) : base(message)
        {

        }

        public MvaException(string message, Exception innerException) : base(message, innerException)
        {

        }

    }
}
