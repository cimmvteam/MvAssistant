using MathNet.Filtering.FIR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CToolkitCs.v1_2.Numeric
{
    public class CtkFftOnlineFilter
    {
        OnlineFirFilter filter;

        public CtkPassFilterStruct FilterArgs = new CtkPassFilterStruct();

        public bool isCalibrated { get; private set; }

        public OnlineFirFilter SetFilter(CtkPassFilterStruct pfarg)
        {
            var flag = this.filter != null;
            flag &= this.FilterArgs.Mode == pfarg.Mode;
            flag &= this.FilterArgs.SampleRate == pfarg.SampleRate;
            flag &= this.FilterArgs.CutoffLow == pfarg.CutoffLow;
            flag &= this.FilterArgs.CutoffHigh == pfarg.CutoffHigh;

            if (flag) return filter;

            this.FilterArgs = pfarg;


            var coff = new double[0];
            switch (this.FilterArgs.Mode)
            {
                case CtkEnumPassFilterMode.BandPass:
                    coff = FirCoefficients.BandPass(this.FilterArgs.SampleRate, this.FilterArgs.CutoffLow, this.FilterArgs.CutoffHigh);
                    break;
                case CtkEnumPassFilterMode.LowPass:
                    coff = FirCoefficients.LowPass(this.FilterArgs.SampleRate, this.FilterArgs.CutoffLow);
                    break;
                case CtkEnumPassFilterMode.HighPass:
                    coff = FirCoefficients.HighPass(this.FilterArgs.SampleRate, this.FilterArgs.CutoffHigh);
                    break;
                default:
                    throw new NotImplementedException();
            }

            filter = new OnlineFirFilter(coff);
            return filter;
        }


        public double[] ProcessSamples(IEnumerable<double> samples)
        {
            return this.ProcessSamples(samples.ToArray());
        }
        public double[] ProcessSamples(double[] samples)
        {
            if (this.filter == null) return samples;
            return this.filter.ProcessSamples(samples);
        }

        public void ProcessSamplesCalibrate(double[] samples, int times)
        {
            for (int idx = 0; idx < times; idx++)
                this.ProcessSamples(samples);

            this.isCalibrated = true;

        }

    }
}
