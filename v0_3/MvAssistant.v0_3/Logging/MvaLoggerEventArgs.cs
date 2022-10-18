using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvAssistant.v0_3.Logging
{

    public class MvaLoggerEventArgs : EventArgs
    {

        public string Message;
        public Exception Exception;
        public MvaLoggerEnumLevel Level;
        public DateTime CreateTime = DateTime.Now;


        public static implicit operator MvaLoggerEventArgs(string d)
        {
            var rs = new MvaLoggerEventArgs();
            rs.Message = d;
            rs.Level = MvaLoggerEnumLevel.Info;
            return rs;
        }

        public static implicit operator MvaLoggerEventArgs(Exception ex)
        {
            var rs = new MvaLoggerEventArgs();
            rs.Exception = ex;
            rs.Level = MvaLoggerEnumLevel.Warn;
            return rs;
        }

    }
}
