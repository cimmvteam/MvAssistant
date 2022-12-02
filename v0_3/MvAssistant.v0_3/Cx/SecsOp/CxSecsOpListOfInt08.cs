using CodeExpress.v1_1Core.Secs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CodeExpress.v1_1Core.SecsOp
{
    [Serializable]
    [Guid("D0C5944D-B1EA-45D9-A9B6-29ED3224A0A9")]
    public class CxSecsOpListOfInt08 : CxSecsOpBase
    {
        public CxSecsOpListOfInt08() { this.CxSecsList = new CxSecsIINodeList(); }
        public CxSecsOpListOfInt08(CxSecsIINodeList list)
        {
            this.CxSecsList = list;
        }


        public int Count()
        {
            return this.CxSecsList.Count;
        }
        public void RemoveAt(int idx) { this.CxSecsList.RemoveAt(idx); }



        #region Operation

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

        #endregion


        #region Static Operator

        public static implicit operator CxSecsIINodeList(CxSecsOpListOfInt08 data) { return data.CxSecsList; }
        public static implicit operator CxSecsOpListOfInt08(CxSecsIINodeList data) { return new CxSecsOpListOfInt08(data); }

        #endregion


    }
}
