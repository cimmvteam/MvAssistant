using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.DrawerStateMachineException
{
   public  class DrawerUnloadMoveTrayToPositionOutTimeOutException : StateMachineExceptionBase
    {
        public DrawerUnloadMoveTrayToPositionOutTimeOutException(string message) : base(EnumStateMachineExceptionCode.DrawerUnloadMoveTrayToPositionOutTimeOutException, message)
        {

        }
        public DrawerUnloadMoveTrayToPositionOutTimeOutException() : this("")
        {

        }
    }
}
