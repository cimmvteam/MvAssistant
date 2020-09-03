using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.DrawerStateMachineException
{
    public class DrawerMoveTrayToHomeWaitingLoadInstructionFailException : StateMachineExceptionBase
    {
        public DrawerMoveTrayToHomeWaitingLoadInstructionFailException(string message):base(EnumStateMachineExceptionCode.DrawerMoveTrayToHomeWaitingLoadInstructionFailException)
        {

        }
        public DrawerMoveTrayToHomeWaitingLoadInstructionFailException() : this("")
        {

        }
    }
}
