﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.DeviceDrive.KjMachineDrawer.DrawerEventArgs;
using MvAssistant.Mac.TestMy.MachineRealHal.Hirata_ScenarioTest.Extends;
using MvAssistant.Mac.v1_0;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Hal.CompDrawer;
using MvAssistant.Mac.v1_0.JSon;
using MvAssistant.Mac.v1_0.JSon.RobotTransferFile;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.TestMy.MachineRealHal.Hirata_ScenarioTest
{
    [TestClass]
    public class Ut001_002_004_005
    {
        List<MacEnumDevice> DrawerKeys;
        List<BoxrobotTransferLocation> DrawerLocations;
        BoxrobotTransferPathFile pathFileObj;
        MacHalContext HalContext;
        MacHalUniversal HalUniversal;
        MacHalOpenStage HalOpenStage;
        MacHalBoxTransfer HalBoxTransfer;
        string filePath;
        public Ut001_002_004_005()
        {
            DrawerKeys = HalDrawerExtends.DrawerKeys;
            DrawerLocations = HalDrawerExtends.DrawerLocations;

            PositionInstance.Load(); // 在這裏載入所有(Boxtransfer 及 Masktransfer)的路徑點位資料
            pathFileObj = new BoxrobotTransferPathFile(PositionInstance.BTR_Path);

            HalContext = MacHalContextExtends.Create_MacHalContext_Instance();

            HalUniversal = HalContext.GetUniversalAssembly();
            HalOpenStage = HalContext.GetOpenStageAssembly();
            HalBoxTransfer = HalContext.GetBoxTransferAssembly();

            HalUniversal.HalConnect();
            HalOpenStage.HalConnect();
            HalBoxTransfer.HalConnect();


            // Initial
            HalOpenStage.Initial();
            HalBoxTransfer.Initial();
            HalBoxTransfer.TurnOffCameraLight();
            HalBoxTransfer.TurnToCB1Home();

        }

        /// <summary>
        /// <para>1-01. 光罩盒放 Drawer 內</para>
        /// <para>1-02. Drawer 往機台內部移動 到Box Robot 可以存光罩鐵盒的位置</para>
        /// <para>1-03. Box Robot 從Home 點至Drawer 夾取點, 進行光罩鐵盒夾取(Clamp光鐵盒)</para>
        /// <para>1-04. Box Robot 將光罩盒從Drawer 移動到Open stage Entry</para>
        /// <para>1-05. Drawer 回到Cabinet 內</para>
        /// <para>1-06. Box Robot將光罩盒放在 Open Stage平台上</para>
        /// <para>1-07. Box Robot(無夾持光罩盒) 從Open Stage 移回 Home 點</para>
        /// --------------------------------------------------------
        /// <para>2-01. 光罩鐵盒位於於Open Stage平台上</para>
        /// <para>2-02. Drawer往機台內部移動到Box Robot可以存取光罩鐵盒的位置</para>
        /// <para>2-03. Box Robot從Home點至Open Stage進行光罩鐵盒夾取</para>
        /// <para>2-04. Box Robot從Home點至Open Stage進行光罩鐵盒夾取</para>
        /// <para>2-05. Box Robot退回Home點</para>
        /// <para>2-06. Drawer回到Cabinet內</para>
        /// <para>2-07. 測試(編號13-CCD): 開啟光源 -> 拍照-> 關閉光源, 功能是否正常</para>
        /// </summary>
        /// <paramref name="boxType"/>
        /// <paramref name="autoConnect"/>
        /// <paramref name="getComeraShot"/>
        [TestMethod]
        [DataRow(BoxType.IronBox,true)]
      //  [DataRow(BoxType.CrystalBox,true)]
        public void Test_MainMethod(BoxType boxType, bool getComeraShot)
        {
            /** Index & array
             * [0]   [1]  [2]    
             * 1-2,  1-3, 1-4     IP: 31~33
             * ------------------
             * [3]   [4]  [5]    
             * 2-2,  2-3, 2-4,    IP: 41~43
             * ------------------
             * [6]   [7]  [8]    
             * 3-2,  3-3, 3-4,    IP: 51~53
             * ----------------
             * [9]   [10] [11]    
             * 4-2,  4-3, 4-4,    IP: 61~63
             * ------------------
             * [12]  [13] [14]   
             * 5-2,  5-3, 5-4,    IP: 71~73
             * ------------------
             * [15]  [16] [17] 
             * 6-2,  6-3, 6-4,    IP: 81~83
             *--------------------
             * [18]  [19]    
             * 7-2,  7-3,         IP: 91~92
             */
           

            int BREAK_POINT = 0;
         
            
            // Connect, 所有Drawer 
            var failedConnectDrawers =HalContext.DrawersConnect();

          
            for(var i = 0; i < DrawerKeys.Count; i++)
             {
                try
                {
                    var drawerKey = DrawerKeys[i];
                    var drawerLocation = DrawerLocations[i];
                    var drawer = HalContext.GetDrawer(drawerKey);
                    var drawerHome = drawerLocation.GetCabinetHomeCode().Item2;
                    HalBoxTransfer.TurnToCB1Home();

                    // 1-01 光罩盒在 Drawer 內
                    BREAK_POINT++;// 確認光罩盒在 Drawer 內

                    // 1-02  Drawer 往機台內部移動 到Box Robot 可以存光罩鐵盒的位置
                    drawer.MoveTrayToIn();


                    BREAK_POINT++;  // 確試 Drawer Tray  已經移到 In

                    // 1-03 Box Robot 從Home 點至Drawer 夾取點, 進行光罩鐵盒夾取(Clamp光鐵盒)
                    if (drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                    {
                        HalBoxTransfer.TurnToCB2Home();
                    }
                    filePath = pathFileObj.GetFromCabinetHomeToDrawerGetPath(drawerLocation);
                    HalBoxTransfer.Move(filePath);

                    BREAK_POINT++;  // 確認 BoxTransfer 是否到了Drawer 

                    HalBoxTransfer.Clamp((uint)boxType);


                    BREAK_POINT++;  // 確認 Box robot 是否夾到了 盒子

                    //1-04. Box Robot 將光罩盒從Drawer 移動到Open stage Entry</para>
                    filePath = pathFileObj.GetFromDrawerToCabitnetHomeGetPath(drawerLocation);
                    HalBoxTransfer.Move(filePath);

                    BREAK_POINT++;  // 確認 Box robot 是否到了   Cabinet 1( or 2) Home

                    if (drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                    {
                        HalBoxTransfer.TurnToCB1Home();
                        BREAK_POINT++;  // 確認 Box robot 是否到了   Cabinet 1 Home
                    }



                    HalOpenStage.ReadRobotIntrude(true, null);
                    HalOpenStage.SortUnclampAndLock();


                    BREAK_POINT++; // 確認 OPen Stage 的狀態是否可以放入盒子


                    filePath = pathFileObj.FromCabinet01HomeToOpenStage_PUT_PathFile();
                    HalBoxTransfer.Move(filePath);

                    BREAK_POINT++; // 確認 BOx Robot 是否已到Open Stage


                    // 1-05. Drawer 回到Cabinet 內
                    drawer.MoveTrayToHome();

                    // 1-06. Box Robot將光罩盒放在 Open Stage平台上
                    HalBoxTransfer.Unclamp();

                    // 1-07.Box Robot(無夾持光罩盒) 從Open Stage 移回 Home 點
                    filePath = pathFileObj.FromOpenStageToCabinet01Home_PUT_PathFile();
                    HalBoxTransfer.Move(filePath);

                

                    BREAK_POINT++;// 確認 BoxRobot 是否回到 Cabitnet 1 Home 

                    HalOpenStage.ReadRobotIntrude(false, null);

                    BREAK_POINT++;// 

                    HalOpenStage.SetBoxTypeAndSortClamp(boxType);

                    BREAK_POINT++;// 確認 OPenStage  是否  已固定  

                    // 2-01 光罩鐵盒位於於Open Stage平台上
                    // 目視,　確認
                    BREAK_POINT++;

                    // 2-02. Drawer往機台內部移動到Box Robot可以存取光罩鐵盒的位置
                    drawer.CommandTrayMotionIn();

                    BREAK_POINT++;// 確認 Drawer Tray 是否在 In

                    // 2-03. Box Robot從Home點至Open Stage進行光罩鐵盒夾取
                    HalOpenStage.ReadRobotIntrude(true, null);
                    HalOpenStage.SortUnclampAndLock();

                    BREAK_POINT++;// 確認 Open Stage 是否已經放開鐵盒 ?
                     
                    filePath = pathFileObj.FromCabinet01HomeToOpenStage_GET_PathFile();
                    HalBoxTransfer.Move(filePath);

                    BREAK_POINT++; // 確認 Boxrobot 是否已經到了 Cabitnet 1 Home

                    HalBoxTransfer.Clamp((uint)boxType);

                    BREAK_POINT++; // 確認 Boxrobot 已經夾到盒子

                    // 2-04. Box Robot將光罩鐵盒從Open Stage夾取並放置於Drawer內 (release光罩鐵盒)

                    filePath = pathFileObj.FromOpenStageToCabinet01Home_GET_PathFile();
                    HalBoxTransfer.Move(filePath);

                    BREAK_POINT++; // 確認Boxrobot 已經到 Cabitnet 1 HOme 

                    HalOpenStage.ReadRobotIntrude(false, null);

                    BREAK_POINT++;

                    if (drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                    {
                        HalBoxTransfer.TurnToCB2Home();
                        BREAK_POINT++; // 確認Boxrobot 已經到 Cabitnet 2 HOme 
                    }

                    filePath = pathFileObj.GetFromCabinetHomeToDrawerPutPath(drawerLocation);
                    HalBoxTransfer.Move(filePath);

                    BREAK_POINT++; // 1. 確認 DRAWER Tray 是否在In, 2.確認 BoxTRansfer 是否到位 

                    HalBoxTransfer.Unclamp();

                    BREAK_POINT++;// 確認 Box 是否已在 Tray 上 

                    // 2-05. Box Robot退回Home點
                    filePath = pathFileObj.GetFromDrawerToCabitnetHomePutPath(drawerLocation);
                    HalBoxTransfer.Move(filePath);

                    BREAK_POINT++; //Box robot  是否到了 Cabinet 1(or 2) Home
                    if (drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                    {
                        HalBoxTransfer.TurnToCB1Home();

                        BREAK_POINT++; //Box robot  是否到了 Cabinet 1 Home
                    }


                    // 2-06. Drawer回到Cabinet內
                    drawer.Initial();

                    if (getComeraShot)
                    {
                        // 2-07.測試(編號13-CCD): 開啟光源 -> 拍照-> 關閉光源, 功能是否正常
                        HalBoxTransfer.CameraShot("Ut001_002_004_005");
                    }
                    BREAK_POINT++;
                }
                catch(Exception ex)
                {
                    Debug.WriteLine("EX=" + ex.Message);
                }
            }

          //  Repeat();
        }


        public void Test_Ut003_CB_ByOP(bool autoConnect)
        {
            try
            {
                using (var halContext = MacHalContextExtends.Create_MacHalContext_Instance())
                {
                    var universal = halContext.GetUniversalAssembly(autoConnect);

                    universal.HalConnect();
                   
                    for (var i = 0; i < DrawerKeys.Count; i++)
                    {
                        var drawer = halContext.GetDrawer(DrawerKeys[i], true);
                        Debug.WriteLine("Drawer Initial, DeviceIndex=" + drawer.DeviceIndex);
                        try
                        {


                            #region command
                            drawer.CommandINI();


                            #endregion command

                            //  drawer.CommandTrayMotionOut();
                            drawer.OnButtonEventHandler += (sender, e) =>
                            {
                                var rtnDrawer = ((IMacHalDrawer)sender);
                                Debug.WriteLine("(Ut003)Invoke OnButtonEventHandler,  Drawer= " + rtnDrawer.DeviceIndex);
                                rtnDrawer.CommandPositionRead();
                            };
                            drawer.OnPositionStatusHandler += (sender, e) =>
                            {

                                var eventArgs = (OnReplyPositionEventArgs)e;
                                var rtnDrawer = ((IMacHalDrawer)sender);
                                Debug.WriteLine("(Ut003)Invoke OnPositionStatusHandler,  Drawer= " + rtnDrawer.DeviceIndex + ", IHOStatus=" + eventArgs.IHOStatus);
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
                        catch (Exception ex)
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





        /// <summary>綁定事件</summary>
        void  BindEvent(  List<MacEnumDevice> connectErrDrawerKey)
        {
            foreach(var drawerKey in DrawerKeys)
            {
                if (connectErrDrawerKey.Contains(drawerKey)) { return; }
                var drawer = HalContext.GetDrawer(drawerKey);
                drawer.OnButtonEventHandler += (sender, e) =>
                {
                    drawer.CommandPositionRead();
                };
                drawer.OnPositionStatusHandler += (sender, e) =>
                {
                    var eventArgs = (OnReplyPositionEventArgs)e;
                    var rtnDrawer = ((IMacHalDrawer)sender);
                    Debug.WriteLine("Invoke OnPositionStatusHandler,  Drawer= " + rtnDrawer.DeviceIndex + ", IHOStatus=" + eventArgs.IHOStatus);
                    if (eventArgs.IHOStatus == "111")   //在Home, 往外推
                    {
                        rtnDrawer.MoveTrayToOut();
                    }
                    else
                    {
                        rtnDrawer.MoveTrayToHome();
                    }
                };
                drawer.OnTrayArriveHomeHandler += (sender, e) =>
                {
                    var rtnDrawer = ((IMacHalDrawer)sender);
                   // Debug.WriteLine("Invoke OnPositionStatusHandler,  Drawer= " + rtnDrawer.DeviceIndex + ", IHOStatus=" + eventArgs.IHOStatus);
                };
                drawer.OnTrayArriveOutHandler += (sender, e) =>
                {
                    var rtnDrawer = ((IMacHalDrawer)sender);
                    //Debug.WriteLine("Invoke OnPositionStatusHandler,  Drawer= " + rtnDrawer.DeviceIndex + ", IHOStatus=" + eventArgs.IHOStatus);
                };
                drawer.OnTrayArriveInHandler += (sender, e) =>
                {
                    var rtnDrawer = ((IMacHalDrawer)sender);
                    //ebug.WriteLine("Invoke OnPositionStatusHandler,  Drawer= " + rtnDrawer.DeviceIndex + ", IHOStatus=" + eventArgs.IHOStatus);
                    // 1.3  BoxRobot 從 Home 點移到 Drawer 夾取點,進行光罩鐵盒夾取(Clamp)
                };

            }
        }

        public void Repeat()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(10);
            }
        }
    }
}
