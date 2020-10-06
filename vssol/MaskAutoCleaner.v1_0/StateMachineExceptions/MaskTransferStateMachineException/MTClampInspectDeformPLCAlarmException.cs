using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.MaskTransferStateMachineException
{
    public class MTClampInspectDeformPLCAlarmException : StateMachineExceptionBase
    {
        public MTClampInspectDeformPLCAlarmException(string message) : base(EnumStateMachineExceptionCode.ClampInspectDeformPLCAlarmException, message)
        {

        }
        public MTClampInspectDeformPLCAlarmException() : this("")
        {

        }
    }
}
