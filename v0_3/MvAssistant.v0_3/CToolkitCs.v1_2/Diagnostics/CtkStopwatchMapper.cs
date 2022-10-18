using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2.Diagnostics
{
    public class CtkStopwatchMapper : Dictionary<string, CtkStopwatch>
    {


        static CtkStopwatchMapper m_singleton;

        public static CtkStopwatchMapper Singleton
        {
            get
            {
                if (m_singleton == null)
                    m_singleton = new CtkStopwatchMapper();
                return m_singleton;
            }
        }

        public static CtkStopwatch SGet(string key = "")
        {
            var me = CtkStopwatchMapper.Singleton;
            lock (me)
                if (!me.ContainsKey(key))
                    me[key] = new CtkStopwatch();

            return me[key];
        }

        public static String SMsgRestart(String format, string key = "")
        {
            var sw = SGet(key);
            return sw.MsgRestart(format);
        }

        public static String SMsgStop(String format, string key = "")
        {
            var sw = SGet(key);
            return sw.MsgStop(format);
        }

        public static CtkStopwatch SRestart(string key = "")
        {
            var sw = SGet(key);
            sw.Restart();
            return sw;
        }

        public static CtkStopwatch SStop(string key = "")
        {
            var sw = SGet(key);
            sw.Stop();
            return sw;
        }







    }
}