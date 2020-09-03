using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.DrawerStateMachineException
{
   public   class DrawerMoveTrayToHomeWaitingUnloadInstructionFailException : StateMachineExceptionBase
    {
        public DrawerMoveTrayToHomeWaitingUnloadInstructionFailException(string message):base(EnumStateMachineExceptionCode.DrawerMoveTrayToHomeWaitingUnloadInstructionFailException)
        {

        }
        public DrawerMoveTrayToHomeWaitingUnloadInstructionFailException() : this("")
        {

        }
    }
}
