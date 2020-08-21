using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.LoadportStateMachineException
{
    public class LoadportResetFailException : StateMachineExceptionBase
    {
        public LoadportResetFailException(string message) : base(EnumStateMachineExceptionCode.LoadportResetFailException, message)
        {

        }
        public LoadportResetFailException() : this("")
        {

        }
    }
}
