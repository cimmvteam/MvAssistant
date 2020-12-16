using MvAssistant.v0_2.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvAssistant.v0_2
{
    public class MvaLog
    {

        public static MvaLogger DefaultLogger { get { return MvaLoggerMapper.Singleton.Get(); } }
        public static string LoggerAssemblyName { get { return System.Reflection.Assembly.GetExecutingAssembly().GetName().Name; } }
        
        
        
        #region Default Logger
        public static void Debug(string msg, params object[] args) { DefaultLogger.Write(string.Format(msg, args), MvaLoggerEnumLevel.Debug); }
        public static void Debug(MvaLoggerEventArgs ea) { DefaultLogger.Write(ea, MvaLoggerEnumLevel.Debug); }
        public static void Error(string msg, params object[] args) { DefaultLogger.Write(string.Format(msg, args), MvaLoggerEnumLevel.Error); }
        public static void Error(MvaLoggerEventArgs ea) { DefaultLogger.Write(ea, MvaLoggerEnumLevel.Error); }
        public static void Fatal(string msg, params object[] args) { DefaultLogger.Write(string.Format(msg, args), MvaLoggerEnumLevel.Fatal); }
        public static void Fatal(MvaLoggerEventArgs ea) { DefaultLogger.Write(ea, MvaLoggerEnumLevel.Fatal); }
        /// <summary>
        /// 使用 空ID 記錄Log
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void Info(string msg, params object[] args) { DefaultLogger.Write(string.Format(msg, args), MvaLoggerEnumLevel.Info); }
        public static void Info(MvaLoggerEventArgs ea) { DefaultLogger.Write(ea, MvaLoggerEnumLevel.Info); }
        public static void Verbose(string msg, params object[] args) { DefaultLogger.Write(string.Format(msg, args), MvaLoggerEnumLevel.Verbose); }
        public static void Verbose(MvaLoggerEventArgs ea) { DefaultLogger.Write(ea, MvaLoggerEnumLevel.Verbose); }
        public static void Warn(string msg, params object[] args) { DefaultLogger.Write(string.Format(msg, args), MvaLoggerEnumLevel.Warn); }
        public static void Warn(MvaLoggerEventArgs ea) { DefaultLogger.Write(ea, MvaLoggerEnumLevel.Warn); }
        public static void Write(MvaLoggerEventArgs ea)
        {
            DefaultLogger.Write(ea);
        }
        public static void Write(MvaLoggerEventArgs ea, MvaLoggerEnumLevel _level)
        {
            ea.Level = _level;
            DefaultLogger.Write(ea, _level);
        }

        #endregion


        #region Namespace Logger

        public static void DebugNs(object sender, string msg, params object[] args) { GetAssemblyLogger(sender).Write(string.Format(msg, args), MvaLoggerEnumLevel.Debug); }

        public static void ErrorNs(object sender, string msg, params object[] args) { GetAssemblyLogger(sender).Write(string.Format(msg, args), MvaLoggerEnumLevel.Error); }

        public static void ErrorNs(object sender, MvaLoggerEventArgs ea) { GetAssemblyLogger(sender).Write(ea, MvaLoggerEnumLevel.Error); }

        public static void FatalNs(object sender, string msg, params object[] args) { GetAssemblyLogger(sender).Write(string.Format(msg, args), MvaLoggerEnumLevel.Fatal); }

        public static void FatalNs(object sender, MvaLoggerEventArgs ea) { GetAssemblyLogger(sender).Write(ea, MvaLoggerEnumLevel.Fatal); }

        /// <summary>
        /// 使用 Namespace 記錄Log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void InfoNs(object sender, string msg, params object[] args) { GetAssemblyLogger(sender).Write(string.Format(msg, args), MvaLoggerEnumLevel.Info); }

        public static void InfoNs(object sender, MvaLoggerEventArgs ea) { GetAssemblyLogger(sender).Write(ea, MvaLoggerEnumLevel.Info); }

        public static void VerboseNs(object sender, string msg, params object[] args) { GetAssemblyLogger(sender).Write(string.Format(msg, args), MvaLoggerEnumLevel.Verbose); }

        public static void WarnNs(object sender, string msg, params object[] args) { GetAssemblyLogger(sender).Write(string.Format(msg+"", args), MvaLoggerEnumLevel.Warn); }

        public static void WarnNs(object sender, MvaLoggerEventArgs ea) { GetAssemblyLogger(sender).Write(ea, MvaLoggerEnumLevel.Warn); }

        public static void WriteNs(object sender, MvaLoggerEventArgs ea)
        {
            GetAssemblyLogger(sender).Write(ea);
        }
        public static void WriteNs(object sender, MvaLoggerEventArgs ea, MvaLoggerEnumLevel _level)
        {
            ea.Level = _level;
            GetAssemblyLogger(sender).Write(ea, _level);
        }
        #endregion


        #region Specified ID Logger

        public static void DebugId(string loggerId, string msg, params object[] args) { GetLoggerById(loggerId).Write(string.Format(msg, args), MvaLoggerEnumLevel.Debug); }

        public static void ErrorId(string loggerId, string msg, params object[] args) { GetLoggerById(loggerId).Write(string.Format(msg, args), MvaLoggerEnumLevel.Error); }

        public static void FatalId(string loggerId, string msg, params object[] args) { GetLoggerById(loggerId).Write(string.Format(msg, args), MvaLoggerEnumLevel.Fatal); }

        /// <summary>
        /// 使用 指定ID 記錄Log
        /// </summary>
        /// <param name="loggerId"></param>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void InfoId(string loggerId, string msg, params object[] args) { GetLoggerById(loggerId).Write(string.Format(msg, args), MvaLoggerEnumLevel.Info); }

        public static void VerboseId(string loggerId, string msg, params object[] args) { GetLoggerById(loggerId).Write(string.Format(msg, args), MvaLoggerEnumLevel.Verbose); }

        public static void WarnId(string loggerId, string msg, params object[] args) { GetLoggerById(loggerId).Write(string.Format(msg, args), MvaLoggerEnumLevel.Warn); }

        public static void WriteId(string loggerId, MvaLoggerEventArgs ea)
        {
            GetLoggerById(loggerId).Write(ea);
        }
        public static void WriteId(string loggerId, MvaLoggerEventArgs ea, MvaLoggerEnumLevel _level)
        {
            ea.Level = _level;
            GetLoggerById(loggerId).Write(ea, _level);
        }
        #endregion


        #region Namespace.Specified ID Logger


        public static void DebugNsId(object sender, string loggerId, string msg, params object[] args) { GetAssemblyLoggerById(sender, loggerId).Write(string.Format(msg, args), MvaLoggerEnumLevel.Debug); }

        public static void ErrorNsId(object sender, string loggerId, string msg, params object[] args) { GetAssemblyLoggerById(sender, loggerId).Write(string.Format(msg, args), MvaLoggerEnumLevel.Error); }

        public static void FatalNsId(object sender, string loggerId, string msg, params object[] args) { GetAssemblyLoggerById(sender, loggerId).Write(string.Format(msg, args), MvaLoggerEnumLevel.Fatal); }

        /// <summary>
        /// 使用 Namespace + 指定ID 記錄Log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="loggerId"></param>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void InfoNsId(object sender, string loggerId, string msg, params object[] args) { GetAssemblyLoggerById(sender, loggerId).Write(string.Format(msg, args), MvaLoggerEnumLevel.Info); }

        public static void VerboseNsId(object sender, string loggerId, string msg, params object[] args) { GetAssemblyLoggerById(sender, loggerId).Write(string.Format(msg, args), MvaLoggerEnumLevel.Verbose); }

        public static void WarnNsId(object sender, string loggerId, string msg, params object[] args) { GetAssemblyLoggerById(sender, loggerId).Write(string.Format(msg, args), MvaLoggerEnumLevel.Warn); }

        public static void WriteNsId(object sender, string loggerId, MvaLoggerEventArgs ea)
        {
            GetAssemblyLoggerById(sender, loggerId).Write(ea);
        }
        public static void WriteNsId(object sender, string loggerId, MvaLoggerEventArgs ea, MvaLoggerEnumLevel _level)
        {
            ea.Level = _level;
            GetAssemblyLoggerById(sender, loggerId).Write(ea, _level);
        }
        #endregion


        public static MvaLogger GetAssemblyLogger(Object sender)
        {
            var type = sender.GetType();
            if (sender is Type)
                type = sender as Type;

            var name = type.Assembly.FullName;
            return MvaLoggerMapper.Singleton.Get(name);
        }

        public static MvaLogger GetAssemblyLoggerById(Object sender, string loggerId)
        {
            var type = sender.GetType();
            var name = type.Assembly.FullName + (string.IsNullOrEmpty(loggerId) ? "" : "." + loggerId);
            return MvaLoggerMapper.Singleton.Get(name);
        }

        public static MvaLogger GetLoggerById(string loggerId) { return MvaLoggerMapper.Singleton.Get(loggerId); }
        public static void RegisterEveryLogWrite(EventHandler<MvaLoggerEventArgs> eh) { MvaLogger.EhEveryLogWrite += eh; }


    }
}
