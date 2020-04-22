using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MvAssistant.Manifest
{
    [Serializable]
    public class MachineAssemblyDriverReference
    {
        [XmlAttribute]
        public string Description { get; set; }

        [XmlAttribute]
        public string Path { get; set; }

        [XmlAttribute]
        public string CheckSum { get; set; }
    }
}
