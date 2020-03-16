using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Xml.Serialization;

namespace MvAssistant
{
    public class MvUtil
    {
        public static DateTime? ConvertToDateTime(string datetime, string srcFormat)
        {
            DateTime rsdate;
            if (DateTime.TryParse(datetime, out rsdate)
                ||

                DateTime.TryParseExact(datetime, srcFormat
                    , System.Globalization.CultureInfo.InvariantCulture
                    , System.Globalization.DateTimeStyles.None
                    , out rsdate))
            { return rsdate; }
            return null;
        }


        /// <summary>
        /// 泛型 Enum.Parse
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        /// <returns></returns>
        public static T EnumParse<T>(string val) { return (T)Enum.Parse(typeof(T), val, true); }

        public static bool EnumTryParse<T>(String val, out T result) where T : struct { return Enum.TryParse<T>(val, true, out result); }

        public static T[] FromByteArray<T>(byte[] source) where T : struct
        {
            T[] destination = new T[source.Length / Marshal.SizeOf(typeof(T))];
            GCHandle handle = GCHandle.Alloc(destination, GCHandleType.Pinned);
            try
            {
                IntPtr pointer = handle.AddrOfPinnedObject();
                Marshal.Copy(source, 0, pointer, source.Length);
                return destination;
            }
            finally
            {
                if (handle.IsAllocated)
                    handle.Free();
            }
        }

        public static T FromBytes<T>(byte[] buffer)
        {
            int size = Marshal.SizeOf(typeof(T));
            IntPtr ptr = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.Copy(buffer, 0, ptr, size);
                T data = (T)Marshal.PtrToStructure(ptr, typeof(T));
                return data;
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                    Marshal.FreeHGlobal(ptr);
            }


        }

        /// <summary>
        /// 取得電腦顯示的第一個IP
        /// </summary>
        /// <returns></returns>
        public static IPAddress GetFirstIp()
        {
            string strHostName = Dns.GetHostName();
            var iphostentry = Dns.GetHostEntry(strHostName);
            return iphostentry.AddressList.FirstOrDefault();
        }

        public static IPAddress GetLikelyIp(string target_ip, string setting_ip)
        {
            IPAddress localIp = null;
            if (string.IsNullOrEmpty(target_ip))
            {
                IPAddress.TryParse(setting_ip, out localIp);
                return localIp;
            }


            var remoteEndPoint = IPAddress.Parse(target_ip);

            //if (String.IsNullOrEmpty(setting_ip))
            {
                string strHostName = Dns.GetHostName();
                var iphostentry = Dns.GetHostEntry(strHostName);
                var likelyCount = -1;
                foreach (IPAddress ipaddress in iphostentry.AddressList)
                {
                    var localIpBytes = ipaddress.GetAddressBytes();
                    var remoteIpBytes = remoteEndPoint.GetAddressBytes();
                    int idx = 0;
                    for (idx = 0; idx < localIpBytes.Length; idx++)
                        if (localIpBytes[idx] != remoteIpBytes[idx])
                            break;

                    if (idx > likelyCount)
                    {
                        likelyCount = idx;
                        localIp = ipaddress;
                    }
                }
            }

            return localIp;
        }

        public static IPAddress GetLikelyOr127Ip(string target_ip = null, string setting_ip = null)
        {
            var ipaddr = GetLikelyIp(target_ip, setting_ip);
            if (ipaddr == null)
                ipaddr = IPAddress.Parse("127.0.0.1");

            return ipaddr;
        }

        public static IPAddress GetLikelyOrFirstIp(string target_ip = null, string setting_ip = null)
        {
            var ipaddr = GetLikelyIp(target_ip, setting_ip);
            if (ipaddr == null)
                ipaddr = GetFirstIp();
            return ipaddr;
        }

        public static IPAddress GetLikelyOrLocalIp(string target_ip = null, string setting_ip = null)
        {
            var ipaddr = GetLikelyIp(target_ip, setting_ip);
            if (ipaddr == null)
                ipaddr = IPAddress.Parse("localhost");

            return ipaddr;
        }

