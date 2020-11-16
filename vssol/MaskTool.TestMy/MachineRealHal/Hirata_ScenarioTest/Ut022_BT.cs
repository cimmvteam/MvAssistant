using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.TestMy.MachineRealHal.Hirata_ScenarioTest.Extends;
using MvAssistant.Mac.v1_0;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.JSon;
using MvAssistant.Mac.v1_0.JSon.RobotTransferFile;
using MvAssistant.Mac.v1_0.Manifest;

namespace MvAssistant.Mac.TestMy.MachineRealHal.Hirata_ScenarioTest
{
    [TestClass]
    public class Ut022_BT
    {
        List<MacEnumDevice> DrawerKeys;
        List<BoxrobotTransferLocation> DrawerLocations;
        BoxrobotTransferPathFile pathFileObj;//= new BoxrobotTransferPathFile(PositionInstance.BTR_Path);

        /// <summary>建構式</summary>
        public Ut022_BT()
        {
            DrawerKeys = HalDrawerExtends.DrawerKeys;
            DrawerLocations = HalDrawerExtends.DrawerLocations;

            PositionInstance.Load(); // 在這裏載入所有(Boxtransfer 及 Masktransfer)的路徑點位資料
            pathFileObj = new BoxrobotTransferPathFile(PositionInstance.BTR_Path);
        }

