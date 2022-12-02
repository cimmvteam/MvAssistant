using CodeExpress.v1_1Core.Secs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace CodeExpress.v1_1Core.SecsOp
{
    [Serializable]
    [Guid("84BD620B-8C9B-4C54-B8F7-8DE8A724D1EF")]
    public class CxSecsOpListOfUInt08 : CxSecsOpBase
    {
        public CxSecsOpListOfUInt08() { this.CxSecsList = new CxSecsIINodeList(); }
        public CxSecsOpListOfUInt08(CxSecsIINodeList list)
        {
            this.CxSecsList = list;
        }

        public int Count()
        {
            return this.CxSecsList.Count;
        }
        public void RemoveAt(int idx) { this.CxSecsList.RemoveAt(idx); }



        #region Operation

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

        #endregion


        #region Static Operator

        public static implicit operator CxSecsIINodeList(CxSecsOpListOfUInt08 data) { return data.CxSecsList; }
        public static implicit operator CxSecsOpListOfUInt08(CxSecsIINodeList data) { return new CxSecsOpListOfUInt08(data); }

        #endregion


    }
}
