using CToolkit.v1_1.Numeric;
using Cudafy.Types;
using MathNet.Numerics.LinearAlgebra.Complex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SensingNet.v0_2.Storage
{
    /// <summary>
    /// 提供每秒的資料收集
    /// 因應人們習慣的最小單位(秒)為收集依據
    /// 
    /// </summary>
    [Obsolete("請使用 SNetDspTimeSignalSetSecond 替代")]
    public class SNetSignalPerSec
    {
        public DateTime dt;
        public List<double> signals = new List<double>();
        public ComplexD[] fft { get; private set; }
        public ComplexD[] spectrum { get; private set; }

        public double Max { get; private set; }
        public double Min { get; private set; }
        public double Avg { get; private set; }

        public ComplexD[] ComputeFft()
        {
            if (this.fft != null) return this.fft;
            this.fft = new ComplexD[this.signals.Count()];
            if (this.signals.Count <= 0) return this.fft;

            this.fft = CtkNumContext.GetOrCreate().FftForwardD(this.signals);
            return this.fft;
        }

        public ComplexD[] ComputeSpectrumH()
        {
            var fft = this.ComputeFft();

            var freqData = CtkNumContext.GetOrCreate().SpectrumFftD(fft);

            this.spectrum = new ComplexD[fft.Length / 2];
            for (int idx = 0; idx < spectrum.Length; idx++)
                spectrum[idx] = freqData[idx];

            return this.spectrum;
        }

        public void Refresh()
        {
            this.fft = null;
            this.spectrum = null;
            this.ComputeSpectrumH();

            this.Max = this.signals.Max();
            this.Min = this.signals.Min();
            this.Avg = this.signals.Average();
        }


        public void Interpolation(int dataSize)
        {
            if (dataSize == this.signals.Count) return;
            var list = CtkNumUtil.Interpolation(this.signals.ToArray(), dataSize);
            this.signals.Clear();
            this.signals.AddRange(list);
        }



    }
}
