using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.DrawerStateMachineException
{
  public   class DrawerMoveTrayToHomeWaitingUnloadInstructionTimeOutException : StateMachineExceptionBase
    {
        public DrawerMoveTrayToHomeWaitingUnloadInstructionTimeOutException(string message):base(EnumStateMachineExceptionCode.DrawerMoveTrayToHomeWaitingUnloadInstructionTimeOutException)
        {

        }
        public DrawerMoveTrayToHomeWaitingUnloadInstructionTimeOutException() : this("")
        {

        }
    }
}
