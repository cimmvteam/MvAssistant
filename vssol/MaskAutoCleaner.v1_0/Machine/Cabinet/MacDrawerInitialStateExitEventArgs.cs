using MaskAutoCleaner.v1_0.StateMachineBeta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet
{
    public class MacDrawerInitialStateExitEventArgs: MacStateExitEventArgs
    {
      
        public MacDrawerStateInitialResult InitialResult { get; private set; }

        private MacDrawerInitialStateExitEventArgs() { }

        public MacDrawerInitialStateExitEventArgs(MacDrawerStateInitialResult result):this() 
        {
            InitialResult = result;

        }
    }

    public enum MacDrawerStateInitialResult
    {
        Complete,
        Failed,
        TimeOut,
    }
}
