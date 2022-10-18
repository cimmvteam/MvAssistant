using CodeExpress.v1_0.Secs;
using CToolkit;
using CToolkit.v1_1;
using CToolkit.v1_1.Protocol;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensingNet.v0_2.DvcSensor.SignalTrans
{


    public class SNetSignalTransSecs001 : ISNetSignalTransBase
    {


        public List<SNetSignalTransEventArgs> AnalysisSignal<T>(object sender, object msg, IList<T> infos)
        {

            var result = new List<SNetSignalTransEventArgs>();
            var secsMsg = msg as CxHsmsMessage;

            try
            {
                var list = secsMsg.rootNode as CxSecsIINodeList;

                for (int idx = 0; idx < list.Data.Count; idx++)
                {

                    var ea = new SNetSignalTransEventArgs();
                    ea.Sender = sender;
                    var data = list.Data[idx] as CxSecsIINodeASCII;
                    if (data.Data.Count <= 0) continue;

                    ea.Data = new List<double>();
                    ea.Data.Add(double.Parse(data.GetString()));

                    //this.OnDataTrigger(ea);
                    result.Add(ea);
                }
                return result;

            }
            catch (Exception ex) { CtkLog.Write(ex); }

            return null;
        }

        public CtkProtocolTrxMessage CreateDataReqMsg<T>(IList<T> reqInfos)
        {
            var listInfo = reqInfos as IList<SNetSignalTransCfg>;
            if (listInfo == null) throw new ArgumentException("未定義此型別的操作方式");


            var txMsg = new CxHsmsMessage();
            txMsg.header.StreamId = 1;
            txMsg.header.FunctionId = 3;
            txMsg.header.WBit = true;
            var sList = new CxSecsIINodeList();
            //var sSvid = new CToolkit.v1_0.Secs.SecsIINodeInt64();

            foreach (var scfg in listInfo)
            {
                var sSvid = new CxSecsIINodeUInt64();
                sSvid.Data.Add(scfg.Svid);
                sList.Data.Add(sSvid);
            }

            txMsg.rootNode = sList;

            return txMsg;


        }


        public CtkProtocolTrxMessage CreateAckMsg<T>(IList<T> reqInfos)
        {
            var listInfo = reqInfos as IList<SNetSignalTransCfg>;
            if (listInfo == null) throw new ArgumentException("未定義此型別的操作方式");

            return null;
        }
    }
}
