using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.UniversalStateMachineException
{
    public class UniversalGeneralWarningException : StateMachineExceptionBase
    {
        public UniversalGeneralWarningException(string message) : base(EnumStateMachineExceptionCode.UniversalGeneralWarningException, message)
        {

        }
        public UniversalGeneralWarningException() : this("")
        {

        }
    }
}
