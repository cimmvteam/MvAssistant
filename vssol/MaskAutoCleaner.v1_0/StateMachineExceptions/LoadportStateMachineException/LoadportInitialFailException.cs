using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.LoadportStateMachineException
{
    public class LoadportInitialFailException:StateMachineExceptionBase
    {
        public LoadportInitialFailException(string message):base(EnumStateMachineExceptionCode.LoadportInitialFailException)
        {

        }
        public LoadportInitialFailException() : this("")
        {

        }
    }
}
