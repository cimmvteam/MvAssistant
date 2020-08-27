using System;
using MaskAutoCleaner.v1_0.Machine;
using MaskAutoCleaner.v1_0.Machine.InspectionCh;
using MaskAutoCleaner.v1_0.TestMy.UserData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MaskAutoCleaner.v1_0.TestMy.Machine
{
    [TestClass]
    public class UtMcInspectionCh
    {
        [TestMethod]
        public void TestMethod1()
        {
            try
            {
                var MachineMgr = new MacMachineMgr();
                MachineMgr.MvCfInit();
                var MachineCtrl = MachineMgr.CtrlMachines[EnumMachineID.MID_IC_A_ASB.ToString()] as MacMcInspectionCh;
                MachineCtrl.StateMachine.Initial();
                MachineCtrl.StateMachine.GlassInspect();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
