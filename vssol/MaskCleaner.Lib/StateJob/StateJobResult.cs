using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.StateJob
{
    public class StateJobResult
    {
        public bool IsSuccess;
        public StateJobJudgeSpec SpecJudge;

        public static implicit operator StateJobResult(bool isSuccess) { return new StateJobResult() { IsSuccess = isSuccess }; }
    }
}
