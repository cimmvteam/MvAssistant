using System;
using MaskAutoCleaner.v1_0.Machine;
using MaskAutoCleaner.v1_0.Machine.InspectionCh;
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
                MachineMgr.MvaCfInit();
                var MachineCtrl = MachineMgr.CtrlMachines[EnumMachineID.MID_IC_A_ASB.ToString()] as MacMcInspectionCh;
                var MS = MachineCtrl.StateMachine;
                MS.SystemBootup();
                MS.InspectPellicle();
                MS.ReturnToIdleAfterReleasePellicle();
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
                MachineMgr.MvaCfInit();
                var MachineCtrl = MachineMgr.CtrlMachines[EnumMachineID.MID_IC_A_ASB.ToString()] as MacMcInspectionCh;
                var MS = MachineCtrl.StateMachine;
                MS.SystemBootup();
                MS.InspectGlass();
                MS.ReturnToIdleAfterReleaseGlass();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
