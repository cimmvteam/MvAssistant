using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2.Text
{
    public class CtkFormat
    {


        /// <summary>
        /// comma and 小數後2位
        /// </summary>
        public static string ToCurrency<T>(T val) { return string.Format("{0:n}", val); }
        /// <summary>
        /// comma and 小數後0位
        /// </summary>
        public static string ToMoney<T>(T val) { return string.Format("{0:#,0}", val); }



    }
}
