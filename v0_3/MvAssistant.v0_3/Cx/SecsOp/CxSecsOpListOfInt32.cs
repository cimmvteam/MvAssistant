using CodeExpress.v1_1Core.Secs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CodeExpress.v1_1Core.SecsOp
{
    [Serializable]
    [Guid("228C97FA-B818-4AC8-97E6-D975780B24A5")]
    public class CxSecsOpListOfInt32 : CxSecsOpBase
    {
        public CxSecsOpListOfInt32() { this.CxSecsList = new CxSecsIINodeList(); }
        public CxSecsOpListOfInt32(CxSecsIINodeList list)
        {
            this.CxSecsList = list;
        }


        public int Count()
        {
            return this.CxSecsList.Count;
        }
        public void RemoveAt(int idx) { this.CxSecsList.RemoveAt(idx); }




        #region Operation

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

        #endregion


        #region Static Operator

        public static implicit operator CxSecsIINodeList(CxSecsOpListOfInt32 data) { return data.CxSecsList; }
        public static implicit operator CxSecsOpListOfInt32(CxSecsIINodeList data) { return new CxSecsOpListOfInt32(data); }

        #endregion


    }
}
