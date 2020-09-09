using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MaskAutoCleaner.v1_0.Msg;

namespace MaskAutoCleaner.v1_0.Machine.CabinetDrawer
{
    [Guid("CFDD465E-0156-43CE-8ABF-B3EEFFBB510F")]
    public  class MacMcCabinetDrawer : MacMachineCtrlBase
    {
        public MacMsCabinetDrawer StateMachine { get { return this.msAssembly as MacMsCabinetDrawer; } set { this.msAssembly = value; } }
        private List<MacMsCabinetDrawer> StateeMachines = null;
        public MacMcCabinetDrawer()
        {
            this.msAssembly = new MacMsCabinetDrawer();
        }
        public override int RequestProcMsg(MsgBase msg)
        {
            return 0;
        }
    }
}
