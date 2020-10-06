using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.InspectionChStateMachineException
{
    public class InspectionChPLCAlarmException : StateMachineExceptionBase
    {
        public InspectionChPLCAlarmException(string message) : base(EnumStateMachineExceptionCode.InspectionChPLCAlarmException, message)
        {

        }
        public InspectionChPLCAlarmException() : this("")
        {

        }
    }
}
