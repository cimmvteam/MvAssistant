﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.v0_3.Mac.TestMy.ToolHal.Hirata_ScenarioTest.Extends;
using MvAssistant.v0_3.Mac.TestMy.ToolHal.Hirata_ScenarioTest.Types;
using MvAssistant.v0_3.Mac;
using MvAssistant.v0_3.Mac.Hal;
using MvAssistant.v0_3.Mac.Hal.Assembly;
using MvAssistant.v0_3.Mac.Hal.CompDrawer;
using MvAssistant.v0_3.Mac.JSon;
using MvAssistant.v0_3.Mac.JSon.RobotTransferFile;
using MvAssistant.v0_3.Mac.Manifest;
using MvaCToolkitCs.v1_2;

namespace MvAssistant.v0_3.Mac.TestMy.ToolHal.Hirata_ScenarioTest
{
    [TestClass]
    public class Ut022_BT
    {
        List<EnumMacDeviceId> DrawerKeys;
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
        public void DrawerTest()
        {
            using (var halContext = new MacHalContext("UserData/Manifest/Manifest.xml.real"))
            {
                halContext.InitialAndLoad();
                var s=  halContext.DrawersConnect();
                var drawer = halContext.GetDrawer(DrawerKeys[1]);
                try
                {
                    drawer.Initial();
                   // while (true)
                    //{
                     // System.Threading.Thread.  Sleep(100);
                   // }
                    drawer.MoveTrayToOut();
                    drawer.MoveTrayToHome();
                   drawer.MoveTrayToIn();
                }
                catch(Exception ex)                { CtkLog.WarnAn(this, ex); }
            }
        }
        [TestMethod]
        [DataRow(EnumMacMaskBoxType.IronBox, true,DrawerReplaceBoxPlace.In)] // 鐵盒
        //[DataRow(BoxType.CrystalBox,true)]  // 水晶盒
        public void TestMethod1(EnumMacMaskBoxType boxType, bool autoConnect,DrawerReplaceBoxPlace drawerReplaceBoxPlace)
        {
            var BREAK_POINT = 0;
            using (var halContext = new MacHalContext("UserData/Manifest/Manifest.xml.real"))
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

                halContext.MvaCfBootup();
                halContext.MvaCfLoad();

                try
                {
                    var uni = halContext.HalDevices[EnumMacDeviceId.eqp_assembly.ToString()] as MacHalEqp;
                    var bt = halContext.HalDevices[EnumMacDeviceId.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;
                    var os = halContext.HalDevices[EnumMacDeviceId.openstage_assembly.ToString()] as MacHalOpenStage;
                    string btMovePathFile = default(string);

                    uni.HalConnect();
                    bt.HalConnect();
                    os.HalConnect();


                    halContext.DrawersConnect();
                    //bt.Reset();
                    bt.Initial();
                    //bt.TurnToCB1Home();
                    int start =8 ;
                    int end =10;

                    for (var i = start; i <end; i++)
                    {
                        try
                        {
                            var drawerLocation = DrawerLocations[i];
                            var drawer = halContext.GetDrawer(DrawerKeys[i]);
                            IMacHalDrawer previousDrawer = null;
                            var drawerHome = drawerLocation.GetCabinetHomeCode().Item2;
                            /** 00 預先動作, 如有上一輪的 Drawer, 將上一輪的 Drawer 推回到 Out , 取出 Box, 將Box放到現在的 Drawer Tray中, 並將 Tray 放回 Home */
                            // 0.1 如有上一輪的 Drawer, 將其 Tray 移到 可抽換 box 的位置
                            if (i > start)
                            {
                                previousDrawer = halContext.GetDrawer(DrawerKeys[i - 1]);
                                previousDrawer.MoveTrayToHome();
                                if (drawerReplaceBoxPlace == DrawerReplaceBoxPlace.In)
                                {
                                    previousDrawer.MoveTrayToIn();
                                }
                                else
                                {
                                    previousDrawer.MoveTrayToOut();
                                }
                            }
                            // 0.2 將目前的  Drawer Tray Initial 
                            drawer.Initial();
                            // 0.3 將 目前的  Drawer Tray 移回 可以置換 Drawer 的地方
                            if (drawerReplaceBoxPlace == DrawerReplaceBoxPlace.In)
                            {
                                drawer.MoveTrayToIn();
                            }
                            else
                            {
                                drawer.MoveTrayToOut();
                            }


                            /**1. 光罩盒置於 Drawer內*/
                            BREAK_POINT++; //[[[[[[[[[[[[[[[[[[一定要暫停]]]]]]]]]]]]]]]]]]]]]]]]]]   確認有 Box後往下執行
                            // 1.1 如有上一個  Drawer, 將上一個 Drawer 移到 Home
                            if (previousDrawer != null)
                            { // 
                                previousDrawer.MoveTrayToHome(); ;
                            }
                            // 1.2 Drawer Tray 往機台內部移動到可以取得光罩的位置
                            drawer.MoveTrayToHome();
                            drawer.MoveTrayToIn();

                            /** 02 Box Robot從Home點至Drawer entry處*/
                            if (drawerHome == BoxrobotTransferLocation.Cabinet_01_Home)
                            {   // Drawer 01-01 ~ Drawer 03-05
                                bt.TurnToCB1Home();
                                btMovePathFile = pathFileObj.FromCabinet01HomeToDrawer_GET_PathFile(drawerLocation); // Box Transfer 要去 Drawer 夾盒子, 所以用 GET

                            }
                            else //if(drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                            {   // // Drawer 04-01 ~ Drawer 07-05
                                //Cabinet 2 Drawer 的 Path
                                bt.TurnToCB1Home();
                                bt.TurnToCB2Home();
                                btMovePathFile = pathFileObj.FromCabinet02HomeToDrawer_GET_PathFile(drawerLocation); // Box Transfer 要去 Drawer 夾盒子, 所以用 GET
                            }
                            bt.Move(btMovePathFile);

                            /** 03 (編號13-CCD): 開啟光源 -> 拍照(FOV正確) -> 關閉光源*/

                            try
                            {
                                var lightValue = bt.GetCameraLightValue(boxType);
                                bt.LightForGripperSetValue(lightValue);
                                bt.Camera_CapToSave("D:/Image/BT/Gripper", "jpg");
                                //bt.LightForGripper(0);
                            }
                            catch(Exception ex)
                            {
                                CtkLog.WarnAn(this, ex);

                            }
                            finally
                            {
                                bt.LightForGripperSetValue(0);
                            }
                        //    bt.Clamp((uint)boxType);
                            //var lightValue = bt.GetCameraLightValue(boxType);
                          //var resultTemp=bt.CameraShot("D:/Image/BT/Gripper", "jpg",lightValue);
                            

                            /** 04 Box Robot從Drawer Entry處移至Drawer內進行光罩鐵盒夾取*/
                            var resultTemp = bt.Clamp((uint)boxType);

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
                            {   // Drawer 04_01~07~05 回到  Cabinet 1 Home 
                                btMovePathFile = pathFileObj.FromDrawerToCabinet02Home_GET_PathFile(drawerLocation);// Box Transfer 夾到盒子後,由 Drawer 回到 Home, 用GET
                                bt.Move(btMovePathFile);
                                bt.TurnToCB1Home();

                            }
                          
                            // 6.2 boxrobot 移到 OpenStage
                            os.SetRobotIntrude(true, null);
                            btMovePathFile = pathFileObj.FromCabinet01HomeToOpenStage_PUT_PathFile(); // boxrobot 目前有盒子, 要到 Open Stage, 用PUT
                            bt.Move(btMovePathFile);

                            /** 07 光罩鐵盒放置前確認Box Robot是水平狀態 (by 水平儀)*/
                            Level = bt.ReadLevelSensor();

                            /** 08 Box Robot將光罩鐵盒放置於Open Stage上*/
                            bt.Unclamp();

                            /** 09 Box Robot退出Open Stage, 並回到Box Robot Home點*/
                            btMovePathFile = pathFileObj.FromOpenStageToCabinet01Home_PUT_PathFile();
                            bt.Move(btMovePathFile);
                            os.SetRobotIntrude(false, null);

                            /** 10 (編號9-CCD): 開啟光源 -> 拍照(FOV正確) -> 關閉光源*/
                            /** 
                             os.LightForTopBarDfsSetValue(200);
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
                            os.SetRobotIntrude(true, null);
                            btMovePathFile = pathFileObj.FromCabinet01HomeToOpenStage_GET_PathFile();
                            bt.Move(btMovePathFile);

                            /** 13 光罩鐵盒夾取前確認Box Robot是水平狀態 (by 水平儀)*/
                            Level = bt.ReadLevelSensor();

                            /** 14 Box Robot從Open Stage上方夾取光罩鐵盒*/
                            bt.Clamp((uint)boxType);

                            /** 15 Box Robot將光罩鐵盒移動至Drawer處*/
                            // 15.1 Boxrobot 移到 Cabitnet 1 Home
                            btMovePathFile = pathFileObj.FromOpenStageToCabinet01Home_GET_PathFile(); // boxrobot 目前有盒子,要回到 Cabinet 1 Home, 用 GET
                            bt.Move(btMovePathFile);
                            os.SetRobotIntrude(false, null);
                            // 15.2
                            if (drawerHome == BoxrobotTransferLocation.Cabinet_01_Home)
                            {   // drawer 01-01 ~ drawer 03-05 
                                btMovePathFile = pathFileObj.FromCabinet01HomeToDrawer_PUT_PathFile(drawerLocation); // boxrobot 目前有盒子,要到 Drawer, 所以用 PUT
                                bt.Move(btMovePathFile);
                            }
                            else //if(drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                            {
                                // drawer 04-01 ~ drawer 07-05
                                bt.TurnToCB2Home();
                                btMovePathFile = pathFileObj.FromCabinet02HomeToDrawer_PUT_PathFile(drawerLocation); // boxrobot 目前有盒子,要到 Drawer, 所以用 PUT
                                bt.Move(btMovePathFile);
                            }


                            /** 16 (編號13-CCD): 開啟光源 -> 拍照(FOV正確) -> 關閉光源*/
                            try
                            {
                                var lightValue = bt.GetCameraLightValue(boxType);
                                bt.LightForGripperSetValue(lightValue);
                                bt.Camera_CapToSave("D:/Image/BT/Gripper", "jpg");
                            }
                            catch(Exception ex)
                            {
                                CtkLog.WarnAn(this, ex);

                            }
                            finally
                            {
                                bt.LightForGripperSetValue(0);
                            }
                               
                                                       //lightValue = bt.GetCameraLightValue(boxType);
                            //resultTemp= bt.CameraShot("D:/Image/BT/Gripper", "jpg", lightValue);

                            /** 17 Box Robot將光罩鐵盒移動至Drawer內*/
                            bt.Unclamp();

                            /** 18 Box Robot (無夾持光罩鐵盒) 從Drawer移回Home點*/
                            // 18.1 Boxrobot 移回 Home 點
                            if (drawerHome == BoxrobotTransferLocation.Cabinet_01_Home)
                            {    
                                btMovePathFile = pathFileObj.FromDrawerToCabinet01Home_PUT_PathFile(drawerLocation); // 
                            }
                            else //if(drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                            {
                                btMovePathFile = pathFileObj.FromDrawerToCabinet02Home_PUT_PathFile(drawerLocation); // T
                            }
                            bt.Move(btMovePathFile);

                            // 18.2 Drawer 04-01 ~ Drawer 07-05 時 Boxrobot 轉回 Cabitnet 1 Home 
                            if (drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                            {
                                bt.TurnToCB1Home();
                            }
                           
                            
                            // drawer.CommandINI();
                            /** 19 重複1~18步驟, 完成20個Drawer的光罩鐵盒測試*/
                            /** 20 重複1~19步驟, 完成20個Drawer的光罩水晶盒測試*/

                            BREAK_POINT++; // [一定要暫停] 這個 drawer 做好了, 準備下一個

                         }
                        catch (Exception ex)                       {                            CtkLog.WarnAn(this, ex);                        }
                    }
                }
                catch (Exception ex)                { CtkLog.WarnAn(this, ex); }

            }
        }

        private void Sleep(int v)
        {
            throw new NotImplementedException();
        }
    }
}
