using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CToolkit.v1_1.Timing;
using SensingNet.v0_2.TimeSignal;

namespace SensingNet.v0_2.Framework.Storage
{
    /// <summary>
    /// DateTime(yyyy/MM/dd HH:mm:ss+08),UTC timestamp, data1, data2, ...
    /// </summary>
    public class SNetFileStorageFormat_Csv0_2 : SNetFileStorageFormat
    {
        public const string TimeFormat = "yyyy/MM/dd HH:mm:ss zzz";


        public SNetFileStorageFormat_Csv0_2()
        {
            this.FormatName = this.GetType().Name;
        }


        public override void WriteValues(StreamWriter sw, DateTime datetime, IEnumerable<double> values)
        {
            var dt = datetime.ToLocalTime();
            var dtformat = dt.ToString(TimeFormat);
            sw.Write(dtformat);
            foreach (var val in values)
                sw.Write(",{0}", val);
            sw.WriteLine();
        }

        //public override void ReadStream(StreamReader sr, SNetSignalCollector collector)
        //{
        //    SNetSignalPerSec tfbps = null;
        //    for (var line = sr.ReadLine(); line != null; line = sr.ReadLine())
        //    {
        //        //切割資料
        //        var vals = line.Split(',');
        //        if (vals.Length < 2) continue;

        //        //第一筆為 timestamp
        //        var dt = new DateTime(0);
        //        if (!DateTime.TryParseExact(vals[0], TimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt)) continue;


        //        if (tfbps == null)
        //        {
        //            tfbps = new SNetSignalPerSec();
        //            //tfbps.dt = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);//去掉豪秒數, 只有timestamp有這問題
        //            tfbps.dt = dt;
        //        }
        //        else if ((dt - tfbps.dt).TotalSeconds >= 1.0)
        //        {//若時間變更超過一秒, 就加一個物件來儲存
        //            collector.AddLast(tfbps);
        //            tfbps = new SNetSignalPerSec();
        //            tfbps.dt = dt;
        //        }

        //        for (int idx = 1; idx < vals.Length; idx++)
        //        {
        //            var data = 0.0;
        //            if (!double.TryParse(vals[idx], out data)) continue;
        //            tfbps.signals.Add(data);
        //        }

        //    }

        //    //最後一個物件也要儲存進去
        //    if (tfbps.signals.Count > 0 && collector.LastOrDefault() != tfbps)
        //        collector.AddLast(tfbps);


        //}


        public override void ReadTSignal(StreamReader sr, SNetTSignalSecSetF8 tSignal)
        {
            var signals = new List<double>();
            for (var line = sr.ReadLine(); line != null; line = sr.ReadLine())
            {
                //切割資料
                var vals = line.Split(',');
                if (vals.Length < 2) continue;

                //第一筆為 time
                var dt = new DateTime(0);
                if (!DateTime.TryParseExact(vals[0], TimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt)) continue;

                var time = (CtkTimeSecond)dt;
                signals = tSignal.GetOrCreate(time);


                for (int idx = 1; idx < vals.Length; idx++)
                {
                    var val = 0.0;
                    if (!double.TryParse(vals[idx], out val)) continue;
                    signals.Add(val);
                }

            }

        }


    }
}
