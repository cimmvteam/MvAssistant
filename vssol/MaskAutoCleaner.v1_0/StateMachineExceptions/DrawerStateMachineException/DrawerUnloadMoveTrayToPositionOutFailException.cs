using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.DrawerStateMachineException
{
    public class DrawerUnloadMoveTrayToPositionOutFailException : StateMachineExceptionBase
    {
        public DrawerUnloadMoveTrayToPositionOutFailException(string message) : base(EnumStateMachineExceptionCode.DrawerUnloadMoveTrayToPositionOutFailException, message)
        {

        }
        public DrawerUnloadMoveTrayToPositionOutFailException() : this("")
        {

        }
    }
}
