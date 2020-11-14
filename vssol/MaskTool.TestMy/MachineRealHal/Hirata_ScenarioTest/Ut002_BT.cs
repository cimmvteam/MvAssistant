using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.TestMy.MachineRealHal.Hirata_ScenarioTest.Extends;
using MvAssistant.Mac.v1_0;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.JSon;
using MvAssistant.Mac.v1_0.JSon.RobotTransferFile;
using MvAssistant.Mac.v1_0.Manifest;

namespace MvAssistant.Mac.TestMy.MachineRealHal.Hirata_ScenarioTest
{
    /// <summary>
    /// <para>ID: MI-CT02-ST-002</para>
    /// <para>項目: To drawer clamp/release * 20</para>
    /// </summary>
    [TestClass]
    public class Ut002_BT
    {

        List<MacEnumDevice> DrawerKeys;
        List<BoxrobotTransferLocation> DrawerLocations;
        BoxrobotTransferPathFile pathFileObj;//= new BoxrobotTransferPathFile(PositionInstance.BTR_Path);

        /// <summary>建構式</summary>
        public Ut002_BT()
        {
            DrawerKeys = HalDrawerExtends.DrawerKeys;
            DrawerLocations = HalDrawerExtends.DrawerLocations;

            PositionInstance.Load(); // 在這裏載入所有(Boxtransfer 及 Masktransfer)的路徑點位資料
            pathFileObj = new BoxrobotTransferPathFile(PositionInstance.BTR_Path);
        }

        #region this is the real unit test function

        [TestMethod]
        [DataRow(BoxType.IronBox,false)]// 鐵盒
        //[DataRow(BoxType.CrystalBox,false)]// 水晶盒
        public void Test_Ut002_BT(BoxType boxType, bool autoConnect)
        {
            using (var halContext = MacHalContextExtends.Create_MacHalContext_Instance())
            {

            }

        }
        #endregion
    }
}
