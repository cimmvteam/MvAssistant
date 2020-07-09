using MvAssistant.TypeMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MaskAutoCleaner.v1_0.Machine
{
    public class MacMachineMap
    {

        public Type CtrlMachineType;


        Type m_halType;
        Guid m_halGuid;



        [XmlIgnore]
        public Type HalType
        {
            get { return this.m_halType; }
            set
            {
                var type = value;
                this.m_halType = type;
                var guidAttrs = this.m_halType.GetCustomAttributes(typeof(GuidAttribute), false);
                var guid = guidAttrs.FirstOrDefault() as GuidAttribute;

                if (guid != null ) this.m_halGuid = Guid.Parse(guid.Value);
            }
        }




        [XmlAttribute]
        public string HalImpId
        {
            get { return this.m_halGuid.ToString().ToUpper(); }
            set
            {
                var guid = value.ToUpper();
                this.m_halGuid = Guid.Parse(guid);
                var type = MvTypeMapper.Get(guid);

                this.m_halType = type;
            }
        }

    }
}
