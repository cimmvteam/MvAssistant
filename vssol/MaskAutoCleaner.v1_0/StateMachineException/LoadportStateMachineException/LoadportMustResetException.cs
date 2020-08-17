using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineException.LoadportStateMachineException
{
   public  class LoadportMustResetException:StateMachineExceptionBase
    {
        public LoadportMustResetException(string message) : base(EnumStateMachineExceptionCode.LoadportMustResetException, message)
        {

        }
        public LoadportMustResetException() : this("")
        {

        }
    }
}
