using CodeExpress.v1_1Core.Secs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CodeExpress.v1_1Core.SecsOp
{
    [Serializable]
    [Guid("B426E114-A92B-4CA0-9196-ED9F81064D02")]
    public class CxSecsOpListOfInt64 : CxSecsOpBase
    {
        public CxSecsOpListOfInt64() { this.CxSecsList = new CxSecsIINodeList(); }
        public CxSecsOpListOfInt64(CxSecsIINodeList list)
        {
            this.CxSecsList = list;
        }


        public int Count()
        {
            return this.CxSecsList.Count;
        }
        public void RemoveAt(int idx) { this.CxSecsList.RemoveAt(idx); }




        #region Operation

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

        #endregion


        #region Static Operator

        public static implicit operator CxSecsIINodeList(CxSecsOpListOfInt64 data) { return data.CxSecsList; }
        public static implicit operator CxSecsOpListOfInt64(CxSecsIINodeList data) { return new CxSecsOpListOfInt64(data); }

        #endregion


    }
}
