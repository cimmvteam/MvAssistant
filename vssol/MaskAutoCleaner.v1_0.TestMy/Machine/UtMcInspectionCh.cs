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
        public void InspectPellicle()
        {
            try
            {
                var MachineMgr = new MacMachineMgr();
                MachineMgr.MvCfInit();
                var MachineCtrl = MachineMgr.CtrlMachines[EnumMachineID.MID_IC_A_ASB.ToString()] as MacMcInspectionCh;
                var MS = MachineCtrl.StateMachine;
                MS.Initial();
                MS.WaitForInputPellicle();
                MS.InspectPellicle();
                MS.ReleasePellicle();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        public void InspectGlass()
        {
            try
            {
                var MachineMgr = new MacMachineMgr();
                MachineMgr.MvCfInit();
                var MachineCtrl = MachineMgr.CtrlMachines[EnumMachineID.MID_IC_A_ASB.ToString()] as MacMcInspectionCh;
                var MS = MachineCtrl.StateMachine;
                MS.Initial();
                MS.WaitForInputGlass();
                MS.InspectGlass();
                MS.ReleaseGlass();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
