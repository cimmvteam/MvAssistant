using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskCleaner.StateMachine_v1_0.SmExp
{
    [global::System.Serializable]
    public class StateMachineFatelException : StateMachineException
    {
        public StateMachineFatelException() { }       
        public StateMachineFatelException(string message) : base(message) { }
        public StateMachineFatelException(string message, Exception inner) : base(message, inner) { }
        protected StateMachineFatelException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
