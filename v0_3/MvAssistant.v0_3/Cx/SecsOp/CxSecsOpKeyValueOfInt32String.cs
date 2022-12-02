using CodeExpress.v1_1Core.Secs;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace CodeExpress.v1_1Core.SecsOp
{
    [Serializable]
    [Guid("26BA9E62-F9C0-4256-AF56-F1788BAECDEF")]
    public class CxSecsOpKeyValueOfInt32String : CxSecsOpBase
    {

        public CxSecsOpKeyValueOfInt32String()
        {
            this.CxSecsList = new CxSecsIINodeList();
            this.CxSecsList.Add(new CxSecsIINodeInt32());
            this.CxSecsList.Add(new CxSecsIINodeASCII());//預設字串
        }
        public CxSecsOpKeyValueOfInt32String(CxSecsIINodeList list) { this.CxSecsList = list; }
     

        public CxSecsOpKeyValueOfInt32String(Int32 key, string value)
        {
            this.CxSecsList = new CxSecsIINodeList();

            var nodeKey = new CxSecsIINodeInt32();
            nodeKey.DataSetSingle(key);
            
            var nodeValue = new CxSecsIINodeASCII();
            nodeValue.SetString(value);

            this.CxSecsList.Add(nodeKey);
            this.CxSecsList.Add(nodeValue);
        }
     

        public Int32 Key { get { return (this.CxSecsList[0] as CxSecsIINodeInt32).DataFirstOrDefault(); } set { (this.CxSecsList[0] as CxSecsIINodeInt32).DataSetSingle(value); } }
        public String ValueString
        {
            get
            {
                var node = this.CxSecsList[1] as CxSecsIINodeASCII;
                return node == null ? null : node.GetString();
            }
            set { (this.CxSecsList[1] as  CxSecsIINodeASCII).SetString(value); }
        }




        #region Static

        public static implicit operator CxSecsIINodeList(CxSecsOpKeyValueOfInt32String data) { return data.CxSecsList; }
        public static implicit operator CxSecsOpKeyValueOfInt32String(CxSecsIINodeList data) { return new CxSecsOpKeyValueOfInt32String(data); }

        #endregion


    }
}
