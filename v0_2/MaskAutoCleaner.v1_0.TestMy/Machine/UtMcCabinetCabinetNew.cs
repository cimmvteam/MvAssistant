using MaskAutoCleaner.v1_0.Machine;
using MaskAutoCleaner.v1_0.Machine.Cabinet;
using MaskAutoCleaner.v1_0.Machine.Cabinet.DrawerQueues;
using MaskAutoCleaner.v1_0.Machine.Cabinet.DrawerStatus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.v0_2.Mac;
using MvAssistant.v0_2.Mac.JSon.RobotTransferFile;
using MvAssistant.v0_2.Mac.Manifest;
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
        MacMsCabinet0 Machine = default(MacMsCabinet0);
        public UtMcCabinetCabinetNew()
        {
            var MachineMgr = new MacMachineMgr();
            MachineMgr.MvaCfInit();
            var MachineCtrl = MachineMgr.CtrlMachines[EnumMachineID.MID_CB_A_ASB.ToString()] as MacMcCabinet;
            Machine = MachineCtrl.StateMachine;
        }

        [TestMethod]
        public void Test_CreateInstance()
        {
            var s = Machine.GetDicMacHalDrawers();

            DrawerSatusInfo drawerInfo = new DrawerSatusInfo("boxBarcode",EnumMachineID.MID_DRAWER_01_02,MacMaskBoxType.CrystalBox);
            Machine.Mediater.CabinetMediater.EnqueueBankOutDrawerInfo(drawerInfo);
            var peek = Machine.Mediater.CabinetMediater.PeekBankOut(out drawerInfo);

        }

        /// <summary>系統啟動</summary>
        [TestMethod]
        public void Test_SystemBootup()
        {
            var method = typeof(MacMsCabinet0).GetMethod(EnumMacMcCabinetCmd.SystemBootup.ToString());
            method.Invoke(Machine, null);
            Repeat();
        }

        /// <summary>Bank Out, Load, 將 指定數量的 Tray移到 Out 等待 放置盒子 </summary>
        [TestMethod]
        [DataRow(3)]
        public void Test_BankOutLoadMoveTraysToOutForPutBoxOnTray(int drawerCounts)
        {
            var method = typeof(MacMsCabinet0).GetMethod(EnumMacMcCabinetCmd.BankOutLoadMoveTraysToOutForPutBoxOnTray.ToString());
            method.Invoke(Machine, new object[] { drawerCounts});
            Repeat();
        }

        /// <summary>Bankout, Load, 放入盒子之後, 將指定的 Drawer 的Tray(有盒子) 推入 Home </summary>
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
           var method = typeof(MacMsCabinet0).GetMethod(EnumMacMcCabinetCmd.BankOutLoadMoveTraysToHomeAfterPutBoxOnTray.ToString());
            method.Invoke(Machine, new object[] { list });
            Repeat();
        }

        /// <summary>BankOut, Load, 將指定的 Drawer Tray (從特定的 Que 取得 Drawer Location)將相對的 Drawer Tray 移到 In, 等待 Boxrobot 抓取 </summary>
        [TestMethod]
        [DataRow(BoxrobotTransferLocation.Drawer_01_02)]
        public void Test_BankOutLoadMoveSpecificTrayToInForBoxRobotGrab(BoxrobotTransferLocation drawerLocation)
        {

            #region 測試資料
            var drawerInfo = Machine.GetDicMacHalDrawers().GetKeyValue(drawerLocation);
            drawerInfo.Value.SetBoxType(MacMaskBoxType.CrystalBox);
            drawerInfo.Value.SetDuration(DrawerDuration.BankOut_Load_TrayAtHomeWithBox);
            Machine.BankOutLoadEnqueue(drawerLocation);
            #endregion

            var method = typeof(MacMsCabinet0).GetMethod(EnumMacMcCabinetCmd.BankOutLoadMoveSpecificTrayToInForBoxRobotGrabBox.ToString());
            method.Invoke(Machine,null);
            Repeat();
        }

        /// <summary>BankOut, Load, 將指定的 Drawer Tray (從特定的 Que 取得 Drawer Location)將相對的 Drawer Tray 移到 Home, 等待 下一步(Unload)  </summary>
        [TestMethod]
        [DataRow(BoxrobotTransferLocation.Drawer_01_02)]
        public void Test_BankOutLoadMoveSpecificTrayToHomeAfterBoxrobotGrabBox(BoxrobotTransferLocation drawerLocation)
        {
            #region 測試資料
            var drawerInfo = Machine.GetDicMacHalDrawers().GetKeyValue(drawerLocation);
            drawerInfo.Value.SetBoxType(MacMaskBoxType.CrystalBox);
            drawerInfo.Value.SetDuration(DrawerDuration.BankOut_Load_TrayAtInWithBoxForRobotGrabBox);
            Machine.BankOutLoadEnqueue(drawerLocation);
            #endregion

            var method = typeof(MacMsCabinet0).GetMethod(EnumMacMcCabinetCmd.BankOutLoadMoveSpecificTrayToHomeAfterBoxrobotGrabBox.ToString());
            method.Invoke(Machine, null);
            Repeat();
           
        }

        /// <summary>BankOut, UnLoad, 將指定的 Drawer Tray (從特定的 Que 取得 Drawer Location)將相對的 Drawer Tray 移到 In, 等待 Box Robot 將盒子放在 Tray 上  </summary>
        [TestMethod]
        [DataRow(BoxrobotTransferLocation.Drawer_01_02)]
        public void Test_BankOutUnLaodMoveSpecificTrayToInForBoxrobotPutBox(BoxrobotTransferLocation drawerLocation)
        {
            #region 測試資料
            var drawerInfo = Machine.GetDicMacHalDrawers().GetKeyValue(drawerLocation);
            drawerInfo.Value.SetBoxType(MacMaskBoxType.CrystalBox);
            drawerInfo.Value.SetDuration(DrawerDuration.BankOut_Load_TrayAtHomeNoBox);
            Machine.BankOutLoadEnqueue(drawerLocation);
            #endregion


            var method = typeof(MacMsCabinet0).GetMethod(EnumMacMcCabinetCmd.BankOutUnLoadMoveSpecificTrayToInForBoxrobotPutBox.ToString());
            method.Invoke(Machine, null);
            Repeat();
        }

        /// <summary>Bankout, unload, 將指定的Drawer Tray 經Boxrobot 放上 Box 之後 由 In 移到 Home </summary>
        /// <param name="drawerLocation"></param>
        [TestMethod]
        [DataRow(BoxrobotTransferLocation.Drawer_01_02)]
        public void Test_BankOutUnLoadMoveSpecificTrayToHomeAfterBoxrobotPutBox(BoxrobotTransferLocation drawerLocation)
        {
            #region 測試資料
            var drawerInfo = Machine.GetDicMacHalDrawers().GetKeyValue(drawerLocation);
            drawerInfo.Value.SetBoxType(MacMaskBoxType.CrystalBox);
            drawerInfo.Value.SetDuration(DrawerDuration.BankOut_UnLoad_TrayAtInNoBox);
            Machine.BankOutLoadEnqueue(drawerLocation);
            #endregion

            var method = typeof(MacMsCabinet0).GetMethod(EnumMacMcCabinetCmd.BankOutUnLoadMoveSpecificTrayToHomeAfterBoxrobotPutBox.ToString());
            method.Invoke(Machine, null);
            Repeat();
        }

        /// <summary>Bankout, unload, 將 Drawer Tray 在 Home 而且有盒子者  由 Home 移到 Out </summary>
        [TestMethod]
        [DataRow(new MacEnumDevice[] { MacEnumDevice.cabinet_drawer_01_02, MacEnumDevice.cabinet_drawer_01_03 })]
        public void Test_BankOutUnLoadMoveSpecificTraysToOutForGrabBox(MacEnumDevice[] devices)
        {
            #region  測試資料
            var list = devices.ToList();
            var drawers = Machine.GetDicMacHalDrawers();
            foreach (var l in list)
            {
                var v = drawers[l.ToBoxrobotTransferLocation()];
                v.SetDuration(DrawerDuration.BankOut_UnLoad_TrayAtHomeWithBox);
            }
            #endregion

            var method = typeof(MacMsCabinet0).GetMethod(EnumMacMcCabinetCmd.BankOutUnLoadMoveSpecificTraysToOutForGrabBox.ToString());
            method.Invoke(Machine,null);
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
