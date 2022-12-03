using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace MvaCodeExpress.v1_1.Secs
{

    /// <summary>
    /// Secs II 是一種格式.
    /// HSMS 是一種通訊協定.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class CtkSecsIINodeT<T> : CxSecsIINode
    {

        public List<T> Data { get { return (List<T>)this.m_dataObj; } set { this.m_dataObj = value; } }
        public T this[int index] { get { return this.Data[index]; } set { this.Data[index] = value; } }

        public T DataFirstOrDefault() { return this.Data.FirstOrDefault(); }
        public S DataFirstOrDefaultAs<S>() where S : CxSecsIINode { return this.Data.FirstOrDefault() as S; }
        public S DataGetAs<S>(int index) where S : CxSecsIINode { return this.Data[index] as S; }
        public void DataSetSingle(T data) { this.Data.Clear(); this.Data.Add(data); }



        #region Data Transfer

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stm">start with FormatCode</param>
        public override void FromBytes(System.IO.MemoryStream stm)
        {
            var formatCode = stm.ReadByte();
            var noOfLenBytes = formatCode & 0x3;
            formatCode >>= 2;
            System.Diagnostics.Debug.Assert((int)this.FormatCode == formatCode);
            var lenBytes = new List<byte>();
            for (int idx = 0; idx < noOfLenBytes; idx++)
                lenBytes.Add((byte)stm.ReadByte());
            var length = GetLengthFromLengthBytes(lenBytes.ToArray());

            //var typeSize = Marshal.SizeOf(typeof(T));
            var typeSize = Marshal.SizeOf<T>();

            // length = number of data * type size
            var dataSize = length / typeSize;
            T[] dataAry = new T[dataSize];
            for (int idx = 0; idx < dataSize; idx++)
                if (BitConverter.IsLittleEndian)
                    for (int jdx = typeSize - 1; jdx >= 0; jdx--)
                        System.Buffer.SetByte(dataAry, idx * typeSize + jdx, (byte)stm.ReadByte());
                else
                    for (int jdx = 0; jdx < typeSize; jdx++)
                        System.Buffer.SetByte(dataAry, idx * typeSize + jdx, (byte)stm.ReadByte());

            this.Data.AddRange(dataAry);
        }
        public override void FromSml(System.IO.MemoryStream stm)
        {
            var sbNode = new List<byte>();
            sbNode.Add((byte)stm.ReadByte());
            if (sbNode[0] != '<')
                throw new CxSecsException("第一個字元必須為 '<'");

            //取得此Node到下個節點(包含子節點)之間的字串
            int c = 0;
            var flagDoubleQuotation = false;
            var flagSlash = false;
            while ((c = stm.ReadByte()) >= 0)
            {
                if (c == '"')//遇到雙引號要直到下一個才停止
                    if (flagDoubleQuotation)
                        flagDoubleQuotation = flagSlash ? true : false;//若在字串裡的雙引號前面有slash, 不算結束字串
                    else
                        flagDoubleQuotation = true;

                flagSlash = c == '\\';//在此前的flagSlash為上一次的

                if (!flagDoubleQuotation)
                    if (c == '<' || c == '>')//另一個Node的開始 || 這個Node的結束
                        break;

                sbNode.Add((byte)c);
            }
            if (c == '<')//另一個Node的開頭要還回去
                stm.Seek(-1, System.IO.SeekOrigin.Current);


            //input 沒有結尾'>', ex. <typeName xxx xxx xxx
            var input = CxSecsUtil.Utf8GetString(sbNode.ToArray());
            this.FromSml_DataAdd(stm, input);

        }
        public override void FromSml_DataAdd(System.IO.MemoryStream stm, string input)
        {
            var match = CxSecsIINode.FormatMatch(input);
            var formatCode = GetSmlFormatCode(match.Groups["typeName"].Value);

            if (formatCode != this.FormatCode)
                throw new CxSecsException("讀出的Type與此類別不合");

            var dataAry = Regex.Split(match.Groups["data"].Value, @"\s", RegexOptions.IgnorePatternWhitespace);

            foreach (var data in dataAry)
            {
                var mydata = data.Replace("\t", " ");
                mydata = data.Replace("\r", " ");
                mydata = data.Replace("\n", " ");
                mydata = data.Trim();
                if (string.IsNullOrEmpty(data)) continue;

                this.Data.Add(this.FromSml_DataString(data));
            }
        }
        public virtual T FromSml_DataString(string obj) { return (T)Convert.ChangeType(obj, typeof(T)); }
        /// <summary>
        /// Note: zero length data may cause some device can not identify
        /// </summary>
        /// <returns></returns>
        public override byte[] ToBytes()
        {
            var ary = this.Data.ToArray();

            //var typeSize = Marshal.SizeOf(typeof(T));
            var typeSize = Marshal.SizeOf<T>();
            var dataSize = ary.Length;

            Byte[] dataBytes = new Byte[dataSize * typeSize];
            GCHandle gcHandle = GCHandle.Alloc(ary, GCHandleType.Pinned);
            try { Marshal.Copy(gcHandle.AddrOfPinnedObject(), dataBytes, 0, dataBytes.Length); }
            finally { gcHandle.Free(); }

            if (BitConverter.IsLittleEndian)
                CxSecsUtil.EndianSwitch(dataBytes, 0, dataSize, typeSize);




            var length = dataBytes.Length;
            if (length > 0xffffff)
                throw new Exception("資料長度過大");

            List<Byte> rs = new List<byte>(GetLengthBytesFromLength(length));
            var formatByte = ((int)this.m_formatCode << 2) + rs.Count;
            rs.Insert(0, (byte)formatByte);
            rs.AddRange(dataBytes);

            return rs.ToArray();
        }
        public override string ToSml(StringBuilder sb, string parentPrefix = "\t", string prefix = "\t")
        {
            var typeName = GetSmlTypeName(this.FormatCode);

            sb.AppendFormat(parentPrefix + "<{0}[{1}]", typeName, this.Data.Count);
            foreach (var d in this.Data)
                sb.AppendFormat(" {0}", this.ToSml_DataString(d));
            sb.AppendFormat(">");

            return sb.ToString();
        }
        public virtual string ToSml_DataString(Object obj) { return string.Format("{0}", obj); }

        #endregion


    }




    public abstract class CxSecsIINode
    {
        //public const string NodeTypeCountData = @"^\<\s*(?<typeName>\w+?)($|\s*\[(?<count>\d+?)\]|\s+)(?<data>.*?)";
        //public const string NodeTypeCountData = @"^\<\s*(?<typeName>\w+?)($|\s*\[(?<count>\d+?)\]\s*(?<data>.*?)$|\s*^\[(?<data>.*?)$)";
        public const string NodeTypeCountData1 = @"^\<\s*(?<typeName>[^\s\[]+?)\s*\[(?<count>\d+?)\](?<data>.*?)$";
        public const string NodeTypeCountData2 = @"^\<\s*(?<typeName>[^\s\[]+?)\s*\[(?<count>\d+?)\](?<data>.*?)";
        public const string NodeTypeCountData3 = @"^\<\s*(?<typeName>[^\s\[]+?)\s*(?<data>.*?)$";


        protected Object m_dataObj;
        protected CxSecsIIFormatCode m_formatCode = CxSecsIIFormatCode.List;
        public CxSecsIINode()
        {
        }


        public Object DataObj { get { return m_dataObj; } }
        public CxSecsIIFormatCode FormatCode { get { return m_formatCode; } }

        public T As<T>() where T : CxSecsIINode { return this as T; }


        public Object DataObjFirst() { return this.DataObjGet(0); }
        public Object DataObjFirstOrDefault() { return this.DataObjGetOrDefault(0); }
        public Object DataObjGet(int index) { return (this.DataObj as IList)[index]; }
        public Object DataObjGetOrDefault(int index)
        {
            if (this.DataObj == null) return null;
            var list = this.DataObj as IList;
            if (list == null) return null;
            if (list.Count == 0) return null;
            return this.DataObjGet(index);
        }


        #region Data Transfer

        public virtual void FromBytes(System.IO.MemoryStream stm) { throw new NotImplementedException("請實作此方法"); }
        public virtual void FromSml(System.IO.MemoryStream stm) { throw new NotImplementedException("請實作此方法"); }
        public virtual void FromSml_DataAdd(System.IO.MemoryStream stm, string input) { throw new NotImplementedException("請實作此方法"); }
        public virtual byte[] ToBytes() { throw new NotImplementedException("請實作此方法"); }
        public virtual string ToSml(StringBuilder sb, string prefix, string prefixPattern) { throw new NotImplementedException("請實作此方法"); }

        #endregion









        #region Static


        public static Match FormatMatch(string input)
        {
            var match = Regex.Match(input, CxSecsIINode.NodeTypeCountData1);
            if (string.IsNullOrEmpty(match.Groups["count"].Value))
                match = Regex.Match(input, CxSecsIINode.NodeTypeCountData2);
            if (string.IsNullOrEmpty(match.Groups["count"].Value))
                match = Regex.Match(input, CxSecsIINode.NodeTypeCountData3);
            return match;
        }

        public static CxSecsIINode GetFromBytes(System.IO.MemoryStream stm)
        {
            CxSecsIINode node = null;

            //可能有 header 但空資料
            var firstByte = stm.ReadByte();//讀一個 出來判斷
            if (firstByte < 0) return null;//空資料不轉換
            stm.Seek(-1, System.IO.SeekOrigin.Current);//倒退一個

            var formatCode = (CxSecsIIFormatCode)(firstByte >> 2);


            switch (formatCode)
            {
                case CxSecsIIFormatCode.List: node = new CxSecsIINodeList(); break;
                case CxSecsIIFormatCode.Binary: node = new CxSecsIINodeBinary(); break;
                case CxSecsIIFormatCode.Boolean: node = new CxSecsIINodeBoolean(); break;
                case CxSecsIIFormatCode.ASCII: node = new CxSecsIINodeASCII(); break;
                case CxSecsIIFormatCode.JIS8: node = new CxSecsIINodeJIS8(); break;
                case CxSecsIIFormatCode.Int64: node = new CxSecsIINodeInt64(); break;
                case CxSecsIIFormatCode.Int8: node = new CxSecsIINodeInt8(); break;
                case CxSecsIIFormatCode.Int16: node = new CxSecsIINodeInt16(); break;
                case CxSecsIIFormatCode.Int32: node = new CxSecsIINodeInt32(); break;
                case CxSecsIIFormatCode.Float64: node = new CxSecsIINodeFloat64(); break;
                case CxSecsIIFormatCode.Float32: node = new CxSecsIINodeFloat32(); break;
                case CxSecsIIFormatCode.UInt64: node = new CxSecsIINodeUInt64(); break;
                case CxSecsIIFormatCode.UInt8: node = new CxSecsIINodeUInt8(); break;
                case CxSecsIIFormatCode.UInt16: node = new CxSecsIINodeUInt16(); break;
                case CxSecsIIFormatCode.UInt32: node = new CxSecsIINodeUInt32(); break;
                default: throw new Exception("未設定的FormatCode");
            }

            node.FromBytes(stm);
            return node;
        }



        public static CxSecsIINode GetFromSml(System.IO.MemoryStream stm)
        {
            CxSecsIINode node = null;
            var startIndex = stm.Position;
            List<byte> sbNode = new List<byte>();

            sbNode.Add((byte)stm.ReadByte());
            if (sbNode[0] != '<')
                throw new CxSecsException("第一個字元必須為 '<'");

            //取得此Node到下個節點(包含子節點)之間的字串
            int c = 0;
            while ((c = stm.ReadByte()) >= 0)
            {
                if (c == '<' || c == '>')
                    break;
                sbNode.Add((byte)c);
            }
            stm.Seek(-1, System.IO.SeekOrigin.Current);


            var sbbytes = sbNode.ToArray();
            var input = CxSecsUtil.Utf8GetString(sbbytes, 0, sbbytes.Length);
            var match = FormatMatch(input);

            var formatCode = GetSmlFormatCode(match.Groups["typeName"].Value);
            switch (formatCode)
            {
                case CxSecsIIFormatCode.List: node = new CxSecsIINodeList(); break;
                case CxSecsIIFormatCode.Binary: node = new CxSecsIINodeBinary(); break;
                case CxSecsIIFormatCode.Boolean: node = new CxSecsIINodeBoolean(); break;
                case CxSecsIIFormatCode.ASCII: node = new CxSecsIINodeASCII(); break;
                case CxSecsIIFormatCode.JIS8: node = new CxSecsIINodeJIS8(); break;
                case CxSecsIIFormatCode.Int64: node = new CxSecsIINodeInt64(); break;
                case CxSecsIIFormatCode.Int8: node = new CxSecsIINodeInt8(); break;
                case CxSecsIIFormatCode.Int16: node = new CxSecsIINodeInt16(); break;
                case CxSecsIIFormatCode.Int32: node = new CxSecsIINodeInt32(); break;
                case CxSecsIIFormatCode.Float64: node = new CxSecsIINodeFloat64(); break;
                case CxSecsIIFormatCode.Float32: node = new CxSecsIINodeFloat32(); break;
                case CxSecsIIFormatCode.UInt64: node = new CxSecsIINodeUInt64(); break;
                case CxSecsIIFormatCode.UInt8: node = new CxSecsIINodeUInt8(); break;
                case CxSecsIIFormatCode.UInt16: node = new CxSecsIINodeUInt16(); break;
                case CxSecsIIFormatCode.UInt32: node = new CxSecsIINodeUInt32(); break;
                default: throw new Exception("未設定的FormatCode");
            }

            //在取得Format Code的類型後, 要回到原來的點
            stm.Position = startIndex;
            node.FromSml(stm);
            return node;
        }

        public static byte[] GetLengthBytesFromLength(int len)
        {
            List<Byte> rs = new List<byte>();
            if (len > 0)
                rs.Add((byte)(len & 0xff));
            len >>= 8;
            if (len > 0)
                rs.Add((byte)(len & 0xff));
            len >>= 8;
            if (len > 0)
                rs.Add((byte)(len & 0xff));
            len >>= 8;

            return rs.ToArray();
        }
        public static int GetLengthFromLengthBytes(byte[] lenBytes)
        {
            var rs = 0;
            foreach (var b in lenBytes)
                rs = (rs << 8) + b;
            return rs;
        }

        public static CxSecsIIFormatCode GetSmlFormatCode(string typeName)
        {
            typeName = typeName.ToUpper();

            switch (typeName)
            {
                case "L": return CxSecsIIFormatCode.List;
                case "A": return CxSecsIIFormatCode.ASCII;
                case "B": return CxSecsIIFormatCode.Binary;
                case "J": return CxSecsIIFormatCode.JIS8;
                case "BOOLEAN": return CxSecsIIFormatCode.Boolean;
                case "I1": return CxSecsIIFormatCode.Int8;
                case "I2": return CxSecsIIFormatCode.Int16;
                case "I4": return CxSecsIIFormatCode.Int32;
                case "I8": return CxSecsIIFormatCode.Int64;
                case "U1": return CxSecsIIFormatCode.UInt8;
                case "U2": return CxSecsIIFormatCode.UInt16;
                case "U4": return CxSecsIIFormatCode.UInt32;
                case "U8": return CxSecsIIFormatCode.UInt64;
                case "F4": return CxSecsIIFormatCode.Float32;
                case "F8": return CxSecsIIFormatCode.Float64;
                default: throw new Exception("未設定的TypeName");
            }
        }
        public static string GetSmlTypeName(CxSecsIIFormatCode formatCode)
        {
            switch (formatCode)
            {
                case CxSecsIIFormatCode.List: return "L";
                case CxSecsIIFormatCode.ASCII: return "A";
                case CxSecsIIFormatCode.Binary: return "B";
                case CxSecsIIFormatCode.JIS8: return "J";
                case CxSecsIIFormatCode.Boolean: return "Boolean";
                case CxSecsIIFormatCode.Int8: return "I1";
                case CxSecsIIFormatCode.Int16: return "I2";
                case CxSecsIIFormatCode.Int32: return "I4";
                case CxSecsIIFormatCode.Int64: return "I8";
                case CxSecsIIFormatCode.UInt8: return "U1";
                case CxSecsIIFormatCode.UInt16: return "U2";
                case CxSecsIIFormatCode.UInt32: return "U4";
                case CxSecsIIFormatCode.UInt64: return "U8";
                case CxSecsIIFormatCode.Float32: return "F4";
                case CxSecsIIFormatCode.Float64: return "F8";
                default: throw new Exception("未設定的FormatCode");
            }
        }

        #endregion


    }
}
