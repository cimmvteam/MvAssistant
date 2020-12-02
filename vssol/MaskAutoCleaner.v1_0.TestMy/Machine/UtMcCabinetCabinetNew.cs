using MaskAutoCleaner.v1_0.Machine;
using MaskAutoCleaner.v1_0.Machine.Cabinet;
using MaskAutoCleaner.v1_0.Machine.Cabinet.DrawerQueues;
using MaskAutoCleaner.v1_0.Machine.Cabinet.DrawerStatus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.TestMy.Machine
{
    [TestClass]
    public  class UtMcCabinetCabinetNew
    {
        MacMsCabinet Machine = default(MacMsCabinet);
        public UtMcCabinetCabinetNew()
        {
            var MachineMgr = new MacMachineMgr();
            MachineMgr.MvCfInit();
            var MachineCtrl = MachineMgr.CtrlMachines[EnumMachineID.MID_CB_A_ASB.ToString()] as MacMcCabinet;
            Machine = MachineCtrl.StateMachine;
        }

        [TestMethod]
        public void Test_CreateInstance()
        {
            var s = Machine.GetDicMacHalDrawers();

            DrawerSatusInfo drawerInfo = new DrawerSatusInfo("boxBarcode",EnumMachineID.MID_DRAWER_01_02,BoxType.CrystalBox);
            Machine.Mediater.CabinetMediater.EnqueueBankOutDrawerInfo(drawerInfo);
            var peek = Machine.Mediater.CabinetMediater.PeekBankOut(out drawerInfo);

        }


        [TestMethod]
        public void Test_SystemBootup()
        {
            var method = typeof(MacMsCabinet).GetMethod(EnumMacMcCabinetCmd.SystemBootup.ToString());
            method.Invoke(Machine, null);
        }

        [TestMethod]
        [DataRow(3)]
        public void Test_BankOutLoadMoveTraysToOut(int drawerCounts)
        {
            var method = typeof(MacMsCabinet).GetMethod(EnumMacMcCabinetCmd.BankOutLoadMoveTraysToOutForPutBoxOnTray.ToString());
            method.Invoke(Machine, new object[] { drawerCounts});
        }

        /// <summary>Bankout, LO 將盒子</summary>
        /// <param name="deviceID"></param>
        [TestMethod]
        [DataRow(new MacEnumDevice[] { MacEnumDevice.cabinet_drawer_01_02, MacEnumDevice.cabinet_drawer_01_03 }) ]
        public void Test_BankOutLoadMoveTraysToHomeAfterPutBoxOnTray(MacEnumDevice[] deviceIDs)
        {
            var list = deviceIDs.ToList();
            var drawers = Machine.GetDicMacHalDrawers();
            foreach (var l in list)
            {
                var v = drawers[l.ToBoxrobotTransferLocation()];
                v.SetDuration(DrawerDuration.BankOut_Load_TrayAtOutForPutBoxOnTray);
            }
           var method = typeof(MacMsCabinet).GetMethod(EnumMacMcCabinetCmd.BankOutLoadMoveTraysToHomeAfterPutBoxOnTray.ToString());
            method.Invoke(Machine, new object[] { list });
            Repeat();
        }

        void Repeat()
        {
            while(true)
            {
                Thread.Sleep(100);
            }
        }
    }
}
