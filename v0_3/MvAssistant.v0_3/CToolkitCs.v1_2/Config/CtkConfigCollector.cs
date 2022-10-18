using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace CToolkitCs.v1_2.Config
{
    /// <summary>
    /// 儲存各類Config
    /// 使用時注意, 子項會在更新時, 不斷被清除
    /// 請用Collector集合類別做參考
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class CtkConfigCollector<T> : Dictionary<String, CtkConfigCollectorBase<T>> where T : class, new()
    {
        const string KeyName_ProgConfigs = "DefaultProgConfigs";
        const string KeyName_FolderConfigs = "DefaultFolderConfigs";
        //常用1 - 程式賦值Configs
        public CtkConfigCollectorBase<T> DefaultProgConfigs { get { return this.ContainsKey(KeyName_ProgConfigs) ? this[KeyName_ProgConfigs] : this[KeyName_ProgConfigs] = new CtkConfigCollectorBase<T>(); } }
        //常用2 - 讀取Folder Configs
        public CtkConfigCollectorFolder<T> DefaultFolderConfigs { get { return (CtkConfigCollectorFolder<T>)(this.ContainsKey(KeyName_FolderConfigs) ? this[KeyName_FolderConfigs] : this[KeyName_FolderConfigs] = new CtkConfigCollectorFolder<T>()); } }


        public CtkConfigCollector()
        {
            var dpc = this.DefaultProgConfigs;//自動生成ProgConfigs
            var dfc = this.DefaultFolderConfigs;//自動生成FolderConfigs
        }
        protected CtkConfigCollector(SerializationInfo info, StreamingContext context) { }


        public void ClearAll()
        {
            foreach (var dict in this) dict.Value.Clear();
            this.Clear();
        }





        public List<KeyValuePair<string, T>> GetAllConfigs()
        {
            var list = new List<KeyValuePair<string, T>>();
            foreach (var collector in this)
                list.AddRange(collector.Value);

            return list;
        }





        //--- About Folder Configs ----------------------------------------------------------------------------------------


        public void UpdateFromFolder(Func<string, bool> filter, string folder = null)
        {
            if (folder != null) this.DefaultFolderConfigs.Folder = folder;

            foreach (var dict in this)
            {
                if (dict.Value is CtkConfigCollectorFolder<T>)
                    (dict.Value as CtkConfigCollectorFolder<T>).UpdateFromFolder(filter, folder);
            }
        }

        public void UpdateFromFolder(string folder = null)
        {
            if (folder != null) this.DefaultFolderConfigs.Folder = folder;

            foreach (var dict in this)
            {
                if (dict.Value is CtkConfigCollectorFolder<T>)
                    (dict.Value as CtkConfigCollectorFolder<T>).UpdateFromFolder(folder);
            }
        }

        public void UpdateIfTimeout()
        {
            //各類Config收集器都抓出來更新
            foreach (var dict in this)
            {
                if (dict.Value is CtkConfigCollectorFolder<T>)
                    (dict.Value as CtkConfigCollectorFolder<T>).UpdateIfTimeout();
            }
        }
        public void UpdateIfTimeout(int second)
        {
            foreach (var dict in this)
            {
                if (dict.Value is CtkConfigCollectorFolder<T>)
                    (dict.Value as CtkConfigCollectorFolder<T>).UpdateIfTimeout(second);
            }
        }
        public void UpdateIfTimeout(TimeSpan timeout)
        {
            foreach (var dict in this)
            {
                if (dict.Value is CtkConfigCollectorFolder<T>)
                    (dict.Value as CtkConfigCollectorFolder<T>).UpdateIfTimeout(timeout);
            }
        }




    }
}
