using CToolkit.v1_1.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensingNet.v0_2.DvcSensor.SignalTrans
{
    /// <summary>
    /// 資料的分析處理
    /// Protocol 解譯後, 要如何處理使用資料
    /// </summary>
    public interface ISNetSignalTransBase
    {

        List<SNetSignalTransEventArgs> AnalysisSignal<T>(object sender, object msg, IList<T> infos);
        CtkProtocolTrxMessage CreateDataReqMsg<T>(IList<T> reqInfos);
        CtkProtocolTrxMessage CreateAckMsg<T>(IList<T> reqInfos);


    }
}
