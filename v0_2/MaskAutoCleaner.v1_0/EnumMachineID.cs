using MvAssistant.v0_2.Mac.JSon.RobotTransferFile;
using MvAssistant.v0_2.Mac.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0
{
    public enum EnumMachineID
    {
        MID_BT_A_ASB,
        MID_CC_A_ASB,
        MID_CB_A_ASB,
        MID_IC_A_ASB,
        MID_LP_A_ASB,
        MID_LP_B_ASB,
        MID_MT_A_ASB,
        MID_OS_A_ASB,
        MID_UNI_A_ASB,
        MID_DRAWER_01_01,
        MID_DRAWER_01_02,
        MID_DRAWER_01_03,
        MID_DRAWER_01_04,
        MID_DRAWER_01_05,
        MID_DRAWER_02_01,
        MID_DRAWER_02_02,
        MID_DRAWER_02_03,
        MID_DRAWER_02_04,
        MID_DRAWER_02_05,
        MID_DRAWER_03_01,
        MID_DRAWER_03_02,
        MID_DRAWER_03_03,
        MID_DRAWER_03_04,
        MID_DRAWER_03_05,
        MID_DRAWER_04_01,
        MID_DRAWER_04_02,
        MID_DRAWER_04_03,
        MID_DRAWER_04_04,
        MID_DRAWER_04_05,
        MID_DRAWER_05_01,
        MID_DRAWER_05_02,
        MID_DRAWER_05_03,
        MID_DRAWER_05_04,
        MID_DRAWER_05_05,
        MID_DRAWER_06_01,
        MID_DRAWER_06_02,
        MID_DRAWER_06_03,
        MID_DRAWER_06_04,
        MID_DRAWER_06_05,
        MID_DRAWER_07_01,
        MID_DRAWER_07_02,
        MID_DRAWER_07_03,
        MID_DRAWER_07_04,
        MID_DRAWER_07_05,
    }
    public static class EnumMachineIDExtends
    {

        /// <summary>取得 Drawer State Machine ID 的 範圍</summary>
        /// <param name="inst">任何一個 EnumMachineID 的成員 </param>
        /// <returns>
        /// DrawerStateMachineIDRange
        /// </returns>
        public static DrawerStateMachineIDRange GetDrawerStateMachineIDRange(this EnumMachineID inst)
        {
            var rtnV = new DrawerStateMachineIDRange(EnumMachineID.MID_DRAWER_01_01, EnumMachineID.MID_DRAWER_07_05);
            return rtnV;
        }
        public static Tuple<bool, EnumMacDeviceId> ToMacEnumDeviceForDrawer(this EnumMachineID inst)
        {
            DrawerStateMachineIDRange drawerRangeOfEnumMachineID = inst.GetDrawerStateMachineIDRange();
            var rtnV = default(Tuple<bool, EnumMacDeviceId>);
            if (inst.IsInDrawerRange())
            {
                var diff = inst - drawerRangeOfEnumMachineID.StartID;
                var drawerRangeOfMacEnumDevice = new MacEnumDeviceDrawerRange();
                rtnV = Tuple.Create(true, drawerRangeOfMacEnumDevice.StartID + diff);
            }
            else
            {
                rtnV = Tuple.Create(false, EnumMacDeviceId.boxtransfer_assembly);
            }
            return rtnV;
        }

        public static Tuple<bool, BoxrobotTransferLocation> ToBoxrobotTransferLocationForDrawer(this EnumMachineID inst)
        {
            DrawerStateMachineIDRange drawerRangeOfEnumMachineID = inst.GetDrawerStateMachineIDRange();
            var rtnV = default(Tuple<bool, BoxrobotTransferLocation>);
            if (inst.IsInDrawerRange())
            {
                var diff = inst - drawerRangeOfEnumMachineID.StartID;
                var drawerRangeOfBoxrobotTransferLocation = new BoxrobotTransferLocationDrawerRange();
                rtnV = Tuple.Create(true, drawerRangeOfBoxrobotTransferLocation.Start + diff);
            }
            else
            {
                rtnV = Tuple.Create(false, BoxrobotTransferLocation.Dontcare);
            }
            return rtnV;
        }

        /// <summary>是否在 Drawer 的範圍內 </summary>
        /// <param name="inst"></param>
        /// <returns></returns>
        public static bool IsInDrawerRange(this EnumMachineID inst)
        {
            var range = inst.GetDrawerStateMachineIDRange();
            if (inst > range.EndID || inst < range.StartID)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
