using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2.Mathing
{
    public class CtkGcd
    {
        public static int GCD(int a, int b)
        {
            while (b > 1)
            {
                int mod = a % b;
                a = b;
                b = mod;
            }
            if (b == 0) { return a; }
            return b;

        }
    }
}
