using MvAssistant.TypeMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MvAssistant.Mac.v1_0.Manifest
{
    public class MacManifestDeviceCfg
    {
        [XmlArray("Devices")]
        [XmlArrayItem("Device")]
        public MacManifestDeviceCfg[] Devices { get; set; }

        [XmlAttribute] public string DevConnStr { get; set; }

        /// <summary>
        /// 對應到DeviceEnum的列舉項目
        /// </summary>
        [XmlAttribute] public string DeviceName { get; set; }

        [XmlAttribute] public string DriverId { get; set; }
        /// <summary>
        /// 對應到機台設備清單的Device ID
        /// </summary>
        [XmlAttribute] public string ID { get; set; }
        //[XmlAttribute] public string Level { get; set; }
        [XmlAttribute] public string PositionId { get; set; }



    }
}
