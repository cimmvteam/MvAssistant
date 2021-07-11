using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Sensor.Proto
{
public     class MvaSsProtoCmdMsg
    {

        //public object Sender;
        //public string DeviceUri;
        //public string DeviceName;


        public EnumMvaSsProtoCmdCat ProtoCmdCat;
        public UInt64? Svid;
        public List<double> Data = new List<double>();
        public DateTime RcvDateTime;

    }
}
