using MaskAutoCleaner.v1_0.StateMachineBeta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet
{
   public  class MacDrawerStateLoadPrework1ExitEventArgs: MacStateExitEventArgs
    {
        public MacDrawerStateLoadPrework1Result Result { get; private set; }

        private MacDrawerStateLoadPrework1ExitEventArgs() { }

        public MacDrawerStateLoadPrework1ExitEventArgs(MacDrawerStateLoadPrework1Result result):this() 
        {
            Result = result;

        }
    }
    public enum MacDrawerStateLoadPrework1Result
    {
        Complete,
        Failed,
        TimeOut,
    }
}
