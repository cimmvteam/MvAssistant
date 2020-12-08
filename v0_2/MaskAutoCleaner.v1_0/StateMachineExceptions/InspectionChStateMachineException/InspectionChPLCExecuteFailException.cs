using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.InspectionChStateMachineException
{
    public class InspectionChPLCExecuteFailException : StateMachineExceptionBase
    {
        public InspectionChPLCExecuteFailException(string message) : base(EnumStateMachineExceptionCode.InspectionChPLCExecuteFailException, message)
        {

        }
        public InspectionChPLCExecuteFailException() : this("")
        {

        }
    }
}
