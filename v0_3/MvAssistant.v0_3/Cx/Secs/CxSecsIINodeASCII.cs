using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MvaCodeExpress.v1_1.Secs
{


    public class CxSecsIINodeASCII : CtkSecsIINodeT<Byte>
    {

        public CxSecsIINodeASCII()
        {
            this.m_formatCode = CxSecsIIFormatCode.ASCII;
            this.Data = new List<Byte>();
        }
        public CxSecsIINodeASCII(String value)
        {
            this.m_formatCode = CxSecsIIFormatCode.ASCII;
            this.Data = new List<Byte>();
            this.SetString(value);

        }

        public void AppendString(String str)
        {
            var buf = CxSecsUtil.Utf8GetBytes(str);
            this.Data.AddRange(buf);
        }

        public void ClearString() { this.Data.Clear(); }

        public override void FromSml_DataAdd(System.IO.MemoryStream stm, string input)
        {
            int c = 0;
            var sb = new StringBuilder();
            using (var sr = new System.IO.StringReader(input))
            {

                while ((c = sr.Read()) >= 0)
                {
                    if (c == '"')
                    {
                        while ((c = sr.Read()) >= 0)
                        {
                            if (c == '\\')
                                c = sr.Read();
                            else if (c == '"')
                                break;
                            sb.Append((char)c);
                        }
                    }
                    else if (Regex.IsMatch("" + (char)c, @"\d"))
                    {
                        var buffer = new StringBuilder();
                        buffer.Append((char)c);

                        while ((c = sr.Read()) >= 0)
                        {
                            if (Regex.IsMatch("" + (char)c, @"\s"))
                                break;
                            buffer.Append((char)c);
                        }

                        var strByte = buffer.ToString();
                        if (strByte.IndexOf("0x", StringComparison.OrdinalIgnoreCase) >= 0)
                            sb.Append((char)Convert.ToByte(strByte, 16));
                        else
                            sb.Append((char)Convert.ToByte(strByte));

                    }


                }
            }

            this.Data.AddRange(CxSecsUtil.AsciiGetBytes(sb.ToString()));


        }

        public string GetString()
        {
            return CxSecsUtil.Utf8GetString(this.Data.ToArray());
        }

        public void SetString(string data)
        {
            this.ClearString();
            this.Data.AddRange( CxSecsUtil.Utf8GetBytes(data));
        }

        public override string ToSml(StringBuilder sb, string parentPrefix, string prefix)
        {
            var typeName = GetSmlTypeName(this.FormatCode);

            sb.AppendFormat(parentPrefix + "<{0}", typeName);

            sb.AppendFormat(" \"{0}\"", CxSecsUtil.AsciiGetString(this.Data.ToArray()));
            sb.AppendFormat(">");

            return sb.ToString();
        }



        #region Implicit Operator

        public static implicit operator CxSecsIINodeASCII(string d)
        {
            var rs = new CxSecsIINodeASCII();
            rs.SetString(d);
            return rs;
        }
        public static implicit operator string(CxSecsIINodeASCII d) { return d.GetString(); }

        #endregion

    }
}
