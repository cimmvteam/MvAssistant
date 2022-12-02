using CodeExpress.v1_1Core.Secs;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace CodeExpress.v1_1Core.SecsOp
{
    [Serializable]
    [Guid("0271A313-AC34-4D1B-85E7-E55DFF51D89A")]
    public class CxSecsOpKeyValueOfStringString : CxSecsOpBase
    {

        public CxSecsOpKeyValueOfStringString()
        {
            this.CxSecsList = new CxSecsIINodeList();
            this.CxSecsList.Add(new CxSecsIINodeASCII());
            this.CxSecsList.Add(new CxSecsIINodeASCII());//預設字串
        }
        public CxSecsOpKeyValueOfStringString(CxSecsIINodeList list) { this.CxSecsList = list; }
     

        public CxSecsOpKeyValueOfStringString(string key, string value)
        {
            this.CxSecsList = new CxSecsIINodeList();

            var nodeKey = new CxSecsIINodeASCII();
            nodeKey.SetString(key);
            
            var nodeValue = new CxSecsIINodeASCII();
            nodeValue.SetString(value);

            this.CxSecsList.Add(nodeKey);
            this.CxSecsList.Add(nodeValue);
        }
     

        public String Key { get { return (this.CxSecsList[0] as CxSecsIINodeASCII).GetString(); } set { (this.CxSecsList[0] as CxSecsIINodeASCII).SetString(value); } }
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

        public static implicit operator CxSecsIINodeList(CxSecsOpKeyValueOfStringString data) { return data.CxSecsList; }
        public static implicit operator CxSecsOpKeyValueOfStringString(CxSecsIINodeList data) { return new CxSecsOpKeyValueOfStringString(data); }

        #endregion


    }
}
