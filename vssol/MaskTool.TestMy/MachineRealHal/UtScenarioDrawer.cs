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
        
        void Repeat()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(10);
            }
        }
        
        [TestMethod]
        public void DrawerBasicTest()
        {
            try
            {
                var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real");
                halContext.MvCfLoad();
                var cabinet = halContext.HalDevices[MacEnumDevice.cabinet_assembly.ToString()] as MacHalCabinet;
                var drawer= cabinet.Hals[MacEnumDevice.cabinet_drawer_01_01.ToString()] as MacHalDrawerKjMachine;
                var connected=drawer.HalConnect();
                var isConnected = drawer.HalIsConnected();
                Debug.WriteLine($"IsConnected={isConnected}");
                BindEvents(drawer);

                Repeat();
            }
            catch(Exception ex)
            {

            }
        }

        /// <summary>指令測試</summary>
        [TestMethod]
        public void DrawerCommandTest()
        {
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.MvCfLoad();
                    var cabinet = halContext.HalDevices[MacEnumDevice.cabinet_assembly.ToString()] as MacHalCabinet;
                    var drawer_01_01 = cabinet.Hals[MacEnumDevice.cabinet_drawer_01_01.ToString()] as MacHalDrawerKjMachine;
                    var drawer_01_02 = cabinet.Hals[MacEnumDevice.cabinet_drawer_01_02.ToString()] as MacHalDrawerKjMachine;
                    var isConnected= drawer_01_01.HalConnect();
                    Debug.WriteLine($"IsConnected={isConnected}");
                    drawer_01_02.HalConnect();
                    BindEvents(drawer_01_01);
                    BindEvents(drawer_01_02);

                    /**  CommandSetMotionSpeed    */
                    drawer_01_01.CommandSetMotionSpeed(100);
                    drawer_01_02.CommandSetMotionSpeed(100);
               
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
            drawer.OnINIOKHandler += OnINIOK;
           
            drawer.OnTrayArriveInHandler += OnTrayArriveIn;
            drawer.OnTrayArriveOutHandler += OnTrayArriveOut;

            drawer.OnTrayMotionFailedHandler += OnTrayMotionFailed;
            drawer.OnTrayMotionOKHandler += OnTrayMotionOK;

            drawer.OnTrayMotionSensorOFFHandler += TrayMotionSensorOFF;
            drawer.OnERRORErrorHandler += OnERRORError;
            drawer.OnERRORREcoveryHandler += OnERRORRecovery;

            drawer.OnSysStartUpHandler += OnSysStartUp;
            drawer.OnButtonEventHandler += OnButtonEvent;
            drawer.OnLCDCMsgFailedHandler += OnLCDCMsgFailed;
            drawer.OnLCDCMsgOKHandler += OnLCDCMsgOK;

        }
        #region invoke Event
        void OnSysStartUp(object sender, EventArgs e)
        {
            var drawer = (IMacHalDrawer)sender;
        }
        void OnButtonEvent(object sender, EventArgs e)
        {
            var drawer = (IMacHalDrawer)sender;
            Debug.WriteLine($"DeviceIndex={drawer.DeviceIndex}");
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
            Debug.WriteLine($"DeviceIndex={drawer.DeviceIndex},Invoke={nameof(OnBrightLEDOK)}");
        }
        void OnBrightLEDFailed(object sender, EventArgs e)
        {
            var drawer = (IMacHalDrawer)sender;
            Debug.WriteLine($"DeviceIndex={drawer.DeviceIndex},Invoke={nameof(OnBrightLEDFailed)}");
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

        /// <summary>
        /// Put outside box and load
        /// Move box to inside and unload
        /// </summary>
        [TestMethod]
        public void OutSideLoadToInSideUnload()
        { // [假設 Tray 移到 In, 並已放入 盒子]
          // 1. Tray 移到 Home,
          // 2. 檢查有沒有 Box,
          // 3. (1) 沒有盒子=> 退回In 並 警示
          //    (2) 有盒子=> 移到 Out   

            MacHalDrawerKjMachine testDrawer = null;
            MacHalCabinet cabinet = null;
            MacHalContext halContext = null;
            try
            {
                    halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real");
                    halContext.MvCfLoad();
                    cabinet = halContext.HalDevices[MacEnumDevice.cabinet_assembly.ToString()] as MacHalCabinet;
                    testDrawer = cabinet.Hals[MacEnumDevice.cabinet_drawer_01_01.ToString()] as MacHalDrawerKjMachine;
                    testDrawer.HalConnect();
                    testDrawer.OnTrayMotionFailedHandler += (sender, e) =>
                      {
                          IMacHalDrawer drawer = (IMacHalDrawer)sender;
                          Debug.WriteLine($"Index={drawer.DeviceIndex}, 觸發 {nameof(drawer.OnTrayMotionFailedHandler)} 事件");
                      };
                    testDrawer.OnTrayMotionOKHandler += (sender, e) =>
                    {
                        IMacHalDrawer drawer = (IMacHalDrawer)sender;
                        Debug.WriteLine($"Index={drawer.DeviceIndex}, 觸發 {nameof(drawer.OnTrayMotionOKHandler)} 事件");
                    };
                    testDrawer.OnTrayMotionSensorOFFHandler += (sender, e) =>
                    {
                        IMacHalDrawer drawer = (IMacHalDrawer)sender;
                        Debug.WriteLine($"Index={drawer.DeviceIndex}, 觸發 {nameof(drawer.OnTrayMotionSensorOFFHandler)} 事件");
                    };
                    testDrawer.OnERRORErrorHandler += (sender, e) =>
                    {
                        IMacHalDrawer drawer = (IMacHalDrawer)sender;
                        Debug.WriteLine($"Index={drawer.DeviceIndex}, 觸發 {nameof(drawer.OnERRORErrorHandler)} 事件");
                    };
                    testDrawer.OnERRORREcoveryHandler += (sender, e) =>
                    {
                        IMacHalDrawer drawer = (IMacHalDrawer)sender;
                        Debug.WriteLine($"Index={drawer.DeviceIndex}, 觸發 {nameof(drawer.OnERRORREcoveryHandler)} 事件");
                    };
                    testDrawer.OnTrayMotioningHandler += (sender, e) =>
                    {
                        IMacHalDrawer drawer = (IMacHalDrawer)sender;
                        Debug.WriteLine($"Index={drawer.DeviceIndex}, 觸發 {nameof(drawer.OnTrayMotioningHandler)} 事件");
                    };
                    /**到逹 Home*/
                    testDrawer.OnTrayArriveHomeHandler += ( sender,  e) => 
                    {
                        IMacHalDrawer drawer = (IMacHalDrawer)sender;
                        Debug.WriteLine($"Index={drawer.DeviceIndex}, 檢查有沒有放盒子");
                        drawer.CommandBoxDetection();
                    };
                    /**檢查沒有盒子 */
                    testDrawer.OnDetectedEmptyBoxHandler += ( sender,  e) =>
                    {
                        IMacHalDrawer drawer = (IMacHalDrawer)sender;
                        Debug.WriteLine($"Index={drawer.DeviceIndex}, 沒有盒子");
                        Debug.WriteLine($"Index={drawer.DeviceIndex}, 將 Drawer 送回 In");
                        drawer.CommandTrayMotionIn();
                    };
                    /**檢查有盒子 */
                    testDrawer.OnDetectedHasBoxHandler += ( sender,  e) =>
                    {
                        IMacHalDrawer drawer = (IMacHalDrawer)sender;
                        Debug.Write($"Indexx={drawer.DeviceIndex}, 有放盒子, 將 Drawer 送往 Out");
                        drawer.CommandTrayMotionOut();
                    };
                    testDrawer.OnTrayArriveOutHandler += (sender, e) =>
                    {
                        IMacHalDrawer drawer = (IMacHalDrawer)sender;
                        Debug.WriteLine($"Index={drawer.DeviceIndex}, 已經到達 Out ");
                    };
                    testDrawer.OnTrayArriveInHandler += ( sender,  e) =>
                     {
                         IMacHalDrawer drawer = (IMacHalDrawer)sender;
                         Debug.Write($"Index={drawer.DeviceIndex}, Drawer沒有盒子, 已經退回 In , 請重新裝入盒子");
                     };
                   

                    testDrawer.CommandTrayMotionHome();


                    Repeat();
              
            }
            catch(Exception ex)
            {
                var deviceIndex = testDrawer == null ? "" : testDrawer.DeviceIndex;
                Debug.WriteLine($"Index={deviceIndex},  Exception={ex.Message}");
            }
            finally
            {
                if (halContext != null) { halContext.Dispose(); }
            }
        }

        [TestMethod]
        public void InSideLoadToOutSideUnload()
        {
            // [假設 Tray 移到 Out, 並已放入 盒子]
            // 1. Tray 移到 Home,
            // 2. 檢查有沒有 Box,
            // 3. (1) 沒有盒子=> 退回Out 並 警示
            //    (2) 有盒子=> 移到 In  
            MacHalDrawerKjMachine testDrawer = null;
            MacHalCabinet cabinet = null;
            MacHalContext halContext = null;
            try
            {
                halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real");
                halContext.MvCfLoad();
                cabinet = halContext.HalDevices[MacEnumDevice.cabinet_assembly.ToString()] as MacHalCabinet;
                testDrawer = cabinet.Hals[MacEnumDevice.cabinet_drawer_01_01.ToString()] as MacHalDrawerKjMachine;
                testDrawer.HalConnect();
                testDrawer.OnTrayMotionFailedHandler += (sender, e) =>
                {
                    IMacHalDrawer drawer = (IMacHalDrawer)sender;
                    Debug.WriteLine($"Index={drawer.DeviceIndex}, 觸發 {nameof(drawer.OnTrayMotionFailedHandler)} 事件");
                };
                testDrawer.OnTrayMotionOKHandler += (sender, e) =>
                {
                    IMacHalDrawer drawer = (IMacHalDrawer)sender;
                    Debug.WriteLine($"Index={drawer.DeviceIndex}, 觸發 {nameof(drawer.OnTrayMotionOKHandler)} 事件");
                };
                testDrawer.OnTrayMotionSensorOFFHandler += (sender, e) =>
                {
                    IMacHalDrawer drawer = (IMacHalDrawer)sender;
                    Debug.WriteLine($"Index={drawer.DeviceIndex}, 觸發 {nameof(drawer.OnTrayMotionSensorOFFHandler)} 事件");
                };
                testDrawer.OnERRORErrorHandler += (sender, e) =>
                {
                    IMacHalDrawer drawer = (IMacHalDrawer)sender;
                    Debug.WriteLine($"Index={drawer.DeviceIndex}, 觸發 {nameof(drawer.OnERRORErrorHandler)} 事件");
                };
                testDrawer.OnERRORREcoveryHandler += (sender, e) =>
                {
                    IMacHalDrawer drawer = (IMacHalDrawer)sender;
                    Debug.WriteLine($"Index={drawer.DeviceIndex}, 觸發 {nameof(drawer.OnERRORREcoveryHandler)} 事件");
                };
                testDrawer.OnTrayMotioningHandler += (sender, e) =>
                {
                    IMacHalDrawer drawer = (IMacHalDrawer)sender;
                    Debug.WriteLine($"Index={drawer.DeviceIndex}, 觸發 {nameof(drawer.OnTrayMotioningHandler)} 事件");
                };
                /**到逹 Home*/
                testDrawer.OnTrayArriveHomeHandler += (sender, e) =>
                {
                    IMacHalDrawer drawer = (IMacHalDrawer)sender;
                    Debug.WriteLine($"Index={drawer.DeviceIndex}, 檢查有沒有放盒子");
                    drawer.CommandBoxDetection();
                };
                /**檢查沒有盒子 */
                testDrawer.OnDetectedEmptyBoxHandler += (sender, e) =>
                {
                    IMacHalDrawer drawer = (IMacHalDrawer)sender;
                    Debug.WriteLine($"Index={drawer.DeviceIndex}, 沒有盒子");
                    Debug.WriteLine($"Index={drawer.DeviceIndex}, 將 Drawer 送回 Out");
                    drawer.CommandTrayMotionOut();
                };
                /**檢查有盒子 */
                testDrawer.OnDetectedHasBoxHandler += (sender, e) =>
                {
                    IMacHalDrawer drawer = (IMacHalDrawer)sender;
                    Debug.Write($"Index={drawer.DeviceIndex}, 有放盒子, 將 Drawer 送往 In");
                    drawer.CommandTrayMotionIn();
                };
                testDrawer.OnTrayArriveInHandler += (sender, e) =>
                {
                    IMacHalDrawer drawer = (IMacHalDrawer)sender;
                    Debug.WriteLine($"Index={drawer.DeviceIndex}, 已經到達 In ");
                };
                testDrawer.OnTrayArriveOutHandler += (sender, e) =>
                {
                    IMacHalDrawer drawer = (IMacHalDrawer)sender;
                    Debug.Write($"Index={drawer.DeviceIndex}, Drawer沒有盒子, 已經退回 Out , 請重新裝入盒子");
                };
                testDrawer.CommandTrayMotionHome();
                Repeat();
            }
            catch(Exception ex)
            {
                var deviceIndex = testDrawer == null ? "" : testDrawer.DeviceIndex;
                Debug.WriteLine($"Index={deviceIndex},  Exception={ex.Message}");
            }
            finally
            {
                if(halContext != null)
                {
                    halContext.Dispose();
                }
            }

        }
    }
}
