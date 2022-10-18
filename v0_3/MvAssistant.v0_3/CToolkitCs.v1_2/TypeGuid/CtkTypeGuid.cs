using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CToolkitCs.v1_2.TypeGuid
{
    [Serializable]
    public class CtkTypeGuid
    {

        [XmlIgnore] private Type type;
        [XmlIgnore] private Guid guid;



        [XmlIgnore]
        public Type Type
        {
            get { return this.type; }
            set
            {
                this.type = value;
                var myGuid = CtkUtil.TypeGuid(this.type);
                if (myGuid.HasValue) this.guid = myGuid.Value;
            }
        }

        protected Guid Guid
        {
            get { return this.guid; }
            set
            {
                this.guid = value;
                var type = CtkTypeGuidMapper.Get(guid);
                this.Type = type;
            }
        }


        public static implicit operator CtkTypeGuid(Type type) { return new CtkTypeGuid() { Type = type }; }

    }
}
