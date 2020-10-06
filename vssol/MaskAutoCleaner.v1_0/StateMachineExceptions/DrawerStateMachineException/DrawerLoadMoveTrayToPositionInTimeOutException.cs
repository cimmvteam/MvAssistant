using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.DrawerStateMachineException
{
    public class DrawerLoadMoveTrayToPositionInTimeOutException : StateMachineExceptionBase
    {
        public DrawerLoadMoveTrayToPositionInTimeOutException(string message) : base(EnumStateMachineExceptionCode.DrawerLoadMoveTrayToPositionInTimeOutException, message)
        {

        }
        public DrawerLoadMoveTrayToPositionInTimeOutException() : this("")
        {

        }
    }
}
