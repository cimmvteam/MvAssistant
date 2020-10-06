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
        private MacMsgBase _msgBase;
        public MacMsDrawer StateMachine { get { return this.msAssembly as MacMsDrawer; } set { this.msAssembly = value; } }

        public MacMcDrawer()
        {
            this.msAssembly = new MacMsDrawer();
        }
        public override int RequestProcMsg(MacMsgBase msg)
        {
            _msgBase = msg;
            return 0;
        }
    }
}
