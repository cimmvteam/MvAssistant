using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.UniversalStateMachineException
{
    public class UniversalCoverFanPLCWarningException : StateMachineExceptionBase
    {
        public UniversalCoverFanPLCWarningException(string message) : base(EnumStateMachineExceptionCode.UniversalCoverFanPLCWarningException, message)
        {

        }
        public UniversalCoverFanPLCWarningException() : this("")
        {

        }
    }
}
