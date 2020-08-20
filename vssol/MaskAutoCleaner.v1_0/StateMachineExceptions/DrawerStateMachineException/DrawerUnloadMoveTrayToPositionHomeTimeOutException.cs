using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.DrawerStateMachineException
{
   public  class DrawerUnloadMoveTrayToPositionHomeTimeOutException : StateMachineExceptionBase
    {
        public DrawerUnloadMoveTrayToPositionHomeTimeOutException(string message) : base(EnumStateMachineExceptionCode.DrawerUnloadMoveTrayToPositionHomeTimeOutException, message)
        {

        }
        public DrawerUnloadMoveTrayToPositionHomeTimeOutException() : this("")
        {

        }
    }
}
