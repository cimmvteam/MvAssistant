using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.DrawerStateMachineException
{
    public class CabinetPLCAlarmException : StateMachineExceptionBase
    {
        public CabinetPLCAlarmException(string message) : base(EnumStateMachineExceptionCode.CabinetPLCAlarmException, message)
        {

        }
        public CabinetPLCAlarmException() : this("")
        {

        }
    }
}
