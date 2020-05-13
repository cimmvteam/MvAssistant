using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaskCleaner.StateMachine.SmExp
{
    [global::System.Serializable]
    public class NotCurrentStateException : StateMachineException
    {
        public NotCurrentStateException() { }
        public NotCurrentStateException(string message) : base(message) { }
        public NotCurrentStateException(string message, Exception inner) : base(message, inner) { }
        protected NotCurrentStateException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
