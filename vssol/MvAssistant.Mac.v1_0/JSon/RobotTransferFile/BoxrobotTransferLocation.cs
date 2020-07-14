using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.JSon.RobotTransferFile
{
   public  enum BoxrobotTransferLocation
    {
        Dontcare=0,
        Cabinet_01_Home,
        Cabinet_02_Home,
        OpenStage,
        Drawer_01_01,Drawer_01_02,Drawer_01_03,Drawer_01_04,Drawer_01_05,
        Drawer_02_01,Drawer_02_02,Drawer_02_03,Drawer_02_04,Drawer_02_05,
        Drawer_03_01,Drawer_03_02,Drawer_03_03,Drawer_03_04,Drawer_03_05,
        Drawer_04_01,Drawer_04_02,Drawer_04_03,Drawer_04_04,Drawer_04_05,
        Drawer_05_01,Drawer_05_02,Drawer_05_03,Drawer_05_04,Drawer_05_05,
        Drawer_06_01,Drawer_06_02,Drawer_06_03,Drawer_06_04,Drawer_06_05,
        Drawer_07_01,Drawer_07_02,Drawer_07_03,Drawer_07_04,Drawer_07_05,
        LockCrystalBox,UnlockCrystalBox,
        LockIronBox,UnlockIronBox
    }

    

    public static class BoxrobotTransferLocationExtends
    {
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
    }
}
