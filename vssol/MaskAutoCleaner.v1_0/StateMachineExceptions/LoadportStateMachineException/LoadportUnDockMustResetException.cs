using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.LoadportStateMachineException
{
    public class LoadportUndockMustResetException : StateMachineExceptionBase
    {
        public LoadportUndockMustResetException(string message) : base(EnumStateMachineExceptionCode.LoadportUndockMustResetException, message)
        {

        }
        public LoadportUndockMustResetException() : this("")
        {

        }
    }
}
