using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.LoadportStateMachineException
{
   public  class LoadportInitialMustResetException:StateMachineExceptionBase
    {
        public LoadportInitialMustResetException(string message) : base(EnumStateMachineExceptionCode.LoadportInitialMustResetException, message)
        {

        }
        public LoadportInitialMustResetException() : this("")
        {

        }
    }
}
