using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeExpress.v1_1Core.Secs
{
    public class CxHsmsMessageSet : List<CxHsmsMessage>
    {



        public void LoadSml(string path)
        {
            using (var fs = new System.IO.FileStream(path, System.IO.FileMode.Open))
            {
                var bytesMsg = new List<byte>();
                int c = 0;

                //SML message 切割方法
                Stack<int> nodeStack = new Stack<int>();
                while ((c = fs.ReadByte()) >= 0)
                {

                    if (c == '"')
                    {//把字串讀完
                        bytesMsg.Add((byte)c);
                        while ((c = fs.ReadByte()) >= 0)
                        {
                            if (c == '\\')
                                c = fs.ReadByte();
                            else if (c == '"')
                                break;
                            bytesMsg.Add((byte)c);
                        }
                    }

                    //讀完Node
                    if (c == '<')
                        nodeStack.Push(c);
                    else if (c == '>')
                        nodeStack.Pop();

                    bytesMsg.Add((byte)c);

                    if (c == '.' && nodeStack.Count == 0)
                    {
                        var buf = bytesMsg.ToArray();
                        var msg = CxHsmsMessage.GetFromSml(Encoding.UTF8.GetString(buf, 0, buf.Length));
                        this.Add(msg);
                        bytesMsg.Clear();
                    }
                }
            }
        }


        public CxHsmsMessage Get(string nameSxFy)
        {
            var q = from n in this
                    where string.Format("{0} S{1}F{2}"
                        , n.Name, n.Header.StreamId, n.Header.FunctionId)
                        .ToLower().Contains(nameSxFy.ToLower())
                    select n;
            return q.FirstOrDefault();
        }




    }
}
