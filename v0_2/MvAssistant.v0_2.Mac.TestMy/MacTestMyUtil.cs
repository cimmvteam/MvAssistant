using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Mac.TestMy
{
    public class MacTestMyUtil
    {
        public static void RegisterLog()
        {
            MvLog.RegisterEveryLogWrite((ss, ee) =>
            {
                var now = DateTime.Now;

                var sb = new StringBuilder();
                sb.AppendFormat("[{0}][{1}]", now.ToString("yyyy/MM/dd HH:mm:ss"), ee.Level);
                sb.AppendFormat(" {0}", ee.Message);
                if (ee.Exception != null)
                {
                    sb.AppendFormat(";{0}-{1} - {2}"
                        , ee.Exception.Message
                        ,ee.Exception.GetType().FullName
                        , ee.Exception.StackTrace);
                }

                System.Diagnostics.Debug.WriteLine(sb.ToString());
            });
        }


    }
}
