using System;
using System.Collections.Generic;
using System.Text;

namespace CodeExpress.v1_1Core
{
    public class CxException : Exception
    {
        public CxException() { }
        public CxException(string message) : base(message) { }

    }
}
