using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.StateJob
{
    public class StateJobBasic : List<StateJobBasic>
    {
        public Func<StateJobHandler, StateJobResult> Execute;


        public StateJobBasic() { }
        public StateJobBasic(Func<StateJobHandler, StateJobResult> exec) { this.Execute = exec; }

        public static implicit operator StateJobBasic(Func<StateJobHandler, StateJobResult> exec) { return new StateJobBasic() { Execute = exec }; }
    }
}
