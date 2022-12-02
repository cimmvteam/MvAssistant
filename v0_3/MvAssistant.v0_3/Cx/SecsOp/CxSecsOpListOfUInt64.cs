using CodeExpress.v1_1Core.Secs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CodeExpress.v1_1Core.SecsOp
{
    [Serializable]
    [Guid("97B088C8-55CF-4514-AC19-2136E899B195")]
    public class CxSecsOpListOfUInt64 : CxSecsOpBase
    {
        public CxSecsOpListOfUInt64() { this.CxSecsList = new CxSecsIINodeList(); }
        public CxSecsOpListOfUInt64(CxSecsIINodeList list)
        {
            this.CxSecsList = list;
        }


        public int Count()
        {
            return this.CxSecsList.Count;
        }
        public void RemoveAt(int idx) { this.CxSecsList.RemoveAt(idx); }




        #region Operation

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

        public static implicit operator CxSecsIINodeList(CxSecsOpListOfUInt64 data) { return data.CxSecsList; }
        public static implicit operator CxSecsOpListOfUInt64(CxSecsIINodeList data) { return new CxSecsOpListOfUInt64(data); }

        #endregion


    }
}
