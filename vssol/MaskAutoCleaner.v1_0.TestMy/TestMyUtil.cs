using MvAssistant;
using MvAssistant.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.TestMy
{
    public class TestMyUtil
    {

        public static void RegisterLog()
        {
            MvLog.RegisterEveryLogWrite((ss, ee) =>
            {
                var now = DateTime.Now;

                var sb = new StringBuilder();
                sb.AppendFormat("[{0}] {1}", now.ToString("yyyy/MM/dd HH:mm:ss"), ee.Message);
                if (ee.Exception != null)
                    sb.AppendFormat("; {0} - {1}", ee.Exception.GetType().FullName, ee.Exception.StackTrace);


                System.Diagnostics.Debug.WriteLine(sb.ToString());
            });
        }



    }
}
