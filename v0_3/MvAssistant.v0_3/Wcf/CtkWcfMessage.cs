using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace MvaCToolkitCs.v1_2.Wcf
{
    [Serializable]
    public class CtkWcfMessage
    {

        public String AssemblyName;
        public String TypeName;
        public byte[] DataBytes;

        public void SetDataObj(object obj)
        {
            var type = obj.GetType();
            this.AssemblyName = type.Assembly.GetName().Name;
            this.TypeName = type.FullName;
            this.DataBytes = CtkObject.DataContractSerializeToByte(obj);


        }
        public Object GetDataObj()
        {
            var assembly = (from row in AppDomain.CurrentDomain.GetAssemblies()
                            where row.GetName().Name == this.AssemblyName
                            select row).FirstOrDefault();
            var type = assembly.GetType(this.TypeName);



            return CtkObject.DataContractDeserialize(type, this.DataBytes);
        }


        public static CtkWcfMessage Create(object obj)
        {
            var msg = new CtkWcfMessage();
            msg.SetDataObj(obj);
            return msg;
        }

        public static implicit operator CtkWcfMessage(string val)
        {
            var msg = new CtkWcfMessage();
            msg.SetDataObj(val);
            return msg;
        }




    }
}
