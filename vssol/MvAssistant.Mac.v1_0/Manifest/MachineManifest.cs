using MvAssistant;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MvAssistant.Mac.v1_0.Manifest
{
    [XmlRoot("Manifest")]
    public class MachineManifest
    {

        private List<MachineDevice> m_devices = new List<MachineDevice>();
        private List<MachineDriver> m_drivers = new List<MachineDriver>();

        [XmlArray("Devices")]
        [XmlArrayItem("Device")]
        public List<MachineDevice> Devices
        {
            get { return m_devices; }
            set { m_devices = value; }
        }

        [XmlArray("Drivers")]
        [XmlArrayItem("Driver")]
        public List<MachineDriver> Drivers
        {
            get { return m_drivers; }
            set { m_drivers = value; }
        }

        [XmlAttribute]
        public string Version { get; set; }

        [XmlAttribute]
        public string CheckSum { get; set; }



        public void SaveToXmlFile(string fn) { MvUtil.SaveToXmlFile(this, fn); }



        #region static function
        /// <summary>
        /// 從xml content反序列化成指定物件
        /// </summary>
        /// <typeparam name="T">反序列化的物件</typeparam>
        /// <param name="s">xml content</param>
        /// <returns></returns>
        public static T Deserialize<T>(string s)
        {
            XmlDocument xdoc = new XmlDocument();

            try
            {
                xdoc.LoadXml(s);
                XmlNodeReader reader = new XmlNodeReader(xdoc.DocumentElement);
                XmlSerializer ser = new XmlSerializer(typeof(T));
                object obj = ser.Deserialize(reader);

                return (T)obj;
            }
            catch
            {
                return default(T);
            }
        }
        /// <summary>
        /// 將物件序列化成xml format
        /// </summary>
        /// <param name="o">愈序列化的物件</param>
        /// <returns>xml content</returns>
        public static string Serialize(object o)
        {
            XmlSerializer ser = new XmlSerializer(o.GetType());
            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);
            ser.Serialize(writer, o);
            return sb.ToString();
        }
        /// <summary>
        /// deserialize (反序列化) MachineManifest object from xml file
        /// </summary>
        /// <param name="filePath">xml file path for loading</param>
        /// <returns></returns>
        public static MachineManifest LoadFromXmlFile(string filePath) { return MvUtil.LoadFromXmlFile<MachineManifest>(filePath); }
        /// <summary>
        /// serialize (序列化) manifest object, and save as xml file
        /// </summary>
        /// <param name="manifest">MachineManifest object</param>
        /// <param name="filePath">xml file path for saving</param>
        public static void SaveToXmlFile(MachineManifest manifest, string filePath) { MvUtil.SaveToXmlFile<MachineManifest>(manifest, filePath); }
        #endregion


    }
}
