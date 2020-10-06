using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.LoadportStateMachineException
{
    public class LoadportAlarmResetFailException : StateMachineExceptionBase
    {
        public LoadportAlarmResetFailException(string message) : base(EnumStateMachineExceptionCode.LoadportAlarmResetFailException, message)
        {

        }
        public LoadportAlarmResetFailException() : this("")
        {

        }
    }
}
