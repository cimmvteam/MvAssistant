using System;
using MaskAutoCleaner.v1_0.Machine;
using MaskAutoCleaner.v1_0.Machine.BoxTransfer;
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
            bool BankIn = false;
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
                MS.MoveToLock();  // Fake OK
                MS.MoveToUnlock();  // Fake OK
            }
        }



        /// <summary></summary>
        /// <remarks>
        /// <para>2020/10/30 State Machine Fake Test</para>
        /// /// </remarks>
        /// <param name="drawerNumber"></param>
        [TestMethod]
        //[DataRow(BoxrobotTransferLocation.Drawer_01_01)]
        [DataRow(BoxrobotTransferLocation.Drawer_07_01)]
        public void TestMethod_BankOut(BoxrobotTransferLocation drawerNumber)
        {
           
            var MachineMgr = new MacMachineMgr();
            MachineMgr.MvCfInit();
            var MachineCtrl = MachineMgr.CtrlMachines[EnumMachineID.MID_BT_A_ASB.ToString()] as MacMcBoxTransfer;
            var MS = MachineCtrl.StateMachine;
            MS.Initial();

            MS.MoveToCabinetGet(drawerNumber); // Fake OK
            MS.MoveToOpenStagePut(); // Fake OK

        }

        [TestMethod]
        //[DataRow(BoxrobotTransferLocation.Drawer_01_01)]
        [DataRow(BoxrobotTransferLocation.Drawer_07_01)]
        public void TestMethod_BankIn(BoxrobotTransferLocation drawerNumber)
        {
            var MachineMgr = new MacMachineMgr();
            MachineMgr.MvCfInit();
            var MachineCtrl = MachineMgr.CtrlMachines[EnumMachineID.MID_BT_A_ASB.ToString()] as MacMcBoxTransfer;
            var MS = MachineCtrl.StateMachine;
            MS.Initial();
           
            MS.MoveToOpenStageGet();   // Fake OK
            MS.MoveToCabinetPut(drawerNumber);  // Fake OK

        }

    }
}
