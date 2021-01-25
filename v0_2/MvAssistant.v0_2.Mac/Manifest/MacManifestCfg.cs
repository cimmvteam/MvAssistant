using MvAssistant.v0_2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MvAssistant.v0_2.Mac.Manifest
{
    [XmlRoot("Manifest")]
    public class MacManifestCfg
    {

        private List<MacManifestDeviceCfg> m_devices = new List<MacManifestDeviceCfg>();
        private List<MacManifestDriverCfg> m_drivers = new List<MacManifestDriverCfg>();

        [XmlArray("Devices")]
        [XmlArrayItem("Device")]
        public List<MacManifestDeviceCfg> Devices
        {
            get { return m_devices; }
            set { m_devices = value; }
        }

        [XmlArray("Drivers")]
        [XmlArrayItem("Driver")]
        public List<MacManifestDriverCfg> Drivers
        {
            get { return m_drivers; }
            set { m_drivers = value; }
        }

        [XmlAttribute]
        public string Version { get; set; }

        [XmlAttribute]
        public string CheckSum { get; set; }



        public void SaveToXmlFile(string fn) { MvaUtil.SaveXmlToFile(this, fn); }

        #region static function
        /// <summary>
        /// deserialize (反序列化) MachineManifest object from xml file
        /// </summary>
        /// <param name="filePath">xml file path for loading</param>
        /// <returns></returns>
        public static MacManifestCfg LoadFromXmlFile(string filePath) { return MvaUtil.LoadFromXmlFile<MacManifestCfg>(filePath); }
        /// <summary>
        /// serialize (序列化) manifest object, and save as xml file
        /// </summary>
        /// <param name="manifest">MachineManifest object</param>
        /// <param name="filePath">xml file path for saving</param>
        public static void SaveToXmlFile(MacManifestCfg manifest, string filePath) { MvaUtil.SaveXmlToFile<MacManifestCfg>(manifest, filePath); }
        #endregion


    }
}
