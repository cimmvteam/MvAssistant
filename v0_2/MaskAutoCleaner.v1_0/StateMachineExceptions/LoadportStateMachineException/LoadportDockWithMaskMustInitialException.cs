using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.LoadportStateMachineException
{
    public class LoadportDockWithMaskMustInitialException : StateMachineExceptionBase
    {
        public LoadportDockWithMaskMustInitialException(string message) : base(EnumStateMachineExceptionCode.LoadportDockWithMaskMustInitialException, message)
        {

        }
        public LoadportDockWithMaskMustInitialException() : this("")
        {

        }
    }
}
