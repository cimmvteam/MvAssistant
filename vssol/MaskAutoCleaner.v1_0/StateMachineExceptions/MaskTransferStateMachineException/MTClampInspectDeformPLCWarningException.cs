using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.MaskTransferStateMachineException
{
    public class MTClampInspectDeformPLCWarningException : StateMachineExceptionBase
    {
        public MTClampInspectDeformPLCWarningException(string message) : base(EnumStateMachineExceptionCode.ClampInspectDeformPLCWarningException, message)
        {

        }
        public MTClampInspectDeformPLCWarningException() : this("")
        {

        }
    }
}