        /// <summary>
        /// 使用此方法取得成員名稱, 方便重新命名時, 不會遺漏
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="memberAccess"></param>
        /// <returns></returns>
        public static string GetMemberName<T, TValue>(Expression<Func<T, TValue>> memberAccess)
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

        public static string GetMethodName<T>(Expression<Func<T, Delegate>> expression)
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

        public static string GetMethodNameBase(LambdaExpression expression)
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

        public static T LoadFromXmlFile<T>(String fn) where T : class, new()
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

        public static void MemoryCopy<S, D>(S[] src, int srcOffset, D[] dst, int sdtOffset, int length)
        {
            if (srcOffset + length > src.Length) throw new ArgumentException();
            if (sdtOffset + length > dst.Length) throw new ArgumentException();

            int sizeTIn = Marshal.SizeOf(typeof(S));
            int sizeTOut = Marshal.SizeOf(typeof(D));
            int sizeBytes = length * sizeTIn;
            int sizeOut = sizeBytes / sizeTOut;

            var srcGcH = GCHandle.Alloc(src, GCHandleType.Pinned);
            var dstGcH = GCHandle.Alloc(dst, GCHandleType.Pinned);
            try
            {

            }
            finally
            {
                if (srcGcH != null && srcGcH.IsAllocated) srcGcH.Free();
                if (dstGcH != null && dstGcH.IsAllocated) dstGcH.Free();
            }
        }

        public static void PingSync(string ip)
        {
            var psi = new System.Diagnostics.ProcessStartInfo();
            psi.FileName = @"ping";
            psi.Arguments = ip;
            psi.RedirectStandardOutput = true;
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            var ps = new System.Diagnostics.Process();
            ps.StartInfo = psi;
            ps.Start();
            ps.WaitForExit();
        }

        public static void RemoveEvents(EventHandler eh)
        {
            eh = null;
        }

        public static void RunWorkerAsyn(DoWorkEventHandler work)
        {
            var bgworker = new BackgroundWorker();
            bgworker.WorkerSupportsCancellation = true;
            bgworker.DoWork += work;
            bgworker.RunWorkerAsync();
        }

        public static void RunWorkerAsyn(Action work)
        {
            var bgworker = new BackgroundWorker();
            bgworker.WorkerSupportsCancellation = true;
            bgworker.DoWork += delegate(object sender, DoWorkEventArgs e)
            {
                work();
            };
            bgworker.RunWorkerAsync();
        }

        public static void SaveToXmlFile<T>(T obj, String fn)
        {
            var seri = new System.Xml.Serialization.XmlSerializer(typeof(T));
            var fi = new FileInfo(fn);

            if (!fi.Directory.Exists) fi.Directory.Create();

            using (var stm = fi.Open(FileMode.Create))
            {
                seri.Serialize(stm, obj);
            }
        }


        public static byte[] ToByteArray<T>(T[] source) where T : struct
        {
            GCHandle handle = GCHandle.Alloc(source, GCHandleType.Pinned);
            try
            {
                IntPtr pointer = handle.AddrOfPinnedObject();
                byte[] destination = new byte[source.Length * Marshal.SizeOf(typeof(T))];
                Marshal.Copy(pointer, destination, 0, destination.Length);
                return destination;
            }
            finally
            {
                if (handle.IsAllocated)
                    handle.Free();
            }
        }

        public static byte[] ToBytes<T>(T obj)
        {

            Byte[] bytes = new Byte[Marshal.SizeOf(typeof(T))];
            GCHandle pinStructure = GCHandle.Alloc(obj, GCHandleType.Pinned);
            try
            {
                Marshal.Copy(pinStructure.AddrOfPinnedObject(), bytes, 0, bytes.Length);
                return bytes;
            }
            finally { pinStructure.Free(); }
        }
        public static object TryCatch(Action theMethod, params object[] parameters)
        {
            try
            {
                return theMethod.DynamicInvoke(parameters);
            }
            catch (Exception ex)
            {
                MvLog.Write(ex);
                return ex;
            }
        }
    }
}
