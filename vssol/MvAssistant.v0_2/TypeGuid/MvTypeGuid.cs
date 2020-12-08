using CToolkit.v1_1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MvAssistant.TypeGuid
{
    public class MvTypeGuid
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

        [XmlAttribute]
        public Guid Guid
        {
            get { return this.guid; }
            set
            {
                this.guid = value;
                var type = MvTypeGuidMapper.Get(guid);
                this.Type = type;
            }
        }


        public static implicit operator MvTypeGuid(Type type) { return new MvTypeGuid() { Type = type }; }


    }
}
