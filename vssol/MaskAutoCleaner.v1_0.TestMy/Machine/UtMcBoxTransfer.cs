using System;
using MaskAutoCleaner.v1_0.Machine;
using MaskAutoCleaner.v1_0.Machine.BoxTransfer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0;
using MvAssistant.Mac.v1_0.JSon.RobotTransferFile;

namespace MaskAutoCleaner.v1_0.TestMy.Machine
{
    [TestClass]
    public class UtMcBoxTransfer
    {
        MacMachineMgr MachineMgr;
        MacMcBoxTransfer MachineCtrl;
        MacMsBoxTransfer StateMachines;
        public UtMcBoxTransfer()
        {
            MachineMgr = new MacMachineMgr();
            MachineMgr.MvCfInit();
            MachineCtrl = MachineMgr.CtrlMachines[EnumMachineID.MID_BT_A_ASB.ToString()] as MacMcBoxTransfer;
            StateMachines= MachineCtrl.StateMachine;
        }

        [TestMethod]
        [DataRow(BoxType.IronBox)]
       // [DataRow(BoxType.CrystalBox)]
        public void TestMethod1(BoxType boxType)
        {


            StateMachines.SystemBootup();
            StateMachines.MoveToLock(boxType);  // Fake OK
            StateMachines.MoveToUnlock(boxType);  // Fake OK
            /**
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
                MS.MoveToLock(boxType);  // Fake OK
                MS.MoveToUnlock(boxType);  // Fake OK
            }
    */
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
            StateMachines.Initial();
            StateMachines.MoveToCabinetGet(drawerNumber); // Fake OK
            StateMachines.MoveToOpenStagePut(); // Fake OK
            /**
            var MachineMgr = new MacMachineMgr();
            MachineMgr.MvCfInit();
            var MachineCtrl = MachineMgr.CtrlMachines[EnumMachineID.MID_BT_A_ASB.ToString()] as MacMcBoxTransfer;
            var MS = MachineCtrl.StateMachine;
            MS.Initial();

            MS.MoveToCabinetGet(drawerNumber); // Fake OK
            MS.MoveToOpenStagePut(); // Fake OK
           */
        }

        [TestMethod]
        //[DataRow(BoxrobotTransferLocation.Drawer_01_01)]
        [DataRow(BoxrobotTransferLocation.Drawer_07_01)]
        public void TestMethod_BankIn(BoxrobotTransferLocation drawerNumber)
        {
            StateMachines.Initial();
            StateMachines.MoveToOpenStageGet();   // Fake OK
            StateMachines.MoveToCabinetPut(drawerNumber);  // Fake OK
            /**
            var MachineMgr = new MacMachineMgr();
            MachineMgr.MvCfInit();
            var MachineCtrl = MachineMgr.CtrlMachines[EnumMachineID.MID_BT_A_ASB.ToString()] as MacMcBoxTransfer;
            var MS = MachineCtrl.StateMachine;
            MS.Initial();
           
            MS.MoveToOpenStageGet();   // Fake OK
            MS.MoveToCabinetPut(drawerNumber);  // Fake OK
       */
        }

    }
}
