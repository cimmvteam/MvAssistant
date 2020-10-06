using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.DrawerStateMachineException
{
    public class DrawerLoadMoveTrayToPositionOutFailException: StateMachineExceptionBase
    {
        public DrawerLoadMoveTrayToPositionOutFailException(string message) : base(EnumStateMachineExceptionCode.DrawerLoadMoveTrayToPositionOutFailException, message)
        {

        }
        public DrawerLoadMoveTrayToPositionOutFailException() : this("")
        {

        }
    }
}
