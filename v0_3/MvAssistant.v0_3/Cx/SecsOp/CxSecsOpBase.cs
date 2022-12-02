using CodeExpress.v1_1Core.Secs;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace CodeExpress.v1_1Core.SecsOp
{
    [Serializable]
    [Guid("F516CA5F-39CF-4A8A-B7EF-4FAECB77827C")]
    public class CxSecsOpBase : ICxSecsOp
    {
        
        public CxSecsOpBase() { this.CxSecsList = new CxSecsIINodeList(); }

        /// <summary> 子類別務必實作 帶Secs List的建構子 </summary>
        public CxSecsOpBase(CxSecsIINodeList list) { this.CxSecsList = list; }


        public CxSecsIINodeList CxSecsList { get; set; }
        
        
        
        
        #region Static

        public static T CreateInstance<T>(CxSecsIINodeList secsList) where T : CxSecsOpBase { return (T)Activator.CreateInstance(typeof(T), secsList); }
        public static T CreateInstance<T>(CxSecsOpBase node) where T : CxSecsOpBase { return (T)Activator.CreateInstance(typeof(T), node.CxSecsList); }

        #endregion



        #region Static Operator

        public static implicit operator CxSecsIINodeList(CxSecsOpBase data) { return data.CxSecsList; }
        public static implicit operator CxSecsOpBase(CxSecsIINodeList data) { return new CxSecsOpBase(data); }

        #endregion



    }
}
