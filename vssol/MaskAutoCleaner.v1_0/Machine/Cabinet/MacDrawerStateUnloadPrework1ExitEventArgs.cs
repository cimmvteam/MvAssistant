using MaskAutoCleaner.v1_0.StateMachineBeta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet
{
    public class MacDrawerStateUnloadPrework1ExitEventArgs : MacStateExitEventArgs
    {
        public MacDrawerStateUnloadPrework1Result Result { get; private set; }

        private MacDrawerStateUnloadPrework1ExitEventArgs() { }

        public MacDrawerStateUnloadPrework1ExitEventArgs(MacDrawerStateUnloadPrework1Result result):this() 
        {
            Result = result;

        }
    }
    public enum MacDrawerStateUnloadPrework1Result
    {
        Complete,
        Failed,
        TimeOut,
    }
}
