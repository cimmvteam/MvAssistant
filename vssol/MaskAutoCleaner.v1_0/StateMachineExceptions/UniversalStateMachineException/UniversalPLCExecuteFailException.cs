using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.UniversalStateMachineException
{
    public class UniversalPLCExecuteFailException : StateMachineExceptionBase
    {
        public UniversalPLCExecuteFailException(string message) : base(EnumStateMachineExceptionCode.UniversalPLCExecuteFailException, message)
        {

        }
        public UniversalPLCExecuteFailException() : this("")
        {

        }
    }
}
