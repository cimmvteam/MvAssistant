using MvaCodeExpress.v1_1.Secs;
using System;
using System.Runtime.InteropServices;

namespace MvaCodeExpress.v1_1.SecsOp
{
    [Serializable]
    [Guid("AC51F6DB-1E24-464B-ABFD-71A65E29F303")]
    public class CxSecsOpListOfString : CxSecsOpBase
    {
        public CxSecsOpListOfString() { this.CxSecsList = new CxSecsIINodeList(); }
        public CxSecsOpListOfString(CxSecsIINodeList list)
        {
            this.CxSecsList = list;
        }


        public int Count()
        {
            return this.CxSecsList.Count;
        }
        public void RemoveAt(int idx) { this.CxSecsList.RemoveAt(idx); }



        #region Operation Ascii / String


        public void AddAscii()
        {
            var node = new CxSecsIINodeASCII();
            this.CxSecsList.Add(node);
        }
        public void AddAscii(Byte data)
        {
            var node = new CxSecsIINodeASCII();
            node.DataSetSingle(data);
            this.CxSecsList.Add(node);
        }
        public void AddString()
        {
            var node = new CxSecsIINodeASCII();
            this.CxSecsList.Add(node);
        }

        public void AddString(string data)
        {
            var node = new CxSecsIINodeASCII();
            node.SetString(data);
            this.CxSecsList.Add(node);
        }

        public Byte GetAscii(int idx)
        {
            var cx = this.CxSecsList.DataGetAs<CxSecsIINodeASCII>(idx);
            return cx.DataFirstOrDefault();
        }

        public string GetString(int idx)
        {
            var cx = this.CxSecsList.DataGetAs<CxSecsIINodeASCII>(idx);
            return cx.GetString();
        }
        public void SetAscii(int idx, Byte data)
        {
            var node = this.CxSecsList[idx].As<CxSecsIINodeASCII>();
            node.DataSetSingle(data);
        }

        public void SetString(int idx, string data)
        {
            var node = this.CxSecsList[idx].As<CxSecsIINodeASCII>();
            node.SetString(data);
        }

        #endregion


      






        #region Static Operator

        public static implicit operator CxSecsIINodeList(CxSecsOpListOfString data) { return data.CxSecsList; }
        public static implicit operator CxSecsOpListOfString(CxSecsIINodeList data) { return new CxSecsOpListOfString(data); }

        #endregion


    }
}
