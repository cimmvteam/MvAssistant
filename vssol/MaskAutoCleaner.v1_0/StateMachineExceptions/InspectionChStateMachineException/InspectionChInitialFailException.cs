using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.InspectionChStateMachineException
{
    public class InspectionChInitialFailException : StateMachineExceptionBase
    {
        public InspectionChInitialFailException(string message) : base(EnumStateMachineExceptionCode.InspectionChInitialFailException, message)
        {

        }
        public InspectionChInitialFailException() : this("")
        {

        }
    }
}
