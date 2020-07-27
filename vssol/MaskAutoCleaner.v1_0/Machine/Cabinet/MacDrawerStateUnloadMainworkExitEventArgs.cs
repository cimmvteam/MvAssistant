using MaskAutoCleaner.v1_0.StateMachineBeta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet
{
    public  class MacDrawerStateUnloadMainworkExitEventArgs:MacStateExitEventArgs
    {
        public MacDrawerStateUnloadMainworkResult Result { get; private set; }

        private MacDrawerStateUnloadMainworkExitEventArgs() { }

        public MacDrawerStateUnloadMainworkExitEventArgs(MacDrawerStateUnloadMainworkResult result):this() 
        {
            Result = result;

        }
    }

    public enum MacDrawerStateUnloadMainworkResult
    {
        GotoHomeComplete,
        GotoHomeFail,
        GotoHomeTimeOut,
        GotoInComplete,
        GotoInFail,
        GotoInTimeOut,
    }
}
