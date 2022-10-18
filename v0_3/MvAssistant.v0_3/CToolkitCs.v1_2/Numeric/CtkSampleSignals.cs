using MathNet.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace CToolkitCs.v1_2.Numeric
{
    public class CtkSampleSignals
    {

        
        public static Complex[] GetCase1(int length = 1000, int samplingRate = 1000)
        {

            var s1 = Generate.Sinusoidal(length, samplingRate, 50.0, 0.7);
            var s2 = Generate.Sinusoidal(length, samplingRate, 120, 1.0);


            var ss = new Complex[length];
            for (int idx = 0; idx < length; idx++)
            {
                ss[idx] = new Complex(s1[idx] + s2[idx], 0.0);
            }

            return ss;
        }


        public static Complex[] GetCase2(int length = 1000)
        {
            double samplingRate = length / 3.0;

            Complex[] timeDomain = new Complex[length];
            double[] s1 = Generate.Sinusoidal(length, samplingRate, 1.0, 10.0);
            double[] s2 = Generate.Sinusoidal(length, samplingRate, 6.0, 2.0);

            double[] s = new double[length];
            for (int i = 0; i < s.Length; i++)
                s[i] = s1[i] + s2[i];


            return timeDomain;
        }



        public static Complex[] GetOrignal(int length = 1000, double samplingRate = 100, double freq =1.0, double amp = 10.0)
        {
            Complex[] ss = new Complex[length];
            //freq = 2.0, samplingRate = 1000, 代表頻率為2的資料被取樣1000
            //若資料長度為 1000, 則 可看到2個弦波
            //若資料長度為 2000, 則 可看到4個弦波
            double[] s1 = Generate.Sinusoidal(length, samplingRate, freq, amp);
            for (int i = 0; i < ss.Length; i++)
                ss[i] = s1[i];
            return ss;
        }
    }
}
