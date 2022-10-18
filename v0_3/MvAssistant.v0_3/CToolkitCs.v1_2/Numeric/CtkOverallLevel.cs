using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2.Numeric
{
    public class CtkOverallLevel : List<CtkOverallLevelData>
    {

        public double CalculateOaRms(IList<double> fft)
        {
            var oa = 0.0;

            foreach (var d in this)
            {
                var s = 0.0;
                var n = 0;
                for (int idx = (int)d.low; idx <= d.high; idx++)
                {
                    s += System.Math.Pow(fft[idx], 2);
                    n++;
                }
                s = System.Math.Sqrt(s / n);

                oa += s * d.weight;
            }
            return oa;
        }


        public static CtkOverallLevel Create(int start, int end, int interval)
        {
            var oaLevel = new CtkOverallLevel();
            for (int idx = start; idx <= end; idx += interval)
            {
                var data = new CtkOverallLevelData();
                data.low = start;
                data.high = start + interval;
                if (data.high > end) data.high = end;

                oaLevel.Add(data);
            }
            return oaLevel;
        }




    }
}
