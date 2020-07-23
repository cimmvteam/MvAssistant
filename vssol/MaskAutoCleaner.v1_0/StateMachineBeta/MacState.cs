using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineBeta
{
    public class MacState
    {
        public string Name;

        public MacState() { }
        public MacState(string name) { this.Name = name; }


        #region Event Provide

        public event EventHandler<MacStateEntryEventArgs> OnEntry;
        public event EventHandler<MacStateExitEventArgs> OnExit;

        public virtual void DoExit(MacStateExitEventArgs args)
        {
            if (OnExit == null) return;
            this.OnExit(this, args);
        }

        public void DoEntry(MacStateEntryEventArgs seea)
        {
            if (OnEntry == null) return;
            this.OnEntry(this, seea);
        }
        #endregion
    }
}
