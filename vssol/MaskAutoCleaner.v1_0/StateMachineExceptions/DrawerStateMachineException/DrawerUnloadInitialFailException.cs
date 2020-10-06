using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.DrawerStateMachineException
{
    public class DrawerUnloadInitialFailException : StateMachineExceptionBase
    {
        public DrawerUnloadInitialFailException(string message) : base(EnumStateMachineExceptionCode.DrawerUnloadInitialFailException, message)
        {

        }
        public DrawerUnloadInitialFailException() : this("")
        {

        }
    }
}
