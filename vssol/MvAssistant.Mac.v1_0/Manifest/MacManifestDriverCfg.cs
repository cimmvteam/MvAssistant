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
    [Serializable]
    public class MacManifestDriverCfg
    {

        [XmlAttribute]
        public string DriverId { get; set; }
        [XmlAttribute]
        public string DriverPath { get; set; }


        [XmlAttribute]
        public string Vendor { get; set; }
        [XmlAttribute]
        public string Product { get; set; }
        [XmlAttribute]
        public string Remark { get; set; }
        

        #region Assign Type

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
            }
        }

        [XmlAttribute]
        public string HalImpId
        {
            get { return this.m_TypeGuid.ToString().ToUpper(); }
            set
            {
                var guid = value.ToUpper();
                this.m_TypeGuid = Guid.Parse(guid);
                var type = TypeMapper.Get(guid);

                this.m_AssignType = type;
            }
        }

        [XmlAttribute]
        public string HalImpRefName { get { return this.AssignType.Name; } set { } }


        #endregion







    }
}
