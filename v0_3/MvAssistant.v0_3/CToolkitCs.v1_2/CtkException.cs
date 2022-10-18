using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2
{
    public class CtkException : Exception
    {
        public CtkException() : base() { }
        public CtkException(string message) : base(message) { }
        public CtkException(string message, Exception innerException) : base(message, innerException) { }


        public CtkException(Type type, string method, string message)
            : base(string.Format("{0}.{1}.{2}", type.FullName, method, message)) { }
        public CtkException(Type type, string method, string message, Exception innerException)
            : base(string.Format("{0}.{1}.{2}", type.FullName, method, message), innerException) { }
    }
}