using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.CleanChStateMachineException
{
    public class CleanChPLCAlarmException : StateMachineExceptionBase
    {
        public CleanChPLCAlarmException(string message) : base(EnumStateMachineExceptionCode.CleanChPLCAlarmException, message)
        {

        }
        public CleanChPLCAlarmException() : this("")
        {

        }
    }
}
