using MvaCToolkitCs.v1_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.Mac.JSon.RobotTransferFile
{
    public enum BoxrobotTransferLocation
    {
        Dontcare = 0,
        Cabinet_01_Home,
        Cabinet_02_Home,
        OpenStage,
        Drawer_01_01,
        Drawer_01_02,
        Drawer_01_03,
        Drawer_01_04,
        Drawer_01_05,
        Drawer_02_01,
        Drawer_02_02,
        Drawer_02_03,
        Drawer_02_04,
        Drawer_02_05,
        Drawer_03_01,
        Drawer_03_02,
        Drawer_03_03,
        Drawer_03_04,
        Drawer_03_05,
        Drawer_04_01,
        Drawer_04_02,
        Drawer_04_03,
        Drawer_04_04,
        Drawer_04_05,
        Drawer_05_01,
        Drawer_05_02,
        Drawer_05_03,
        Drawer_05_04,
        Drawer_05_05,
        Drawer_06_01,
        Drawer_06_02,
        Drawer_06_03,
        Drawer_06_04,
        Drawer_06_05,
        Drawer_07_01,
        Drawer_07_02,
        Drawer_07_03,
        Drawer_07_04,
        Drawer_07_05,
        LockCrystalBox, UnlockCrystalBox,
        LockIronBox, UnlockIronBox
    }



    public static class BoxrobotTransferLocationExtends
    {
        const BoxrobotTransferLocation DrawerStart = BoxrobotTransferLocation.Drawer_01_01;
        const BoxrobotTransferLocation DrawerEnd = BoxrobotTransferLocation.Drawer_07_05;
        const BoxrobotTransferLocation Cabinet1End = BoxrobotTransferLocation.Drawer_03_05;
        const BoxrobotTransferLocation Cabinet2Start = BoxrobotTransferLocation.Drawer_04_01;

        public static BoxrobotTransferLocationDrawerRange GetDrawerRange(this BoxrobotTransferLocation inst)
        {
            var rtnV = new BoxrobotTransferLocationDrawerRange();
            return rtnV;
        }

        public static string ToDefaultText(this BoxrobotTransferLocation inst)
        {
            return default(string);
        }

        public static string ToText(this BoxrobotTransferLocation inst)
        {
            string rtnV = inst.ToDefaultText();
            if (inst != BoxrobotTransferLocation.Dontcare)
            {
                rtnV = inst.ToString();
            }
            return rtnV;
        }

        /// <summary>
        /// <para>判斷自己是屬於哪個 Cabinet Home;</para>
        /// 可以判斷: 回傳 Tuple&lt;true, Cabinet Home 代碼&gt;
        /// <para>自己就是 Cabinet Home: 回傳 Tuple&lt;false, 自己&gt;</para>
        /// 無法判別: 回傳回傳型態的預設值
        /// </summary>
        /// <param name="inst"></param>
        /// <returns></returns>
        public static Tuple<bool, BoxrobotTransferLocation> GetCabinetHomeCode(this BoxrobotTransferLocation inst)
        {
            if (inst.IsDrawer())
            {
                if (inst < BoxrobotTransferLocationExtends.Cabinet2Start)
                {
                    return Tuple.Create(true, BoxrobotTransferLocation.Cabinet_01_Home);
                }
                else
                {
                    return Tuple.Create(true, BoxrobotTransferLocation.Cabinet_02_Home);
                }


            }
            else
            {
                if (inst == BoxrobotTransferLocation.Cabinet_01_Home || inst == BoxrobotTransferLocation.Cabinet_02_Home)
                {
                    return Tuple.Create(false, inst);
                }
                else
                {
                    return default(Tuple<bool, BoxrobotTransferLocation>);
                }
            }

        }

        /// <summary>是不是 Drawer 的代碼?</summary>
        /// <param name="inst"></param>
        /// <returns></returns>
        public static bool IsDrawer(this BoxrobotTransferLocation inst)
        {

            if (inst < BoxrobotTransferLocationExtends.DrawerStart || inst > BoxrobotTransferLocationExtends.DrawerEnd)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        /// <summary>
        /// <para>將自己轉換為 4 碼的Drawer 代碼, 如果是,回傳 Tuple&lt;true,代碼&gt;如果不是 Drawer Location 的列舉, 就回傳 Tuple&lt;false,""&gt;</para>
        /// <para>代碼格式, 如:0101, 0102, 0704, ......</para>
        /// </summary>
        /// <param name="inst"></param>
        /// <returns></returns>
        public static Tuple<bool, string> To4DigitDrawerCode(this BoxrobotTransferLocation inst)
        {
            Tuple<bool, string> rtnV = null;
            if (inst.IsDrawer())
            {
                try
                {
                    var drawerText = inst.ToString();
                    string code = "";
                    string[] ary = drawerText.Split('_');
                    int aryLength = ary.Length;
                    code = ary[ary.Length - 2] + ary[aryLength - 1];
                    rtnV = Tuple.Create(true, code);
                }
                catch (Exception ex)
                {
                    rtnV = Tuple.Create(false, string.Empty);
                    CtkLog.Warn(ex);

                }

            }
            else
            {
                rtnV = Tuple.Create(false, string.Empty);
            }
            return rtnV;
        }
    }


}
