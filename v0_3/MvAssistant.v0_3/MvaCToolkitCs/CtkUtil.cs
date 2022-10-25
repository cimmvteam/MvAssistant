using MvaCToolkitCs.v1_2.Threading;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MvaCToolkitCs.v1_2
{
    public class CtkUtil
    {



        public static T ChangeType<T>(object data) { return (T)Convert.ChangeType(data, typeof(T)); }




        public static TAttr GetAttribute<TEnum, TAttr>(TEnum val) where TAttr : Attribute
        {
            var type = typeof(TEnum);
            var memberInfos = type.GetMember(val.ToString());
            var mi = memberInfos.FirstOrDefault(x => x.DeclaringType == type);
            if (mi == null) return default(TAttr);
            var attr = mi.GetCustomAttribute(typeof(TAttr), false);
            return (TAttr)attr;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethod()
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            return sf.GetMethod().Name;
        }

        public static string GetEntryVersion()
        {
            var assembly = Assembly.GetEntryAssembly();
            return assembly.GetName().Version.ToString();
        }

        public static string GetExecutingVersion()
        {
            var assembly = Assembly.GetExecutingAssembly();
            return assembly.GetName().Version.ToString();
        }

        public static bool MonitorTryEnter(object obj, int millisecond, Action act)
        {
            try
            {
                if (!Monitor.TryEnter(obj, millisecond)) return false;
                act();
                return true;
            }
            finally { Monitor.Exit(obj); }
        }




        public static int RandomInt()
        {
            var rnd = new Random((int)DateTime.Now.Ticks);
            var cnt = rnd.Next(32);
            for (var idx = 0; idx < cnt; idx++) rnd.Next();

            return rnd.Next();
        }
        public static int RandomInt(int max)
        {
            var rnd = new Random((int)DateTime.Now.Ticks);
            var cnt = rnd.Next(32);
            for (var idx = 0; idx < cnt; idx++) rnd.Next();

            return rnd.Next(max);
        }
        public static int RandomInt(int min, int max)
        {
            var rnd = new Random((int)DateTime.Now.Ticks);
            var cnt = rnd.Next(32);
            for (var idx = 0; idx < cnt; idx++) rnd.Next();

            return rnd.Next(min, max);
        }





        #region Enum

        public static List<T> EnumList<T>()
        {
            var ary = Enum.GetValues(typeof(T));
            var list = new List<T>();
            foreach (var e in ary) list.Add((T)e);
            return list;
        }
        public static Enum EnumParse(String val, Type type) { return (Enum)Enum.Parse(type, val, true); }
        public static T EnumParse<T>(String val) { return (T)Enum.Parse(typeof(T), val, true); }
        public static bool EnumParseTry<T>(String val, out T rs) where T : struct { return Enum.TryParse(val, true, out rs); }
        public static T EnumParseOrDefault<T>(String val) where T : struct
        {
            var rs = default(T);
            Enum.TryParse(val, true, out rs);
            return rs;
        }
        public static T EnumParseOrDefault<T>(String val, T def) where T : struct
        {
            var rs = def;
            Enum.TryParse(val, true, out rs);
            return rs;
        }

        #endregion



        #region Member Name

        public static string GetMemberName<TType, TValue>(Expression<Func<TType, TValue>> memberAccess)
        {
            var body = memberAccess.Body;
            var member = body as MemberExpression;
            if (member != null) return member.Member.Name;

            var unary = body as UnaryExpression;
            if (unary != null)
            {
                if (unary.Method != null) return unary.Method.Name;
            }

            throw new ArgumentException();
        }



        public static string GetMethodName<TType, TDelegate>(Expression<Func<TType, TDelegate>> expression)
        {
            LambdaExpression lambda = expression;
            return GetMethodName(lambda);
        }
        public static string GetMethodName<T>(Expression<Func<T, Delegate>> expression)
        {
            LambdaExpression lambda = expression;
            return GetMethodName(lambda);
        }
        public static string GetMethodName(LambdaExpression expression)
        {
            var unaryExpression = (UnaryExpression)expression.Body;
            var methodCallExpression = (MethodCallExpression)unaryExpression.Operand;

            var IsNET45 = Type.GetType("System.Reflection.ReflectionContext", false) != null;
            if (IsNET45)
            {
                var methodCallObject = (ConstantExpression)methodCallExpression.Object;
                var methodInfo = (MethodInfo)methodCallObject.Value;
                return methodInfo.Name;
            }
            else
            {
                var methodInfoExpression = (ConstantExpression)methodCallExpression.Arguments.Last();
                var methodInfo = (MemberInfo)methodInfoExpression.Value;
                return methodInfo.Name;
            }
        }

        public static string GetMethodNameAct<T>(Expression<Func<T, Action>> expression)
        {
            LambdaExpression lambda = expression;
            return GetMethodName(lambda);
        }
        #endregion




        #region Type Guid

        public static Guid? TypeGuid(System.Type type)
        {
            var attrs = type.GetTypeInfo().GetCustomAttributes(typeof(GuidAttribute), false);
            var attr = attrs.FirstOrDefault() as GuidAttribute;
            if (attr == null) return null;
            return Guid.Parse(attr.Value);
        }
        public static Guid? TypeGuid<T>()
        {
            var type = typeof(T);
            return TypeGuid(type);
        }

        public static Guid? TypeGuiInst(object inst)
        {
            var type = inst.GetType();
            return TypeGuid(type);
        }

        #endregion



        #region Serialize

        public static T XmlDeserialize<T>(String xml) where T : class, new()
        {
            var seri = new XmlSerializer(typeof(T));
            using (var xr = XmlReader.Create(new StringReader(xml)))
                return seri.Deserialize(xr) as T;
        }
        public static string XmlSerialize(object obj)
        {
            var seri = new XmlSerializer(obj.GetType());
            using (var sw = new StringWriter())
            using (var xw = XmlWriter.Create(sw))
            {
                seri.Serialize(xw, obj);
                return sw.ToString();
            }
        }

        #endregion



        #region Foreach

        public static void Foreach<T>(IEnumerable<T> list, Action<T> act)
        {
            foreach (var obj in list) act(obj);
        }

        #endregion
        public static object TryCatch(Action theMethod, params object[] parameters)
        {
            try
            {
                return theMethod.DynamicInvoke(parameters);
            }
            catch (Exception ex)
            {
                CtkLog.Write(ex);
                return ex;
            }
        }

        public static void Try(Action act, int times = 3)
        {
            for (var idx = 0; idx < times; idx++)
            {
                try
                {
                    act();
                    return;
                }
                catch (Exception ex)
                {
                    if (idx >= times - 1)
                        throw ex;
                }
            }
        }


        #region Process

        public static long MemorySize(string processName)
        {
            var procs = Process.GetProcessesByName(processName);
            var sum = 0L;
            foreach (var p in procs)
                sum += p.WorkingSet64;
            return sum;
        }
        public static void TaskkillByName(string name, bool isWaitForExit = true)
        {
            var arguments = string.Format("/IM \"{0}\" /F", name);
            var myproc = Process.Start("taskkill", arguments);
            if (isWaitForExit) myproc.WaitForExit();
        }

        #endregion 



        #region Serialization

        public static T DeserializeBinary<T>(byte[] dataArray)
        {
            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream(dataArray))
            {
                var obj = bf.Deserialize(ms);
                return (T)obj;
            }
        }
        public static T LoadXml<T>(String xml)
        {
            var seri = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (var ms = new MemoryStream())
            {
                using (var sw = new StreamWriter(ms))
                    sw.Write(xml);

                ms.Position = 0;

                using (var sr = new StreamReader(ms))
                    return (T)seri.Deserialize(sr);
            }
        }

        public static T LoadXmlFile<T>(String fn)
        {
            var seri = new System.Xml.Serialization.XmlSerializer(typeof(T));
            var fi = new FileInfo(fn);
            if (!fi.Exists) return default(T);


            using (var stm = fi.OpenRead())
            {
                return (T)seri.Deserialize(stm);
            }
        }
        public static T LoadXmlFileOrNew<T>(String fn) where T : class, new()
        {
            var seri = new System.Xml.Serialization.XmlSerializer(typeof(T));
            var fi = new FileInfo(fn);
            if (!fi.Exists)
            {
                var config = new T();
                return config;
            }


            using (var stm = fi.OpenRead())
            {
                return seri.Deserialize(stm) as T;
            }
        }
        public static void SaveXmlFileT<T>(T obj, String fn)
        {
            var seri = new System.Xml.Serialization.XmlSerializer(typeof(T));
            var fi = new FileInfo(fn);

            if (!fi.Directory.Exists) fi.Directory.Create();

            using (var stm = fi.Open(FileMode.Create))
            {
                seri.Serialize(stm, obj);
            }
        }

        public static void SaveXmlFile(object obj, String fn)
        {
            var seri = new System.Xml.Serialization.XmlSerializer(obj.GetType());
            var fi = new FileInfo(fn);

            if (!fi.Directory.Exists) fi.Directory.Create();

            using (var stm = fi.Open(FileMode.Create))
            {
                seri.Serialize(stm, obj);
            }
        }
        public static void SaveXmlFile(System.Type type, object obj, String fn)
        {
            var seri = new System.Xml.Serialization.XmlSerializer(type);
            var fi = new FileInfo(fn);

            if (!fi.Directory.Exists) fi.Directory.Create();

            using (var stm = fi.Open(FileMode.Create))
            {
                seri.Serialize(stm, obj);
            }
        }
        public static byte[] SerializeBinary(object obj)
        {
            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                ms.Flush();
                return ms.ToArray();
            }
        }

        #endregion



        #region Dispose

        public static void DisposeObj(IDisposable obj)
        {
            if (obj == null) return;
            obj.Dispose();
        }
        public static void DisposeObj(IEnumerable<IDisposable> objs)
        {
            foreach (var obj in objs) DisposeObj(obj);
        }
        public static void DisposeObjN(ref IDisposable obj)
        {
            if (obj == null) return;
            obj.Dispose();
            obj = null;
        }

        public static bool DisposeObjTry(IDisposable obj, Action<Exception> exceptionHandler = null)
        {
            if (obj == null) return true;
            try
            {
                DisposeObj(obj);
                return true;
            }
            catch (Exception ex)
            {
                if (exceptionHandler != null) exceptionHandler(ex);
                else CtkLog.Write(ex);
                return false;
            }

        }
        public static void DisposeObjTry(IEnumerable<IDisposable> objs, Action<Exception> exceptionHandler = null)
        {
            foreach (var obj in objs) DisposeObjTry(obj, exceptionHandler);
        }

        public static void DisposeTask(Task task, int millisecond = 0)
        {
            if (task == null) return;
            if (task.Status < TaskStatus.RanToCompletion)
            {
                if (millisecond < 0) task.Wait();//無限等待
                else if (millisecond > 0) task.Wait(millisecond);//等待一段時間
                //else 不等待
            }
            task.Dispose();
        }
        public static void DisposeTask(CtkTask task, int? millisecond = null)
        {
            if (task == null) return;
            if (millisecond.HasValue) task.DisposeWaitTime = millisecond.Value;
            task.Dispose();
        }
        public static bool DisposeTaskTry(Task task, int millisecond = 0)
        {
            try
            {
                DisposeTask(task, millisecond);
                return true;
            }
            catch (Exception ex)
            {
                CtkLog.Warn(ex);
                return false;
            }
        }
        public static bool DisposeTaskTry(CtkTask task, int? millisecond = null)
        {
            try
            {
                DisposeTask(task, millisecond);
                return true;
            }
            catch (Exception ex)
            {
                CtkLog.Warn(ex);
                return false;
            }
        }


        #endregion

        #region Foreach

        public static void ForeachTry<T>(IEnumerable<T> list, Action<T> act, Action<Exception> exceptionHandler = null)
        {
            foreach (var obj in list)
            {
                try { act(obj); }
                catch (Exception ex)
                {
                    if (exceptionHandler == null) exceptionHandler(ex);
                    else CtkLog.Write(ex);
                }
            }
        }

        #endregion



    }




}
