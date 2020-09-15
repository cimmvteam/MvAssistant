using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.BoxTransferStateMachineException
{
    public class BoxTransferPLCAlarmException : StateMachineExceptionBase
    {
        public BoxTransferPLCAlarmException(string message) : base(EnumStateMachineExceptionCode.BoxTransferPLCAlarmException, message)
        {

        }
        public BoxTransferPLCAlarmException() : this("")
        {

        }
    }
}
