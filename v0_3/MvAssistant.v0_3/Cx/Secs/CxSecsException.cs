using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeExpress.v1_1Core.Secs
{
    public class CxSecsException : Exception
    {
        public CxSecsException() : base() { }
        public CxSecsException(string message) : base(message) { }
        public CxSecsException(string message, Exception innerException) : base(message, innerException) { }

        public CxSecsException(Type type, string method, string message)
            : base(string.Format("{0}.{1}.{2}", type.FullName, method, message)) { }
        public CxSecsException(Type type, string method, string message, Exception innerException)
            : base(string.Format("{0}.{1}.{2}", type.FullName, method, message), innerException) { }
    }
}