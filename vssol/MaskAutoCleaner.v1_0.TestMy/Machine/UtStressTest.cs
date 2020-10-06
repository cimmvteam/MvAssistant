using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MaskAutoCleaner.v1_0.Machine;
using MaskAutoCleaner.v1_0.Machine.BoxTransfer;
using MaskAutoCleaner.v1_0.Machine.CabinetDrawer;
using MaskAutoCleaner.v1_0.Machine.CleanCh;
using MaskAutoCleaner.v1_0.Machine.InspectionCh;
using MaskAutoCleaner.v1_0.Machine.LoadPort;
using MaskAutoCleaner.v1_0.Machine.MaskTransfer;
using MaskAutoCleaner.v1_0.Machine.OpenStage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MaskAutoCleaner.v1_0.TestMy.Machine
{
    [TestClass]
    public class UtStressTest
    {
        [TestMethod]
        public void OCAP()
        {
            var MachineMgr = new MacMachineMgr();
            MachineMgr.MvCfInit();
            var LPAMC = MachineMgr.CtrlMachines[EnumMachineID.MID_LP_A_ASB.ToString()] as MacMcLoadPort;
            var LPBMC = MachineMgr.CtrlMachines[EnumMachineID.MID_LP_B_ASB.ToString()] as MacMcLoadPort;
            var MTMC = MachineMgr.CtrlMachines[EnumMachineID.MID_MT_A_ASB.ToString()] as MacMcMaskTransfer;
            var ICMC = MachineMgr.CtrlMachines[EnumMachineID.MID_IC_A_ASB.ToString()] as MacMcInspectionCh;
            var CCMC = MachineMgr.CtrlMachines[EnumMachineID.MID_CC_A_ASB.ToString()] as MacMcCleanCh;
            var LPAMS = LPAMC.StateMachine;
            var LPBMS = LPBMC.StateMachine;
            var MTMS = MTMC.StateMachine;
            var ICMS = ICMC.StateMachine;
            var CCMS = CCMC.StateMachine;


            var LPA_Task = Task.Factory.StartNew(() => { LPAMS.SystemBootup(); });
            var LPB_Task = Task.Factory.StartNew(() => { LPBMS.SystemBootup(); });
            var IC_Task = Task.Factory.StartNew(() => { ICMS.SystemBootup(); });
            var CC_Task = Task.Factory.StartNew(() => { CCMS.SystemBootup(); });
            var MT_Task = Task.Factory.StartNew(() => { MTMS.SystemBootup(); });

            Task.WaitAll(LPA_Task, LPB_Task, MT_Task);
            MTMS.LPHomeToLPAGetMaskReturnToLPHomeClamped();
            MTMS.LPHomeClampedToICHomeClamped();
            //To IC Inspect Glass
            MTMS.ICHomeClampedToICGlassReleaseReturnToICHome();
            ICMS.InspectGlass();
            MTMS.ICHomeToICGlassGetReturnToICClamped();
            //To IC Inspect Pellicle
            MTMS.ICHomeClampedToICReleaseReturnToICHome();
            ICMS.InspectPellicle();
            MTMS.ICHomeToICGetReturnToICClamped();

            MTMS.ICHomeClampedToICHomeInspected();
            MTMS.ICHomeInspectedToCCHomeClamped();
            //To CC Clean Glass
            MTMS.CCHomeClampedToCCGlass();
            MTMS.InCCGlassMoveToClean();
            MT_Task = Task.Factory.StartNew(() => { MTMS.CleanGlass(); });
            CCMS.CleanGlass();
            Task.WaitAll(MT_Task);
            CCMS.FinishCleanGlass();
            MTMS.CCGlassCleanedReturnInCCGlass();
            MTMS.InCCGlassMoveToInspect();
            MT_Task = Task.Factory.StartNew(() => { MTMS.InspectGlass(); });
            CCMS.InspectGlass();
            Task.WaitAll(MT_Task);
            CCMS.FinishInspectGlass();
            MTMS.CCGlassInspectedReturnInCCGlass();
            MTMS.InCCGlassToCCHomeClamped();
            //To CC Clean Pellicle
            MTMS.CCHomeClampedToCC();
            MTMS.InCCMoveToClean();
            MT_Task = Task.Factory.StartNew(() => { MTMS.CleanPellicle(); });
            CCMS.CleanPellicle();
            Task.WaitAll(MT_Task);
            CCMS.FinishCleanPellicle();
            MTMS.CCCleanedReturnInCC();
            MTMS.InCCMoveToInspect();
            MT_Task = Task.Factory.StartNew(() => { MTMS.InspectPellicle(); });
            CCMS.InspectPellicle();
            Task.WaitAll(MT_Task);
            CCMS.FinishInspectPellicle();
            MTMS.CCInspectedReturnInCC();
            MTMS.InCCToCCHomeClamped();

            MTMS.CCHomeClampedToCCHomeCleaned();
            MTMS.CCHomeCleanedToLPHomeCleaned();

            MTMS.LPHomeCleanedToLPARelease();
        }
        [TestMethod]
        public void BankIn()
        {
            var MachineMgr = new MacMachineMgr();
            MachineMgr.MvCfInit();
            var LPAMC = MachineMgr.CtrlMachines[EnumMachineID.MID_LP_A_ASB.ToString()] as MacMcLoadPort;
            var LPBMC = MachineMgr.CtrlMachines[EnumMachineID.MID_LP_B_ASB.ToString()] as MacMcLoadPort;
            var MTMC = MachineMgr.CtrlMachines[EnumMachineID.MID_MT_A_ASB.ToString()] as MacMcMaskTransfer;
            var OSMC = MachineMgr.CtrlMachines[EnumMachineID.MID_OS_A_ASB.ToString()] as MacMcOpenStage;
            var BTMC = MachineMgr.CtrlMachines[EnumMachineID.MID_BT_A_ASB.ToString()] as MacMcBoxTransfer;
            var CBMC = MachineMgr.CtrlMachines[EnumMachineID.MID_DRAWER_01_01.ToString()] as MacMcCabinetDrawer;

            var LPAMS = LPAMC.StateMachine;
            var LPBMS = LPBMC.StateMachine;
            var MTMS = MTMC.StateMachine;
            var OSMS = OSMC.StateMachine;
            var BTMS = BTMC.StateMachine;
            var CBMS = CBMC.StateMachine;


            var LPA_Task = Task.Factory.StartNew(() => { LPAMS.SystemBootup(); });
            var LPB_Task = Task.Factory.StartNew(() => { LPBMS.SystemBootup(); });
            var MT_Task = Task.Factory.StartNew(() => { MTMS.SystemBootup(); });
            var OS_Task = Task.Factory.StartNew(() => { OSMS.SystemBootup(); OSMS.InputBox(); });
            var BT_Task = Task.Factory.StartNew(() => { BTMS.SystemBootup(); });
            var CB_Task = Task.Factory.StartNew(() => { CBMS.SystemBootup(); CBMS.Unload_MoveTrayToIn(); });

            Task.WaitAll(BT_Task, CB_Task);
            BTMS.MoveToCabinetGet("0101");
            Task.WaitAll(OS_Task);
            BTMS.MoveToOpenStagePut();
            OSMS.CalibrationClosedBox();
            BTMS.MoveToUnlock();
            OS_Task = Task.Factory.StartNew(() => { OSMS.OpenBox(); });
            Task.WaitAll(LPA_Task, LPB_Task, MT_Task);
            MTMS.LPHomeToLPAGetMaskReturnToLPHomeClamped();
            Task.WaitAll(OS_Task);
            MTMS.LPHomeClampedToOSReleaseMaskReturnToLPHome();
            OSMS.CloseBoxWithMask();
            BTMS.MoveToLock();
            OSMS.ReleaseBoxWithMask();
            BTMS.MoveToOpenStageGet();
            OS_Task = Task.Factory.StartNew(() => { OSMS.ReturnToIdleAfterReleaseBoxWithMask(); });
            BTMS.MoveToCabinetPut("0101");
        }
        [TestMethod]
        public void BankOut()
        {
            var MachineMgr = new MacMachineMgr();
            MachineMgr.MvCfInit();
            var LPAMC = MachineMgr.CtrlMachines[EnumMachineID.MID_LP_A_ASB.ToString()] as MacMcLoadPort;
            var LPBMC = MachineMgr.CtrlMachines[EnumMachineID.MID_LP_B_ASB.ToString()] as MacMcLoadPort;
            var MTMC = MachineMgr.CtrlMachines[EnumMachineID.MID_MT_A_ASB.ToString()] as MacMcMaskTransfer;
            var OSMC = MachineMgr.CtrlMachines[EnumMachineID.MID_OS_A_ASB.ToString()] as MacMcOpenStage;
            var BTMC = MachineMgr.CtrlMachines[EnumMachineID.MID_BT_A_ASB.ToString()] as MacMcBoxTransfer;
            var CBMC = MachineMgr.CtrlMachines[EnumMachineID.MID_DRAWER_01_01.ToString()] as MacMcCabinetDrawer;
            var ICMC = MachineMgr.CtrlMachines[EnumMachineID.MID_IC_A_ASB.ToString()] as MacMcInspectionCh;
            var CCMC = MachineMgr.CtrlMachines[EnumMachineID.MID_CC_A_ASB.ToString()] as MacMcCleanCh;

            var LPAMS = LPAMC.StateMachine;
            var LPBMS = LPBMC.StateMachine;
            var MTMS = MTMC.StateMachine;
            var OSMS = OSMC.StateMachine;
            var BTMS = BTMC.StateMachine;
            var CBMS = CBMC.StateMachine;
            var ICMS = ICMC.StateMachine;
            var CCMS = CCMC.StateMachine;

            var LPA_Task = Task.Factory.StartNew(() => { LPAMS.SystemBootup(); });
            var LPB_Task = Task.Factory.StartNew(() => { LPBMS.SystemBootup(); });
            var MT_Task = Task.Factory.StartNew(() => { MTMS.SystemBootup(); });
            var OS_Task = Task.Factory.StartNew(() => { OSMS.SystemBootup(); OSMS.InputBox(); });
            var BT_Task = Task.Factory.StartNew(() => { BTMS.SystemBootup(); });
            var CB_Task = Task.Factory.StartNew(() => { CBMS.SystemBootup(); CBMS.Unload_MoveTrayToIn(); });
            var IC_Task = Task.Factory.StartNew(() => { ICMS.SystemBootup(); });
            var CC_Task = Task.Factory.StartNew(() => { CCMS.SystemBootup(); });

            Task.WaitAll(BT_Task, CB_Task);
            BTMS.MoveToCabinetGet("0101");
            Task.WaitAll(OS_Task);
            BTMS.MoveToOpenStagePut();
            OSMS.CalibrationClosedBoxWithMask();
            BTMS.MoveToUnlock();
            OSMS.OpenBoxWithMask();
            Task.WaitAll(LPA_Task, LPB_Task, MT_Task);
            MTMS.LPHomeToOSGetMaskReturnToLPHomeClamped();
            MTMS.LPHomeClampedToICHomeClamped();
            //To IC Inspect Glass
            MTMS.ICHomeClampedToICGlassReleaseReturnToICHome();
            ICMS.InspectGlass();
            MTMS.ICHomeToICGlassGetReturnToICClamped();
            //To IC Inspect Pellicle
            MTMS.ICHomeClampedToICReleaseReturnToICHome();
            ICMS.InspectPellicle();
            MTMS.ICHomeToICGetReturnToICClamped();

            MTMS.ICHomeClampedToICHomeInspected();
            MTMS.ICHomeInspectedToCCHomeClamped();
            //To CC Clean Glass
            MTMS.CCHomeClampedToCCGlass();
            MTMS.InCCGlassMoveToClean();
            MT_Task = Task.Factory.StartNew(() => { MTMS.CleanGlass(); });
            CCMS.CleanGlass();
            Task.WaitAll(MT_Task);
            CCMS.FinishCleanGlass();
            MTMS.CCGlassCleanedReturnInCCGlass();
            MTMS.InCCGlassMoveToInspect();
            MT_Task = Task.Factory.StartNew(() => { MTMS.InspectGlass(); });
            CCMS.InspectGlass();
            Task.WaitAll(MT_Task);
            CCMS.FinishInspectGlass();
            MTMS.CCGlassInspectedReturnInCCGlass();
            MTMS.InCCGlassToCCHomeClamped();
            //To CC Clean Pellicle
            MTMS.CCHomeClampedToCC();
            MTMS.InCCMoveToClean();
            MT_Task = Task.Factory.StartNew(() => { MTMS.CleanPellicle(); });
            CCMS.CleanPellicle();
            Task.WaitAll(MT_Task);
            CCMS.FinishCleanPellicle();
            MTMS.CCCleanedReturnInCC();
            MTMS.InCCMoveToInspect();
            MT_Task = Task.Factory.StartNew(() => { MTMS.InspectPellicle(); });
            CCMS.InspectPellicle();
            Task.WaitAll(MT_Task);
            CCMS.FinishInspectPellicle();
            MTMS.CCInspectedReturnInCC();
            MTMS.InCCToCCHomeClamped();

            MT_Task = Task.Factory.StartNew(() =>
            {
                  MTMS.CCHomeClampedToCCHomeCleaned();
                  MTMS.CCHomeCleanedToLPHomeCleaned();

                  MTMS.LPHomeCleanedToLPARelease();
            });

            OSMS.CloseBox();
            BTMS.MoveToLock();
            OSMS.ReleaseBox();
            BTMS.MoveToOpenStageGet();
            OS_Task = Task.Factory.StartNew(() => { OSMS.ReturnToIdleAfterReleaseBox(); });
            BTMS.MoveToCabinetPut("0101");
        }
    }
}