        [TestMethod]
        [DataRow(BoxType.IronBox, true)] // 鐵盒
        //[DataRow(BoxType.CrystalBox,true)]  // 水晶盒
        public void TestMethod1(BoxType boxType, bool autoConnect)
        {
            var BREAK_POINT = 0;
            using (var halContext = MacHalContextExtends.Create_MacHalContext_Instance())
            {
                try
                {

                    var uni = halContext.GetUniversalAssembly(autoConnect);
                    var bt = halContext.GetBoxTransferAssembly(autoConnect);
                    var os = halContext.GetOpenStageAssembly(autoConnect);
                    string btMovePathFile = default(string);
                    if (!autoConnect)
                    {
                        uni.HalConnect();
                        bt.HalConnect();
                        os.HalConnect();
                    }

                    halContext.DrawersConnect();

                    bt.Initial();
                    for (var i = 0; i < DrawerKeys.Count; i++)
                    {
                        try
                        {
                            BREAK_POINT = 0;

                            var drawerLocation = DrawerLocations[i];
                            var drawer = halContext.GetDrawer(DrawerKeys[i]);

                            var drawerHome = drawerLocation.GetCabinetHomeCode().Item2;

                            drawer.Initial();

                            /** 01 光罩鐵盒放置於Drawer內*/
                            // [1.1] Tray 移到 Out
                            drawer.MoveTrayToOut();
                            // [1.2] 移到 Out 之後等待手動放入鐵盒
                            BREAK_POINT = 0;

                            // [1.3] Drawer往機台內部移動到Box Robot可以取得光罩的位置
                            drawer.MoveTrayToHome();
                            drawer.MoveTrayToIn();

                            /** 02 Box Robot從Home點至Drawer entry處*/
                            if (drawerHome == BoxrobotTransferLocation.Cabinet_01_Home)
                            {  // Cabinet 1  Drawer 的 Path
                                btMovePathFile = pathFileObj.FromCabinet01HomeToDrawer_GET_PathFile(drawerLocation); // Box Transfer 要去 Drawer 夾盒子, 所以用 GET
                            }
                            else //if(drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                            {
                                //Cabinet 2 Drawer 的 Path
                                btMovePathFile = pathFileObj.FromCabinet02HomeToDrawer_GET_PathFile(drawerLocation); // Box Transfer 要去 Drawer 夾盒子, 所以用 GET
                            }
                            bt.Move(btMovePathFile);

                            /** 03 (編號13-CCD): 開啟光源 -> 拍照(FOV正確) -> 關閉光源*/
                            bt.LightForGripper(200);
                            bt.Camera_CapToSave("D:/Image/BT/Gripper", "jpg");
                            bt.LightForGripper(0);

                            /** 04 Box Robot從Drawer Entry處移至Drawer內進行光罩鐵盒夾取*/
                            bt.Clamp((uint)boxType);

                            /** 05 光罩鐵盒夾取前確認Box Robot是水平狀態 (by 水平儀)*/
                            var Level = bt.ReadLevelSensor();

                            /** 06 Box Robot將光罩鐵盒從Drawer移動至Open Stage上方*/
                            // 6.1 回到 Cabinet 1 Home
                            if (drawerHome == BoxrobotTransferLocation.Cabinet_01_Home)
                            {
                                btMovePathFile = pathFileObj.FromDrawerToCabinet01Home_GET_PathFile(drawerLocation); // Box Transfer 夾到盒子後,由 Drawer 回到 Home, 用GET
                            }
                            else // if(drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                            {
                                btMovePathFile = pathFileObj.FromDrawerToCabinet02Home_GET_PathFile(drawerLocation);// Box Transfer 夾到盒子後,由 Drawer 回到 Home, 用GET
                            }
                            bt.Move(btMovePathFile);
                            if (drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                            { bt.TurnToCB1Home(); }
                            // 6.2 boxrobot 移到 OpenStage
                            os.ReadRobotIntrude(true, null);
                            btMovePathFile = pathFileObj.FromCabinet01HomeToOpenStage_PUT_PathFile(); // boxrobot 目前有盒子, 要到 Open Stage, 用PUT
                            bt.Move(btMovePathFile);

                            /** 07 光罩鐵盒放置前確認Box Robot是水平狀態 (by 水平儀)*/
                            Level = bt.ReadLevelSensor();

                            /** 08 Box Robot將光罩鐵盒放置於Open Stage上*/
                            bt.Unclamp();

                            /** 09 Box Robot退出Open Stage, 並回到Box Robot Home點*/
                            btMovePathFile = pathFileObj.FromOpenStageToCabinet01Home_PUT_PathFile(); // boxrobot 目前有盒子,要回到 Cabinet 1 Home, 用 GET
                            bt.Move(btMovePathFile);

                            /** 10 (編號9-CCD): 開啟光源 -> 拍照(FOV正確) -> 關閉光源*/
                            os.LightForTopBarDfsSetValue(200);
                            os.Camera_Top_CapToSave("D:/Image/OS/Top", "jpg");
                            os.LightForTopBarDfsSetValue(0);

                            /** 11 (編號12-CCD): 開啟光源 -> 拍照(FOV正確) -> 關閉光源*/
                            os.LightForSideBarDfsSetValue(200);
                            os.Camera_Side_CapToSave("D:/Image/OS/Side", "jpg");
                            os.LightForSideBarDfsSetValue(0);

                            /** 12 Box Robot(無夾持光罩鐵盒)從Home點移動進入Open Stage上方*/
                            /** 13 光罩鐵盒夾取前確認Box Robot是水平狀態 (by 水平儀)*/
                            /** 14 Box Robot從Open Stage上方夾取光罩鐵盒*/
                            /** 15 Box Robot將光罩鐵盒移動至Drawer處*/
                            /** 16 (編號13-CCD): 開啟光源 -> 拍照(FOV正確) -> 關閉光源*/
                            /** 17 Box Robot將光罩鐵盒移動至Drawer內*/
                            /** 18 Box Robot (無夾持光罩鐵盒) 從Drawer移回Home點*/
                            /** 19 重複1~18步驟, 完成20個Drawer的光罩鐵盒測試*/
                            /** 20 重複1~19步驟, 完成20個Drawer的光罩水晶盒測試*/





                            /** 03 Box Robot從Home點至Drawer夾取前的檢查點位*/
                            // 3.1 BoxTransfer 轉到 Cabitnet 1 Home 
                            bt.TurnToCB1Home();
                            //*******[BR]*******
                            // 3.2 如果是 Cabitnet 2 的Drawer, 再轉到 Cabinet 2 Home
                            if (drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                            {
                                bt.TurnToCB2Home();
                            }


                            BREAK_POINT = 0;


                            /** 04 (編號13-CCD): 開啟光源 -> 拍照(FOV正確) -> 關閉光源, 確認Drawer中有光罩鐵盒*/ //(先不測)


                            BREAK_POINT = 0;


                            /** 5 Box Robot移動至Drawer夾取點, 進行光罩鐵盒夾取 (clamp光罩鐵盒)*/
                            // 5.1 Boxtransfer 移到夾取點
                            if (drawerHome == BoxrobotTransferLocation.Cabinet_01_Home)
                            {  // Cabinet 1  Drawer 的 Path
                                btMovePathFile = pathFileObj.FromCabinet01HomeToDrawer_GET_PathFile(drawerLocation); // Box Transfer 要去 Drawer 夾盒子, 所以用 GET
                            }
                            else //if(drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                            {
                                //Cabinet 2 Drawer 的 Path
                                btMovePathFile = pathFileObj.FromCabinet02HomeToDrawer_GET_PathFile(drawerLocation); // Box Transfer 要去 Drawer 夾盒子, 所以用 GET
                            }
                            bt.Move(btMovePathFile);

                            // 照相
                            bt.Camera_CapToSave("D:/Image/BT/Gripper", "jpg");

                            // 5.2 夾取
                            bt.Clamp((uint)boxType);

                            BREAK_POINT = 0;

                            /** 6 Box Robot將光罩鐵盒從Drawer移動至Open Stage Entry處*/
                            // 6.1 回到 Cabinet 1 Home
                            if (drawerHome == BoxrobotTransferLocation.Cabinet_01_Home)
                            {
                                btMovePathFile = pathFileObj.FromDrawerToCabinet01Home_GET_PathFile(drawerLocation); // Box Transfer 夾到盒子後,由 Drawer 回到 Home, 用GET
                            }
                            else // if(drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                            {
                                btMovePathFile = pathFileObj.FromDrawerToCabinet02Home_GET_PathFile(drawerLocation);// Box Transfer 夾到盒子後,由 Drawer 回到 Home, 用GET
                            }
                            bt.Move(btMovePathFile);
                            if (drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                            { bt.TurnToCB1Home(); }
                            // 6.2 boxrobot 移到 OpenStage
                            os.ReadRobotIntrude(true, null);
                            btMovePathFile = pathFileObj.FromCabinet01HomeToOpenStage_PUT_PathFile(); // boxrobot 目前有盒子, 要到 Open Stage, 用PUT
                            bt.Move(btMovePathFile);


                            BREAK_POINT = 0;

                            /** 7 Box Robot將光罩鐵盒從Open Stage Entry處, 移至Drawer可放置光罩盒的位置, 並且放置光罩鐵盒 (release光罩鐵盒)*/
                            // 7.1 Boxtransfer 回到 Cabinet 1 Home
                            btMovePathFile = pathFileObj.FromOpenStageToCabinet01Home_GET_PathFile(); // boxrobot 目前有盒子,要回到 Cabinet 1 Home, 用 GET
                            bt.Move(btMovePathFile);
                            // 7.2 如果是 Cabinet 2 的 Drawer, 轉向 Cabinet 2 Home
                            if (drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                            {
                                bt.TurnToCB2Home();
                            }
                            // 7.3 移到 Drawer
                            if (drawerHome == BoxrobotTransferLocation.Cabinet_01_Home)
                            {
                                btMovePathFile = pathFileObj.FromCabinet01HomeToDrawer_PUT_PathFile(drawerLocation); // boxrobot 目前有盒子,要到 Drawer, 所以用 PUT
                            }
                            else //if(drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                            {
                                btMovePathFile = pathFileObj.FromCabinet01HomeToDrawer_PUT_PathFile(drawerLocation); // boxrobot 目前有盒子,要到 Drawer, 所以用 PUT
                            }
                            bt.Move(btMovePathFile);
                            // 7.3 放下 Box
                            bt.Unclamp();

                            BREAK_POINT = 0;

                            /** 8 Box Robot (無夾持光罩盒) 從Drawer移回Home點*/
                            if (drawerHome == BoxrobotTransferLocation.Cabinet_01_Home)
                            {
                                btMovePathFile = pathFileObj.FromDrawerToCabinet01Home_PUT_PathFile(drawerLocation); // 
                            }
                            else //if(drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                            {
                                btMovePathFile = pathFileObj.FromDrawerToCabinet02Home_PUT_PathFile(drawerLocation); // T
                            }
                            bt.Move(btMovePathFile);
                            if (drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                            {
                                bt.TurnToCB1Home();
                            }
                            BREAK_POINT = 0;

                            /** 9 Drawer回到Cabinet內*/
                            drawer.MoveTrayToHome();

                            // 10.重複1~9步驟, 完成20個Drawer的光罩鐵盒測試
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    //12.重複1~9步驟, 完成20個Drawer的光罩盒測試

                }
                catch (Exception ex)
                {


                }

            }
        }
    }
}
