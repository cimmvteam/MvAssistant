using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2.Extension
{
    public static class CtkExtNumberic
    {
        public static double CtkValueOrZero(this Nullable<double> val)
        {
            if (val.HasValue)
                return val.Value;

            return 0.0;
        }

        public static double CtkVal(this Nullable<double> val, double defVal)
        {
            if (val.HasValue)
                return val.Value;

            return defVal;
        }

        public static decimal CtkValueOrZero(this Nullable<decimal> val)
        {
            if (val.HasValue)
                return val.Value;
            return 0;
        }

        public static double CtkDoubleValueOrZero(this Nullable<decimal> val)
        {
            if (val.HasValue)
                return (double)val.Value;
            return 0;
        }

        public static decimal CtkCtkParseVal(this Nullable<decimal> val)
        {
            if (val.HasValue)
                return val.Value;

            throw new ArgumentException("值為null");
        }

    }
}
