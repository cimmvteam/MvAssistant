using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineException.MaskTransferStateMachineException
{
   public class MaskTransferInitialFailException:StateMachineExceptionBase
    {
        public MaskTransferInitialFailException(string message):base(EnumStateMachineExceptionCode.MaskTransferInitialFailException, message)
        {

        }
        public MaskTransferInitialFailException() : this("")
        {

        }
    }
}
