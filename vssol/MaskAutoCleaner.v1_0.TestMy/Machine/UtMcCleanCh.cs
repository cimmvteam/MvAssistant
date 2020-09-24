using System;
using MaskAutoCleaner.v1_0.Machine;
using MaskAutoCleaner.v1_0.Machine.CleanCh;
using MaskAutoCleaner.v1_0.UserData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MaskAutoCleaner.v1_0.TestMy.Machine
{
    [TestClass]
    public class UtMcCleanCh
    {
        [TestMethod]
        public void TestMethod1()
        {
            var MachineMgr = new MacMachineMgr();
            MachineMgr.MvCfInit();
            var MachineCtrl = MachineMgr.CtrlMachines[EnumMachineID.MID_CC_A_ASB.ToString()] as MacMcCleanCh;
            var MS = MachineCtrl.StateMachine;
            MS.SystemBootup();
            MS.CleanPellicle();
            MS.FinishCleanPellicle();
            MS.InspectPellicle();
            MS.FinishInspectPellicle();
            MS.CleanGlass();
            MS.FinishCleanGlass();
            MS.InspectGlass();
            MS.FinishInspectGlass();
        }
    }
}
