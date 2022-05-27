using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.v0_2.DeviceDrive.KjMachineDrawer.DrawerEventArgs;
using MvAssistant.v0_2.Mac.TestMy.ToolHal.Hirata_ScenarioTest.Extends;
using MvAssistant.v0_2.Mac;
using MvAssistant.v0_2.Mac.Hal.CompDrawer;
using MvAssistant.v0_2.Mac.JSon.RobotTransferFile;
using MvAssistant.v0_2.Mac.Manifest;

namespace MvAssistant.v0_2.Mac.TestMy.ToolHal.Hirata_ScenarioTest
{
    [TestClass]
    public class Ut003_CB
    {
        List<EnumMacDeviceId> DrawerKeys;
        List<BoxrobotTransferLocation> DrawerLocations;
        public Ut003_CB()
        {
            DrawerKeys = HalDrawerExtends.DrawerKeys;
            DrawerLocations = HalDrawerExtends.DrawerLocations;
        }



        /// <summary>
        /// <para>1. 操作人員透過S/W開啟Drawer (從Cabinet退出至機台外部)</para>
        /// <para>2. 操作人員將光罩鐵盒放入Drawer內</para>
        /// <para>3. 操作人員透過S/W將Drawer送回Cabinet內</para>
        /// <para>4. 重複1 ~3步驟, 完成20個Drawer的光罩鐵盒置入測試</para>
        /// <para>5. 重複1 ~3步驟, 完成20個Drawer的光罩水晶盒置入測試</para>
        /// </summary>
        /// <param name="boxType"></param>
        /// <param name="autoConnect"></param>
        [TestMethod]
        [DataRow(EnumMacMaskBoxType.IronBox,false )]// 鐵盒
        //[DataRow(BoxType.CrystalBox, false)]// 水晶盒
        public void Test_Ut003_CB(EnumMacMaskBoxType boxType  ,bool autoConnect)
        {
            Debug.WriteLine("---------MI-CT02-ST-003-------");
            Debug.WriteLine("BoxType=" + boxType.ToString());
            try
            {
                var BREAK_POINT = 0;
                using (var halContext = MacHalContextExtends.Create_MacHalContext_Instance())
                {
                    
                    var universal = halContext.GetUniversalAssembly(autoConnect);
                    if(!autoConnect)
                    {
                        universal.HalConnect();
                    }
                    for(var i=0;i< DrawerKeys.Count;i++)
                    {
                        var drawerKey = DrawerKeys[i];
                        var drawerLocation = DrawerLocations[i];
                        Debug.WriteLine("Drawer Location=" + drawerLocation);
                        try
                        {
                            var drawer = halContext.GetDrawer(drawerKey);
                         
                            Debug.WriteLine("Drawer DeviceIndex=" + drawer.DeviceId);
                            drawer.Initial();

                            /** 1. 操作人員透過S/W開啟Drawer (從Cabinet退出至機台外部) */
                            drawer.MoveTrayToOut();

                            /** 2. 操作人員將光罩鐵盒放入Drawer內*/
                            BREAK_POINT = 0; // 確認

                            /** 3. 操作人員透過S/W將Drawer送回Cabinet內*/
                            drawer.MoveTrayToHome();

                            /** 3.1 如果需要, 讓 Drawer Tray 移出 取出Box*/
                            drawer.MoveTrayToOut();

                            BREAK_POINT = 0;

                            /** 3.2 如果 3.1 有操作, 也要將 Tray 移回 Home*/
                            drawer.MoveTrayToHome();
                            Debug.WriteLine("OK");
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("Drawer Exception,  Message=" + ex.Message);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine("MacHalContext Exception, Message=" + ex.Message); 
            }
        }


        /// <summary>
        /// <para>1. 操作人員透過S/W開啟Drawer (從Cabinet退出至機台外部)</para>
        /// <para>2. 操作人員將光罩鐵盒放入Drawer內</para>
        /// <para>3. 操作人員透過S/W將Drawer送回Cabinet內</para>
        /// <para>4. 重複1 ~3步驟, 完成20個Drawer的光罩鐵盒置入測試</para>
        /// <para>5. 重複1 ~3步驟, 完成20個Drawer的光罩水晶盒置入測試</para>
        /// </summary>
        /// <param name="boxType"></param>
        /// <param name="autoConnect"></param>
        [TestMethod]
        [DataRow(false)]
        public void Test_Ut003_CB_ByOP( bool autoConnect)
        {
            try
            {
                using (var halContext = MacHalContextExtends.Create_MacHalContext_Instance())
                {
                    var universal = halContext.GetUniversalAssembly(autoConnect);

                    universal.HalConnect();
                    //for (var i= 0;i < DrawerKeys.Count;i++  )
                    // for (var i = 0; i < DrawerKeys.Count; i++)
                        for (var i =0; i <20; i++)
                        {
                        var drawer = halContext.GetDrawer(DrawerKeys[i], true);
                        Debug.WriteLine("Drawer Initial, DeviceIndex=" + drawer.DeviceId);
                        try
                        {
                           drawer.CommandINI();
                          
                          //  drawer.CommandTrayMotionOut();
                            drawer.OnButtonEventHandler += (sender, e) =>
                            {
                                var rtnDrawer = ((IMacHalDrawer)sender);
                                Debug.WriteLine("(Ut003)Invoke OnButtonEventHandler,  Drawer= " + rtnDrawer.DeviceId  );
                                rtnDrawer.CommandPositionRead();
                            };
                            drawer.OnPositionStatusHandler += (sender, e) =>
                            {
                               
                                var eventArgs = (OnReplyPositionEventArgs)e;
                                var rtnDrawer = ((IMacHalDrawer)sender);
                                Debug.WriteLine("(Ut003)Invoke OnPositionStatusHandler,  Drawer= " + rtnDrawer.DeviceId + ", IHOStatus=" + eventArgs.IHOStatus);
                                if (eventArgs.IHOStatus == "111")   //在Home, 往外推
                                {
                                    rtnDrawer.CommandTrayMotionOut();
                                }
                                else
                                {
                                    rtnDrawer.CommandTrayMotionHome();
                                }
                            };
                        }
                        catch(Exception ex)
                        {
                            Debug.WriteLine("Drawer Exception,  Message=" + ex.Message);
                        }
                    }

                    Repeat();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MacHalContext Exception, Message=" + ex.Message);
            }
        }

        [TestMethod]
        public void Test_Drawer()
        {
            try
            {
                using (var halContext = MacHalContextExtends.Create_MacHalContext_Instance())
                {
                    var universal = halContext.GetUniversalAssembly(true);
                    universal.HalConnect();

                    // get drawer
                    var drawer0 = halContext.GetDrawer(EnumMacDeviceId.cabinet_drawer_01_04,true);
                   // var drawer1 = halContext.GetDrawer(MacEnumDevice.cabinet_drawer_04_02, true);
                    //var drawer2 = halContext.GetDrawer(MacEnumDevice.cabinet_drawer_04_04, true);
                    //var drawer3 = halContext.GetDrawer(MacEnumDevice.cabinet_drawer_03_02, true);
                    //var drawer4 = halContext.GetDrawer(MacEnumDevice.cabinet_drawer_03_03, true);
                    //var drawer5 = halContext.GetDrawer(MacEnumDevice.cabinet_drawer_03_04, true);
                 //   drawer0.CommandTrayMotionIn();
                 //  drawer1.CommandTrayMotionIn();
                 //  drawer2.CommandTrayMotionIn();
                 // drawer3.CommandTrayMotionIn();
                 // drawer4.CommandTrayMotionIn();
                 // drawer5.CommandTrayMotionIn();
                    //drawer0.MoveTrayToHome();
                    //drawer1.MoveTrayToHome();
                    //drawer2.MoveTrayToHome();
                   drawer0.CommandINI();
                  

                    // drawer command

                               Repeat();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MacHalContext Exception, Message=" + ex.Message);
            }
        }

        void Repeat()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(10);
            }

        }
    }
}
