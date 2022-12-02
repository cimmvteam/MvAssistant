using MvaCToolkitCs.v1_2;
using MvaCToolkitCs.v1_2.Protocol;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeExpress.v1_1Core.Secs
{
    public class CxHsmsMessage
    {
        public CxHsmsHeader Header;
        public string Name;
        public CxSecsIINode RootNode;


        public CxHsmsMessage()
        {
            CxUtil.CheckLicenseEncrypt();
        }

        public static CxHsmsMessage CtrlMsg_LinktestReq()
        {
            var msg = new CxHsmsMessage();
            msg.Header.DeviceId = Convert.ToInt16("0xffff", 16);
            msg.Header.SType = 5;
            return msg;
        }

        public static CxHsmsMessage CtrlMsg_LinktestRsp()
        {
            var msg = new CxHsmsMessage();
            msg.Header.DeviceId = Convert.ToInt16("0xffff", 16);
            msg.Header.SType = 6;
            return msg;
        }

        public static CxHsmsMessage CtrlMsg_SelectReq()
        {
            var msg = new CxHsmsMessage();
            msg.Header.DeviceId = Convert.ToInt16("0xffff", 16);
            msg.Header.SType = 1;
            return msg;
        }

        public static CxHsmsMessage CtrlMsg_SelectRsp(byte selectStatus)
        {
            var msg = new CxHsmsMessage();
            msg.Header.DeviceId = Convert.ToInt16("0xffff", 16);
            msg.Header.SType = 2;
            msg.Header.FunctionId = selectStatus; //stype=2 -> FunctionID=SelectStatus
            return msg;
        }

        public static CxHsmsMessage CtrlMsg_SeparateReq()
        {
            //Secs告知對方要中斷連線, 所以沒有response
            //下一步就直接中斷
            var msg = new CxHsmsMessage();
            msg.Header.DeviceId = Convert.ToInt16("0xffff", 16);
            msg.Header.SType = 9;
            return msg;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataBytes">data bytes must be completely message</param>
        /// <returns></returns>
        public static CxHsmsMessage GetFromBytes(byte[] dataBytes)
        {
            var msg = new CxHsmsMessage();

            using (var stm = new System.IO.MemoryStream(dataBytes))
            {
                var headerBytes = new byte[10];
                stm.Read(headerBytes, 0, headerBytes.Length);
                msg.Header = CxHsmsHeader.FromBytes(headerBytes);
                msg.RootNode = CxSecsIINode.GetFromBytes(stm);
            }
            return msg;
        }


        public static CxHsmsMessage GetFromSmlFile(string file)
        {
            var sml = File.ReadAllText(file);
            return GetFromSml(sml);
        }

        public static CxHsmsMessage GetFromSml(string sml)
        {
            var msg = new CxHsmsMessage();

            var regex = new Regex(@"((?<name>[^\:]*?)\:)?\s*'?(?<sxfy>S\d+F\d+?)[^\d]", RegexOptions.IgnoreCase);
            var match = regex.Match(sml);//第一個相符的

            msg.Name = match.Groups["name"].Value.Trim().Replace("\r", "").Replace("\n", "");
            var sxfy = match.Groups["sxfy"].Value;
            if (sxfy.Length < 4) throw new Exception("找不到Stream & Function ID");
            var xy = sxfy.Trim().ToLower().Substring(1).Split('f');
#if DEBUG
            System.Diagnostics.Debug.Assert(xy.Length == 2);//Function有問題, 用Assert
#endif
            msg.Header.StreamId = Convert.ToByte(xy[0]);
            msg.Header.FunctionId = Convert.ToByte(xy[1]);

            msg.Header.WBit = Regex.IsMatch(sml, @"S\d+F\d+[^\d]\s*(W|\[W\])");


            using (var stm = new System.IO.MemoryStream(Encoding.UTF8.GetBytes(sml)))
            {
                int c = 0;
                while ((c = stm.ReadByte()) >= 0)
                {
                    if (c == '<')
                    {
                        stm.Seek(-1, System.IO.SeekOrigin.Current);
                        msg.RootNode = CxSecsIINode.GetFromSml(stm);
                    }
                }


            }



            return msg;
        }

        public static implicit operator CtkProtocolTrxMessage(CxHsmsMessage msg) { return new CtkProtocolTrxMessage() { TrxMessage = msg }; }

        public byte[] ToBytes()
        {
            var nodeBuffer = this.RootNode == null ? null : this.RootNode.ToBytes();
            return CxSecsUtil.MsgToByte(this.Header.ToBytes(), nodeBuffer);
        }


        public string ToSml(string parentPrefix = "\t", string prefix = "\t")
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("S{0}F{1}", this.Header.StreamId, this.Header.FunctionId);
            sb.AppendLine();
            if (this.RootNode != null)
                this.RootNode.ToSml(sb, parentPrefix, prefix);

            sb.Append("."); // A period(.) ends the message definition
            return sb.ToString();
        }
    }
}
