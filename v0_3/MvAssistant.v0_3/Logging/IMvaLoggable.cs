using MvAssistant.v0_3.Threading;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace MvAssistant.v0_3.Logging
{
    public interface IMvaLoggable
    {

        void Write(MvaLoggerEventArgs ea);
        void Write(MvaLoggerEventArgs ea, MvaLoggerEnumLevel _level = MvaLoggerEnumLevel.Info);
        void WriteSyn(MvaLoggerEventArgs ea);
        void WriteAsyn(MvaLoggerEventArgs ea);



        void Verbose(string msg, params object[] args);
        void Debug(string msg, params object[] args);
        void Info(string msg, params object[] args);
        void Warn(string msg, params object[] args);
        void Error(string msg, params object[] args);
        void Fatal(string msg, params object[] args);




        event EventHandler<MvaLoggerEventArgs> EhLogWrite;





    }
}