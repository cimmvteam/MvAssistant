using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.DrawerStateMachineException
{
   public  class DrawerLoadMoveTrayToPositionOutTimeOutException : StateMachineExceptionBase
    {
        public DrawerLoadMoveTrayToPositionOutTimeOutException(string message) : base(EnumStateMachineExceptionCode.DrawerLoadMoveTrayToPositionOutTimeOutException, message)
        {

        }
        public DrawerLoadMoveTrayToPositionOutTimeOutException() : this("")
        {

        }
    }
}
