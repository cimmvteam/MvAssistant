using MvaCodeExpress.v1_1.Secs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MvaCodeExpress.v1_1.SecsOp
{
    [Serializable]
    [Guid("12EC06BC-C67A-4B04-9CDB-0F27DBFF39BD")]
    public class CxSecsOpListOfUInt32 : CxSecsOpBase
    {
        public CxSecsOpListOfUInt32() { this.CxSecsList = new CxSecsIINodeList(); }
        public CxSecsOpListOfUInt32(CxSecsIINodeList list)
        {
            this.CxSecsList = list;
        }


        public int Count()
        {
            return this.CxSecsList.Count;
        }
        public void RemoveAt(int idx) { this.CxSecsList.RemoveAt(idx); }




        #region Operation

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


        #endregion


        #region Static Operator

        public static implicit operator CxSecsIINodeList(CxSecsOpListOfUInt32 data) { return data.CxSecsList; }
        public static implicit operator CxSecsOpListOfUInt32(CxSecsIINodeList data) { return new CxSecsOpListOfUInt32(data); }

        #endregion


    }
}
