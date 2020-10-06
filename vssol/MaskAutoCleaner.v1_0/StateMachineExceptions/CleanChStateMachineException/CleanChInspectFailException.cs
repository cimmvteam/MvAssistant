using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.CleanChStateMachineException
{
    public class CleanChInspectFailException : StateMachineExceptionBase
    {
        public CleanChInspectFailException(string message) : base(EnumStateMachineExceptionCode.CleanChInspectFailException, message)
        {

        }
        public CleanChInspectFailException() : this("")
        {

        }
    }
}
