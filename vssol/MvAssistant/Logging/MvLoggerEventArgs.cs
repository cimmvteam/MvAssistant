using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvAssistant.Logging
{

    public class MvLoggerEventArgs : EventArgs
    {

        public string Message;
        public Exception Exception;
        public MvLoggerEnumLevel Level;



        public static implicit operator MvLoggerEventArgs(string d)
        {
            var rs = new MvLoggerEventArgs();
            rs.Message = d;
            rs.Level = MvLoggerEnumLevel.Info;
            return rs;
        }

        public static implicit operator MvLoggerEventArgs(Exception ex)
        {
            var rs = new MvLoggerEventArgs();
            rs.Exception = ex;
            rs.Level = MvLoggerEnumLevel.Warn;
            return rs;
        }

    }
}
