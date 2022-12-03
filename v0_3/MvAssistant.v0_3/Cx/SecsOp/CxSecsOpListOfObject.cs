using MvaCodeExpress.v1_1.Secs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MvaCodeExpress.v1_1.SecsOp
{
    [Serializable]
    [Guid("8FADCA92-B523-4ACC-BB6D-C2EC663A2023")]
    public class CxSecsOpListOfObject : CxSecsOpBase
    {
        public CxSecsOpListOfObject() { this.CxSecsList = new CxSecsIINodeList(); }
        public CxSecsOpListOfObject(CxSecsIINodeList list)
        {
            this.CxSecsList = list;
        }


        public int Count()
        {
            return this.CxSecsList.Count;
        }
        public void RemoveAt(int idx) { this.CxSecsList.RemoveAt(idx); }



        #region Operation Ascii / String


        public void AddAscii()
        {
            var node = new CxSecsIINodeASCII();
            this.CxSecsList.Add(node);
        }
        public void AddAscii(Byte data)
        {
            var node = new CxSecsIINodeASCII();
            node.DataSetSingle(data);
            this.CxSecsList.Add(node);
        }
        public void AddString()
        {
            var node = new CxSecsIINodeASCII();
            this.CxSecsList.Add(node);
        }

        public void AddString(string data)
        {
            var node = new CxSecsIINodeASCII();
            node.SetString(data);
            this.CxSecsList.Add(node);
        }

        public Byte GetAscii(int idx)
        {
            var cx = this.CxSecsList.DataGetAs<CxSecsIINodeASCII>(idx);
            return cx.DataFirstOrDefault();
        }

        public string GetString(int idx)
        {
            var cx = this.CxSecsList.DataGetAs<CxSecsIINodeASCII>(idx);
            return cx.GetString();
        }
        public void SetAscii(int idx, Byte data)
        {
            var node = this.CxSecsList[idx].As<CxSecsIINodeASCII>();
            node.DataSetSingle(data);
        }

        public void SetString(int idx, string data)
        {
            var node = this.CxSecsList[idx].As<CxSecsIINodeASCII>();
            node.SetString(data);
        }
        #endregion


        #region Operation - Add


        public void AddBinary()
        {
            var node = new CxSecsIINodeBinary();
            this.CxSecsList.Add(node);
        }
        public void AddBinary(byte data)
        {
            var node = new CxSecsIINodeBinary();
            node.Data.Add(data);
            this.CxSecsList.Add(node);
        }
        public void AddBinary(IEnumerable<byte> data)
        {
            var node = new CxSecsIINodeBinary();
            node.Data.AddRange(data);
            this.CxSecsList.Add(node);
        }

        public void AddBoolean()
        {
            var node = new CxSecsIINodeBoolean();
            this.CxSecsList.Add(node);
        }
        public void AddBoolean(byte data)
        {
            var node = new CxSecsIINodeBoolean();
            node.Data.Add(data);
            this.CxSecsList.Add(node);
        }
        public void AddBoolean(bool data)
        {
            var node = new CxSecsIINodeBoolean();
            node.Data.Add(data ? (byte)1 : (byte)0);
            this.CxSecsList.Add(node);
        }

        public void AddBoolean(IEnumerable<byte> data)
        {
            var node = new CxSecsIINodeBoolean();
            node.Data.AddRange(data);
            this.CxSecsList.Add(node);
        }
        public void AddFloat32()
        {
            var node = new CxSecsIINodeFloat32();
            this.CxSecsList.Add(node);
        }
        public void AddFloat32(float data)
        {
            var node = new CxSecsIINodeFloat32();
            node.Data.Add(data);
            this.CxSecsList.Add(node);
        }
        public void AddFloat32(IEnumerable<float> data)
        {
            var node = new CxSecsIINodeFloat32();
            node.Data.AddRange(data);
            this.CxSecsList.Add(node);
        }
        public void AddFloat64()
        {
            var node = new CxSecsIINodeFloat64();
            this.CxSecsList.Add(node);
        }
        public void AddFloat64(double data)
        {
            var node = new CxSecsIINodeFloat64();
            node.Data.Add(data);
            this.CxSecsList.Add(node);
        }
        public void AddFloat64(IEnumerable<double> data)
        {
            var node = new CxSecsIINodeFloat64();
            node.Data.AddRange(data);
            this.CxSecsList.Add(node);
        }
        public void AddInt08()
        {
            var node = new CxSecsIINodeInt8();
            this.CxSecsList.Add(node);
        }
        public void AddInt08(sbyte data)
        {
            var node = new CxSecsIINodeInt8();
            node.Data.Add(data);
            this.CxSecsList.Add(node);
        }
        public void AddInt08(IEnumerable<sbyte> data)
        {
            var node = new CxSecsIINodeInt8();
            node.Data.AddRange(data);
            this.CxSecsList.Add(node);
        }
        public void AddInt16()
        {
            var node = new CxSecsIINodeInt16();
            this.CxSecsList.Add(node);
        }
        public void AddInt16(Int16 data)
        {
            var node = new CxSecsIINodeInt16();
            node.Data.Add(data);
            this.CxSecsList.Add(node);
        }
        public void AddInt16(IEnumerable<Int16> data)
        {
            var node = new CxSecsIINodeInt16();
            node.Data.AddRange(data);
            this.CxSecsList.Add(node);
        }
        public void AddInt32()
        {
            var node = new CxSecsIINodeInt32();
            this.CxSecsList.Add(node);
        }
        public void AddInt32(Int32 data)
        {
            var node = new CxSecsIINodeInt32();
            node.Data.Add(data);
            this.CxSecsList.Add(node);
        }
        public void AddInt32(IEnumerable<Int32> data)
        {
            var node = new CxSecsIINodeInt32();
            node.Data.AddRange(data);
            this.CxSecsList.Add(node);
        }

        public void AddInt64()
        {
            var node = new CxSecsIINodeInt64();
            this.CxSecsList.Add(node);
        }
        public void AddInt64(Int64 data)
        {
            var node = new CxSecsIINodeInt64();
            node.Data.Add(data);
            this.CxSecsList.Add(node);
        }
        public void AddInt64(IEnumerable<Int64> data)
        {
            var node = new CxSecsIINodeInt64();
            node.Data.AddRange(data);
            this.CxSecsList.Add(node);
        }
        public void AddJis8()
        {
            var node = new CxSecsIINodeJIS8();
            this.CxSecsList.Add(node);
        }
        public void AddJis8(IEnumerable<byte> data)
        {
            var node = new CxSecsIINodeJIS8();
            node.Data.AddRange(data);
            this.CxSecsList.Add(node);
        }
        public CxSecsOpListOfObject AddList()
        {
            var node = new CxSecsIINodeList();
            this.CxSecsList.Add(node);
            return new CxSecsOpListOfObject(node);
        }
        public void AddUInt08()
        {
            var node = new CxSecsIINodeUInt8();
            this.CxSecsList.Add(node);
        }
        public void AddUInt08(byte data)
        {
            var node = new CxSecsIINodeUInt8();
            node.Data.Add(data);
            this.CxSecsList.Add(node);
        }
        public void AddUInt08(IEnumerable<byte> data)
        {
            var node = new CxSecsIINodeUInt8();
            node.Data.AddRange(data);
            this.CxSecsList.Add(node);
        }
        public void AddUInt16()
        {
            var node = new CxSecsIINodeUInt16();
            this.CxSecsList.Add(node);
        }
        public void AddUInt16(UInt16 data)
        {
            var node = new CxSecsIINodeUInt16();
            node.Data.Add(data);
            this.CxSecsList.Add(node);
        }
        public void AddUInt16(IEnumerable<UInt16> data)
        {
            var node = new CxSecsIINodeUInt16();
            node.Data.AddRange(data);
            this.CxSecsList.Add(node);
        }
        public void AddUInt32()
        {
            var node = new CxSecsIINodeUInt32();
            this.CxSecsList.Add(node);
        }
        public void AddUInt32(UInt32 data)
        {
            var node = new CxSecsIINodeUInt32();
            node.Data.Add(data);
            this.CxSecsList.Add(node);
        }
        public void AddUInt32(IEnumerable<UInt32> data)
        {
            var node = new CxSecsIINodeUInt32();
            node.Data.AddRange(data);
            this.CxSecsList.Add(node);
        }
        public void AddUInt64()
        {
            var node = new CxSecsIINodeUInt64();
            this.CxSecsList.Add(node);
        }
        public void AddUInt64(UInt64 data)
        {
            var node = new CxSecsIINodeUInt64();
            node.Data.Add(data);
            this.CxSecsList.Add(node);
        }
        public void AddUInt64(IEnumerable<UInt64> data)
        {
            var node = new CxSecsIINodeUInt64();
            this.CxSecsList.Add(node);
            node.Data.AddRange(data);
        }

        #endregion


        #region Operation - Get

        public byte GetBinary(int idx)
        {
            var cx = this.CxSecsList.DataGetAs<CxSecsIINodeBinary>(idx);
            return cx.Data.FirstOrDefault();
        }

        public List<byte> GetBinaryArray(int idx)
        {
            var cx = this.CxSecsList.DataGetAs<CxSecsIINodeBinary>(idx);
            return cx.Data;
        }

        public byte GetBoolean(int idx)
        {
            var cx = this.CxSecsList.DataGetAs<CxSecsIINodeBoolean>(idx);
            return cx.Data.FirstOrDefault();
        }

        public List<byte> GetBooleanArray(int idx)
        {
            var cx = this.CxSecsList.DataGetAs<CxSecsIINodeBoolean>(idx);
            return cx.Data;
        }

        public Single GetFloat32(int idx)
        {
            var cx = this.CxSecsList.DataGetAs<CxSecsIINodeFloat32>(idx);
            return cx.Data.FirstOrDefault();
        }

        public List<Single> GetFloat32Array(int idx)
        {
            var cx = this.CxSecsList.DataGetAs<CxSecsIINodeFloat32>(idx);
            return cx.Data;
        }

        public Double GetFloat64(int idx)
        {
            var cx = this.CxSecsList.DataGetAs<CxSecsIINodeFloat64>(idx);
            return cx.Data.FirstOrDefault();
        }

        public List<Double> GetFloat64Array(int idx)
        {
            var cx = this.CxSecsList.DataGetAs<CxSecsIINodeFloat64>(idx);
            return cx.Data;
        }

        public sbyte GetInt08(int idx)
        {
            var cx = this.CxSecsList.DataGetAs<CxSecsIINodeInt8>(idx);
            return cx.Data.FirstOrDefault();
        }

        public List<sbyte> GetInt08Array(int idx)
        {
            var cx = this.CxSecsList.DataGetAs<CxSecsIINodeInt8>(idx);
            return cx.Data;
        }

        public Int16 GetInt16(int idx)
        {
            var cx = this.CxSecsList.DataGetAs<CxSecsIINodeInt16>(idx);
            return cx.Data.FirstOrDefault();
        }

        public List<Int16> GetInt16Array(int idx)
        {
            var cx = this.CxSecsList.DataGetAs<CxSecsIINodeInt16>(idx);
            return cx.Data;
        }

        public Int32 GetInt32(int idx)
        {
            var cx = this.CxSecsList.DataGetAs<CxSecsIINodeInt32>(idx);
            return cx.Data.FirstOrDefault();
        }

        public List<Int32> GetInt32Array(int idx)
        {
            var cx = this.CxSecsList.DataGetAs<CxSecsIINodeInt32>(idx);
            return cx.Data;
        }

        public Int64 GetInt64(int idx)
        {
            var cx = this.CxSecsList.DataGetAs<CxSecsIINodeInt64>(idx);
            return cx.Data.FirstOrDefault();
        }

        public List<Int64> GetInt64Array(int idx)
        {
            var cx = this.CxSecsList.DataGetAs<CxSecsIINodeInt64>(idx);
            return cx.Data;
        }

        public byte GetJis8(int idx)
        {
            var cx = this.CxSecsList.DataGetAs<CxSecsIINodeJIS8>(idx);
            return cx.Data.FirstOrDefault();
        }

        public List<byte> GetJis8Array(int idx)
        {
            var cx = this.CxSecsList.DataGetAs<CxSecsIINodeJIS8>(idx);
            return cx.Data;
        }
        public CxSecsOpListOfObject GetList(int idx)
        {
            var cx = this.CxSecsList.DataGetAs<CxSecsIINodeList>(idx);
            return new CxSecsOpListOfObject(cx);
        }

        public Object GetObject(int idx)
        {
            var node = this.CxSecsList[idx];

            if (node as CxSecsIINodeASCII != null) { return (node as CxSecsIINodeASCII).GetString(); }
            else if (node as CxSecsIINodeBinary != null) { return (node as CxSecsIINodeBinary).Data.FirstOrDefault(); }
            else if (node as CxSecsIINodeBoolean != null) { return Convert.ToBoolean((node as CxSecsIINodeBoolean).Data.FirstOrDefault()); }
            else if (node as CxSecsIINodeInt8 != null) { return (node as CxSecsIINodeInt8).Data.FirstOrDefault(); }
            else if (node as CxSecsIINodeInt16 != null) { return (node as CxSecsIINodeInt16).Data.FirstOrDefault(); }
            else if (node as CxSecsIINodeInt32 != null) { return (node as CxSecsIINodeInt32).Data.FirstOrDefault(); }
            else if (node as CxSecsIINodeInt64 != null) { return (node as CxSecsIINodeInt64).Data.FirstOrDefault(); }
            else if (node as CxSecsIINodeUInt8 != null) { return (node as CxSecsIINodeUInt8).Data.FirstOrDefault(); }
            else if (node as CxSecsIINodeUInt16 != null) { return (node as CxSecsIINodeUInt16).Data.FirstOrDefault(); }
            else if (node as CxSecsIINodeUInt32 != null) { return (node as CxSecsIINodeUInt32).Data.FirstOrDefault(); }
            else if (node as CxSecsIINodeUInt64 != null) { return (node as CxSecsIINodeUInt64).Data.FirstOrDefault(); }
            else if (node as CxSecsIINodeFloat32 != null) { return (node as CxSecsIINodeFloat32).Data.FirstOrDefault(); }
            else if (node as CxSecsIINodeFloat64 != null) { return (node as CxSecsIINodeFloat64).Data.FirstOrDefault(); }
            else throw new CxException("Node type is not exist.");
        }
        public byte GetUInt08(int idx)
        {
            var cx = this.CxSecsList.DataGetAs<CxSecsIINodeUInt8>(idx);
            return cx.Data.FirstOrDefault();
        }
        public List<byte> GetUInt08Array(int idx)
        {
            var cx = this.CxSecsList.DataGetAs<CxSecsIINodeUInt8>(idx);
            return cx.Data;
        }
        public UInt16 GetUInt16(int idx)
        {
            var cx = this.CxSecsList.DataGetAs<CxSecsIINodeUInt16>(idx);
            return cx.Data.FirstOrDefault();
        }
        public List<UInt16> GetUInt16Array(int idx)
        {
            var cx = this.CxSecsList.DataGetAs<CxSecsIINodeUInt16>(idx);
            return cx.Data;
        }
        public UInt32 GetUInt32(int idx)
        {
            var cx = this.CxSecsList.DataGetAs<CxSecsIINodeUInt32>(idx);
            return cx.Data.FirstOrDefault();
        }
        public List<UInt32> GetUInt32Array(int idx)
        {
            var cx = this.CxSecsList.DataGetAs<CxSecsIINodeUInt32>(idx);
            return cx.Data;
        }
        public UInt64 GetUInt64(int idx)
        {
            var cx = this.CxSecsList.DataGetAs<CxSecsIINodeUInt64>(idx);
            return cx.Data.FirstOrDefault();
        }
        public List<UInt64> GetUInt64Array(int idx)
        {
            var cx = this.CxSecsList.DataGetAs<CxSecsIINodeUInt64>(idx);
            return cx.Data;
        }

        #endregion


        #region Operation - Set


        public void SetBinary(int idx, byte data)
        {
            var node = this.CxSecsList[idx].As<CxSecsIINodeBinary>();
            node.Data[0] = data;
        }
        public void SetBinary(int idx, IEnumerable<byte> data)
        {
            var node = this.CxSecsList[idx].As<CxSecsIINodeBinary>();
            node.Data.Clear();
            node.Data.AddRange(data);
        }
        public void SetBoolean(int idx, byte data)
        {
            var node = this.CxSecsList[idx].As<CxSecsIINodeBoolean>();
            node.Data[0] = data;
        }
        public void SetBoolean(int idx, IEnumerable<byte> data)
        {
            var node = this.CxSecsList[idx].As<CxSecsIINodeBoolean>();
            node.Data.Clear();
            node.Data.AddRange(data);
        }
        public void SetFloat32(int idx, Single data)
        {
            var node = this.CxSecsList[idx].As<CxSecsIINodeFloat32>();
            node.Data[0] = data;
        }
        public void SetFloat32(int idx, IEnumerable<Single> data)
        {
            var node = this.CxSecsList[idx].As<CxSecsIINodeFloat32>();
            node.Data.Clear();
            node.Data.AddRange(data);
        }
        public void SetFloat64(int idx, Double data)
        {
            var node = this.CxSecsList[idx].As<CxSecsIINodeFloat64>();
            node.Data[0] = data;
        }
        public void SetFloat64(int idx, IEnumerable<Double> data)
        {
            var node = this.CxSecsList[idx].As<CxSecsIINodeFloat64>();
            node.Data.Clear();
            node.Data.AddRange(data);
        }
        public void SetInt08(int idx, sbyte data)
        {
            var node = this.CxSecsList[idx].As<CxSecsIINodeInt8>();
            node.Data[0] = data;
        }
        public void SetInt08(int idx, IEnumerable<sbyte> data)
        {
            var node = this.CxSecsList[idx].As<CxSecsIINodeInt8>();
            node.Data.Clear();
            node.Data.AddRange(data);
        }
        public void SetInt16(int idx, Int16 data)
        {
            var node = this.CxSecsList[idx].As<CxSecsIINodeInt16>();
            node.Data[0] = data;
        }
        public void SetInt16(int idx, IEnumerable<Int16> data)
        {
            var node = this.CxSecsList[idx].As<CxSecsIINodeInt16>();
            node.Data.Clear();
            node.Data.AddRange(data);
        }
        public void SetInt32(int idx, Int32 data)
        {
            var node = this.CxSecsList[idx].As<CxSecsIINodeInt32>();
            node.Data[0] = data;
        }
        public void SetInt32(int idx, IEnumerable<Int32> data)
        {
            var node = this.CxSecsList[idx].As<CxSecsIINodeInt32>();
            node.Data.Clear();
            node.Data.AddRange(data);
        }

        public void SetInt64(int idx, Int64 data)
        {
            var node = this.CxSecsList[idx].As<CxSecsIINodeInt64>();
            node.Data[0] = data;
        }
        public void SetInt64(int idx, IEnumerable<Int64> data)
        {
            var node = this.CxSecsList[idx].As<CxSecsIINodeInt64>();
            node.Data.Clear();
            node.Data.AddRange(data);
        }
        public void SetJis8(int idx, byte data)
        {
            var node = this.CxSecsList[idx].As<CxSecsIINodeJIS8>();
            node.Data[0] = data;
        }
        public void SetJis8(int idx, IEnumerable<byte> data)
        {
            var node = this.CxSecsList[idx].As<CxSecsIINodeJIS8>();
            node.Data.Clear();
            node.Data.AddRange(data);
        }
        public void SetList(int idx, CxSecsOpListOfObject list)
        {
            this.CxSecsList[idx] = list.CxSecsList;
        }
        public void SetUInt08(int idx, byte data)
        {
            var node = this.CxSecsList[idx].As<CxSecsIINodeUInt8>();
            node.Data[0] = data;
        }
        public void SetUInt08(int idx, IEnumerable<byte> data)
        {
            var node = this.CxSecsList[idx].As<CxSecsIINodeUInt8>();
            node.Data.Clear();
            node.Data.AddRange(data);
        }
        public void SetUInt16(int idx, UInt16 data)
        {
            var node = this.CxSecsList[idx].As<CxSecsIINodeUInt16>();
            node.Data[0] = data;
        }
        public void SetUInt16(int idx, IEnumerable<UInt16> data)
        {
            var node = this.CxSecsList[idx].As<CxSecsIINodeUInt16>();
            node.Data.Clear();
            node.Data.AddRange(data);
        }
        public void SetUInt32(int idx, UInt32 data)
        {
            var node = this.CxSecsList[idx].As<CxSecsIINodeUInt32>();
            node.Data[0] = data;
        }
        public void SetUInt32(int idx, IEnumerable<UInt32> data)
        {
            var node = this.CxSecsList[idx].As<CxSecsIINodeUInt32>();
            node.Data.Clear();
            node.Data.AddRange(data);
        }
        public void SetUInt64(int idx, UInt64 data)
        {
            var node = this.CxSecsList[idx].As<CxSecsIINodeUInt64>();
            node.Data[0] = data;
        }
        public void SetUInt64(int idx, IEnumerable<UInt64> data)
        {
            var node = this.CxSecsList[idx].As<CxSecsIINodeUInt64>();
            node.Data.Clear();
            node.Data.AddRange(data);
        }


        #endregion


        #region Static Operator

        public static implicit operator CxSecsIINodeList(CxSecsOpListOfObject data) { return data.CxSecsList; }
        public static implicit operator CxSecsOpListOfObject(CxSecsIINodeList data) { return new CxSecsOpListOfObject(data); }

        #endregion


    }
}
