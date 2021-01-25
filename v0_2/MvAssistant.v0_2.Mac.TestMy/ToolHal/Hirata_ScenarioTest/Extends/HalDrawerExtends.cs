using MvAssistant.v0_2.Mac.Hal;
using MvAssistant.v0_2.Mac.Hal.CompDrawer;
using MvAssistant.v0_2.Mac.JSon.RobotTransferFile;
using MvAssistant.v0_2.Mac.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Mac.TestMy.ToolHal.Hirata_ScenarioTest.Extends
{
    public static class HalDrawerExtends
    {
       static int timeoutMilliSecs = 30000;
       static List<MacEnumDevice> drawerCode = null;
        static List<BoxrobotTransferLocation> drawerLocations = null;
        static readonly object lockDrawerCode=new object();
        static readonly object lockDrawerLocation = new object();
        public static List<MacEnumDevice> DrawerKeys
        {
            get
            {
                if (drawerCode == null)
                {
                    lock (lockDrawerCode)
                    {
                        if (drawerCode == null)
                        { 
                          drawerCode = new List<MacEnumDevice>(
                            new MacEnumDevice[]
                            {
                             MacEnumDevice.cabinet_drawer_01_02,MacEnumDevice.cabinet_drawer_01_03,MacEnumDevice.cabinet_drawer_01_04,
                             MacEnumDevice.cabinet_drawer_02_02,MacEnumDevice.cabinet_drawer_02_03,MacEnumDevice.cabinet_drawer_02_04,
                            MacEnumDevice.cabinet_drawer_03_02,MacEnumDevice.cabinet_drawer_03_03,MacEnumDevice.cabinet_drawer_03_04,
                            MacEnumDevice.cabinet_drawer_04_02,MacEnumDevice.cabinet_drawer_04_03,MacEnumDevice.cabinet_drawer_04_04,
                            MacEnumDevice.cabinet_drawer_05_02,MacEnumDevice.cabinet_drawer_05_03,MacEnumDevice.cabinet_drawer_05_04,
                            MacEnumDevice.cabinet_drawer_06_02,MacEnumDevice.cabinet_drawer_06_03,MacEnumDevice.cabinet_drawer_06_04,
                            MacEnumDevice.cabinet_drawer_07_02,MacEnumDevice.cabinet_drawer_07_03
                           });
                       }
                    }
                }
                return drawerCode;
            }
        }

        public static List<BoxrobotTransferLocation> DrawerLocations
        {
            get
            {
                if (drawerLocations == null)
                {
                    lock (lockDrawerLocation)
                    {
                        if (drawerLocations == null)
                        {
                            drawerLocations = new List<BoxrobotTransferLocation>(
                               new BoxrobotTransferLocation[]
                               {
                                BoxrobotTransferLocation.Drawer_01_02,BoxrobotTransferLocation.Drawer_01_03,BoxrobotTransferLocation.Drawer_01_04,
                                BoxrobotTransferLocation.Drawer_02_02,BoxrobotTransferLocation.Drawer_02_03,BoxrobotTransferLocation.Drawer_02_04,
                                BoxrobotTransferLocation.Drawer_03_02,BoxrobotTransferLocation.Drawer_03_03,BoxrobotTransferLocation.Drawer_03_04,
                                BoxrobotTransferLocation.Drawer_04_02,BoxrobotTransferLocation.Drawer_04_03,BoxrobotTransferLocation.Drawer_04_04,
                                BoxrobotTransferLocation.Drawer_05_02,BoxrobotTransferLocation.Drawer_05_03,BoxrobotTransferLocation.Drawer_05_04,
                                BoxrobotTransferLocation.Drawer_06_02,BoxrobotTransferLocation.Drawer_06_03,BoxrobotTransferLocation.Drawer_06_04,
                                BoxrobotTransferLocation.Drawer_07_02,BoxrobotTransferLocation.Drawer_07_03

                                });
                        }
                    }
                }
                return drawerLocations;
            }

        }
       
       
        

      public static bool Initial(this IMacHalDrawer instance)
       {
          instance.CommandINI();
          var rtn = SpinWait.SpinUntil(
            () => { return instance.CurrentWorkState == DrawerWorkState.InitialFailed || instance.CurrentWorkState == DrawerWorkState.TrayArriveAtPositionHome; }, timeoutMilliSecs);


          if (instance.CurrentWorkState == DrawerWorkState.TrayArriveAtPositionHome)
          { return true; }
          else if (instance.CurrentWorkState == DrawerWorkState.InitialFailed)
          {
               throw new Exception("Drawer Initial Failed");
          }
          else
          {
            throw (new Exception("Drawer Initial Time Out"));
          }
      }


        public static bool MoveTrayToIn(this IMacHalDrawer instance)
        {
            instance.CommandTrayMotionIn();
            var rtn = SpinWait.SpinUntil(
             () => { return instance.CurrentWorkState == DrawerWorkState.TrayArriveAtPositionIn || instance.CurrentWorkState == DrawerWorkState.TrayMotionFailed; }, timeoutMilliSecs);


            if (instance.CurrentWorkState == DrawerWorkState.TrayArriveAtPositionIn)
            { return true; }
            else if (instance.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
            {
                throw new Exception("Drawer Move Tray In  Failed");
            }
            else
            {
                throw (new Exception("Drawer Move Tray In Time Out"));
            }
        }

        public static bool MoveTrayToHome(this IMacHalDrawer instance)
        {

            instance.CommandTrayMotionHome();
            var rtn = SpinWait.SpinUntil(
             () => { return instance.CurrentWorkState == DrawerWorkState.TrayArriveAtPositionHome || instance.CurrentWorkState == DrawerWorkState.TrayMotionFailed; }, timeoutMilliSecs);


            if (instance.CurrentWorkState == DrawerWorkState.TrayArriveAtPositionHome)
            { return true; }
            else if (instance.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
            {
                throw new Exception("Drawer Move Tray Home  Failed");
            }
            else
            {
                throw (new Exception("Drawer Move Tray Out Time Out"));
            }

        }

        public static bool MoveTrayToOut(this IMacHalDrawer instance)
        {

            instance.CommandTrayMotionOut();
            var rtn = SpinWait.SpinUntil(
             () => { return instance.CurrentWorkState == DrawerWorkState.TrayArriveAtPositionOut || instance.CurrentWorkState == DrawerWorkState.TrayMotionFailed; }, timeoutMilliSecs);


            if (instance.CurrentWorkState == DrawerWorkState.TrayArriveAtPositionOut)
            { return true; }
            else if (instance.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
            {
                throw new Exception("Drawer Move Tray Out  Failed");
            }
            else
            {
                throw (new Exception("Drawer Move Tray Out Time Out"));
            }

        }

    }

    

}
