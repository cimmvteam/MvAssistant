using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.InspectionChStateMachineException
{
    public class InspectionChPLCWarningException : StateMachineExceptionBase
    {
        public InspectionChPLCWarningException(string message) : base(EnumStateMachineExceptionCode.InspectionChPLCWarningException, message)
        {

        }
        public InspectionChPLCWarningException() : this("")
        {

        }
    }
}
