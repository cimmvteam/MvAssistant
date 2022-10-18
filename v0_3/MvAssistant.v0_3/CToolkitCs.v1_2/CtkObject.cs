using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace CToolkitCs.v1_2
{
    public class CtkObject
    {



        #region Xml Serialization

        public static T XmlDeserialize<T>(string xml, IEnumerable<System.Type> types = null) { return (T)XmlDeserialize(typeof(T), xml, types); }

        public static Object XmlDeserialize(System.Type type, string xml, IEnumerable<System.Type> types = null)
        {
            var seri = new XmlSerializer(type);
            if (types != null) seri = new XmlSerializer(type, types.ToArray());
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            using (var sr = new StreamReader(ms))
            {
                return seri.Deserialize(sr);
            }
        }



        public static T XmlDeserialize<T>(byte[] buffer, IEnumerable<System.Type> types = null) { return (T)XmlDeserialize(typeof(T), buffer, types); }

        public static Object XmlDeserialize(System.Type type, byte[] buffer, IEnumerable<System.Type> types = null)
        {
            var seri = new XmlSerializer(type);
            if (types != null) seri = new XmlSerializer(type, types.ToArray());
            using (var ms = new MemoryStream(buffer))
            using (var sr = new StreamReader(ms))
            {
                return seri.Deserialize(sr);
            }
        }



        public static byte[] XmlSerializeToBytes<T>(T obj, IEnumerable<System.Type> types = null)
        {
            var seri = new XmlSerializer(obj.GetType());
            if (types != null) seri = new XmlSerializer(obj.GetType(), types.ToArray());
            using (var ms = new MemoryStream())
            {
                seri.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                return ms.ToArray();
            }
        }

        public static string XmlSerializeToString<T>(T obj, IEnumerable<System.Type> types = null)
        {
            var seri = new XmlSerializer(obj.GetType());
            if (types != null) seri = new XmlSerializer(obj.GetType(), types.ToArray());
            using (var ms = new MemoryStream())
            using (var sw = new StreamWriter(ms, Encoding.UTF8))
            {
                seri.Serialize(sw, obj);
                return sw.ToString();
            }
        }
        #endregion


        #region Data Contract Serialization

        public static Object DataContractDeserialize(System.Type type, MemoryStream stream, IEnumerable<System.Type> types = null)
        {
            var seri = new DataContractSerializer(type);
            if (types != null) seri = new DataContractSerializer(type, types);
            return seri.ReadObject(stream);
        }
        public static Object DataContractDeserialize(System.Type type, byte[] buffer, IEnumerable<System.Type> types = null)
        {
            using (var stm = new MemoryStream(buffer))
                return DataContractDeserialize(type, stm, types);
        }
        public static T DataContractDeserialize<T>(MemoryStream stream, IEnumerable<System.Type> types = null) { return (T)DataContractDeserialize(typeof(T), stream, types); }
        public static T DataContractDeserialize<T>(byte[] buffer, IEnumerable<System.Type> types = null) { return (T)DataContractDeserialize(typeof(T), buffer, types); }

        public static byte[] DataContractSerializeToByte<T>(T obj, IEnumerable<System.Type> types = null)
        {
            using (var stm = DataContractSerializeToStream(obj))
                return stm.ToArray();//.GetBuffer();
        }
        public static MemoryStream DataContractSerializeToStream<T>(T obj, IEnumerable<System.Type> types = null)
        {
            var type = obj.GetType();
            var seri = new DataContractSerializer(type);
            if (types != null) seri = new DataContractSerializer(type, types);
            var memoryStream = new MemoryStream();
            seri.WriteObject(memoryStream, obj);
            memoryStream.Position = 0;
            return memoryStream;
        }

        #endregion



    }


}
