using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2.Mathing
{
    public class CtkMathOpUtil
    {


        public static int Factorial(int n)
        {
            int rs = 1;
            for (int i = 1; i <= n; i++) { rs *= i; }
            return rs;
        }



        public static int Combinations(int n, int r)
        {
            int rs = 1;
            for (int i = n - r + 1; i <= n; i++)
            { rs *= i; }
            for (int i = 2; i <= r; i++)
            { rs /= i; }
            return rs;
        }


        public static int GreatestCommonDivisor(int a, int b)
        {
            if (a == 0 || b == 0)
            { return 0; }

            while (a > 0 && b > 0)
            {
                if (a > b)
                    a = a % b;
                else
                    b = b % a;
            }
            if (a == 0) { return b; }
            else { return a; }
        }

    }
}
