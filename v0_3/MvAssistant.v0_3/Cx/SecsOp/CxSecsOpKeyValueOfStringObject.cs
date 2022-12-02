using CodeExpress.v1_1Core.Secs;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace CodeExpress.v1_1Core.SecsOp
{
    [Serializable]
    [Guid("4CF704BE-E35E-46C4-8A66-DAAF5DE93492")]
    public class CxSecsOpKeyValueOfStringObject : CxSecsOpBase
    {

        public CxSecsOpKeyValueOfStringObject()
        {
            this.CxSecsList = new CxSecsIINodeList();
            this.CxSecsList.Add(new CxSecsIINodeASCII());
            this.CxSecsList.Add(new CxSecsIINodeASCII());//預設字串
        }
        public CxSecsOpKeyValueOfStringObject(CxSecsIINodeList list) { this.CxSecsList = list; }
        public CxSecsOpKeyValueOfStringObject(string key, CxSecsOpBase value)
        {
            this.CxSecsList = new CxSecsIINodeList();

            var nodeKey = new CxSecsIINodeASCII();
            nodeKey.SetString(key);
            
            var nodeValue = value.CxSecsList;

            this.CxSecsList.Add(nodeKey);
            this.CxSecsList.Add(nodeValue);
        }
        public CxSecsOpKeyValueOfStringObject(string key, string value)
        {
            this.CxSecsList = new CxSecsIINodeList();

            var nodeKey = new CxSecsIINodeASCII();
            nodeKey.SetString(key);
            
            var nodeValue = new CxSecsIINodeASCII();
            nodeValue.SetString(value);

            this.CxSecsList.Add(nodeKey);
            this.CxSecsList.Add(nodeValue);
        }
        public CxSecsOpKeyValueOfStringObject(string key, Int32 value)
        {

            this.CxSecsList = new CxSecsIINodeList();

            var nodeKey = new CxSecsIINodeASCII();
            nodeKey.SetString(key);
            
            var nodeValue = new CxSecsIINodeInt32();
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
        public Int32? ValueInt32
        {
            get

            {

                var node = this.CxSecsList[1] as CxSecsIINodeInt32;

                return node == null ? new Nullable<Int32>() : node.DataFirstOrDefault();

            }
            set

            {

                var node = new CxSecsIINodeInt32(); node.DataSetSingle(value.GetValueOrDefault());

                this.CxSecsList[1] = node;

            }
        }
        public String ValueString
        {
            get

            {

                var node = this.CxSecsList[1] as CxSecsIINodeASCII;

                return node == null ? null : node.GetString();

            }
            set { this.CxSecsList[1] = new CxSecsIINodeASCII(value); }
        }
        public UInt32? ValueUInt32

        {

            get

            {

                var node = this.CxSecsList[1] as CxSecsIINodeUInt32;

                return node == null ? new Nullable<UInt32>() : node.DataFirstOrDefault();

            }

            set

            {

                var node = new CxSecsIINodeUInt32(); node.DataSetSingle(value.GetValueOrDefault());

                this.CxSecsList[1] = node;

            }

        }
        public CxSecsOpBase ValueGet()
        {
            var list = this.CxSecsList[1] as CxSecsIINodeList;
            return new CxSecsOpBase(list);
        }
        public T ValueGet<T>() where T : CxSecsOpBase
        {
            var list = this.CxSecsList[1] as CxSecsIINodeList;
            return CxSecsOpBase.CreateInstance<T>(list);
        }
        public void ValueSet<T>(T node) where T : CxSecsOpBase { this.CxSecsList[1] = node; }






        #region Static

        public static implicit operator CxSecsIINodeList(CxSecsOpKeyValueOfStringObject data) { return data.CxSecsList; }
        public static implicit operator CxSecsOpKeyValueOfStringObject(CxSecsIINodeList data) { return new CxSecsOpKeyValueOfStringObject(data); }

        #endregion


    }
}
