using System;
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
using MvAssistant.Mac.v1_0;
using MvAssistant.Mac.v1_0.JSon.RobotTransferFile;

namespace MaskAutoCleaner.v1_0.TestMy.Machine
{
    [TestClass]
    public class UtScenario
    {
        [TestMethod]
        public void MTMoveToLPAGetAndPut()
        {
            var MachineMgr = new MacMachineMgr();
            MachineMgr.MvCfInit();
            var LPAMC = MachineMgr.CtrlMachines[EnumMachineID.MID_LP_A_ASB.ToString()] as MacMcLoadPort;
            var LPBMC = MachineMgr.CtrlMachines[EnumMachineID.MID_LP_B_ASB.ToString()] as MacMcLoadPort;
            var MTMC = MachineMgr.CtrlMachines[EnumMachineID.MID_MT_A_ASB.ToString()] as MacMcMaskTransfer;
            var LPAMS = LPAMC.StateMachine;
            var LPBMS = LPBMC.StateMachine;
            var MTMS = MTMC.StateMachine;

            var LPA_Task = Task.Factory.StartNew(() => { LPAMS.SystemBootup(); });
            var LPB_Task = Task.Factory.StartNew(() => { LPBMS.SystemBootup(); });
            var MT_Task = Task.Factory.StartNew(() => { MTMS.SystemBootup(); });

            Task.WaitAll(LPA_Task, LPB_Task, MT_Task);
            LPAMS.Dock();
            MTMS.LPHomeToLPAGetMaskReturnToLPHomeClamped();
            MTMS.LPHomeInspectedToLPARelease();
            //MTMS.LPHomeCleanedToLPARelease();
            LPAMS.Undock();
        }
        [TestMethod]
        public void MTMoveToLPBGetAndPut()
        {
            var MachineMgr = new MacMachineMgr();
            MachineMgr.MvCfInit();
            var LPAMC = MachineMgr.CtrlMachines[EnumMachineID.MID_LP_A_ASB.ToString()] as MacMcLoadPort;
            var LPBMC = MachineMgr.CtrlMachines[EnumMachineID.MID_LP_B_ASB.ToString()] as MacMcLoadPort;
            var MTMC = MachineMgr.CtrlMachines[EnumMachineID.MID_MT_A_ASB.ToString()] as MacMcMaskTransfer;
            var LPAMS = LPAMC.StateMachine;
            var LPBMS = LPBMC.StateMachine;
            var MTMS = MTMC.StateMachine;

            var LPA_Task = Task.Factory.StartNew(() => { LPAMS.SystemBootup(); });
            var LPB_Task = Task.Factory.StartNew(() => { LPBMS.SystemBootup(); });
            var MT_Task = Task.Factory.StartNew(() => { MTMS.SystemBootup(); });

            Task.WaitAll(LPA_Task, LPB_Task, MT_Task);
            LPBMS.Dock();
            MTMS.LPHomeToLPBGetMaskReturnToLPHomeClamped();
            MTMS.LPHomeInspectedToLPBRelease();
            //MTMS.LPHomeCleanedToLPBRelease();
            LPBMS.Undock();
        }
        [TestMethod]
        public void MTMoveToICForInspectMask()
        {
            var MachineMgr = new MacMachineMgr();
            MachineMgr.MvCfInit();
            var MTMC = MachineMgr.CtrlMachines[EnumMachineID.MID_MT_A_ASB.ToString()] as MacMcMaskTransfer;
            var ICMC = MachineMgr.CtrlMachines[EnumMachineID.MID_IC_A_ASB.ToString()] as MacMcInspectionCh;
            var ICMS = ICMC.StateMachine;
            var MTMS = MTMC.StateMachine;

            var IC_Task = Task.Factory.StartNew(() => { ICMS.SystemBootup(); });
            var MT_Task = Task.Factory.StartNew(() => { MTMS.SystemBootup(); });

            Task.WaitAll(IC_Task, MT_Task);
            MTMS.LPHomeClampedToICHomeClamped();
            //Mask進入IC檢測Glass面
            MTMS.ICHomeClampedToICGlassReleaseReturnToICHome();
            ICMS.InspectGlass();
            MTMS.ICHomeToICGlassGetReturnToICClamped();
            ICMS.ReturnToIdleAfterReleaseGlass();

            //Mask進入IC檢測Pellicle面
            MTMS.ICHomeClampedToICPellicleReleaseReturnToICHome();
            ICMS.InspectPellicle();
            MTMS.ICHomeToICPellicleGetReturnToICClamped();
            ICMS.ReturnToIdleAfterReleasePellicle();
            MTMS.ICHomeClampedToICHomeInspected();
        }
        [TestMethod]
        public void MTMoveToCCForCleanMask()
        {
            var MachineMgr = new MacMachineMgr();
            MachineMgr.MvCfInit();
            var MTMC = MachineMgr.CtrlMachines[EnumMachineID.MID_MT_A_ASB.ToString()] as MacMcMaskTransfer;
            var CCMC = MachineMgr.CtrlMachines[EnumMachineID.MID_CC_A_ASB.ToString()] as MacMcCleanCh;
            var CCMS = CCMC.StateMachine;
            var MTMS = MTMC.StateMachine;

            var CC_Task = Task.Factory.StartNew(() => { CCMS.SystemBootup(); });
            var MT_Task = Task.Factory.StartNew(() => { MTMS.SystemBootup(); });

            Task.WaitAll(CC_Task, MT_Task);
            MTMS.ICHomeInspectedToCCHomeClamped();
            //清理Glass面並進行檢測
            MTMS.CCHomeClampedToCCGlass();
            MTMS.InCCGlassMoveToClean();
            MT_Task = Task.Factory.StartNew(() => MTMS.CleanGlass());
            CC_Task = Task.Factory.StartNew(() => CCMS.CleanGlass());
            Task.WaitAll(MT_Task);
            CC_Task = Task.Factory.StartNew(() => CCMS.FinishCleanGlass());
            MTMS.CCGlassCleanedReturnInCCGlass();
            MTMS.InCCGlassMoveToInspect();
            MT_Task = Task.Factory.StartNew(() => MTMS.InspectGlass());
            CC_Task = Task.Factory.StartNew(() => CCMS.InspectGlass());
            Task.WaitAll(MT_Task);
            CC_Task = Task.Factory.StartNew(() => CCMS.FinishInspectGlass());
            MTMS.CCGlassInspectedReturnInCCGlass();
            MTMS.InCCGlassToCCHomeClamped();

            //清理Pellicle面並進行檢測
            MTMS.CCHomeClampedToCCPellicle();
            MTMS.InCCPellicleMoveToClean();
            MT_Task = Task.Factory.StartNew(() => MTMS.CleanPellicle());
            CC_Task = Task.Factory.StartNew(() => CCMS.CleanPellicle());
            Task.WaitAll(MT_Task);
            CC_Task = Task.Factory.StartNew(() => CCMS.FinishCleanPellicle());
            MTMS.CCPellicleCleanedReturnInCCPellicle();
            MTMS.InCCPellicleMoveToInspect();
            MT_Task = Task.Factory.StartNew(() => MTMS.InspectPellicle());
            CC_Task = Task.Factory.StartNew(() => CCMS.InspectPellicle());
            Task.WaitAll(MT_Task);
            CC_Task = Task.Factory.StartNew(() => CCMS.FinishInspectPellicle());
            MTMS.CCPellicleInspectedReturnInCCPellicle();
            MTMS.InCCPellicleToCCHomeClamped();

            MTMS.CCHomeClampedToCCHomeCleaned();
        }
        [TestMethod]
        public void MTMoveToOSGetAndPut()
        {
            var MachineMgr = new MacMachineMgr();
            MachineMgr.MvCfInit();
            var MTMC = MachineMgr.CtrlMachines[EnumMachineID.MID_MT_A_ASB.ToString()] as MacMcMaskTransfer;
            var OSMC = MachineMgr.CtrlMachines[EnumMachineID.MID_OS_A_ASB.ToString()] as MacMcOpenStage;
            var BTMC = MachineMgr.CtrlMachines[EnumMachineID.MID_BT_A_ASB.ToString()] as MacMcBoxTransfer;
            var OSMS = OSMC.StateMachine;
            var MTMS = MTMC.StateMachine;
            var BTMS = BTMC.StateMachine;

            var OS_Task = Task.Factory.StartNew(() => { OSMS.SystemBootup(); });
            var MT_Task = Task.Factory.StartNew(() => { MTMS.SystemBootup(); });
            var BT_Task = Task.Factory.StartNew(() => { BTMS.SystemBootup(); });

            Task.WaitAll(OS_Task, MT_Task, BT_Task);
            //將空盒放上OS開盒，由MT放入Mask並關盒由BT取走盒子
            OSMS.InputBox();
            OSMS.CalibrationClosedBox();
            BTMS.MoveToUnlock(BoxType.IronBox);// TODO: 暫時為 鐵盒
            OSMS.OpenBox();
            MTMS.LPHomeClampedToOSReleaseMaskReturnToLPHome();
            OSMS.CloseBoxWithMask();
            BTMS.MoveToLock(BoxType.IronBox); // TODO: 暫時為 鐵盒
            OSMS.ReleaseBoxWithMask();
            BTMS.MoveToOpenStageGet();
            OSMS.ReturnToIdleAfterReleaseBoxWithMask();

            //將盒子放上OS開盒，由MT取出Mask並關盒由BT取走空盒
            OSMS.InputBoxWithMask();
            OSMS.CalibrationClosedBoxWithMask();
            BTMS.MoveToUnlock(BoxType.IronBox); // TODO: 暫時為 鐵盒
            OSMS.OpenBox();
            MTMS.LPHomeToOSGetMaskReturnToLPHomeClamped();
            OSMS.CloseBox();
            BTMS.MoveToLock(BoxType.IronBox); // TODO: 暫時為 鐵盒
            OSMS.ReleaseBox();
            BTMS.MoveToOpenStageGet();
            OSMS.ReturnToIdleAfterReleaseBox();
        }

