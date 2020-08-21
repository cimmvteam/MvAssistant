using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.LoadportStateMachineException
{
    public class LoadportUndockMustInitialException : StateMachineExceptionBase
    {
        public LoadportUndockMustInitialException(string message) : base(EnumStateMachineExceptionCode.LoadportUndockMustInitialException, message)
        {

        }
        public LoadportUndockMustInitialException() : this("")
        {

        }
    }
}
