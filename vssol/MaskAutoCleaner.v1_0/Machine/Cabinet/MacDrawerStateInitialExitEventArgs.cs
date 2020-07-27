using MaskAutoCleaner.v1_0.StateMachineBeta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet
{
    public class MacDrawerStateInitialExitEventArgs: MacStateExitEventArgs
    {
      
        public MacDrawerStateInitialResult Result { get; private set; }

        private MacDrawerStateInitialExitEventArgs() { }

        public MacDrawerStateInitialExitEventArgs(MacDrawerStateInitialResult result):this() 
        {
            Result = result;

        }
    }

    public enum MacDrawerStateInitialResult
    {
        Complete,
        Failed,
        TimeOut,
    }
}
