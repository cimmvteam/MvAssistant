using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.LoadportStateMachineException
{
   public class LoadportSystemBootupAlarmResetTimeOutException : StateMachineExceptionBase
    {
        public LoadportSystemBootupAlarmResetTimeOutException(string message) : base(EnumStateMachineExceptionCode.LoadportSystemBootupAlarmResetTimeOutException, message)
        {

        }
        public LoadportSystemBootupAlarmResetTimeOutException() : this("")
        {

        }
    }
}
