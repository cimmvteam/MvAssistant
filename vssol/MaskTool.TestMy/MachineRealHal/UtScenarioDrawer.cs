using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.DeviceDrive.KjMachineDrawer;
using MvAssistant.DeviceDrive.KjMachineDrawer.DrawerEventArgs;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Hal.CompDrawer;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.TestMy.MachineRealHal
{
    [TestClass ]
    public class UtScenarioDrawer
    {
        #region Cabinet Action

        void Repeat()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(100);
            }
        }
        #region Drawer
        [TestMethod]
        public void DrawerTest()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();
                    var cabinet = halContext.HalDevices[MacEnumDevice.cabinet_assembly.ToString()] as MacHalCabinet;
                    var drawer_01_01 = cabinet.Hals[MacEnumDevice.cabinet_drawer_01_01.ToString()] as MacHalDrawerKjMachine;
                    var drawer_01_02 = cabinet.Hals[MacEnumDevice.cabinet_drawer_01_02.ToString()] as MacHalDrawerKjMachine;
                    drawer_01_01.HalConnect();
                    drawer_01_02.HalConnect();
                    BindEvents(drawer_01_01);
                    BindEvents(drawer_01_02);

                    /**  CommandSetMotionSpeed
                    drawer_01_01.CommandSetMotionSpeed(100);
                    drawer_01_02.CommandSetMotionSpeed(100);
                   */
                    /** CommandPositionRead
                    drawer_01_02.CommandPositionRead();
                    drawer_01_01.CommandPositionRead();
                    */
                    /** CommandBoxDetection
                    drawer_01_01.CommandBoxDetection();
                    drawer_01_02.CommandBoxDetection();
                     */
                    /**CommandBrightLEDAllOn()
                   drawer_01_01.CommandBrightLEDAllOn();
                   drawer_01_02.CommandBrightLEDAllOn();
                   */
                    /**CommandBrightLEDAllOff()
                     drawer_01_01.CommandBrightLEDAllOff();
                     drawer_01_02.CommandBrightLEDAllOff();
                    */
                    /** CommandBrightLEDGreenOn()
                    drawer_01_01.CommandBrightLEDGreenOn();
                    drawer_01_02.CommandBrightLEDGreenOn();
                    */
                    /** CommandBrightLEDRedOn()
                     drawer_01_01.CommandBrightLEDRedOn();
                     drawer_01_02.CommandBrightLEDRedOn();
                    */
                    /** CommandINI() 
                    drawer_01_01.CommandINI();
                    drawer_01_02.CommandINI();
                     */
                    /** CommandINI()  */
                    drawer_01_01.CommandSetTimeOut(100);
                    drawer_01_02.CommandSetTimeOut(100);
                  
                    Repeat();

                }
            }
            catch (Exception ex)
            {

            }

        }
        void BindEvents(MacHalDrawerKjMachine drawer)
        {
            drawer.OnSetMotionSpeedFailedHandler += OnSetMotionSpeedFailed;
            drawer.OnSetMotionSpeedOKHandler += OnSetMotionSpeedOK;
            drawer.OnPositionStatusHandler += OnPosionStatus;
            drawer.OnDetectedEmptyBoxHandler += OnDetectedEmptyBox;
            drawer.OnDetectedHasBoxHandler += OnDetectedHasBox;
            drawer.OnBrightLEDFailedHandler += OnBrightLEDFailed;
            drawer.OnBrightLEDOKHandler += OnBrightLEDOK;
            drawer.OnTrayMotioningHandler += OnTrayMotioning;
            drawer.OnTrayArriveHomeHandler += OnTrayArriveHome;
            drawer.OnSetTimeOutFailedHandler += SetTimeOutFailed;
            drawer.OnSetTimeOutOKHandler += SetTimeOutOK;
        } 
        void SetTimeOutFailed(object sender, EventArgs e)
        {
            var drawer = (MacHalDrawerKjMachine)sender;
        }
        void SetTimeOutOK(object sender, EventArgs e)
        {
            var drawer = (MacHalDrawerKjMachine)sender;
        }

        void OnTrayArriveHome(object sender, EventArgs e)
        {
            var drawer = (MacHalDrawerKjMachine)sender;
        }

        void OnTrayMotioning(object sender, EventArgs e)
        {
            var drawer = (MacHalDrawerKjMachine)sender;
        }

        void OnBrightLEDOK(object sender, EventArgs e)
        {
            var drawer = (MacHalDrawerKjMachine)sender;
        }
        void OnBrightLEDFailed(object sender, EventArgs e)
        {
            var drawer = (MacHalDrawerKjMachine)sender;
        }
        void OnDetectedEmptyBox(object sender,EventArgs e)
        {
            var drawer = (MacHalDrawerKjMachine)sender;
        }
        void OnDetectedHasBox(object sender, EventArgs e)
        {
            var drawer = (MacHalDrawerKjMachine)sender;
        }
        void OnSetMotionSpeedFailed(object sender, EventArgs e)
        {
            var drawer = (MacHalDrawerKjMachine)sender;

        }
        void OnSetMotionSpeedOK(object sender, EventArgs e)
        {
            var drawer = (MacHalDrawerKjMachine)sender;
        }
        void OnPosionStatus(object sender, EventArgs e)
        {
            var drawer = (MacHalDrawerKjMachine)sender;
            var eventArgs=(OnReplyPositionEventArgs)e;
        }
        #endregion
        #endregion
    }
}
