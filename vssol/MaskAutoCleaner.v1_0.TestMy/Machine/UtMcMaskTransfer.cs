using System;
using MaskAutoCleaner.v1_0.Machine;
using MaskAutoCleaner.v1_0.Machine.MaskTransfer;
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
                MS.SystemBootup();
                MS.LPHomeToLPAGetMaskReturnToLPHomeClamped();
                MS.LPHomeClampedToOSReleaseMaskReturnToLPHome();
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
                MS.SystemBootup();
                MS.LPHomeToLPAGetMaskReturnToLPHomeClamped();
                MS.LPHomeClampedToICHomeClamped();
                MS.ICHomeClampedToICPellicleReleaseReturnToICHome();
                MS.ICHomeToICPellicleGetReturnToICClamped();
                MS.ICHomeClampedToICGlassReleaseReturnToICHome();
                MS.ICHomeToICGlassGetReturnToICClamped();
                MS.ICHomeClampedToICHomeInspected();
                MS.ICHomeInspectedToLPHomeInspected();
                MS.LPHomeInspectedToLPARelease();
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
                MS.SystemBootup();
                MS.LPHomeToLPBGetMaskReturnToLPHomeClamped();
                MS.LPHomeClampedToICHomeClamped();
                MS.ICHomeClampedToICPellicleReleaseReturnToICHome();
                MS.ICHomeToICPellicleGetReturnToICClamped();
                MS.ICHomeClampedToICGlassReleaseReturnToICHome();
                MS.ICHomeToICGlassGetReturnToICClamped();
                MS.ICHomeClampedToICHomeInspected();
                MS.ICHomeInspectedToLPHomeInspected();
                MS.LPHomeInspectedToLPBRelease();
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
                MS.SystemBootup();
                MS.LPHomeToOSGetMaskReturnToLPHomeClamped();
                MS.LPHomeClampedToICHomeClamped();
                MS.ICHomeClampedToICPellicleReleaseReturnToICHome();
                MS.ICHomeToICPellicleGetReturnToICClamped();
                MS.ICHomeClampedToICGlassReleaseReturnToICHome();
                MS.ICHomeToICGlassGetReturnToICClamped();
                MS.ICHomeClampedToICHomeInspected();
                MS.ICHomeInspectedToLPHomeInspected();
                MS.LPHomeInspectedToLPARelease();
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
                MS.SystemBootup();
                MS.LPHomeToLPAGetMaskReturnToLPHomeClamped();
                MS.LPHomeClampedToICHomeClamped();
                MS.ICHomeClampedToICPellicleReleaseReturnToICHome();
                MS.ICHomeToICPellicleGetReturnToICClamped();
                MS.ICHomeClampedToICGlassReleaseReturnToICHome();
                MS.ICHomeToICGlassGetReturnToICClamped();
                MS.ICHomeClampedToICHomeInspected();
                MS.ICHomeInspectedToCCHomeClamped();
                MS.CCHomeClampedToCCPellicle();
                MS.InCCPellicleMoveToClean();
                MS.CCPellicleCleanedReturnInCC();
                MS.InCCPellicleMoveToInspect();
                MS.CCPellicleInspectedReturnInCC();
                MS.InCCPellicleToCCHomeClamped();
                MS.CCHomeClampedToCCGlass();
                MS.InCCGlassMoveToClean();
                MS.CCGlassCleanedReturnInCCGlass();
                MS.InCCGlassMoveToInspect();
                MS.CCGlassInspectedReturnInCCGlass();
                MS.InCCGlassToCCHomeClamped();
                MS.CCHomeClampedToCCHomeCleaned();
                MS.CCHomeCleanedToLPHomeCleaned();
                MS.LPHomeCleanedToLPARelease();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
