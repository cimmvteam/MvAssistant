using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineBeta
{
    public class MacStateEntryEventArgs : EventArgs
    {
      
        /// <summary>temp Property</summary>
        public object Parameter { get; private set; }
        /// <summary>temp Constructor</summary>
        /// <param name="parameter"></param>
        public MacStateEntryEventArgs(object parameter)
        {
            Parameter = parameter;
        }

    }
}
