using CodeExpress.v1_1Core.Secs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CodeExpress.v1_1Core.SecsOp
{
    [Serializable]
    [Guid("1B098E40-63DA-4030-8AE1-07304B57824C")]
    public class CxSecsOpListOfFloat32 : CxSecsOpBase
    {
        public CxSecsOpListOfFloat32() { this.CxSecsList = new CxSecsIINodeList(); }
        public CxSecsOpListOfFloat32(CxSecsIINodeList list)
        {
            this.CxSecsList = list;
        }


        public int Count()
        {
            return this.CxSecsList.Count;
        }
        public void RemoveAt(int idx) { this.CxSecsList.RemoveAt(idx); }




        #region Operation

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
     
        #endregion


        #region Static Operator

        public static implicit operator CxSecsIINodeList(CxSecsOpListOfFloat32 data) { return data.CxSecsList; }
        public static implicit operator CxSecsOpListOfFloat32(CxSecsIINodeList data) { return new CxSecsOpListOfFloat32(data); }

        #endregion


    }
}
