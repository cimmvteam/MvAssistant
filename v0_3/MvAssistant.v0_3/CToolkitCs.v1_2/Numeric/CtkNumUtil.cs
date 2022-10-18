using MathNet.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace MvaCToolkitCs.v1_2.Numeric
{
    public class CtkNumUtil
    {


        public static double[] Interpolation(double[] input, int dataSize)
        {
            var points = Generate.LinearSpaced(input.Length, 0, input.Length - 1);
            var xs = Generate.LinearSpaced(dataSize, 0, input.Length - 1);


            var output = new double[dataSize];
            if (input.Length == dataSize)
            {
                Array.Copy(input, output, dataSize);
            }
            else
            {
                //var method = Interpolate.CubicSpline(points, input);
                var method = Interpolate.Linear(points, input);
                for (int idx = 0; idx < dataSize; idx++)
                    output[idx] = method.Interpolate(xs[idx]);
            }
            return output;
        }

        public static double[] Interpolation(IEnumerable<double> input, int dataSize) { return Interpolation(input.ToArray(), dataSize); }

        public static double[] InterpolationCanOneOrZero(double[] input, int dataSize)
        {
            if (input.Length < 2)
            {
                var rs = new double[input.Length];
                Array.Copy(input, rs, input.Length);
                return rs;
            }

            return Interpolation(input, dataSize);
        }





        /// <summary>
        /// 若只有0筆或1筆, 就回傳0或1筆, 不會有錯誤
        /// </summary>
        /// <param name="input"></param>
        /// <param name="dataSize"></param>
        /// <returns></returns>
        public static double[] InterpolationCanOneOrZero(IEnumerable<double> input, int dataSize) { return InterpolationCanOneOrZero(input.ToArray(), dataSize); }


        public static double[] InterpolationForce(double[] input, int dataSize)
        {
            if (input.Length == 0) throw new ArgumentException("no data can computation");
            if (input.Length == 1)
            {
                var rs = new double[2];
                rs[0] = rs[1] = input[0];
                return Interpolation(rs, dataSize);
            }
            return Interpolation(input, dataSize);
        }

        /// <summary>
        /// 若只有1筆, 強制展開, 即 每筆都同樣的資料. 0筆沒辦法展開, 會拋錯誤
        /// </summary>
        /// <param name="input"></param>
        /// <param name="dataSize"></param>
        /// <returns></returns>
        public static double[] InterpolationForce(IEnumerable<double> input, int dataSize) { return InterpolationCanOneOrZero(input.ToArray(), dataSize); }


        /// <summary>
        /// Get level of c between a and b
        /// </summary>
        /// <param name="a">Left</param>
        /// <param name="b">Righ</param>
        /// <param name="c">you value</param>
        /// <returns>level of c</returns>
        public static double Interpolate1d(double a, double b, double c)
        {
            var A = (b - a);
            var B = (c - a);
            return A * B / Math.Abs(A);
        }
        public static double Interpolate1d(double? a, double? b, double? c) { return Interpolate1d(a.Value, b.Value, c.Value); }

    }
}
