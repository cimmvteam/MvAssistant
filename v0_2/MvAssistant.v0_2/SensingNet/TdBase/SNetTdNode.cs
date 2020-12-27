using CToolkit.v1_1;
using CToolkit.v1_1.Timing;
using CToolkit.v1_1.TriggerDiagram;
using Newtonsoft.Json;
using SensingNet.v0_2.TimeSignal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SensingNet.v0_2.TdBase
{
    [Serializable]
    public class SNetTdNode : ICtkTdNode, IDisposable
    {
        public bool IsEnalbed = true;
        protected String _identifier = Guid.NewGuid().ToString();
        ~SNetTdNode() { this.Dispose(false); }

        public string CtkTdIdentifier { get { return this._identifier; } set { this._identifier = value; } }
        public string CtkTdName { get; set; }




        public long GetMemorySizeBin()
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                //byte[] Array;
                bf.Serialize(ms, this);
                return ms.Length;
                //Array = ms.ToArray();
                //return Array.Length;
            }
        }
        public long GetMemorySizeJson()
        {
            var seri = new JsonSerializer();
            using (var ms = new MemoryStream())
            using (var sw = new StreamWriter(ms))
            {
                seri.Serialize(sw, this);
                sw.Flush();
                return ms.Length;
            }

            //return JsonConvert.SerializeObject(this).Length;
        }



        #region IDisposable
        // Flag: Has Dispose already been called?
        protected bool disposed = false;
        // Public implementation of Dispose pattern callable by consumers.
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            this.DisposeSelf();
            disposed = true;
        }
        protected virtual void DisposeSelf()
        {
            CtkEventUtil.RemoveEventHandlersOfOwnerByFilter(this, (dlgt) => true);//移除自己的Event Delegate
        }
        #endregion

    }
}
