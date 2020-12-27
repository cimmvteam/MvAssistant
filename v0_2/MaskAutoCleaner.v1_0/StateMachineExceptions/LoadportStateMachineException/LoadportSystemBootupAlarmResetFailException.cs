using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.LoadportStateMachineException
{
     public  class LoadportSystemBootupAlarmResetFailException : StateMachineExceptionBase
    {
        public LoadportSystemBootupAlarmResetFailException(string message) : base(EnumStateMachineExceptionCode.LoadportSystemBootupAlarmResetFailException, message)
        {

        }
        public LoadportSystemBootupAlarmResetFailException() : this("")
        {

        }
    }
}
