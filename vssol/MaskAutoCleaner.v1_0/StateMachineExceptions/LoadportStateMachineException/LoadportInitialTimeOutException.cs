using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.LoadportStateMachineException
{
    public class LoadportInitialTimeOutException : StateMachineExceptionBase
    {
        public LoadportInitialTimeOutException(string message) : base(EnumStateMachineExceptionCode.LoadportInitialTimeOutException, message)
        {
        }
        public LoadportInitialTimeOutException() : this("")
        {

        }
    }
}
