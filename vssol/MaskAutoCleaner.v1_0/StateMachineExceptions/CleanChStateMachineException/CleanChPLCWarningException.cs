using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.CleanChStateMachineException
{
    public class CleanChPLCWarningException : StateMachineExceptionBase
    {
        public CleanChPLCWarningException(string message) : base(EnumStateMachineExceptionCode.CleanChPLCWarningException, message)
        {

        }
        public CleanChPLCWarningException() : this("")
        {

        }
    }
}
