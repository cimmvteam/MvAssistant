using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.TestMy.MachineRealHal.Hirata_ScenarioTest.Extends;
using MvAssistant.Mac.v1_0;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Hal.CompDrawer;
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
            using (var halContext = new MacHalContext("UserData/Manifest/Manifest.xml.real"))
            {
                halContext.MvCfInit();
                halContext.MvCfLoad();

                try
                {
                    var uni = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                    var bt = halContext.HalDevices[MacEnumDevice.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;
                    var os = halContext.HalDevices[MacEnumDevice.openstage_assembly.ToString()] as MacHalOpenStage;
                    string btMovePathFile = default(string);

                    uni.HalConnect();
                    bt.HalConnect();
                    os.HalConnect();


                    halContext.DrawersConnect();
                  
                    bt.Initial();
                    bt.TurnToCB1Home();
                    for (var i =11; i <12; i++)
                    {
                        try
                        {
                            var drawerLocation = DrawerLocations[i];
                            var drawer = halContext.GetDrawer(DrawerKeys[i]);
                            IMacHalDrawer previousDrawer = null;
                            var drawerHome = drawerLocation.GetCabinetHomeCode().Item2;
                            if (i != 0)
                            {
                                previousDrawer = halContext.GetDrawer(DrawerKeys[i - 1]);
                                try
                                {
                                    previousDrawer.Initial();
                                    previousDrawer.MoveTrayToOut();
                                }
                                catch(Exception ex)
                                {
                                    BREAK_POINT++;
                                }
                            }
                            try
                            {
                                drawer.Initial();
                            }
                            catch(Exception ex)
                            {
                                BREAK_POINT ++ ;
                            }


                            /** 01 光罩鐵盒放置於Drawer內*/
                            // [1.1] Drawer Tray 移到 Out
                            try
                            {
                                drawer.MoveTrayToOut(); 
                            }
                            catch (Exception ex)
                            {
                                BREAK_POINT++;
                            }
                            // [1.2] Draert Tray 移到 Out 之後等待手動放入鐵盒
                            BREAK_POINT ++;
                            if (previousDrawer != null)
                            { // 如有上一個  Drawer, 將上一個 Drawer 移到 Home
                                previousDrawer.CommandINI();
                            }
                            // [1.3] Drawer往機台內部移動到Box Robot可以取得光罩的位置

                            try
                            {
                                drawer.Initial();
                                drawer.MoveTrayToIn();
                            }
                            catch(Exception ex)
                            {
                                BREAK_POINT++;
                            }

                            /** 02 Box Robot從Home點至Drawer entry處*/
                            if (drawerHome == BoxrobotTransferLocation.Cabinet_01_Home)
                            {  // Cabinet 1  Drawer 的 Path
                                btMovePathFile = pathFileObj.FromCabinet01HomeToDrawer_GET_PathFile(drawerLocation); // Box Transfer 要去 Drawer 夾盒子, 所以用 GET
                            }
                            else //if(drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                            {
                                //Cabinet 2 Drawer 的 Path
                                bt.TurnToCB2Home();
                                btMovePathFile = pathFileObj.FromCabinet02HomeToDrawer_GET_PathFile(drawerLocation); // Box Transfer 要去 Drawer 夾盒子, 所以用 GET
                            }
                            bt.Move(btMovePathFile);

                            /** 03 (編號13-CCD): 開啟光源 -> 拍照(FOV正確) -> 關閉光源*/

                            //bt.LightForGripper(200);
                            //bt.Camera_CapToSave("D:/Image/BT/Gripper", "jpg");
                            //bt.LightForGripper(0);
                            var lightValue = bt.GetCameraLightValue(boxType);
                            var resultTemp=bt.CameraShot("D:/Image/BT/Gripper", "jpg",lightValue);
                            




                            /** 04 Box Robot從Drawer Entry處移至Drawer內進行光罩鐵盒夾取*/
                            resultTemp = bt.Clamp((uint)boxType);

                            /** 05 光罩鐵盒夾取前確認Box Robot是水平狀態 (by 水平儀)*/
                            var Level = bt.ReadLevelSensor();

                            /** 06 Box Robot將光罩鐵盒從Drawer移動至Open Stage上方*/
                            // 6.1 回到 Cabinet 1 Home
                            if (drawerHome == BoxrobotTransferLocation.Cabinet_01_Home)
                            {
                                btMovePathFile = pathFileObj.FromDrawerToCabinet01Home_GET_PathFile(drawerLocation); // Box Transfer 夾到盒子後,由 Drawer 回到 Home, 用GET
                                bt.Move(btMovePathFile);
                            }
                            else // if(drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                            {
                                btMovePathFile = pathFileObj.FromDrawerToCabinet02Home_GET_PathFile(drawerLocation);// Box Transfer 夾到盒子後,由 Drawer 回到 Home, 用GET
                                bt.Move(btMovePathFile);
                                bt.TurnToCB1Home();

                            }
                          
                            // 6.2 boxrobot 移到 OpenStage
                            os.ReadRobotIntrude(true, null);
                            btMovePathFile = pathFileObj.FromCabinet01HomeToOpenStage_PUT_PathFile(); // boxrobot 目前有盒子, 要到 Open Stage, 用PUT
                            bt.Move(btMovePathFile);

                            /** 07 光罩鐵盒放置前確認Box Robot是水平狀態 (by 水平儀)*/
                            Level = bt.ReadLevelSensor();

                            /** 08 Box Robot將光罩鐵盒放置於Open Stage上*/
                            bt.Unclamp();

                            /** 09 Box Robot退出Open Stage, 並回到Box Robot Home點*/
                            btMovePathFile = pathFileObj.FromOpenStageToCabinet01Home_PUT_PathFile();
                            bt.Move(btMovePathFile);
                            os.ReadRobotIntrude(false, null);

                            /** 10 (編號9-CCD): 開啟光源 -> 拍照(FOV正確) -> 關閉光源*/
                            /** os.LightForTopBarDfsSetValue(200);
                             os.Camera_Top_CapToSave("D:/Image/OS/Top", "jpg");
                             os.LightForTopBarDfsSetValue(0);
                          */
                            try
                            {
                                os.LightForFrontBarDfsSetValue(85);//bar 0~255
                                os.LightForSideBarDfsSetValue(140);//bar 0~255
                                os.LightForTopBarDfsSetValue(20);//bar 0~255
                                os.Camera_Top_CapToSave("D:/Image/OS/Top", "jpg");
                                Debug.WriteLine("Open Stage Camera Shot(Top) OK");
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine("Open Stage Camera Shot(Top) Error=" + ex.Message);
                            }
                            finally
                            {
                                os.LightForFrontBarDfsSetValue(0);
                                os.LightForSideBarDfsSetValue(0);
                                os.LightForTopBarDfsSetValue(0);
                            }



                            /** 11 (編號12-CCD): 開啟光源 -> 拍照(FOV正確) -> 關閉光源*/
                            /**
                            os.LightForSideBarDfsSetValue(200);
                            os.Camera_Side_CapToSave("D:/Image/OS/Side", "jpg");
                            os.LightForSideBarDfsSetValue(0);
                            */
                            try
                            {
                                os.LightForFrontBarDfsSetValue(85);//bar 0~255
                                os.LightForSideBarDfsSetValue(140);//bar 0~255
                                os.LightForTopBarDfsSetValue(20);//bar 0~255
                                os.Camera_Side_CapToSave("D:/Image/OS/Side", "jpg");
                                
                                Debug.WriteLine("Open Stage Camera Shot OK");
                            }
                            catch(Exception ex)
                            {
                                Debug.WriteLine("Open Stage Camera Shot(Side) Error=" + ex.Message);
                            }
                            finally
                            {
                                os.LightForFrontBarDfsSetValue(0);
                                os.LightForSideBarDfsSetValue(0);
                                os.LightForTopBarDfsSetValue(0);
                            }

                            /** 12 Box Robot(無夾持光罩鐵盒)從Home點移動進入Open Stage上方*/
                            os.ReadRobotIntrude(true, null);
                            btMovePathFile = pathFileObj.FromCabinet01HomeToOpenStage_GET_PathFile();
                            bt.Move(btMovePathFile);

                            /** 13 光罩鐵盒夾取前確認Box Robot是水平狀態 (by 水平儀)*/
                            Level = bt.ReadLevelSensor();

                            /** 14 Box Robot從Open Stage上方夾取光罩鐵盒*/
                            bt.Clamp((uint)boxType);

                            /** 15 Box Robot將光罩鐵盒移動至Drawer處*/
                            btMovePathFile = pathFileObj.FromOpenStageToCabinet01Home_GET_PathFile(); // boxrobot 目前有盒子,要回到 Cabinet 1 Home, 用 GET
                            bt.Move(btMovePathFile);
                            os.ReadRobotIntrude(false, null);
                            if (drawerHome == BoxrobotTransferLocation.Cabinet_01_Home)
                            {
                                btMovePathFile = pathFileObj.FromCabinet01HomeToDrawer_PUT_PathFile(drawerLocation); // boxrobot 目前有盒子,要到 Drawer, 所以用 PUT
                                bt.Move(btMovePathFile);
                            }
                            else //if(drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                            {
                                bt.TurnToCB2Home();
                                btMovePathFile = pathFileObj.FromCabinet02HomeToDrawer_PUT_PathFile(drawerLocation); // boxrobot 目前有盒子,要到 Drawer, 所以用 PUT
                                bt.Move(btMovePathFile);
                            }
                           

                            /** 16 (編號13-CCD): 開啟光源 -> 拍照(FOV正確) -> 關閉光源*/
                            /**
                            bt.LightForGripper(200);
                            bt.Camera_CapToSave("D:/Image/BT/Gripper", "jpg");
                            bt.LightForGripper(0);
                            */
                            lightValue = bt.GetCameraLightValue(boxType);
                            resultTemp= bt.CameraShot("D:/Image/BT/Gripper", "jpg", lightValue);

                            /** 17 Box Robot將光罩鐵盒移動至Drawer內*/
                            bt.Unclamp();

                            /** 18 Box Robot (無夾持光罩鐵盒) 從Drawer移回Home點*/
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
                           
                            
                            // drawer.CommandINI();
                            /** 19 重複1~18步驟, 完成20個Drawer的光罩鐵盒測試*/
                            /** 20 重複1~19步驟, 完成20個Drawer的光罩水晶盒測試*/

                            BREAK_POINT++; // 這個 drawer 做好了, 準備下一個

                         }
                        catch (Exception ex)
                        {

                        }
                    }
                }
                catch (Exception ex)
                {


                }

            }
        }

        private void Sleep(int v)
        {
            throw new NotImplementedException();
        }
    }
}
