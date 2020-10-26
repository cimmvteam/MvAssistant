using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MaskAutoCleaner.v1_0.Msg;

namespace MaskAutoCleaner.v1_0.Machine.Drawer
{
    [Guid("8DDAA02F-8D2D-46C6-9E8B-3E861E431FF2")]
    public class MacMcDrawer : MacMachineCtrlBase
    {
        private IMacMsg _msgBase;
        public MacMsDrawer StateMachine { get { return this.MsAssembly as MacMsDrawer; } set { this.MsAssembly = value; } }

        public MacMcDrawer()
        {
            this.MsAssembly = new MacMsDrawer();
        }
        public override int RequestProcMsg(IMacMsg msg)
        {
            _msgBase = msg;
            return 0;
        }
    }
}
