using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.LoadportStateMachineException
{
    public class LoadportResetTimeOutException : StateMachineExceptionBase
    {
        public LoadportResetTimeOutException(string message) : base(EnumStateMachineExceptionCode.LoadportResetTimeOutException, message)
        {

        }
        public LoadportResetTimeOutException() : this("")
        {

        }
    }
}
