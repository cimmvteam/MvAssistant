using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.LoadportStateMachineException
{
  public   class LoadportSystemBootupInitialMustResetException : StateMachineExceptionBase
    {
        public LoadportSystemBootupInitialMustResetException(string message) : base(EnumStateMachineExceptionCode.LoadportSystemBootupInitialMustResetException, message)
        {

        }
        public LoadportSystemBootupInitialMustResetException() : this("")
        {

        }
    }

}
