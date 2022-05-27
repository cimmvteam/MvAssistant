using MvAssistant.v0_2.Mac.Hal.Assembly;
using MvAssistant.v0_2.Mac.JSon.RobotTransferFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Mac.TestMy.ToolHal.Hirata_ScenarioTest.Extends
{
   public static class BoxrobotTransferPathFileExtends
    {

        public static string GetFromDrawerToCabitnetHomeGetPath(this BoxrobotTransferPathFile instance, BoxrobotTransferLocation drawerLocation)
        {
            var drawerHome = drawerLocation.GetCabinetHomeCode();
            if (drawerHome.Item1 == false) { return ""; }
            if (drawerHome.Item2 == BoxrobotTransferLocation.Cabinet_01_Home)
            {
                var path = instance.FromDrawerToCabinet01Home_GET_PathFile(drawerLocation);

                return path;
            }
            else// if(drawerHome.Item2 == BoxrobotTransferLocation.Cabinet_02_Home)
            {
                var path = instance.FromDrawerToCabinet02Home_GET_PathFile(drawerLocation);
                return path;
            }

        }

        public static string GetFromDrawerToCabitnetHomePutPath(this BoxrobotTransferPathFile instance, BoxrobotTransferLocation drawerLocation)
        {
            var drawerHome = drawerLocation.GetCabinetHomeCode();
            if (drawerHome.Item1 == false) { return ""; }
            if (drawerHome.Item2 == BoxrobotTransferLocation.Cabinet_01_Home)
            {
                var path = instance.FromDrawerToCabinet01Home_PUT_PathFile(drawerLocation);

                return path;
            }
            else// if(drawerHome.Item2 == BoxrobotTransferLocation.Cabinet_02_Home)
            {
                var path = instance.FromDrawerToCabinet02Home_PUT_PathFile(drawerLocation);
                return path;
            }

        } 


        public static string GetFromCabinetHomeToDrawerGetPath(this BoxrobotTransferPathFile instance, BoxrobotTransferLocation drawerLocation)
        {

            var drawerHome = drawerLocation.GetCabinetHomeCode();
            if (drawerHome.Item1 == false) { return ""; }
            if (drawerHome.Item2 == BoxrobotTransferLocation.Cabinet_01_Home)
            {
                var path = instance.FromCabinet01HomeToDrawer_GET_PathFile(drawerLocation);

                return path;
            }
            else// if(drawerHome.Item2 == BoxrobotTransferLocation.Cabinet_02_Home)
            {
                var path = instance.FromCabinet02HomeToDrawer_GET_PathFile(drawerLocation);
                return path;
            }

        }
        public static string GetFromCabinetHomeToDrawerPutPath(this BoxrobotTransferPathFile instance, BoxrobotTransferLocation drawerLocation)
        {

            var drawerHome = drawerLocation.GetCabinetHomeCode();
            if (drawerHome.Item1 == false) { return ""; }
            if (drawerHome.Item2 == BoxrobotTransferLocation.Cabinet_01_Home)
            {
                var path = instance.FromCabinet01HomeToDrawer_PUT_PathFile(drawerLocation);
                
                return path;
            }
            else// if(drawerHome.Item2 == BoxrobotTransferLocation.Cabinet_02_Home)
            {
                 var path = instance.FromCabinet02HomeToDrawer_PUT_PathFile(drawerLocation);
                return path;
            }

        }
    }
}