        [TestMethod]
        [DataRow(BoxrobotTransferLocation.Drawer_01_01)]
        //public void BTMoveToCBGetAndPut()
        public void BTMoveToCBGetAndPut(BoxrobotTransferLocation drawerLocation)
        {
            var MachineMgr = new MacMachineMgr();
            MachineMgr.MvCfInit();
            var BTMC = MachineMgr.CtrlMachines[EnumMachineID.MID_BT_A_ASB.ToString()] as MacMcBoxTransfer;
            var CBMC = MachineMgr.CtrlMachines[EnumMachineID.MID_DRAWER_01_01.ToString()] as MacMcCabinetDrawer;
            var BTMS = BTMC.StateMachine;
            var CBMS = CBMC.StateMachine;

            var BT_Task = Task.Factory.StartNew(() => { BTMS.SystemBootup(); });
            var CB_Task = Task.Factory.StartNew(() => { CBMS.SystemBootup(); });

            Task.WaitAll(CB_Task, BT_Task);
            //放入盒子到Cabinet_01_01
            CBMS.Load_MoveTrayToIn();

            //BTMS.MoveToCabinetPut("0101");
            BTMS.MoveToCabinetPut(drawerLocation);

            CBMS.Load_MoveTrayToHome();

            //從Cabinet_01_01取出盒子
            CBMS.Unload_MoveTrayToIn();

            //BTMS.MoveToCabinetGet("0101");
            BTMS.MoveToCabinetGet(drawerLocation);

            CBMS.Unload_MoveTrayToHome();
        }
    }
}
