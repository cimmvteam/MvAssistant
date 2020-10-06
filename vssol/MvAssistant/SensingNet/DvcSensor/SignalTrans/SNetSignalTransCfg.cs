using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SensingNet.v0_2.DvcSensor.SignalTrans
{
    public class SNetSignalTransCfg
    {

        [XmlAttribute] public UInt64 Svid = 0;
        [XmlAttribute] public String SignalName;

        [XmlAttribute] public double CalibrateSysScale = 1.0;
        [XmlAttribute] public double CalibrateSysOffset = 0.0;

        [XmlAttribute] public double CalibrateUserScale = 1.0;
        [XmlAttribute] public double CalibrateUserOffset = 0.0;


        //public String StorageDirectory = "Signals/toolid/svid";
        //public long PurgeTimestamp = 180 * 24 * 60 * 60;//預設Purge Rule 

    }
}
