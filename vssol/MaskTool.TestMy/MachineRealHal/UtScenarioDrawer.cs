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
                    /** CommandSetTimeOut() 
                    drawer_01_01.CommandSetTimeOut(100);
                    drawer_01_02.CommandSetTimeOut(100);
                  */

                    /** CommandTrayMotionHome()   
                    drawer_01_01.CommandTrayMotionHome();
                    drawer_01_02.CommandTrayMotionHome();
                    */
                    /** CommandTrayMotionHome()  
                    drawer_01_01.CommandTrayMotionIn();
                    drawer_01_02.CommandTrayMotionIn();
                     */
                    /** CommandTrayMotionHome()  
                    drawer_01_01.CommandTrayMotionOut();
                    drawer_01_01.CommandTrayMotionOut();
                    */

                  
                    Repeat();

                }
            }
            catch (Exception ex)
            {

            }

        }
        void BindEvents(IMacHalDrawer drawer)
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
            drawer.OnSetTimeOutFailedHandler += OnSetTimeOutFailed;
            drawer.OnSetTimeOutOKHandler += OnSetTimeOutOK;
            drawer.OnINIFailedHandler += OnINIFailed;
            drawer.OnINIOkHandler += OnINIOK;
           
            drawer.OnTrayArriveInHandler += OnTrayArriveIn;
            drawer.OnTrayArriveOutHandler += OnTrayArriveOut;

            drawer.OnTrayMotionFailedHandler += OnTrayMotionFailed;
            drawer.OnTrayMotionOKHandler += OnTrayMotionOK;

            drawer.OnTrayMothingSensorOFFHandler += TrayMotionSensorOFF;
            drawer.OnERRORErrorHandler += OnERRORError;
            drawer.OnERRORREcoveryHandler += OnERRORRecovery;

            drawer.OnSysStartUpHandler += OnSysStartUp;
            drawer.OnButtonEventHandler += OnButtonEvent;
            drawer.OnLCDCMsgFailedHandler += OnLCDCMsgFailed;
            drawer.OnLCDCMsgOKHandler += OnLCDCMsgOK;

        }
        void OnSysStartUp(object sender, EventArgs e)
        {
            var drawer = (IMacHalDrawer)sender;
        }
        void OnButtonEvent(object sender, EventArgs e)
        {
            var drawer = (IMacHalDrawer)sender;
        }
        void OnLCDCMsgFailed(object sender, EventArgs e)
        {
            var drawer = (IMacHalDrawer)sender;
        }

        void OnLCDCMsgOK(object sender, EventArgs e)
        {
            var drawer = (IMacHalDrawer)sender;
        }



        void OnERRORError(object sender, EventArgs e)
        {
            var drawer = (IMacHalDrawer)sender;
        }
        void OnERRORRecovery(object sender, EventArgs e)
        {
            var drawer = (IMacHalDrawer)sender;
        }

        void TrayMotionSensorOFF(object sender, EventArgs e)
        {
            var drawer = (IMacHalDrawer)sender;
        }

        void OnTrayMotionOK(object sender, EventArgs e)
        {
            var drawer = (IMacHalDrawer)sender;
        }
        void OnTrayMotionFailed(object sender, EventArgs e)
        {
            var drawer = (IMacHalDrawer)sender;
        }

        void OnTrayArriveHome(object sender, EventArgs e)
        {
            var drawer = (IMacHalDrawer)sender;
        }

        void OnTrayArriveIn(object sender, EventArgs e)
        {
            var drawer = (IMacHalDrawer)sender;
        }
        void OnTrayArriveOut(object sender, EventArgs e)
        {
            var drawer = (IMacHalDrawer)sender;
        }
        void OnINIOK(object sender,EventArgs e)
        {
            var drawer = (IMacHalDrawer)sender;
        }
        void OnINIFailed(object sender, EventArgs e)
        {
            var drawer = (IMacHalDrawer)sender;
        }
        
        void OnSetTimeOutFailed(object sender, EventArgs e)
        {
            var drawer = (IMacHalDrawer)sender;
        }
        void OnSetTimeOutOK(object sender, EventArgs e)
        {
            var drawer = (IMacHalDrawer)sender;
        }

     

        void OnTrayMotioning(object sender, EventArgs e)
        {
            var drawer = (IMacHalDrawer)sender;
        }

        void OnBrightLEDOK(object sender, EventArgs e)
        {
            var drawer = (IMacHalDrawer)sender;
        }
        void OnBrightLEDFailed(object sender, EventArgs e)
        {
            var drawer = (IMacHalDrawer)sender;
        }
        void OnDetectedEmptyBox(object sender,EventArgs e)
        {
            var drawer = (IMacHalDrawer)sender;
        }
        void OnDetectedHasBox(object sender, EventArgs e)
        {
            var drawer = (IMacHalDrawer)sender;
        }
        void OnSetMotionSpeedFailed(object sender, EventArgs e)
        {
            var drawer = (IMacHalDrawer)sender;

        }
        void OnSetMotionSpeedOK(object sender, EventArgs e)
        {
            var drawer = (IMacHalDrawer)sender;
        }
        void OnPosionStatus(object sender, EventArgs e)
        {
            var drawer = (IMacHalDrawer)sender;
            var eventArgs=(OnReplyPositionEventArgs)e;
        }
        #endregion
        #endregion
    }
}
