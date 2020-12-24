using CToolkit.v1_1.Protocol;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensingNet.v0_2.DvcSensor.SignalTrans
{
    public class SNetSignalTransSNetCmd : ISNetSignalTransBase
    {





        int ReadData(String[] args, int start, List<double> data)
        {
            //讀取資料, 皆為double, 否則視為結束
            //return 最後一筆資料的索引

            var d = 0.0;
            //第一筆為 -reqData
            int idx = 0;
            for (idx = start + 1; idx < args.Length; idx++)
            {
                if (Double.TryParse(args[idx], out d))
                    data.Add(d);
                else
                    break;
            }

            return idx - 1;
        }
        public List<SNetSignalTransEventArgs> AnalysisSignal<T>(object sender, object msg, IList<T> infos)
        {
            var result = new List<SNetSignalTransEventArgs>();

            var line = msg as string;

            var ea = new SNetSignalTransEventArgs();
            ea.Sender = sender;
            var args = line.Split(new char[] { '\0', ' ' });

            ea.Data = new List<double>();

            UInt32 val = 0;
            for (int idx = 0; idx < args.Length; idx++)
            {
                var arg = args[idx];


                if (args[idx] == "-respData" || args[idx] == "-resp_data")
                {
                    continue;
                }
                else if (args[idx] == "-svid" || args[idx] == "-channel")
                {
                    idx++;
                    if (args.Length <= idx) continue;
                    
                    if (UInt32.TryParse(args[idx], out val))
                        ea.Svid = val;
                    continue;
                }
                else if (args[idx] == "-data")
                {
                    idx = ReadData(args, idx, ea.Data);
                    continue;
                }
            }

            result.Add(ea);
            return result;
        }

        public CtkProtocolTrxMessage CreateDataReqMsg<T>(IList<T> reqInfos)
        {
            var listInfo = reqInfos as IList<SNetSignalTransCfg>;
            if (listInfo == null) throw new ArgumentException("未定義此型別的操作方式");
            //public static byte[] TxDataAck { get { return Encoding.UTF8.GetBytes("\n"); } }//減少處理量, 只以換行作為Ack

            var result = new StringBuilder();
            result.Append("cmd -reqData -svid ");

            foreach (var cfg in listInfo)
                result.AppendFormat(" {0} ", cfg.Svid);


            result.Append("\n");//.AppendLine();

            return result.ToString();
        }

        public CtkProtocolTrxMessage CreateAckMsg<T>(IList<T> reqInfos)
        {
            var listInfo = reqInfos as IList<SNetSignalTransCfg>;
            if (listInfo == null) throw new ArgumentException("未定義此型別的操作方式");

            var result = new StringBuilder();
            result.Append("cmd  \n");

            return result.ToString();
        }


    }
}
