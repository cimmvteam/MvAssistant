using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.StateMachine_v1_0.SmExp
{
    public class StateMachineAlterFailException : StateMachineException
    {
        public StateMachineAlterFailException() { }
        public StateMachineAlterFailException(string message) : base(message) { }
        public StateMachineAlterFailException(string message, Exception inner) : base(message, inner) { }
        protected StateMachineAlterFailException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
