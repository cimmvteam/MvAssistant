using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.DeviceDrive.KjMachineDrawer.DrawerEventArgs;
using MvAssistant.Mac.TestMy.MachineRealHal.Hirata_ScenarioTest.Extends;
using MvAssistant.Mac.TestMy.MachineRealHal.Hirata_ScenarioTest.Types;
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
        public bool InitialError = false;
        public string InitialErrorMessage;
        string filePath;
        public Ut001_002_004_005()
        {
            try
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
            //    HalOpenStage.ReadRobotIntrude(false, false);
                HalOpenStage.Initial();
                HalBoxTransfer.Initial();
                HalBoxTransfer.TurnOffCameraLight();
            }
            catch(Exception ex)
            {
               
                InitialError = true;
                InitialErrorMessage = ex.Message;
            }
           // HalBoxTransfer.TurnToCB1Home();

           

        }
        [TestMethod]
        public void TestDrawer()
        {
            if (InitialError)
            {
                var message = InitialErrorMessage;
                return;
            }
            var failedConnectDrawers = HalContext.DrawersConnect();


            int start = 0;// 0; // 3-2~ 4-4
            int end = DrawerKeys.Count;// DrawerKeys.Count;
            //int end = DrawerKeys.Count;
            for (var i = start; i < end; i++) {


                try
                {
                    var drawer = HalContext.GetDrawer(DrawerKeys[i]);
                    /** drawer.Initial();
                     drawer.MoveTrayToOut();
                     drawer.MoveTrayToHome();
                     drawer.MoveTrayToIn();
                     drawer.MoveTrayToHome();
                     **/
                    drawer.CommandINI();
                    //Repeat();
                    Debug.WriteLine(DrawerKeys[i] + ", [OK]");
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(DrawerKeys[i] + ", [Error], Message=" + ex.Message);
                }
                
            }
            Repeat();
        }

        [TestMethod]
        public void TestFilePath()
        {
            PositionInstance.Load(); // 在這裏載入所有(Boxtransfer 及 Masktransfer)的路徑點位資料
            pathFileObj = new BoxrobotTransferPathFile(PositionInstance.BTR_Path);
            var path00=  pathFileObj.GetFromCabinetHomeToDrawerGetPath(BoxrobotTransferLocation.Drawer_01_01);
            var path01 =  pathFileObj.GetFromCabinetHomeToDrawerPutPath(BoxrobotTransferLocation.Drawer_01_01);
            var path02= pathFileObj.GetFromCabinetHomeToDrawerGetPath(BoxrobotTransferLocation.Drawer_04_01);
            var path03 = pathFileObj.GetFromCabinetHomeToDrawerPutPath(BoxrobotTransferLocation.Drawer_04_01);

            //pathFileObj = new BoxrobotTransferPathFile(PositionInstance.BTR_Path);
            var path04 = pathFileObj.GetFromDrawerToCabitnetHomeGetPath(BoxrobotTransferLocation.Drawer_01_01);
            var path05 = pathFileObj.GetFromDrawerToCabitnetHomePutPath(BoxrobotTransferLocation.Drawer_01_01);
            var path06 = pathFileObj.GetFromDrawerToCabitnetHomeGetPath(BoxrobotTransferLocation.Drawer_04_01);
            var path07 = pathFileObj.GetFromDrawerToCabitnetHomePutPath(BoxrobotTransferLocation.Drawer_04_01);
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
        /// <param name="boxType">盒子種類</param>
        /// <param name="getComeraShot">是否照相</param>
        /// <param name="drawerReplaceBoxPlace">更換盒子的地方</param>
        [TestMethod]
        // [DataRow(BoxType.IronBox,true,ReplaceBoxPlace.In)]
        [DataRow(BoxType.CrystalBox,true, DrawerReplaceBoxPlace.In)]
        public void Test_MainMethod(BoxType boxType, bool getComeraShot, DrawerReplaceBoxPlace drawerReplaceBoxPlace)
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


            if (InitialError)
            {
                var message = InitialErrorMessage;
                return;
            }

            int BREAK_POINT = 0;
         
            
            // Connect, 所有Drawer 
            var failedConnectDrawers =HalContext.DrawersConnect();


            int start =2;// 0; // 3-2~ 4-4
            int end = 6;// DrawerKeys.Count;// DrawerKeys.Count;
            //int end = DrawerKeys.Count;
            for (var i = start; i < end; i++)
             {
                var drawerKey = DrawerKeys[i];
                var drawerLocation = DrawerLocations[i];
                try
                {
                   
                    var drawer = HalContext.GetDrawer(drawerKey);
                    IMacHalDrawer previousDrawer = null;
                    var drawerHome = drawerLocation.GetCabinetHomeCode().Item2;
                    
                    if (i != start)
                    {
                        // 取得前一個 Drawer
                        previousDrawer = HalContext.GetDrawer(DrawerKeys[i-1]);
                        // 將前一個 Drawer Tray 移回 Home 點
                        previousDrawer.MoveTrayToHome();
                        // 將 前一個 Drawer 移到可以 抽換 Box 的位置
                        if (drawerReplaceBoxPlace == DrawerReplaceBoxPlace.In)
                        {
                            previousDrawer.MoveTrayToIn();
                        }
                        else
                        {
                            previousDrawer.MoveTrayToOut();
                        }
                    }

                    /** 004-01 光罩盒在 Drawer 內 */
                    drawer.Initial();
                    // 將 目前 Drawer 的 Tray 移到可抽換Box 的位置
                    if ( drawerReplaceBoxPlace==  DrawerReplaceBoxPlace.In)
                    {
                        drawer.MoveTrayToIn();
                    }
                    else
                    {
                        drawer.MoveTrayToOut();
                    }
                 

                    BREAK_POINT++;// [[[[[[[[一定要暫停]]]]]]]]]]]]     將 Box 放入 Tray 中 (如果是第2個及第2個以後的 Drawer, 將 盒子從前一個測試的 Drawer 取出來, 放到這個 Drawer 當中)

                    // 前個 Drawer Tray 回 Home
                    if (previousDrawer != null)
                    {
                        previousDrawer.MoveTrayToHome();
                    }


                    /** 004-02  Drawer 往機台內部移動 到Box Robot 可以存光罩鐵盒的位置 */
                    // Drawer Tray 回 Home
                    drawer.MoveTrayToHome();
                    // Drawer Tray 移到  Box Robot 可以取得 光罩鐵盒的位置
                    drawer.MoveTrayToIn();
                    

                    BREAK_POINT++;  // 確試 Drawer Tray  已經移到 In

                    /** 004-03 Box Robot 從Home 點至Drawer 夾取點, 進行光罩鐵盒夾取(Clamp光鐵盒)*/
                    // BoxRobot 回 Cabinet 1 Home 
                    HalBoxTransfer.TurnToCB1Home();
                    // Drawer 04-01 ~ Drawer 07-05 回 Cabitnet 1 Home 轉向 Cabitnet 2 Home
                    if (drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                    {  
                        HalBoxTransfer.TurnToCB2Home();
                    }
                    filePath = pathFileObj.GetFromCabinetHomeToDrawerGetPath(drawerLocation);
                    HalBoxTransfer.Move(filePath);

                    BREAK_POINT++;  // 確認 BoxTransfer 是否到了Drawer 
                    // 夾取
                    HalBoxTransfer.Clamp((uint)boxType);


                    BREAK_POINT++;  // 確認 Box robot 是否夾到了 盒子

                    /**004-04. Box Robot 將光罩盒從Drawer 移動到Open stage Entry</para>*/
                    // Box robot 回 Home
                    filePath = pathFileObj.GetFromDrawerToCabitnetHomeGetPath(drawerLocation);
                    HalBoxTransfer.Move(filePath);

                    BREAK_POINT++;  // 確認 Box robot 是否到了   Cabinet 1( or 2) Home
                    
                    //  Drawer 04-01 ~ Drawer 07-05 回 Cabitnet 1 Home 
                    if (drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                    {  
                        HalBoxTransfer.TurnToCB1Home();
                        BREAK_POINT++;  // 確認 Box robot 是否到了   Cabinet 1 Home
                    }


                    // OpenStage 入侵
                    HalOpenStage.ReadRobotIntrude(true, null);
                    //HalOpenStage.SortUnclampAndLock();

                    BREAK_POINT++; // 確認 OPen Stage 的狀態是否可以放入盒子

                    // Boxrobot 移到 Open Stage
                    filePath = pathFileObj.FromCabinet01HomeToOpenStage_PUT_PathFile();
                    HalBoxTransfer.Move(filePath);

                    BREAK_POINT++; // 確認 BOx Robot 是否已到Open Stage


                    /** 004-05. Drawer 回到Cabinet 內 */
                    drawer.MoveTrayToHome();

                    /** 004-06. Box Robot將光罩盒放在 Open Stage平台上 */
                    HalBoxTransfer.Unclamp();

                    /** 004-07.Box Robot(無夾持光罩盒) 從Open Stage 移回 Home 點*/
                    // Boxrobot 回 Cabitnet 1 Home
                    filePath = pathFileObj.FromOpenStageToCabinet01Home_PUT_PathFile();
                    HalBoxTransfer.Move(filePath);

                    BREAK_POINT++;// 確認 BoxRobot 是否回到 Cabitnet 1 Home 

                    // 解除 Open Stage 入侵
                    HalOpenStage.ReadRobotIntrude(false, null);

                    BREAK_POINT++;// 

                   // HalOpenStage.SetBoxTypeAndSortClamp(boxType);

                    //BREAK_POINT++;// 確認 OPenStage  是否  已固定  

                    /** 005-01 光罩鐵盒位於於Open Stage平台上 */
                    // 目視,　確認
                    BREAK_POINT++;

                    /** 005-02. Drawer往機台內部移動到Box Robot可以存取光罩鐵盒的位置 */
                    drawer.MoveTrayToIn();

                    BREAK_POINT++;// 確認 Drawer Tray 是否在 In

                    /** 005-03. Box Robot從Home點至Open Stage進行光罩鐵盒夾取*/
                    // 入侵 OpenStage
                    HalOpenStage.ReadRobotIntrude(true, null);
                  //  HalOpenStage.SortUnclampAndLock();

                    BREAK_POINT++;// 確認 Open Stage 是否已經放開鐵盒 ?
                    
                    // Boxrobot 移到 Open Stage 
                    filePath = pathFileObj.FromCabinet01HomeToOpenStage_GET_PathFile();
                    HalBoxTransfer.Move(filePath);

                    BREAK_POINT++; // 確認 Boxrobot 是否已經到了 Cabitnet 1 Home

                    // Clamp
                    HalBoxTransfer.Clamp((uint)boxType);

                    BREAK_POINT++; // 確認 Boxrobot 已經夾到盒子

                    /** 005-04. Box Robot將光罩鐵盒從Open Stage夾取並放置於Drawer內 (release光罩鐵盒)*/
                    // Boxrobot 回到 Cabinet 1 Home
                    filePath = pathFileObj.FromOpenStageToCabinet01Home_GET_PathFile();
                    HalBoxTransfer.Move(filePath);

                    BREAK_POINT++; // 確認Boxrobot 已經到 Cabitnet 1 HOme 

                    // 解除 入侵
                    HalOpenStage.ReadRobotIntrude(false, null);

                    BREAK_POINT++;

                    if (drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                    {   // Drawer 04-01 ~ Drawer 07-05 , Boxrobot 移到 Cabinet 2 Home
                        HalBoxTransfer.TurnToCB2Home();
                        BREAK_POINT++; // 確認Boxrobot 已經到 Cabitnet 2 HOme 
                    }

                    // Boxrobot 移到 Drawer
                    filePath = pathFileObj.GetFromCabinetHomeToDrawerPutPath(drawerLocation);
                    HalBoxTransfer.Move(filePath);

                    BREAK_POINT++; // 1. 確認 DRAWER Tray 是否在In, 2.確認 BoxTRansfer 是否到位 

                    // Unclamp
                    HalBoxTransfer.Unclamp();

                    BREAK_POINT++;// 確認 Box 是否已在 Tray 上 

                    /** 005-05. Box Robot退回Home點 */
                    // Boxrobot 回 Home 點
                    filePath = pathFileObj.GetFromDrawerToCabitnetHomePutPath(drawerLocation);
                    HalBoxTransfer.Move(filePath);

                    BREAK_POINT++; //Box robot  是否到了 Cabinet 1(or 2) Home
                    if (drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                    {  // Drawer 04-01 ~ Drawer 07-05, Boxrobot 回 Cabitnet 2 Home
                        HalBoxTransfer.TurnToCB1Home();

                        BREAK_POINT++; //Box robot  是否到了 Cabinet 1 Home
                    }


                    /** 005-06. Drawer回到Cabinet內 */
                     drawer.MoveTrayToHome();

                    /** 005-07 測試(編號13-CCD): 開啟光源 -> 拍照-> 關閉光源, 功能是否正常 */
                    if (getComeraShot)
                    {
                        // 2-07.測試(編號13-CCD): 開啟光源 -> 拍照-> 關閉光源, 功能是否正常
                        var lightValue = HalBoxTransfer.GetCameraLightValue(boxType);
                        var resultTemp = HalBoxTransfer.CameraShot("D:/Image/BT/Gripper", "jpg", lightValue);
                    }


                    /** 999999 ok*/
                    BREAK_POINT++;  //[[[[[[[[[[[[[[[[[[一定要暫停]]]]]]]]]]]]]]]]]]]]]]]]]] 準備下一個
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
