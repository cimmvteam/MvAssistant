using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaskAutoCleaner.v1_0.StateMachineBeta
{
    public class MacStateExitEventArgs : EventArgs
    {
        public object Parameter { get; private set; }
        public MacStateExitEventArgs(object parameter):this()
        {
            Parameter = parameter;
        }
        public MacStateExitEventArgs()
        {
            Parameter = null;
        }
     
    }
}
