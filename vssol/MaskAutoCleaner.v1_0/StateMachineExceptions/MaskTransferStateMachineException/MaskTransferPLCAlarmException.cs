using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.MaskTransferStateMachineException
{
    public class MaskTransferPLCAlarmException : StateMachineExceptionBase
    {
        public MaskTransferPLCAlarmException(string message) : base(EnumStateMachineExceptionCode.MaskTransferException, message)
        {

        }
        public MaskTransferPLCAlarmException() : this("")
        {

        }
    }
}
