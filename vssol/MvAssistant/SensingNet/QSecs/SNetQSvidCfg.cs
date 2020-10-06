using CToolkit.v1_1;
using CToolkit.v1_1.Numeric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SensingNet.v0_2.QSecs
{
    /// <summary>
    /// Query Svid
    /// </summary>
    public class SNetQSvidCfg
    {
        [XmlAttribute] public UInt64 QSvid;
        [XmlAttribute] public SNetEnumStatisticsMethod StatisticsMethod = SNetEnumStatisticsMethod.Average;
        [XmlAttribute] public int StatisticsSecond = 1;

        public string DeviceUid;//若DeviceIp/Port不存在, 以Device Name區分
        public String DeviceUri;//可為空值
        public UInt64 DeviceSvid;

        public String StoragePath;



        public void SaveToXmlFile(string fn) { CtkUtilFw.SaveToXmlFileT(this, fn); }

    }

}
