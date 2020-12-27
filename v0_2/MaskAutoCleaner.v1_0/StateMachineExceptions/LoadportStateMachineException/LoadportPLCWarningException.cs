using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.LoadportStateMachineException
{
    public class LoadportPLCWarningException : StateMachineExceptionBase
    {
        public LoadportPLCWarningException(string message) : base(EnumStateMachineExceptionCode.LoadportPLCWarningException, message)
        {

        }
        public LoadportPLCWarningException() : this("")
        {

        }
    }
}
