using MaskAutoCleaner.v1_0.Machine;
using MaskAutoCleaner.v1_0.Machine.Cabinet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.TestMy.Machine
{
    [TestClass]
    public  class UtMcCabinetCabinetNew
    {
        MacMsCabinet Machine = default(MacMsCabinet);
        public UtMcCabinetCabinetNew()
        {
            var MachineMgr = new MacMachineMgr();
            MachineMgr.MvCfInit();
            var MachineCtrl = MachineMgr.CtrlMachines[EnumMachineID.MID_CB_A_ASB.ToString()] as MacMcCabinet;
            Machine = MachineCtrl.StateMachine;
        }

        [TestMethod]
        public void Test_CreateInstance()
        {
            var s = Machine.GetDicMacHalDrawers();
        }
    }
}
