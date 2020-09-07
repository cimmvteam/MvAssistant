using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.DrawerStateMachineException
{
    public class CabinetPLCWarningException : StateMachineExceptionBase
    {
        public CabinetPLCWarningException(string message) : base(EnumStateMachineExceptionCode.CabinetPLCWarningException, message)
        {

        }
        public CabinetPLCWarningException() : this("")
        {

        }
    }
}
