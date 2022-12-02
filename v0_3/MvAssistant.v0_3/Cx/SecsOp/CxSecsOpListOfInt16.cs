using CodeExpress.v1_1Core.Secs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CodeExpress.v1_1Core.SecsOp
{
    [Serializable]
    [Guid("1F5B1094-6801-47D7-9FD4-DB59C2503F86")]
    public class CxSecsOpListOfInt16 : CxSecsOpBase
    {
        public CxSecsOpListOfInt16() { this.CxSecsList = new CxSecsIINodeList(); }
        public CxSecsOpListOfInt16(CxSecsIINodeList list)
        {
            this.CxSecsList = list;
        }


        public int Count()
        {
            return this.CxSecsList.Count;
        }
        public void RemoveAt(int idx) { this.CxSecsList.RemoveAt(idx); }





        #region Operation

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
        
        #endregion


        #region Static Operator

        public static implicit operator CxSecsIINodeList(CxSecsOpListOfInt16 data) { return data.CxSecsList; }
        public static implicit operator CxSecsOpListOfInt16(CxSecsIINodeList data) { return new CxSecsOpListOfInt16(data); }

        #endregion


    }
}
