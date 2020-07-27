using MaskAutoCleaner.v1_0.StateMachineBeta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet
{
   public  class MacDrawerStateLoadMainworkExitEventArgs: MacStateExitEventArgs
    {
        public MacDrawerStateLoadMainworkResult Result { get; private set; }

        private MacDrawerStateLoadMainworkExitEventArgs() { }

        public MacDrawerStateLoadMainworkExitEventArgs(MacDrawerStateLoadMainworkResult result):this() 
        {
            Result = result;

        }
    }
    public enum MacDrawerStateLoadMainworkResult
    {
        GotoHomeComplete,
        GotoHomeFail,
        GotoHomeTimeOut,
        GotoOutComplete,
        GotoOutFail,
        GotoOutTimeOut,
    }
}
