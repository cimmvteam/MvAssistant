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
        [DataRow(true)]
        public void Test_SystemBootUp(bool mainTest)
        {
            var method = typeof(MacMsBoxTransfer).GetMethod(EnumMacMcBoxTransferCmd.SystemBootup.ToString());
            method.Invoke(StateMachine, null);
            if (mainTest)
            {
                Repeat();
            }
        }


        [TestMethod]
        [DataRow(BoxType.CrystalBox,true)]
        [DataRow(BoxType.IronBox,true)]
        public void Test_MoveToLock(BoxType boxType,bool mainTest)
        {
            Test_Initial(mainTest);
            var method = typeof(MacMsBoxTransfer).GetMethod(EnumMacMcBoxTransferCmd.MoveToLock.ToString());
            method.Invoke(StateMachine, new object[] { boxType });
            if (mainTest)
            { Repeat(); }
        }

        [TestMethod]
        [DataRow(BoxType.CrystalBox)]
        //[DataRow(BoxType.IronBox)]
        public void Test_MoveToUnLock(BoxType boxType)
        {
            Test_Initial(false);
            var method = typeof(MacMsBoxTransfer).GetMethod(EnumMacMcBoxTransferCmd.MoveToUnlock.ToString());
            method.Invoke(StateMachine, new object[] { boxType });
            Repeat();
        }


        [TestMethod]
        [DataRow(true)]
        public void Test_Initial(bool mainTest)
        {
            var method = typeof(MacMsBoxTransfer).GetMethod(EnumMacMcBoxTransferCmd.Initial.ToString());
            method.Invoke(StateMachine, null);
            if (mainTest)
            {
                Repeat();
            }
        }

        [TestMethod]
        [DataRow(BoxrobotTransferLocation.Drawer_01_01, BoxType.IronBox,true)]
        //[DataRow(BoxrobotTransferLocation.Drawer_04_02, BoxType.CrystalBox,true)]
        public void Test_MoveToCabinetGet(BoxrobotTransferLocation drawerNumber, BoxType boxType,bool mainTest)
        {
            Test_Initial(mainTest);
            var method = typeof(MacMsBoxTransfer).GetMethod(EnumMacMcBoxTransferCmd.MoveToCabinetGet.ToString());
            method.Invoke(StateMachine, new object[] { drawerNumber, boxType });
            if (mainTest)
            {
                Repeat();
            }
        }

        [TestMethod]
        [DataRow(BoxrobotTransferLocation.Drawer_01_01, BoxType.IronBox, true)]
        //[DataRow(BoxrobotTransferLocation.Drawer_04_02, BoxType.CrystalBox,true)]
        public void Test_MoveToOpenStagePut(BoxrobotTransferLocation drawerNumber, BoxType boxType,bool mainTest)
        {
            Test_MoveToCabinetGet(drawerNumber, boxType, false);
            var method = typeof(MacMsBoxTransfer).GetMethod(EnumMacMcBoxTransferCmd.MoveToOpenStagePut.ToString());
            method.Invoke(StateMachine,null);
            if (mainTest)
            {
                Repeat();
            }
        }

        [TestMethod]
        [DataRow(BoxType.CrystalBox,true)]
        //[DataRow(BoxType.IronBox, true)]
        public void Test_MoveToOpenStageGet(BoxType boxType, bool mainTest)
        {
            Test_Initial(false);
            var method = typeof(MacMsBoxTransfer).GetMethod(EnumMacMcBoxTransferCmd.MoveToOpenStageGet.ToString());
            method.Invoke(StateMachine, new object[] { boxType});
            if (mainTest)
            {
                Repeat();
            }

        }


        [TestMethod]
        //[DataRow(BoxrobotTransferLocation.Drawer_01_01)]
        [DataRow(BoxrobotTransferLocation.Drawer_07_01)]
        public void Test_Method_BankIn(BoxrobotTransferLocation drawerNumber)
        {
            StateMachine.Initial();
            StateMachine.MoveToOpenStageGet(BoxType.CrystalBox);   // Fake OK
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
