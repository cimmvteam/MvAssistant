using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.DrawerStateMachineException
{
    public class DrawerLoadMoveTrayToPositionHomeTimeOutException : StateMachineExceptionBase
    {
        public DrawerLoadMoveTrayToPositionHomeTimeOutException(string message) : base(EnumStateMachineExceptionCode.DrawerLoadMoveTrayToPositionHomeTimeOutException, message)
        {

        }
        public DrawerLoadMoveTrayToPositionHomeTimeOutException() : this("")
        {

        }
    }
}
