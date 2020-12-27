using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.DrawerStateMachineException
{
    class DrawerLoadNoBoxRejectTrayToPositionOutFromPositionHomeTimeOutException : StateMachineExceptionBase
    {
        public DrawerLoadNoBoxRejectTrayToPositionOutFromPositionHomeTimeOutException(string message) : base(EnumStateMachineExceptionCode.DrawerLoadNoBoxRejectTrayToPositionOutFromPositionHomeTimeOutException, message)
        {

        }
        public DrawerLoadNoBoxRejectTrayToPositionOutFromPositionHomeTimeOutException() : this("")
        {

        }
    }
}
