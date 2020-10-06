using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaskAutoCleaner.v1_0.StateMachineAlpha
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
