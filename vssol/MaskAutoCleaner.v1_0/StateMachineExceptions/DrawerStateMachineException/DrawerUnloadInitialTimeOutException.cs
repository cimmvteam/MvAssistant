using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.DrawerStateMachineException
{
    public class DrawerUnloadInitialTimeOutException : StateMachineExceptionBase
    {
        public DrawerUnloadInitialTimeOutException(string message) : base(EnumStateMachineExceptionCode.DrawerUnloadInitialTimeOutException, message)
        {

        }
        public DrawerUnloadInitialTimeOutException() : this("")
        {

        }
    }
}
