using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaskAutoCleaner.v1_0.StateMachineAlpha.SmExp
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
