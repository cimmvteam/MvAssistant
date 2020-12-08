using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.InspectionChStateMachineException
{
    class InspectionChInspectFailException : StateMachineExceptionBase
    {
        public InspectionChInspectFailException(string message) : base(EnumStateMachineExceptionCode.InspectionChInspectFailException, message)
        {

        }
        public InspectionChInspectFailException() : this("")
        {

        }
    }
}
