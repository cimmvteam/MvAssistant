using CToolkit.v1_1;
using CToolkit.v1_1.Timing;
using CToolkit.v1_1.TriggerDiagram;
using Newtonsoft.Json;
using SensingNet.v0_2.TimeSignal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;

namespace SensingNet.v0_2.TdBase
{
    [Serializable]
    [Guid("CA483706-9EAE-4E60-8CE7-2C4C3CAFAD32")]
    public class SNetTdNode : ICtkTdNode, IDisposable
    {
        public bool IsEnalbed = true;
        protected String _identifier = Guid.NewGuid().ToString();
        ~SNetTdNode() { this.Dispose(false); }

        public string CtkTdIdentifier { get { return this._identifier; } set { this._identifier = value; } }
        public string CtkTdName { get; set; }


        public Dictionary<String, SNetTdNode> TdNodes = new Dictionary<string, SNetTdNode>();

        public T AddNode<T>() where T : SNetTdNode, new()
        {
            var node = new T();
            return this.AddNode(node);
        }

        public T AddNode<T>(T node) where T : SNetTdNode
        {
            try
            {
                if (!Monitor.TryEnter(this.TdNodes, 30 * 1000)) throw new SNetException("Cannot add TdNodes in 30 seconds");

                if (this.TdNodes.ContainsKey(node.CtkTdIdentifier)) throw new ArgumentException("Already exist identifier");
                this.TdNodes[node.CtkTdIdentifier] = node;
                return node;
            }
            finally { Monitor.Exit(this.TdNodes); }
        }


        public void RefreshNodeId()
        {
            var dspNodes = this.TdNodes.Values;
            lock (this.TdNodes)
            {
                this.TdNodes.Clear();
                foreach (var node in dspNodes)
                    this.AddNode(node);
            }
        }




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
            this.TdNodes.Clear();
            CtkEventUtil.RemoveEventHandlersOfOwnerByFilter(this, (dlgt) => true);//移除自己的Event Delegate
        }
        #endregion

    }
}
