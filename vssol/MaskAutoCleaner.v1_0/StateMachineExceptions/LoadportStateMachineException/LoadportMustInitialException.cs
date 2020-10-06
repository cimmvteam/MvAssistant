using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.LoadportStateMachineException
{
    public class LoadportMustInitialException:StateMachineExceptionBase
    { 
        public LoadportMustInitialException(string message) : base(EnumStateMachineExceptionCode.LoadportMustInitialException, message)
        {

        }
        public LoadportMustInitialException() : this("")
        {

        }
    }
}
