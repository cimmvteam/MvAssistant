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
        public void LPAToOS()
        {
            try
            {
                var MachineMgr = new MacMachineMgr();
                MachineMgr.MvCfInit();
                var MachineCtrl = MachineMgr.CtrlMachines[EnumMachineID.MID_MT_A_ASB.ToString()] as MacMcMaskTransfer;
                var MS = MachineCtrl.StateMachine;
                MS.Initial();
                MS.MoveToLPAGetMaskReturnToLPHomeClamped();
                MS.LPHomeClampedReleaseOS();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        public void LPAInspectedToLPA()
        {
            try
            {
                var MachineMgr = new MacMachineMgr();
                MachineMgr.MvCfInit();
                var MachineCtrl = MachineMgr.CtrlMachines[EnumMachineID.MID_MT_A_ASB.ToString()] as MacMcMaskTransfer;
                var MS = MachineCtrl.StateMachine;
                MS.Initial();
                MS.MoveToLPAGetMaskReturnToLPHomeClamped();
                MS.LPHomeClampedToICHomeClamped();
                MS.ICHomeClampedReleaseToIC();
                MS.ICHomeGetFromIC();
                MS.ICHomeClampedReleaseToICGlass();
                MS.ICHomeGetFromICGlass();
                MS.ICHomeClampedToICHomeInspected();
                MS.ICHomeInspectedToLPHomeInspected();
                MS.LPHomeInspectedReleaseLPA();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        public void LPBInspectedToLPB()
        {
            try
            {
                var MachineMgr = new MacMachineMgr();
                MachineMgr.MvCfInit();
                var MachineCtrl = MachineMgr.CtrlMachines[EnumMachineID.MID_MT_A_ASB.ToString()] as MacMcMaskTransfer;
                var MS = MachineCtrl.StateMachine;
                MS.Initial();
                MS.MoveToLPBGetMaskReturnToLPHomeClamped();
                MS.LPHomeClampedToICHomeClamped();
                MS.ICHomeClampedReleaseToIC();
                MS.ICHomeGetFromIC();
                MS.ICHomeClampedReleaseToICGlass();
                MS.ICHomeGetFromICGlass();
                MS.ICHomeClampedToICHomeInspected();
                MS.ICHomeInspectedToLPHomeInspected();
                MS.LPHomeInspectedReleaseLPB();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        public void OSInspectedToLPA()
        {
            try
            {
                var MachineMgr = new MacMachineMgr();
                MachineMgr.MvCfInit();
                var MachineCtrl = MachineMgr.CtrlMachines[EnumMachineID.MID_MT_A_ASB.ToString()] as MacMcMaskTransfer;
                var MS = MachineCtrl.StateMachine;
                MS.Initial();
                MS.MoveToOSGetMaskReturnToLPHomeClamped();
                MS.LPHomeClampedToICHomeClamped();
                MS.ICHomeClampedReleaseToIC();
                MS.ICHomeGetFromIC();
                MS.ICHomeClampedReleaseToICGlass();
                MS.ICHomeGetFromICGlass();
                MS.ICHomeClampedToICHomeInspected();
                MS.ICHomeInspectedToLPHomeInspected();
                MS.LPHomeInspectedReleaseLPA();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        public void LPACleanedToLPA()
        {
            try
            {
                var MachineMgr = new MacMachineMgr();
                MachineMgr.MvCfInit();
                var MachineCtrl = MachineMgr.CtrlMachines[EnumMachineID.MID_MT_A_ASB.ToString()] as MacMcMaskTransfer;
                var MS = MachineCtrl.StateMachine;
                MS.Initial();
                MS.MoveToLPAGetMaskReturnToLPHomeClamped();
                MS.LPHomeClampedToICHomeClamped();
                MS.ICHomeClampedReleaseToIC();
                MS.ICHomeGetFromIC();
                MS.ICHomeClampedReleaseToICGlass();
                MS.ICHomeGetFromICGlass();
                MS.ICHomeClampedToICHomeInspected();
                MS.ICHomeInspectedToCCHomeClamped();
                MS.CCHomeClampedToCC();
                MS.CCCleanedToCapture();
                MS.CCCapturedToCCHomeClamped();
                MS.CCHomeClampedToCCGlass();
                MS.CCGlassCleanedToCapture();
                MS.CCGlassCapturedToCCHomeClamped();
                MS.CCHomeClampedToCCHomeCleaned();
                MS.CCHomeCleanedToLPHomeCleaned();
                MS.LPHomeCleanedReleaseLPA();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
