using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.MaskTransferStateMachineException
{
   public  class MaskTransferInitialTimeOutException:StateMachineExceptionBase
    {
        public MaskTransferInitialTimeOutException( string message):base(EnumStateMachineExceptionCode.MaskTransferInitialTimeOutException, message)
        {

        }
        public MaskTransferInitialTimeOutException() : this("")
        {

        }
    }
}
