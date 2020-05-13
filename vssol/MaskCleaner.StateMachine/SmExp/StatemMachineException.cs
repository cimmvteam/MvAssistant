using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaskCleaner.StateMachine.SmExp
{

    public class StateMachineException : Exception
    {

        public string transName = null;
        public EnumAlarmAction AlarmAction = EnumAlarmAction.Warning;


        public StateMachineException() { }
        public StateMachineException(string message) : base(message) { }
        public StateMachineException(string message, string transName)
            : base(message)
        {
            this.transName = transName;
        }
        public StateMachineException(string message, Exception inner) : base(message, inner) { }
        protected StateMachineException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
