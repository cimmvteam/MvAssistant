using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.UniversalStateMachineException
{
    public class UniversalCoverFanPLCAlarmException : StateMachineExceptionBase
    {
        public UniversalCoverFanPLCAlarmException(string message) : base(EnumStateMachineExceptionCode.UniversalCoverFanPLCAlarmException, message)
        {

        }
        public UniversalCoverFanPLCAlarmException() : this("")
        {

        }
    }
}
