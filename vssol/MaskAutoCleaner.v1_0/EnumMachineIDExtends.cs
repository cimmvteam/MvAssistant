using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0
{
   public static  class EnumMachineIDExtends
    {

        /// <summary>取得 Drawer State Machine ID 的 範圍</summary>
        /// <param name="inst">任何一個 EnumMachineID 的成員 </param>
        /// <returns>
        /// Item1: 第一個 Drawer 的 ID
        /// Item2: 最後一個 Drawer 的 ID
        /// </returns>
        public static DrawerStateMachineIDRange GetDrawerStateMachineIDRange(this EnumMachineID inst)
        {
            var rtnV = new DrawerStateMachineIDRange(EnumMachineID.MID_DRAWER_01_01, EnumMachineID.MID_DRAWER_01_04);
            return rtnV;
        }
        

    }


    public class DrawerStateMachineIDRange
    {
        EnumMachineID _startID;
        EnumMachineID _endID;
        public DrawerStateMachineIDRange(EnumMachineID start, EnumMachineID end)
        {
             _startID=start;
            _endID = end;
        }
        public EnumMachineID StartID{ get {  return _startID; } }
        public EnumMachineID EndID { get {return _endID; } }
    }
}
