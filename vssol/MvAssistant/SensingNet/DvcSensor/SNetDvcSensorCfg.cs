using CToolkit.v1_1;
using CToolkit.v1_1.DigitalPort;
using SensingNet.v0_2.DvcSensor.Protocol;
using SensingNet.v0_2.DvcSensor.SignalTrans;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SensingNet.v0_2.DvcSensor
{

    [Serializable]
    public class SNetDvcSensorCfg
    {
        /// <summary>
        /// Device的唯一識別符
        /// </summary>
        [XmlAttribute] public String DeviceUid;
        /// <summary>
        /// Device的名稱
        /// </summary>
        [XmlAttribute] public String DeviceName;
        /// <summary>
        /// Device是否為主動連線
        /// </summary>
        public bool IsActivelyConnect = false;
        /// <summary>
        /// Device是否主動傳送訊號
        /// </summary>
        public bool IsActivelyTx = false;

        public String LocalUri;
        public SNetEnumProtoConnect ProtoConnect = SNetEnumProtoConnect.Tcp;
        public SNetEnumProtoFormat ProtoFormat = SNetEnumProtoFormat.Secs;
        public SNetEnumProtoSession ProtoSession = SNetEnumProtoSession.Secs;
        public String RemoteUri = "tcp://192.168.123.101:5000";
        public int IntervalTimeOfConnectCheck = 1000;

        public List<SNetSignalTransCfg> SignalCfgList = new List<SNetSignalTransCfg>();
        public SNetEnumSignalTrans SignalTran = SNetEnumSignalTrans.SNetCmd;
        public int TimeoutResponse = 1000;
        /// <summary>
        /// 訊息應間隔幾毫秒, 0=即時
        /// </summary>
        public int TxInterval = 0;

        public void SaveToXmlFile(string fn) { CtkUtilFw.SaveToXmlFileT(this, fn); }
    }
}
