using CToolkitCs.v1_2.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2
{
    public class CtkLog
    {
        public static string LoggerAssemblyName { get { return System.Reflection.Assembly.GetExecutingAssembly().GetName().Name; } }
        public static CtkLogger DefaultLogger { get { return CtkLoggerMapper.Singleton.Get(); } }

        #region Default Logger
        public static void Debug(string msg, params object[] args) { DefaultLogger.Write(string.Format(msg, args), CtkLoggerEnumLevel.Debug); }
        public static void Debug(CtkLoggerEventArgs ea) { DefaultLogger.Write(ea, CtkLoggerEnumLevel.Debug); }
        public static void Error(string msg, params object[] args) { DefaultLogger.Write(string.Format(msg, args), CtkLoggerEnumLevel.Error); }
        public static void Error(CtkLoggerEventArgs ea) { DefaultLogger.Write(ea, CtkLoggerEnumLevel.Error); }
        public static void Fatal(string msg, params object[] args) { DefaultLogger.Write(string.Format(msg, args), CtkLoggerEnumLevel.Fatal); }
        public static void Fatal(CtkLoggerEventArgs ea) { DefaultLogger.Write(ea, CtkLoggerEnumLevel.Fatal); }
        /// <summary>
        /// 使用 空ID 記錄Log
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void Info(string msg, params object[] args) { DefaultLogger.Write(string.Format(msg, args), CtkLoggerEnumLevel.Info); }
        public static void Info(CtkLoggerEventArgs ea) { DefaultLogger.Write(ea, CtkLoggerEnumLevel.Info); }
        public static void Verbose(string msg, params object[] args) { DefaultLogger.Write(string.Format(msg, args), CtkLoggerEnumLevel.Verbose); }
        public static void Verbose(CtkLoggerEventArgs ea) { DefaultLogger.Write(ea, CtkLoggerEnumLevel.Verbose); }
        public static void Warn(string msg, params object[] args) { DefaultLogger.Write(string.Format(msg, args), CtkLoggerEnumLevel.Warn); }
        public static void Warn(CtkLoggerEventArgs ea) { DefaultLogger.Write(ea, CtkLoggerEnumLevel.Warn); }
        public static void Write(CtkLoggerEventArgs ea)
        {
            DefaultLogger.Write(ea);
        }
        public static void Write(CtkLoggerEventArgs ea, CtkLoggerEnumLevel _level)
        {
            ea.Level = _level;
            DefaultLogger.Write(ea, _level);
        }


        public static void WarnTryCatch(Action act) { try { act(); } catch (Exception ex) { Warn(ex); } }


        #endregion

        #region Namespace Logger

        public static void WriteNs(object sender, CtkLoggerEventArgs ea)
        {
            GetAssemblyLogger(sender).Write(ea);
        }
        public static void WriteNs(object sender, CtkLoggerEventArgs ea, CtkLoggerEnumLevel _level)
        {
            ea.Level = _level;
            GetAssemblyLogger(sender).Write(ea, _level);
        }

        public static void VerboseNs(object sender, string msg, params object[] args) { GetAssemblyLogger(sender).Write(string.Format(msg, args), CtkLoggerEnumLevel.Verbose); }
        public static void DebugNs(object sender, string msg, params object[] args) { GetAssemblyLogger(sender).Write(string.Format(msg, args), CtkLoggerEnumLevel.Debug); }
        /// <summary>
        /// 使用 Namespace 記錄Log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void InfoNs(object sender, string msg, params object[] args) { GetAssemblyLogger(sender).Write(string.Format(msg, args), CtkLoggerEnumLevel.Info); }
        public static void InfoNs(object sender, CtkLoggerEventArgs ea) { GetAssemblyLogger(sender).Write(ea, CtkLoggerEnumLevel.Info); }
        public static void WarnNs(object sender, string msg, params object[] args) { GetAssemblyLogger(sender).Write(string.Format(msg, args), CtkLoggerEnumLevel.Warn); }
        public static void WarnNs(object sender, CtkLoggerEventArgs ea) { GetAssemblyLogger(sender).Write(ea, CtkLoggerEnumLevel.Warn); }
        public static void ErrorNs(object sender, string msg, params object[] args) { GetAssemblyLogger(sender).Write(string.Format(msg, args), CtkLoggerEnumLevel.Error); }
        public static void ErrorNs(object sender, CtkLoggerEventArgs ea) { GetAssemblyLogger(sender).Write(ea, CtkLoggerEnumLevel.Error); }
        public static void FatalNs(object sender, string msg, params object[] args) { GetAssemblyLogger(sender).Write(string.Format(msg, args), CtkLoggerEnumLevel.Fatal); }
        public static void FatalNs(object sender, CtkLoggerEventArgs ea) { GetAssemblyLogger(sender).Write(ea, CtkLoggerEnumLevel.Fatal); }

        public static void WarnNsTryCatch(object sender, Action act) { try { act(); } catch (Exception ex) { WarnNs(sender, ex); } }


        #endregion


        #region Specified ID Logger

        public static void WriteId(string loggerId, CtkLoggerEventArgs ea)
        {
            GetLoggerById(loggerId).Write(ea);
        }
        public static void WriteId(string loggerId, CtkLoggerEventArgs ea, CtkLoggerEnumLevel _level)
        {
            ea.Level = _level;
            GetLoggerById(loggerId).Write(ea, _level);
        }

        public static void VerboseId(string loggerId, string msg, params object[] args) { GetLoggerById(loggerId).Write(string.Format(msg, args), CtkLoggerEnumLevel.Verbose); }
        public static void DebugId(string loggerId, string msg, params object[] args) { GetLoggerById(loggerId).Write(string.Format(msg, args), CtkLoggerEnumLevel.Debug); }
        /// <summary>
        /// 使用 指定ID 記錄Log
        /// </summary>
        /// <param name="loggerId"></param>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void InfoId(string loggerId, string msg, params object[] args) { GetLoggerById(loggerId).Write(string.Format(msg, args), CtkLoggerEnumLevel.Info); }
        public static void WarnId(string loggerId, string msg, params object[] args) { GetLoggerById(loggerId).Write(string.Format(msg, args), CtkLoggerEnumLevel.Warn); }
        public static void ErrorId(string loggerId, string msg, params object[] args) { GetLoggerById(loggerId).Write(string.Format(msg, args), CtkLoggerEnumLevel.Error); }
        public static void FatalId(string loggerId, string msg, params object[] args) { GetLoggerById(loggerId).Write(string.Format(msg, args), CtkLoggerEnumLevel.Fatal); }

        #endregion


        #region Namespace.Specified ID Logger


        public static void WriteNsId(object sender, string loggerId, CtkLoggerEventArgs ea)
        {
            GetAssemblyLoggerById(sender, loggerId).Write(ea);
        }
        public static void WriteNsId(object sender, string loggerId, CtkLoggerEventArgs ea, CtkLoggerEnumLevel _level)
        {
            ea.Level = _level;
            GetAssemblyLoggerById(sender, loggerId).Write(ea, _level);
        }

        public static void VerboseNsId(object sender, string loggerId, string msg, params object[] args) { GetAssemblyLoggerById(sender, loggerId).Write(string.Format(msg, args), CtkLoggerEnumLevel.Verbose); }
        public static void DebugNsId(object sender, string loggerId, string msg, params object[] args) { GetAssemblyLoggerById(sender, loggerId).Write(string.Format(msg, args), CtkLoggerEnumLevel.Debug); }
        /// <summary>
        /// 使用 Namespace + 指定ID 記錄Log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="loggerId"></param>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void InfoNsId(object sender, string loggerId, string msg, params object[] args) { GetAssemblyLoggerById(sender, loggerId).Write(string.Format(msg, args), CtkLoggerEnumLevel.Info); }
        public static void WarnNsId(object sender, string loggerId, string msg, params object[] args) { GetAssemblyLoggerById(sender, loggerId).Write(string.Format(msg, args), CtkLoggerEnumLevel.Warn); }
        public static void ErrorNsId(object sender, string loggerId, string msg, params object[] args) { GetAssemblyLoggerById(sender, loggerId).Write(string.Format(msg, args), CtkLoggerEnumLevel.Error); }
        public static void FatalNsId(object sender, string loggerId, string msg, params object[] args) { GetAssemblyLoggerById(sender, loggerId).Write(string.Format(msg, args), CtkLoggerEnumLevel.Fatal); }

        #endregion


        public static CtkLogger GetLoggerById(string loggerId) { return CtkLoggerMapper.Singleton.Get(loggerId); }
        public static CtkLogger GetAssemblyLogger(Object sender)
        {
            var type = sender.GetType();
            if (sender is System.Type)
                type = sender as System.Type;

            var name = type.Assembly.FullName;
            return CtkLoggerMapper.Singleton.Get(name);
        }
        public static CtkLogger GetAssemblyLoggerById(Object sender, string loggerId)
        {
            var type = sender.GetType();
            var name = type.Assembly.FullName + (string.IsNullOrEmpty(loggerId) ? "" : "." + loggerId);
            return CtkLoggerMapper.Singleton.Get(name);
        }



        public static void RegisterEveryLogWrite(EventHandler<CtkLoggerEventArgs> eh) { CtkLogger.EhEveryLogWrite += eh; }
        public static void UnRegisterEveryLogWrite(EventHandler<CtkLoggerEventArgs> eh) { CtkLogger.EhEveryLogWrite -= eh; }
        public static void UnRegisterEveryLogWriteByOwner(object owner) { CtkEventUtil.RemoveEventHandlersOfTypeByTarget(typeof(CtkLogger), owner); }



    }
}
