using CodeExpress.v1_1Core.Secs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CodeExpress.v1_1Core.SecsOp
{
    [Serializable]
    [Guid("800F0F03-E92B-43D2-B86A-A17557005B60")]
    public class CxSecsOpListOfUInt16 : CxSecsOpBase
    {
        public CxSecsOpListOfUInt16() { this.CxSecsList = new CxSecsIINodeList(); }
        public CxSecsOpListOfUInt16(CxSecsIINodeList list)
        {
            this.CxSecsList = list;
        }


        public int Count()
        {
            return this.CxSecsList.Count;
        }
        public void RemoveAt(int idx) { this.CxSecsList.RemoveAt(idx); }



        #region Operation

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

        #endregion


        #region Static Operator

        public static implicit operator CxSecsIINodeList(CxSecsOpListOfUInt16 data) { return data.CxSecsList; }
        public static implicit operator CxSecsOpListOfUInt16(CxSecsIINodeList data) { return new CxSecsOpListOfUInt16(data); }

        #endregion


    }
}
