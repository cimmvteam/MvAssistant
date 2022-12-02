using CodeExpress.v1_1Core.Secs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CodeExpress.v1_1Core.SecsOp
{

    [Serializable]
    [Guid("93C865E8-9792-4542-9503-119F8A967DE2")]
    public class CxSecsOpList<T> : IList<T> where T : CxSecsOpBase

    {

        /*[d20210915] 只能實作 IList, 不能繼承 List.
         * 否則會存取到不同的 List 變數.
         */
        public CxSecsIINodeList CxSecsList;//Wrapper, 不給外界存取, 但同DLL可以

        public CxSecsOpList()
        {
            this.CxSecsList = new CxSecsIINodeList();
        }

        public CxSecsOpList(CxSecsIINodeList list)
        {
            this.CxSecsList = list;
        }



        public IList<T> ToNodeList()

        {

            var q = (from row in this.CxSecsList

                     select row as CxSecsIINodeList);

            return q.Select(x => Activator.CreateInstance(typeof(T), x) as T).ToList();

        }



        #region Operator & Operation

        public int Count { get { return this.CxSecsList.Count; } }
        public bool IsReadOnly { get { return this.CxSecsList.IsReadOnly; } }
        public T this[int idx] { get { return this.Get(idx); } set { this.Set(idx, value); } }
        public void Add(T data) { this.CxSecsList.Add(data.CxSecsList); }



        /// <summary>
        /// Add and return data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public T AddReturn(T data) { this.CxSecsList.Add(data.CxSecsList); return data; }
        public void Clear() { this.CxSecsList.Clear(); }
        public bool Contains(T item) { return this.CxSecsList.Contains(item); }
        public void CopyTo(T[] array, int arrayIndex)

        {

            var list = this.ToNodeList();

            list.CopyTo(array, arrayIndex);

        }
        public T Get(int idx)

        {

            var list = this.CxSecsList[idx];

            var type = typeof(T);

            return Activator.CreateInstance(type, list) as T;

        }
        public IEnumerator<T> GetEnumerator()

        {

            var list = this.ToNodeList();

            return list.GetEnumerator();

        }
        IEnumerator IEnumerable.GetEnumerator() { return this.GetEnumerator(); }
        public int IndexOf(T item) { return this.CxSecsList.IndexOf(item); }
        public void Insert(int index, T item) { this.CxSecsList.Insert(index, item); }
        public bool Remove(T item) { return this.CxSecsList.Remove(item); }
        public void RemoveAt(int idx) { this.CxSecsList.RemoveAt(idx); }
        public void Set(int idx, T data) { this.CxSecsList[idx] = data.CxSecsList; }

        #endregion





        #region Static Operator

        public static implicit operator CxSecsIINodeList(CxSecsOpList<T> data) { return data.CxSecsList; }
        public static implicit operator CxSecsOpList<T>(CxSecsIINodeList data) { return new CxSecsOpList<T>(data); }

        #endregion



    }
}
