using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.InspectionChStateMachineException
{
    public class InspectionChException : StateMachineExceptionBase
    {
        public InspectionChException(string message) : base(EnumStateMachineExceptionCode.InspectionChException, message)
        {

        }
        public InspectionChException() : this("")
        {

        }
    }
}
