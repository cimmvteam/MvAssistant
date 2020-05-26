using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaskCleaner.StateMachine_v1_0
{
    public class StateExitEventArgs : EventArgs
    {
        public IStateParam TriggerStateParam;


        public StateExitEventArgs()
        {
            // TODO: Complete member initialization
        }
   
    }
}
