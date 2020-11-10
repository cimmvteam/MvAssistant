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
        MacMsBoxTransfer StateMachine;
        void Repeat()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(100);
            }
        }
        public UtMcBoxTransfer()
        {
            MachineMgr = new MacMachineMgr();
            MachineMgr.MvCfInit();
            MachineCtrl = MachineMgr.CtrlMachines[EnumMachineID.MID_BT_A_ASB.ToString()] as MacMcBoxTransfer;
            StateMachine= MachineCtrl.StateMachine;
        }


        [TestMethod]
        public void Test_SystemBootUp()
        {
            var method = typeof(MacMsBoxTransfer).GetMethod(EnumMacMcBoxTransferCmd.SystemBootup.ToString());
            method.Invoke(StateMachine, null);

            Repeat();
        }


        [TestMethod]
       // [DataRow(BoxType.CrystalBox)]
        [DataRow(BoxType.IronBox)]
        public void Test_MoveToLock(BoxType boxType)
        {
            var method = typeof(MacMsBoxTransfer).GetMethod(EnumMacMcBoxTransferCmd.MoveToLock.ToString());
            method.Invoke(StateMachine, new object[] { boxType });

            Repeat();
        }

        [TestMethod]
        [DataRow(BoxType.CrystalBox)]
        //[DataRow(BoxType.IronBox)]
        public void Test_MoveToUnLock(BoxType boxType)
        {
            var method = typeof(MacMsBoxTransfer).GetMethod(EnumMacMcBoxTransferCmd.MoveToUnlock.ToString());
            method.Invoke(StateMachine, new object[] { boxType });
            Repeat();
        }


        /**
        [TestMethod]
        [DataRow(BoxType.IronBox)]
       // [DataRow(BoxType.CrystalBox)]
        public void Test_Method1(BoxType boxType)
        {


            StateMachine.SystemBootup();
            StateMachine.MoveToLock(boxType);  // Fake OK
            StateMachine.MoveToUnlock(boxType);  // Fake OK
        }
        */



        [TestMethod]
        [DataRow(BoxrobotTransferLocation.Drawer_01_01,BoxType.IronBox)]
       // [DataRow(BoxrobotTransferLocation.Drawer_04_01,BoxType.CrystalBox)]
        public void Test_BankOut(BoxrobotTransferLocation drawerNumber, BoxType boxType)
        {   
            var method = typeof(MacMsBoxTransfer).GetMethod(EnumMacMcBoxTransferCmd.Initial.ToString());
            method.Invoke(StateMachine,null);

            

            method = typeof(MacMsBoxTransfer).GetMethod(EnumMacMcBoxTransferCmd.BankOut.ToString());
            method.Invoke(StateMachine, new object[] { drawerNumber, boxType });

        }

        /// <summary></summary>
        /// <remarks>
        /// <para>2020/10/30 State Machine Fake Test</para>
        /// /// </remarks>
        /// <param name="drawerNumber"></param>
        [TestMethod]
        //[DataRow(BoxrobotTransferLocation.Drawer_01_01)]
        [DataRow(BoxrobotTransferLocation.Drawer_07_01,BoxType.IronBox)]
        public void Test_BankOutP(BoxrobotTransferLocation drawerNumber,BoxType boxType)
        {
            StateMachine.Initial();
            StateMachine.MoveToCabinetGet(drawerNumber, boxType); // Fake OK


            StateMachine.MoveToOpenStagePut(); // Fake OK
           
        }

        [TestMethod]
        //[DataRow(BoxrobotTransferLocation.Drawer_01_01)]
        [DataRow(BoxrobotTransferLocation.Drawer_07_01)]
        public void Test_Method_BankIn(BoxrobotTransferLocation drawerNumber)
        {
            StateMachine.Initial();
            StateMachine.MoveToOpenStageGet();   // Fake OK
            StateMachine.MoveToCabinetPut(drawerNumber);  // Fake OK
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
