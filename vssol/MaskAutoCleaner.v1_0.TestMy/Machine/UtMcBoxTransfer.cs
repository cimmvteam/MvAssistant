using System;
using MaskAutoCleaner.v1_0.Machine;
using MaskAutoCleaner.v1_0.Machine.BoxTransfer;
using MaskAutoCleaner.v1_0.UserData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0.JSon.RobotTransferFile;

namespace MaskAutoCleaner.v1_0.TestMy.Machine
{
    [TestClass]
    public class UtMcBoxTransfer
    {
        [TestMethod]
        public void TestMethod1()
        {
            var MachineMgr = new MacMachineMgr();
            MachineMgr.MvCfInit();
            var MachineCtrl = MachineMgr.CtrlMachines[EnumMachineID.MID_BT_A_ASB.ToString()] as MacMcBoxTransfer;
            var MS = MachineCtrl.StateMachine;

            MS.SystemBootup();
            bool BankIn = true;
            bool BankOut = false;
            if (BankIn)
            {
                MS.MoveToOpenStageGet();
                MS.MoveToCabinetPut("0101");
            }
            else if (BankOut)
            {
                MS.MoveToCabinetGet("0101");
                MS.MoveToOpenStagePut();
            }
            else
            {
                MS.MoveToLock();
                MS.MoveToUnlock();
            }
        }



        [TestMethod]
        [DataRow(BoxrobotTransferLocation.Drawer_01_01)]
        //[DataRow(BoxrobotTransferLocation.Drawer_07_01)]
        public void TestMethod_BankOut(BoxrobotTransferLocation drawerNumber)
        {
            var MachineMgr = new MacMachineMgr();
            MachineMgr.MvCfInit();
            var MachineCtrl = MachineMgr.CtrlMachines[EnumMachineID.MID_BT_A_ASB.ToString()] as MacMcBoxTransfer;
            var MS = MachineCtrl.StateMachine;
            MS.Initial();
            MS.MoveToCabinetGet(drawerNumber);
            MS.MoveToOpenStagePut();

        }

        [TestMethod]
        [DataRow(BoxrobotTransferLocation.Drawer_01_01)]
        //[DataRow(BoxrobotTransferLocation.Drawer_07_01)]
        public void TestMethod_BankIn(BoxrobotTransferLocation drawerNumber)
        {
            var MachineMgr = new MacMachineMgr();
            MachineMgr.MvCfInit();
            var MachineCtrl = MachineMgr.CtrlMachines[EnumMachineID.MID_BT_A_ASB.ToString()] as MacMcBoxTransfer;
            var MS = MachineCtrl.StateMachine;
            MS.Initial();
           
            MS.MoveToOpenStageGet();
            MS.MoveToCabinetPut(drawerNumber);

        }

    }
}
