using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MvaCToolkitCs.v1_2.Config
{
    /// <summary>
    /// 儲存各類Config
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class CtkConfigCollectorFolder<T> : CtkConfigCollectorBase<T> where T : class, new()
    {
        public DateTime LastUpdate { get; private set; }
        public TimeSpan OverTimeInterval = new TimeSpan(0, 0, 10);

        public string Folder = "Config/DefaultConfigs";
        public Func<string, bool> Filter;



        public void UpdateFromFolder(Func<string, bool> filter, string folder = null)
        {
            this.Filter = filter;//Filter可以為空, 傳空值代表全部同意
            this.UpdateFromFolder(folder);
        }

        public void UpdateFromFolder(string folder = null)
        {
            this.Folder = folder == null ? this.Folder : folder;//Folder不得為空, 所以傳空值就保留原本的

            this.LastUpdate = DateTime.Now;

            //記錄現有的
            var orignalConfigs = new Dictionary<String, Boolean>();
            foreach (var name in this.Keys) orignalConfigs[name] = false;

            var di = new DirectoryInfo(this.Folder);
            if (!di.Exists) di.Create();
            foreach (var fi in di.GetFiles())
            {
                if (this.Filter != null)
                    if (!Filter(fi.Name)) continue;//沒通過就不加入

                try
                {
                    var config = CtkUtil.LoadXmlFileTOrNew<T>(fi.FullName);
                    this[fi.Name] = config;
                }
                catch (Exception ex) { CtkLog.WriteAn(this, ex.Message); }

                //更新存在的
                orignalConfigs[fi.Name] = true;
            }


            //移除沒更新到的
            foreach (var kv in orignalConfigs)
            {
                if (kv.Value) continue;
                this.Remove(kv.Key);
            }

        }

        public void UpdateIfTimeout() { this.UpdateIfTimeout(this.OverTimeInterval); }
        public void UpdateIfTimeout(int second) { this.UpdateIfTimeout(new TimeSpan(0, 0, 1)); }
        public void UpdateIfTimeout(TimeSpan timeout)
        {
            var now = DateTime.Now;
            if (now - LastUpdate > timeout) { this.UpdateFromFolder(); }
        }






    }
}
