using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineException
{
    public abstract class StateMachineExceptionBase:Exception
    {
        public EnumStateMachineExceptionCode ExceptionCode { get; private set; }
     //   public string ExMessage { get; private set; } = "";
        
        private StateMachineExceptionBase(string message) : base(message)
        {
            
        }
        public StateMachineExceptionBase(EnumStateMachineExceptionCode exceptionCode,string message="") : base(message)
        {
            ExceptionCode = exceptionCode;
        }
    }
}
