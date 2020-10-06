using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineAlpha.SmExp
{
    public class PluralTriggerSameTimeException: StateMachineException
    {
        public PluralTriggerSameTimeException() { }       
        public PluralTriggerSameTimeException(string message) : base(message) { }
        public PluralTriggerSameTimeException(string message, Exception inner) : base(message, inner) { }
        protected PluralTriggerSameTimeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
