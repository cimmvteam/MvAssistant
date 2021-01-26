using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Mac
{
    public class MvaMacUtil
    {


        public static Dictionary<string, string> GetDevConnStr(string connStr)
        {
            if (string.IsNullOrEmpty(connStr)) return new Dictionary<string, string>();

            var settings = (from row in connStr.Split(new char[] { ';' })
                            where !string.IsNullOrEmpty(row)
                            select row.Trim()).ToList();

            //Key 全轉小寫
            var dict = (from row in settings
                        where row.Contains("=")
                        select new { row = row, idx = row.IndexOf("=") }
                        ).ToDictionary(x => x.row.Substring(0, x.idx).ToLower(), x => x.row.Substring(x.idx + 1));
            return dict;
        }
    }
}
