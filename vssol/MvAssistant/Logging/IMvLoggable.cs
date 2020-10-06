using MvAssistant.Tasking;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace MvAssistant.Logging
{
    public interface IMvLoggable
    {

        void Write(MvLoggerEventArgs ea);
        void Write(MvLoggerEventArgs ea, MvLoggerEnumLevel _level = MvLoggerEnumLevel.Info);
        void WriteSyn(MvLoggerEventArgs ea);
        void WriteAsyn(MvLoggerEventArgs ea);



        void Verbose(string msg, params object[] args);
        void Debug(string msg, params object[] args);
        void Info(string msg, params object[] args);
        void Warn(string msg, params object[] args);
        void Error(string msg, params object[] args);
        void Fatal(string msg, params object[] args);




        event EventHandler<MvLoggerEventArgs> EhLogWrite;





    }
}