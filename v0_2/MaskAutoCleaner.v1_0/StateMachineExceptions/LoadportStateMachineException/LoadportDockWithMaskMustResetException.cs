using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.LoadportStateMachineException
{
   public class LoadportDockWithMaskMustResetException : StateMachineExceptionBase
    {
        public LoadportDockWithMaskMustResetException(string message) : base(EnumStateMachineExceptionCode.LoadportDockWithMaskMustResetException, message)
        {

        }
        public LoadportDockWithMaskMustResetException() : this("")
        {

        }
    }
}
