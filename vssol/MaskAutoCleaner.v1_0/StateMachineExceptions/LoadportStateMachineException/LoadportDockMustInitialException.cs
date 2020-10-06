using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.LoadportStateMachineException
{
    public class LoadportDockMustInitialException : StateMachineExceptionBase
    {
        public LoadportDockMustInitialException(string message) : base(EnumStateMachineExceptionCode.LoadportDockMustInitialException, message)
        {

        }
        public LoadportDockMustInitialException() : this("")
        {

        }
    }
}
