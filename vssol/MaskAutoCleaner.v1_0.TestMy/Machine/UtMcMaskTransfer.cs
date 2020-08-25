using System;
using MaskAutoCleaner.v1_0.Machine;
using MaskAutoCleaner.v1_0.Machine.MaskTransfer;
using MaskAutoCleaner.v1_0.TestMy.UserData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MaskAutoCleaner.v1_0.TestMy.Machine
{
    [TestClass]
    public class UtMcMaskTransfer
    {
        [TestMethod]
        public void TestInitial()
        {
            try
            {
                var MachineMgr = new MacMachineMgr();
                //var MTmachineMs = new v1_0.Machine.MaskTransfer.MacMsMaskTransfer();
                //var MTmachineMc = new MacMcMaskTransfer();
                MachineMgr.MvCfInit();
                var MachineCtrl = MachineMgr.CtrlMachines[EnumMachineID.MID_MT_A_ASB.ToString()] as MacMcMaskTransfer;
                MachineCtrl.StateMachine.Initial();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
