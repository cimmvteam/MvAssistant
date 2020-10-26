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
        public MacMsCabinetDrawer StateMachine { get { return this.MsAssembly as MacMsCabinetDrawer; } set { this.MsAssembly = value; } }
       public MacMcCabinetDrawer()
        {
            this.MsAssembly = new MacMsCabinetDrawer();
        }
        public override int RequestProcMsg(IMacMsg msg)
        {
            return 0;
        }
    }
}
