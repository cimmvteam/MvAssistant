using CToolkit.v1_1;
using CToolkit.v1_1.Timing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SensingNet.v0_2.Storage
{
    /// <summary>
    /// TC timestamp, data1, data2, ...
    /// </summary>
    [Obsolete("Please use FileStorageFormat_Csv0_1")]
    public class FileStorageFormat_Csv0_0 : SNetFileStorageFormat
    {

        public FileStorageFormat_Csv0_0()
        {
            this.FormatName = this.GetType().Name;
        }




        public override void WriteValues(StreamWriter sw, DateTime utc, IEnumerable<double> values)
        {
            //if utc.Kind is Unspecified then as UTC
            if (utc.Kind == DateTimeKind.Unspecified)
                utc = DateTime.SpecifyKind(utc, DateTimeKind.Utc);

            //Loca / Utc ToUtcTimestamp 皆會轉成 UTC
            var utcTimestamp = CtkTimeUtil.ToUtcTimestamp(utc);
            var localDt = utc.ToLocalTime();

            sw.Write("{0}", utcTimestamp);
            foreach (var val in values)
                sw.Write(",{0}", val);
            sw.WriteLine();

        }


        public override void ReadStream(StreamReader sr, SNetSignalCollector collector)
        {
            SNetSignalPerSec tfbps = null;
            for (var line = sr.ReadLine(); line != null; line = sr.ReadLine())
            {
                //切割資料
                var vals = line.Split(',');
                if (vals.Length < 2) continue;

                //第一筆為 timestamp
                var timestamp = 0.0;
                if (!double.TryParse(vals[0], out timestamp)) continue;

                //來源時間為Universal (檔案儲存時間)
                var dt = CtkTimeUtil.ToLocalDateTimeFromTimestamp(timestamp);

                if (tfbps == null)
                {
                    tfbps = new SNetSignalPerSec();
                    tfbps.dt = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
                }
                else if ((dt - tfbps.dt).TotalSeconds >= 1.0)
                {//若時間變更超過一秒, 就加一個物件來儲存

                    collector.AddLast(tfbps);
                    tfbps = new SNetSignalPerSec();
                    tfbps.dt = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
                }

                for (int idx = 1; idx < vals.Length; idx++)
                {
                    var data = 0.0;
                    if (!double.TryParse(vals[idx], out data)) continue;
                    tfbps.signals.Add(data);
                }

            }
            if (tfbps.signals.Count > 0 && collector.LastOrDefault() != tfbps)
                collector.AddLast(tfbps);


        }

    }
}
