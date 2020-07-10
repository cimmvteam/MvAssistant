using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.DeviceDrive.KjMachineDrawer;
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



                    /**  Led Light
                    drawer1.Tag = BrightLEDType.AllOn;
                    drawer1.CommandBrightLEDAllOn();
                    drawer2.BindResult();
                    drawer2.Tag = BrightLEDType.AllOff;
                    drawer3.Tag = BrightLEDType.GreenOn;
                    drawer3.CommandBrightLEDGreenOn();

                    drawer1.Tag = BrightLEDType.RedOn;
                    drawer1.CommandBrightLEDRedOn();
                      */

                    /**  Motion Move  
                    drawer1.CommandTrayMotionHome();
                    drawer2.CommandTrayMotionIn();
                    drawer3.CommandTrayMotionOut();
                    drawer4.CommandTrayMotionIn();
                      */
                    /** INI
                 drawer1.CommandINI();
                  drawer2.CommandINI();
                 drawer3.CommandINI();
                drawer4.CommandINI();
            */

                    /**  set motion speed
                    drawer1.CommandSetMotionSpeed(100);
                    drawer2.CommandSetMotionSpeed(100);
                    drawer3.CommandSetMotionSpeed(100);
                    drawer4.CommandSetMotionSpeed(100);
                  */
                    /**  set TimeOut
                      drawer1.CommandSetTimeOut(100);
                      drawer2.CommandSetTimeOut(100);
                      drawer3.CommandSetTimeOut(100);
                      drawer4.CommandSetTimeOut(100);
                       */

                    /** CommandPositionRead()                    
                    drawer1.CommandPositionRead();
                    drawer2.CommandPositionRead();
                    drawer3.CommandPositionRead();
                    drawer4.CommandPositionRead();
                    */


                    BindEvents(drawer_01_01);
                    BindEvents(drawer_01_02);
                    Repeat();
                }
            }
            catch (Exception ex)
            {

            }

        }
        void BindEvents(MacHalDrawerKjMachine drawer)
        {
            drawer.OnTrayMotionFailedHandler += this.OnTrayMotionFailed;
            drawer.OnTrayMotionOKHandler += this.OnTrayMotionOK;
        } 

        void OnTrayMotionFailed(object sender,EventArgs e)
        {
            var drawer = (MacHalDrawerKjMachine)sender;
        }

        void OnTrayMotionOK(object sender, EventArgs e)
        {
            var drawer = (MacHalDrawerKjMachine)sender;
        }


        void OnDetectDrawerBoxResult(object sender, EventArgs e)
        {
            var drawer = (MacHalDrawerKjMachine)sender;
            var eventArgs = (HalDrawerBoxDetectReturn)e;

            if (eventArgs.HasBox.HasValue)
            {
                if ((bool)eventArgs.HasBox.HasValue)
                {
                    Debug.WriteLine($"{nameof(OnDetectDrawerBoxResult)}: 有盒子");
                }
                else
                {
                    Debug.WriteLine($"{nameof(OnDetectDrawerBoxResult)}: 没有盒子");
                }
            }
            else
            {
                Debug.WriteLine($"{nameof(OnDetectDrawerBoxResult)}: 無法判定");
            }
        }

        #endregion
        #endregion
    }
}
