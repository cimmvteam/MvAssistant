using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineException.MaskTransferStateMachineException
{
   public class MaskTransferMustResetException:StateMachineExceptionBase
    {
        public  MaskTransferMustResetException(string message) : base(EnumStateMachineExceptionCode.MaskTransferMustResetException, message)
        {

        }
        public MaskTransferMustResetException():this("")
        {

        }
   }
}
