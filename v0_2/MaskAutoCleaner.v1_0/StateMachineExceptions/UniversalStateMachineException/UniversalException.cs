using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.UniversalStateMachineException
{
    public class UniversalException : StateMachineExceptionBase
    {
        public UniversalException(string message) : base(EnumStateMachineExceptionCode.UniversalException, message)
        {

        }
        public UniversalException() : this("")
        {

        }
    }
}
