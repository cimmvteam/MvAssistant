using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaskAutoCleaner.v1_0.Msg;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet.Drawer
{
    public class MacMcDrawer : MacMachineCtrlBase
    {
        private MsgBase _msgBase;
        public MacMcDrawer StateMachine = new MacMcDrawer();
        public override int RequestProcMsg(MsgBase msg)
        {
            _msgBase = msg;
            return 0;
        }
    }
}
