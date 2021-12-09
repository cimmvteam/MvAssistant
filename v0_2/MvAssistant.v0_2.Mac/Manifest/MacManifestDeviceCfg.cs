using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MvAssistant.v0_2.Mac.Manifest
{
    public class MacManifestDeviceCfg
    {
        [XmlArray("Devices")]
        [XmlArrayItem("Device")]
        public MacManifestDeviceCfg[] Devices { get; set; }

        [XmlAttribute] public string DevConnStr { get; set; }

        /// <summary> 對應到DeviceEnum的列舉項目 </summary>
        [XmlAttribute] public string DeviceId { get; set; }

        [XmlAttribute] public string DriverId { get; set; }

        /// <summary> 保留, 但目前不具實質用途 </summary>
        [XmlAttribute] public string HalId { get; set; }
        //[XmlAttribute] public string Level { get; set; }
        [XmlAttribute] public string PositionId { get; set; }


    }
}
