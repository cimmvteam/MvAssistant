using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvaCToolkitCs.v1_2.Diagnostics
{
    public class CtkStopwatch : System.Diagnostics.Stopwatch
    {
        public List<String> HistoryMessage = new List<string>();


        public CtkStopwatch() { }
        ~CtkStopwatch() { this.Clear(); }
        public CtkStopwatch(bool restart) { if (restart) this.Restart(); }

        public String Message(string format) { return string.Format(format, this.ElapsedMilliseconds); }

        public String MsgRestart(string format)
        {
            this.Stop();
            var msg = this.Message(format);
            this.Restart();
            return msg;
        }
        public String MsgStop(string format)
        {
            this.Stop();
            var msg = this.Message(format);
            return msg;
        }


        public string AppendMsg(string format)
        {
            var msg = this.Message(format);
            this.HistoryMessage.Add(msg);
            return msg;
        }

        public void Clear()
        {
            this.Stop();
            this.Reset();
            this.HistoryMessage.Clear();
        }
        public string GetMessage(String separator = "\r\n") { return String.Join(separator, this.HistoryMessage); }

    }


}
