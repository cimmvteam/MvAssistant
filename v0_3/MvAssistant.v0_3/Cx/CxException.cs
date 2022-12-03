using System;
using System.Collections.Generic;
using System.Text;

namespace MvaCodeExpress.v1_1
{
    public class CxException : Exception
    {
        public CxException() { }
        public CxException(string message) : base(message) { }

    }
}
