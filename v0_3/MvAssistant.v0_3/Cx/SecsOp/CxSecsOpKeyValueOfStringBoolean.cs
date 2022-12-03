using MvaCodeExpress.v1_1.Secs;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MvaCodeExpress.v1_1.SecsOp
{
    [Serializable]
    [Guid("5ADDC29D-4410-44A1-B14E-7169D274BC2A")]
    public class CxSecsOpKeyValueOfStringBoolean : CxSecsOpBase
    {

        public CxSecsOpKeyValueOfStringBoolean()
        {
            this.CxSecsList = new CxSecsIINodeList();
            this.CxSecsList.Add(new CxSecsIINodeASCII());
            this.CxSecsList.Add(new CxSecsIINodeBoolean());//預設字串
        }
        public CxSecsOpKeyValueOfStringBoolean(CxSecsIINodeList list) { this.CxSecsList = list; }
        public CxSecsOpKeyValueOfStringBoolean(string key, Boolean value)
        {
            this.CxSecsList = new CxSecsIINodeList();

            var nodeKey = new CxSecsIINodeASCII();
            nodeKey.SetString(key);

            var nodeValue = new CxSecsIINodeBoolean();
            nodeValue.DataSetSingle(value);

            this.CxSecsList.Add(nodeKey);
            this.CxSecsList.Add(nodeValue);
        }




        public String Key { get { return (this.CxSecsList[0] as CxSecsIINodeASCII).GetString(); } set { (this.CxSecsList[0] as CxSecsIINodeASCII).SetString(value); } }



        public Boolean? ValueBoolean
        {
            get
            {
                var node = this.CxSecsList[1] as CxSecsIINodeBoolean;
                return node == null ? new Nullable<Boolean>() : node.DataFirstOrDefault() != 0;
            }
            set
            {
                var node = new CxSecsIINodeBoolean(); node.DataSetSingle(value.Value ? (byte)1 : (byte)0);
                this.CxSecsList[1] = node;
            }
        }
   







        #region Static

        public static implicit operator CxSecsIINodeList(CxSecsOpKeyValueOfStringBoolean data) { return data.CxSecsList; }
        public static implicit operator CxSecsOpKeyValueOfStringBoolean(CxSecsIINodeList data) { return new CxSecsOpKeyValueOfStringBoolean(data); }

        #endregion


    }
}
