using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MvaCodeExpress.v1_1.Secs
{


    public class CxSecsIINodeList : CtkSecsIINodeT<CxSecsIINode>, IList<CxSecsIINode>
    {
        public CxSecsIINodeList()
        {
            this.m_formatCode = CxSecsIIFormatCode.List;
            this.Data = new List<CxSecsIINode>();
        }

        public int Count { get { return this.Data.Count; } }

        public bool IsReadOnly { get { return false; } }




        #region Data Transfer

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


            for (int idx = 0; idx < length; idx++)
            {
                this.Data.Add(GetFromBytes(stm));
            }

        }
        public override void FromSml_DataAdd(System.IO.MemoryStream stm, string input)
        {

            var match = CxSecsIINode.FormatMatch(input);
            var formatCode = GetSmlFormatCode(match.Groups["typeName"].Value);
            if (formatCode != this.FormatCode)
                throw new CxSecsException("讀出的Type與此類別不合");


            int c = 0;
            while ((c = stm.ReadByte()) >= 0)
            {
                if (c == '<')
                {
                    stm.Seek(-1, System.IO.SeekOrigin.Current);
                    this.Data.Add(CxSecsIINode.GetFromSml(stm));
                }
                else if (c == '>')
                    break;
            }
        }
        public override byte[] ToBytes()
        {
            var length = this.Data.Count;
            if (length > 0xffffff)
                throw new Exception("資料長度過大");
            List<Byte> rs = new List<byte>(GetLengthBytesFromLength(length));
            var formatByte = ((int)this.m_formatCode << 2) + rs.Count;
            rs.Insert(0, (byte)formatByte);

            foreach (var d in this.Data)
            {
                rs.AddRange(d.ToBytes());
            }


            return rs.ToArray();
        }
        public override string ToSml(StringBuilder sb, string prefix, string prefixPattern)
        {
            var typeName = GetSmlTypeName(this.FormatCode);

            sb.AppendFormat(prefix + "<{0}[{1}]", typeName, this.Data.Count);
            sb.AppendLine();
            foreach (var d in this.Data)
            {
                d.ToSml(sb, prefix + prefixPattern, prefixPattern);
                sb.AppendLine();
            }
            sb.AppendFormat(prefix + ">");

            return sb.ToString();
        }

        #endregion


        #region IList

        public void Add(CxSecsIINode item) { this.Data.Add(item); }
        public void Clear() { this.Data.Clear(); }
        public bool Contains(CxSecsIINode item) { return this.Data.Contains(item); }
        public void CopyTo(CxSecsIINode[] array, int arrayIndex) { this.Data.CopyTo(array, arrayIndex); }
        public IEnumerator<CxSecsIINode> GetEnumerator() { return this.Data.GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return this.Data.GetEnumerator(); }
        public int IndexOf(CxSecsIINode item) { return this.Data.IndexOf(item); }
        public void Insert(int index, CxSecsIINode item) { this.Data.Insert(index, item); }
        public bool Remove(CxSecsIINode item) { return this.Data.Remove(item); }
        public void RemoveAt(int index) { this.Data.RemoveAt(index); }

        #endregion



        #region Data Operation - Get


        public CxSecsIINodeASCII GetAscii(int idx) { return this.DataGetAs<CxSecsIINodeASCII>(idx); }



        #endregion



        #region Data Operation - Add

        public CxSecsIINodeASCII AddAscii(string data)
        {
            var node = new CxSecsIINodeASCII();
            node.SetString(data);
            this.Add(node);
            return node;
        }
        public CxSecsIINodeBinary AddBinary(Byte data)
        {
            var node = new CxSecsIINodeBinary();
            node.Data.Add(data);
            this.Add(node);
            return node;
        }
        public CxSecsIINodeBoolean AddBoolean(Byte data)
        {
            var node = new CxSecsIINodeBoolean();
            node.Data.Add(data);
            this.Add(node);
            return node;
        }
        public CxSecsIINodeFloat32 AddFloat32(Single data)
        {
            var node = new CxSecsIINodeFloat32();
            node.Data.Add(data);
            this.Add(node);
            return node;
        }
        public CxSecsIINodeFloat64 AddFloat64(Double data)
        {
            var node = new CxSecsIINodeFloat64();
            node.Data.Add(data);
            this.Add(node);
            return node;
        }
        public CxSecsIINodeInt16 AddInt16(Int16 data)
        {
            var node = new CxSecsIINodeInt16();
            node.Data.Add(data);
            this.Add(node);
            return node;
        }
        public CxSecsIINodeInt32 AddInt32(Int32 data)
        {
            var node = new CxSecsIINodeInt32();
            node.Data.Add(data);
            this.Add(node);
            return node;
        }
        public CxSecsIINodeInt64 AddInt64(Int64 data)
        {
            var node = new CxSecsIINodeInt64();
            node.Data.Add(data);
            this.Add(node);
            return node;
        }
        public CxSecsIINodeInt8 AddInt8(SByte data)
        {
            var node = new CxSecsIINodeInt8();
            node.Data.Add(data);
            this.Add(node);
            return node;
        }
        public CxSecsIINodeJIS8 AddJis8(Byte data)
        {
            var node = new CxSecsIINodeJIS8();
            node.Data.Add(data);
            this.Add(node);
            return node;
        }
        public CxSecsIINodeList AddList()
        {
            var node = new CxSecsIINodeList();
            this.Add(node);
            return node;
        }
        public T AddNode<T>(T node) where T : CxSecsIINode
        {
            this.Add(node);
            return node;
        }
        public CxSecsIINodeBinary AddRangeBinary(IEnumerable<Byte> data)
        {
            var node = new CxSecsIINodeBinary();
            node.Data.AddRange(data);
            this.Add(node);
            return node;
        }
        public CxSecsIINodeBoolean AddRangeBoolean(IEnumerable<Byte> data)
        {
            var node = new CxSecsIINodeBoolean();
            node.Data.AddRange(data);
            this.Add(node);
            return node;
        }
        public CxSecsIINodeFloat32 AddRangeFloat32(IEnumerable<Single> data)
        {
            var node = new CxSecsIINodeFloat32();
            node.Data.AddRange(data);
            this.Add(node);
            return node;
        }
        public CxSecsIINodeFloat64 AddRangeFloat64(IEnumerable<Double> data)
        {
            var node = new CxSecsIINodeFloat64();
            node.Data.AddRange(data);
            this.Add(node);
            return node;
        }
        public CxSecsIINodeInt16 AddRangeInt16(IEnumerable<Int16> data)
        {
            var node = new CxSecsIINodeInt16();
            node.Data.AddRange(data);
            this.Add(node);
            return node;
        }
        public CxSecsIINodeInt32 AddRangeInt32(IEnumerable<Int32> data)
        {
            var node = new CxSecsIINodeInt32();
            node.Data.AddRange(data);
            this.Add(node);
            return node;
        }
        public CxSecsIINodeInt64 AddRangeInt64(IEnumerable<Int64> data)
        {
            var node = new CxSecsIINodeInt64();
            node.Data.AddRange(data);
            this.Add(node);
            return node;
        }
        public CxSecsIINodeInt8 AddRangeInt8(IEnumerable<SByte> data)
        {
            var node = new CxSecsIINodeInt8();
            node.Data.AddRange(data);
            this.Add(node);
            return node;
        }
        public CxSecsIINodeJIS8 AddRangeJis8(IEnumerable<Byte> data)
        {
            var node = new CxSecsIINodeJIS8();
            node.Data.AddRange(data);
            this.Add(node);
            return node;
        }
        public CxSecsIINodeUInt16 AddRangeUInt16(IEnumerable<UInt16> data)
        {
            var node = new CxSecsIINodeUInt16();
            node.Data.AddRange(data);
            this.Add(node);
            return node;
        }
        public CxSecsIINodeUInt32 AddRangeUInt32(IEnumerable<UInt32> data)
        {
            var node = new CxSecsIINodeUInt32();
            node.Data.AddRange(data);
            this.Add(node);
            return node;
        }
        public CxSecsIINodeUInt64 AddRangeUInt64(IEnumerable<UInt64> data)
        {
            var node = new CxSecsIINodeUInt64();
            node.Data.AddRange(data);
            this.Add(node);
            return node;
        }
        public CxSecsIINodeUInt8 AddRangeUInt8(IEnumerable<Byte> data)
        {
            var node = new CxSecsIINodeUInt8();
            node.Data.AddRange(data);
            this.Add(node);
            return node;
        }
        public CxSecsIINodeUInt16 AddUInt16(UInt16 data)
        {
            var node = new CxSecsIINodeUInt16();
            node.Data.Add(data);
            this.Add(node);
            return node;
        }
        public CxSecsIINodeUInt32 AddUInt32(UInt32 data)
        {
            var node = new CxSecsIINodeUInt32();
            node.Data.Add(data);
            this.Add(node);
            return node;
        }
        public CxSecsIINodeUInt64 AddUInt64(UInt64 data)
        {
            var node = new CxSecsIINodeUInt64();
            node.Data.Add(data);
            this.Add(node);
            return node;
        }
        public CxSecsIINodeUInt8 AddUInt8(Byte data)
        {
            var node = new CxSecsIINodeUInt8();
            node.Data.Add(data);
            this.Add(node);
            return node;
        }

        #endregion




        #region Operator

        public static implicit operator CxSecsIINodeList(CxSecsIINode[] d)
        {
            var rs = new CxSecsIINodeList();
            rs.Data.AddRange(d); ;
            return rs;
        }

        #endregion

    }
}
