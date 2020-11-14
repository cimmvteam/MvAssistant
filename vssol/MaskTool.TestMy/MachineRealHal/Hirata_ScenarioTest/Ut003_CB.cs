using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.TestMy.MachineRealHal.Hirata_ScenarioTest.Extends;
using MvAssistant.Mac.v1_0;
using MvAssistant.Mac.v1_0.JSon.RobotTransferFile;
using MvAssistant.Mac.v1_0.Manifest;

namespace MvAssistant.Mac.TestMy.MachineRealHal.Hirata_ScenarioTest
{
    [TestClass]
    public class Ut003_CB
    {
        List<MacEnumDevice> DrawerKeys;
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
        [DataRow(BoxType.IronBox,false )]// 鐵盒
        //[DataRow(BoxType.CrystalBox, false)]// 水晶盒
        public void Test_Ut003_CB(BoxType boxType  ,bool autoConnect)
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
                         
                            Debug.WriteLine("Drawer DeviceIndex=" + drawer.DeviceIndex);
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
                  
                    halContext.HalConnect();
                    for (var i= 0;i < DrawerKeys.Count;i++  )
                    {
                        var drawer = halContext.GetDrawer(DrawerKeys[i], true);
                        Debug.WriteLine("Drawer Initial, DeviceIndex=" + drawer.DeviceIndex);
                        try
                        {
                            drawer.CommandINI();
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

       void Repeat()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(10);
            }

        }
    }
}
