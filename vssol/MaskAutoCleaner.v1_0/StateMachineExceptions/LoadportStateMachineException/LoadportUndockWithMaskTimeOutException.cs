//using Microsoft.Analytics.Interfaces;
//using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions.LoadportStateMachineException
{
  public  class LoadportUndockWithMaskTimeOutException : StateMachineExceptionBase
    {
        public LoadportUndockWithMaskTimeOutException(string message) : base(EnumStateMachineExceptionCode.LoadportUndockWithMaskTimeOutException, message)
        {

        }
        public LoadportUndockWithMaskTimeOutException() : this("")
        {

        }
    }
}
