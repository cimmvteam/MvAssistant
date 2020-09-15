using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.DrawerStateMachineException
{
   public  class DrawerMoveTrayToHomeWaitingLoadInstructionTimeOutException : StateMachineExceptionBase
    {
        public DrawerMoveTrayToHomeWaitingLoadInstructionTimeOutException(string message):base(EnumStateMachineExceptionCode.DrawerMoveTrayToHomeWaitingLoadInstructionTimeOutException)
        {

        }
        public DrawerMoveTrayToHomeWaitingLoadInstructionTimeOutException() : this("")
        {

        }
    }
}
