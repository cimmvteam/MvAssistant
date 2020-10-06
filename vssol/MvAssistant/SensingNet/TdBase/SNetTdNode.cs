using CToolkit.v1_1;
using CToolkit.v1_1.Timing;
using CToolkit.v1_1.TriggerDiagram;
using SensingNet.v0_2.TimeSignal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SensingNet.v0_2.TdBase
{
    public class SNetTdNode : ICtkTdNode, IDisposable
    {
        public bool IsEnalbed = true;
        protected String _identifier = Guid.NewGuid().ToString();
        ~SNetTdNode() { this.Dispose(false); }

        public string CtkTdIdentifier { get { return this._identifier; } set { this._identifier = value; } }
        public string CtkTdName { get; set; }
   
        
        
        
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
