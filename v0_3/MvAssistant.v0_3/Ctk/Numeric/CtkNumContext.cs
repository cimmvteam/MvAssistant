using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace MvaCToolkitCs.v1_2.Numeric
{


    public class CtkNumContext
    {
        public bool IsUseCudafy = true;
      



    


        public Complex[] FftForward(double[] input)
        {
            var result = new Complex[input.Length];
            for (int idx = 0; idx < result.Length; idx++)
                result[idx] = new Complex(input[idx], 0);

            return this.FftForward(result);
        }

        public Complex[] FftForward(IEnumerable<double> input) { return this.FftForward(CtkNumConverter.ToSysComplex(input)); }

        public Complex[] FftForward(IEnumerable<Complex> input) { return this.FftForward(input.ToArray()); }

      

        /// <summary>
        /// Return 正確的振幅, 注意 x 軸 Mag 左右對稱
        /// </summary>
        public List<Complex> SpectrumFft(IEnumerable<Complex> fft)
        {
            var result = new List<Complex>();
            var scale = 2.0 / fft.Count();// Math.Net 要選 Matlab FFT 才會用這個
            foreach (var val in fft)
                result.Add(new Complex(val.Real * scale, val.Imaginary * scale));
            return result;
        }

   

        public List<Complex> SpectrumHalfFft(IEnumerable<Complex> fft)
        {
            var result = new List<Complex>();
            var scale = 2.0 / fft.Count();// Math.Net 要選 Matlab FFT 才會用這個
            var ary = fft.ToArray();
            for (int idx = 0; idx < ary.Length / 2; idx++)
                result.Add(ary[idx] * scale);
            return result;
        }

   

        public List<Complex> SpectrumTime(IEnumerable<double> time)
        {
            var fft = this.FftForward(time);
            return this.SpectrumFft(fft);
        }

        public List<Complex> SpectrumTime(IEnumerable<Complex> time)
        {
            var fft = this.FftForward(time);
            return this.SpectrumFft(fft);
        }


        #region Static

        static Dictionary<string, CtkNumContext> singletonMapper = new Dictionary<string, CtkNumContext>();
        public static CtkNumContext GetOrCreate(string key = "")
        {
            if (singletonMapper.ContainsKey(key)) return singletonMapper[key];
            var rs = new CtkNumContext();
            singletonMapper[key] = rs;
            return rs;
        }

        #endregion



    }
}
