using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace CToolkitCs.v1_2.Numeric
{
    public class CtkNumConverter
    {



        public static double[] ToImginary(Complex[] input)
        {
            var result = new double[input.Length];
            ToImgine(input, result);
            return result;
        }
        public static double[] ToImginary(IEnumerable<Complex> input) { return ToImginary(input.ToArray()); }
      

       

        public static void ToImgine(Complex[] input, double[] output)
        {
            for (int idx = 0; idx < input.Length; idx++)
                output[idx] = input[idx].Imaginary;
        }

        public static double[] ToMagnitude(Complex[] input)
        {
            var result = new double[input.Length];
            ToMagnitude(input, result);
            return result;
        }
        public static double[] ToMagnitude(IEnumerable<Complex> input) { return ToMagnitude(input.ToArray()); }
        public static void ToMagnitude(Complex[] input, double[] output)
        {
            for (int idx = 0; idx < input.Length; idx++)
                output[idx] = input[idx].Magnitude;
        }

      


        public static double[] ToReal(Complex[] input)
        {
            var result = new double[input.Length];
            ToReal(input, result);
            return result;
        }
        public static double[] ToReal(IEnumerable<Complex> input) { return ToReal(input.ToArray()); }
        public static void ToReal(Complex[] input, double[] output)
        {
            for (int idx = 0; idx < input.Length; idx++)
                output[idx] = input[idx].Real;
        }



        public static Complex[] ToSysComplex(double[] input)
        {
            var result = new Complex[input.Length];
            ToSysComplex(input, result);
            return result;
        }
        public static Complex[] ToSysComplex(IEnumerable<double> input)
        {
            var result = new Complex[input.Count()];
            ToSysComplex(input.ToArray(), result);
            return result;
        }
        public static void ToSysComplex(double[] input, Complex[] output)
        {
            for (int idx = 0; idx < input.Length; idx++)
                output[idx] = new Complex(input[idx], 0);
        }




    }
}
