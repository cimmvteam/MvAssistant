using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MaskAutoCleaner.v1_0.Machine;
using MaskAutoCleaner.v1_0.Machine.BoxTransfer;
using MaskAutoCleaner.v1_0.Machine.CleanCh;
using MaskAutoCleaner.v1_0.Machine.InspectionCh;
using MaskAutoCleaner.v1_0.Machine.LoadPort;
using MaskAutoCleaner.v1_0.Machine.MaskTransfer;
using MaskAutoCleaner.v1_0.UserData;
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
            MTMS.ICHomeClampedToICGlassReleaseReturnToICHome();
            ICMS.InspectGlass();
            MTMS.ICHomeToICGlassGetReturnToICClamped();
            MTMS.ICHomeClampedToICReleaseReturnToICHome();
            ICMS.InspectPellicle();
            MTMS.ICHomeToICGetReturnToICClamped();
            MTMS.ICHomeClampedToICHomeInspected();
            MTMS.ICHomeInspectedToCCHomeClamped();
            MTMS.CCHomeClampedToCCGlass();
            MTMS.InCCGlassMoveToClean();

        }
        [TestMethod]
        public void BankIn()
        {
            var MachineMgr = new MacMachineMgr();
            MachineMgr.MvCfInit();
            var BTMC = MachineMgr.CtrlMachines[EnumMachineID.MID_BT_A_ASB.ToString()] as MacMcBoxTransfer;
            var BTMS = BTMC.StateMachine;
        }
    }
}
