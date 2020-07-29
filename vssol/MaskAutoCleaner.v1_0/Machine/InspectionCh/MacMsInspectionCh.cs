using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.InspectionCh
{
    public class MacMsInspectionCh : MacMachineStateBase
    {
        public EnumMacMsInspectionChState CurrentWorkState { get; set; }
        public override void LoadStateMachine()
        {
            throw new NotImplementedException();
        }
    }
}
