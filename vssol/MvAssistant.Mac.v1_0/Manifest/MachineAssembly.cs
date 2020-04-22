using MvAssistant.TypeMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MaskAutoCleaner.Manifest
{
    public class MachineAssembly
    {
        [XmlArray("Assemblies")]
        [XmlArrayItem("Assembly")]
        public MachineAssembly[] Assemblies { get; set; }

        [XmlAttribute]
        public string DevConnStr { get; set; }

        /// <summary>
        /// 對應到DeviceEnum的列舉項目
        /// </summary>
        [XmlAttribute]
        public string DeviceName { get; set; }

        [XmlAttribute]
        public string DriverId { get; set; }
        /// <summary>
        /// 對應到機台設備清單的Device ID
        /// </summary>
        [XmlAttribute]
        public string ID { get; set; }
        [XmlAttribute]
        public string Level { get; set; }

        [XmlAttribute]
        public string PositionId { get; set; }





        #region Interface Type

        Type m_AssignType;
        Guid m_TypeGuid = Guid.Empty;


        [XmlIgnore]
        public Type AssignType
        {
            get { return this.m_AssignType; }
            set
            {
                var type = value;
                this.m_AssignType = type;
                var guid_attrs = this.m_AssignType.GetCustomAttributes(typeof(GuidAttribute), false);
                var guid = guid_attrs.FirstOrDefault() as GuidAttribute;

                if (guid != null && this.m_TypeGuid == Guid.Empty) this.m_TypeGuid = Guid.Parse(guid.Value);

                this.HalIntfRefName = type.Name;
            }
        }
        [XmlAttribute]
        public string HalIntfId
        {
            get { return this.m_TypeGuid.ToString().ToUpper(); }
            set
            {
                var guid = value.ToUpper();
                this.m_TypeGuid = Guid.Parse(guid);
                var type = TypeMapper.Get(guid);

                this.m_AssignType = type;

                this.HalIntfRefName = type.Name;
            }
        }

        [XmlAttribute]
        public string HalIntfRefName { get; set; }

        #endregion


    }
}
