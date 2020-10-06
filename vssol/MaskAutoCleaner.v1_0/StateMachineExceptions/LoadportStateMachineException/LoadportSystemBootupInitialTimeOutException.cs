using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.LoadportStateMachineException
{
   public class LoadportSystemBootupInitialTimeOutException : StateMachineExceptionBase
    {
        public LoadportSystemBootupInitialTimeOutException(string message) : base(EnumStateMachineExceptionCode.LoadportSystemBootupInitialTimeOutException, message)
        {

        }
        public LoadportSystemBootupInitialTimeOutException() : this("")
        {

        }
    }
}
