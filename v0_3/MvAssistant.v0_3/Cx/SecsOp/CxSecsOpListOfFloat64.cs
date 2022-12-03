using MvaCodeExpress.v1_1.Secs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MvaCodeExpress.v1_1.SecsOp
{
    [Serializable]
    [Guid("ED4EC26A-8D23-4C4E-AA77-22B43F1D1182")]
    public class CxSecsOpListOfFloat64 : CxSecsOpBase
    {
        public CxSecsOpListOfFloat64() { this.CxSecsList = new CxSecsIINodeList(); }
        public CxSecsOpListOfFloat64(CxSecsIINodeList list)
        {
            this.CxSecsList = list;
        }


     
        public int Count()
        {
            return this.CxSecsList.Count;
        }
  
        public void RemoveAt(int idx) { this.CxSecsList.RemoveAt(idx); }


        #region Operation 

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

        #endregion


        #region Static Operator

        public static implicit operator CxSecsIINodeList(CxSecsOpListOfFloat64 data) { return data.CxSecsList; }
        public static implicit operator CxSecsOpListOfFloat64(CxSecsIINodeList data) { return new CxSecsOpListOfFloat64(data); }

        #endregion


    }
}
