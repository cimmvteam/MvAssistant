using MvAssistant.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvAssistant
{
    public class MvLog
    {

        public static MvLogger DefaultLogger { get { return MvLoggerMapper.Singleton.Get(); } }
        public static string LoggerAssemblyName { get { return System.Reflection.Assembly.GetExecutingAssembly().GetName().Name; } }
        
        
        
        #region Default Logger
        public static void Debug(string msg, params object[] args) { DefaultLogger.Write(string.Format(msg, args), MvLoggerEnumLevel.Debug); }
        public static void Debug(MvLoggerEventArgs ea) { DefaultLogger.Write(ea, MvLoggerEnumLevel.Debug); }
        public static void Error(string msg, params object[] args) { DefaultLogger.Write(string.Format(msg, args), MvLoggerEnumLevel.Error); }
        public static void Error(MvLoggerEventArgs ea) { DefaultLogger.Write(ea, MvLoggerEnumLevel.Error); }
        public static void Fatal(string msg, params object[] args) { DefaultLogger.Write(string.Format(msg, args), MvLoggerEnumLevel.Fatal); }
        public static void Fatal(MvLoggerEventArgs ea) { DefaultLogger.Write(ea, MvLoggerEnumLevel.Fatal); }
        /// <summary>
        /// 使用 空ID 記錄Log
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void Info(string msg, params object[] args) { DefaultLogger.Write(string.Format(msg, args), MvLoggerEnumLevel.Info); }
        public static void Info(MvLoggerEventArgs ea) { DefaultLogger.Write(ea, MvLoggerEnumLevel.Info); }
        public static void Verbose(string msg, params object[] args) { DefaultLogger.Write(string.Format(msg, args), MvLoggerEnumLevel.Verbose); }
        public static void Verbose(MvLoggerEventArgs ea) { DefaultLogger.Write(ea, MvLoggerEnumLevel.Verbose); }
        public static void Warn(string msg, params object[] args) { DefaultLogger.Write(string.Format(msg, args), MvLoggerEnumLevel.Warn); }
        public static void Warn(MvLoggerEventArgs ea) { DefaultLogger.Write(ea, MvLoggerEnumLevel.Warn); }
        public static void Write(MvLoggerEventArgs ea)
        {
            DefaultLogger.Write(ea);
        }
        public static void Write(MvLoggerEventArgs ea, MvLoggerEnumLevel _level)
        {
            ea.Level = _level;
            DefaultLogger.Write(ea, _level);
        }

        #endregion


        #region Namespace Logger

        public static void DebugNs(object sender, string msg, params object[] args) { GetAssemblyLogger(sender).Write(string.Format(msg, args), MvLoggerEnumLevel.Debug); }

        public static void ErrorNs(object sender, string msg, params object[] args) { GetAssemblyLogger(sender).Write(string.Format(msg, args), MvLoggerEnumLevel.Error); }

        public static void ErrorNs(object sender, MvLoggerEventArgs ea) { GetAssemblyLogger(sender).Write(ea, MvLoggerEnumLevel.Error); }

        public static void FatalNs(object sender, string msg, params object[] args) { GetAssemblyLogger(sender).Write(string.Format(msg, args), MvLoggerEnumLevel.Fatal); }

        public static void FatalNs(object sender, MvLoggerEventArgs ea) { GetAssemblyLogger(sender).Write(ea, MvLoggerEnumLevel.Fatal); }

        /// <summary>
        /// 使用 Namespace 記錄Log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void InfoNs(object sender, string msg, params object[] args) { GetAssemblyLogger(sender).Write(string.Format(msg, args), MvLoggerEnumLevel.Info); }

        public static void InfoNs(object sender, MvLoggerEventArgs ea) { GetAssemblyLogger(sender).Write(ea, MvLoggerEnumLevel.Info); }

        public static void VerboseNs(object sender, string msg, params object[] args) { GetAssemblyLogger(sender).Write(string.Format(msg, args), MvLoggerEnumLevel.Verbose); }

        public static void WarnNs(object sender, string msg, params object[] args) { GetAssemblyLogger(sender).Write(string.Format(msg, args), MvLoggerEnumLevel.Warn); }

        public static void WarnNs(object sender, MvLoggerEventArgs ea) { GetAssemblyLogger(sender).Write(ea, MvLoggerEnumLevel.Warn); }

        public static void WriteNs(object sender, MvLoggerEventArgs ea)
        {
            GetAssemblyLogger(sender).Write(ea);
        }
        public static void WriteNs(object sender, MvLoggerEventArgs ea, MvLoggerEnumLevel _level)
        {
            ea.Level = _level;
            GetAssemblyLogger(sender).Write(ea, _level);
        }
        #endregion


        #region Specified ID Logger

        public static void DebugId(string loggerId, string msg, params object[] args) { GetLoggerById(loggerId).Write(string.Format(msg, args), MvLoggerEnumLevel.Debug); }

        public static void ErrorId(string loggerId, string msg, params object[] args) { GetLoggerById(loggerId).Write(string.Format(msg, args), MvLoggerEnumLevel.Error); }

        public static void FatalId(string loggerId, string msg, params object[] args) { GetLoggerById(loggerId).Write(string.Format(msg, args), MvLoggerEnumLevel.Fatal); }

        /// <summary>
        /// 使用 指定ID 記錄Log
        /// </summary>
        /// <param name="loggerId"></param>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void InfoId(string loggerId, string msg, params object[] args) { GetLoggerById(loggerId).Write(string.Format(msg, args), MvLoggerEnumLevel.Info); }

        public static void VerboseId(string loggerId, string msg, params object[] args) { GetLoggerById(loggerId).Write(string.Format(msg, args), MvLoggerEnumLevel.Verbose); }

        public static void WarnId(string loggerId, string msg, params object[] args) { GetLoggerById(loggerId).Write(string.Format(msg, args), MvLoggerEnumLevel.Warn); }

        public static void WriteId(string loggerId, MvLoggerEventArgs ea)
        {
            GetLoggerById(loggerId).Write(ea);
        }
        public static void WriteId(string loggerId, MvLoggerEventArgs ea, MvLoggerEnumLevel _level)
        {
            ea.Level = _level;
            GetLoggerById(loggerId).Write(ea, _level);
        }
        #endregion


        #region Namespace.Specified ID Logger


        public static void DebugNsId(object sender, string loggerId, string msg, params object[] args) { GetAssemblyLoggerById(sender, loggerId).Write(string.Format(msg, args), MvLoggerEnumLevel.Debug); }

        public static void ErrorNsId(object sender, string loggerId, string msg, params object[] args) { GetAssemblyLoggerById(sender, loggerId).Write(string.Format(msg, args), MvLoggerEnumLevel.Error); }

        public static void FatalNsId(object sender, string loggerId, string msg, params object[] args) { GetAssemblyLoggerById(sender, loggerId).Write(string.Format(msg, args), MvLoggerEnumLevel.Fatal); }

        /// <summary>
        /// 使用 Namespace + 指定ID 記錄Log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="loggerId"></param>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void InfoNsId(object sender, string loggerId, string msg, params object[] args) { GetAssemblyLoggerById(sender, loggerId).Write(string.Format(msg, args), MvLoggerEnumLevel.Info); }

        public static void VerboseNsId(object sender, string loggerId, string msg, params object[] args) { GetAssemblyLoggerById(sender, loggerId).Write(string.Format(msg, args), MvLoggerEnumLevel.Verbose); }

        public static void WarnNsId(object sender, string loggerId, string msg, params object[] args) { GetAssemblyLoggerById(sender, loggerId).Write(string.Format(msg, args), MvLoggerEnumLevel.Warn); }

        public static void WriteNsId(object sender, string loggerId, MvLoggerEventArgs ea)
        {
            GetAssemblyLoggerById(sender, loggerId).Write(ea);
        }
        public static void WriteNsId(object sender, string loggerId, MvLoggerEventArgs ea, MvLoggerEnumLevel _level)
        {
            ea.Level = _level;
            GetAssemblyLoggerById(sender, loggerId).Write(ea, _level);
        }
        #endregion


        public static MvLogger GetAssemblyLogger(Object sender)
        {
            var type = sender.GetType();
            if (sender is Type)
                type = sender as Type;

            var name = type.Assembly.FullName;
            return MvLoggerMapper.Singleton.Get(name);
        }

        public static MvLogger GetAssemblyLoggerById(Object sender, string loggerId)
        {
            var type = sender.GetType();
            var name = type.Assembly.FullName + (string.IsNullOrEmpty(loggerId) ? "" : "." + loggerId);
            return MvLoggerMapper.Singleton.Get(name);
        }

        public static MvLogger GetLoggerById(string loggerId) { return MvLoggerMapper.Singleton.Get(loggerId); }
        public static void RegisterEveryLogWrite(EventHandler<MvLoggerEventArgs> eh) { MvLogger.EhEveryLogWrite += eh; }


    }
}
