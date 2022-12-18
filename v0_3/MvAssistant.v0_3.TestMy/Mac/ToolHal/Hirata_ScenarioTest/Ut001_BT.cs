using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.v0_3.Mac.TestMy.ToolHal.Hirata_ScenarioTest.Extends;
using MvAssistant.v0_3.Mac;
using MvAssistant.v0_3.Mac.Hal;
using MvAssistant.v0_3.Mac.Hal.Assembly;
using MvAssistant.v0_3.Mac.Hal.CompDrawer;
using MvAssistant.v0_3.Mac.JSon;
using MvAssistant.v0_3.Mac.JSon.RobotTransferFile;
using MvAssistant.v0_3.Mac.Manifest;
using static System.Net.Mime.MediaTypeNames;
using MvaCToolkitCs.v1_2;

namespace MvAssistant.v0_3.Mac.TestMy.ToolHal.Hirata_ScenarioTest
{
    /// <summary>
    /// <para>ID: MI-CT02-ST-001</para>
    /// <para>項目:To drawer movement * 20</para>
    /// </summary>
    [TestClass]
    public class Ut001_BT
    {
      
        List<EnumMacDeviceId> DrawerKeys;
        List<BoxrobotTransferLocation> DrawerLocations;
        BoxrobotTransferPathFile pathFileObj;//= new BoxrobotTransferPathFile(PositionInstance.BTR_Path);
        public Ut001_BT()
        {
            DrawerKeys = HalDrawerExtends.DrawerKeys;
            DrawerLocations = HalDrawerExtends.DrawerLocations;

            PositionInstance.Load(); // 在這裏載入所有(Boxtransfer 及 Masktransfer)的路徑點位資料
            pathFileObj = new BoxrobotTransferPathFile(PositionInstance.BTR_Path);
        }
        #region do not care this  test function              




        [TestMethod]
        [DataRow(false)]
        public void TestCreateDrawerInstance(bool autoConnect)
        {
            string str = System.Environment.CurrentDirectory;
            using (var halContext = MacHalContextExtends.Create_MacHalContext_Instance())
            {
                
                var universal = halContext.GetUniversalAssembly(autoConnect);
                var boxTransfer = halContext.GetBoxTransferAssembly(autoConnect);
                var cabinet = halContext.GetCabinetAssembly(autoConnect);
                var openStage = halContext.GetOpenStageAssembly(autoConnect);
                universal.HalConnect();
                boxTransfer.HalConnect();
               // boxTransfer.CameraShot("TTTTTTT");
                
                //   halContext.DrawersConnect();
                // connect 所有 Drawer
               // halContext.DrawersConnect();
                for(var i=0;i<2;i++)
                {
                    try
                    {
                        var drawer = halContext.GetDrawer(DrawerKeys[i], false);
                        drawer.CommandINI();
                       
                        Debug.WriteLine(DrawerKeys[i] + ", OK");
                    }
                    catch(Exception ex)
                    {
                        Debug.WriteLine(DrawerKeys[i] + ", Error:" + ex.Message);
                    }

                }
                /*
                if (!autoConnect)
                {
                    universal.HalConnect();
                    boxTransfer.HalConnect();
                    cabinet.HalConnect();
                    openStage.HalConnect();
                }*/
                // connect 所有 Drawer
                //  halContext.DrawersConnect();

                /**
                var lpB = halContext.HalDevices[MacEnumDevice.loadportB_assembly.ToString()] as MacHalLoadPort;
                lpB.HalConnect();
                var lb = lpB.LoadPortUnit;
                var lpA = halContext.HalDevices[MacEnumDevice.loadportA_assembly.ToString()] as MacHalLoadPort;
                lpA.HalConnect();
                var la = lpA.LoadPortUnit;
                */
            }
            Repeat();
            
        }
        #endregion
        void Repeat()
        {
            while(true)
            {
                System.Threading.Thread.Sleep(1000);
            }
        }
        #region this is the real unit test
        /// <summary>
        /// <para>1. 光罩鐵盒放置於Open Stage平台上</para>
        /// <para>2. Drawer往機台內部移動到光罩盒可放置的位置</para>
        /// <para>3. Box Robot從Home點至Open Stage進行光罩鐵盒夾取</para>
        /// <para>4. Box Robot從Open Stage夾持光罩至Drawer放置點位之前一個點位</para>
        /// <para>5. (編號13-CCD): 開啟光源 -> 拍照(FOV正確) -> 關閉光源, 確認Drawer中無光罩盒</para>
        /// <para>6. Box Robot從Open Stage夾持光罩至Drawer放置點位(不做放置光罩鐵盒於drawer的動作)</para>
        /// <para>7. Box Robot夾持光罩回到Drawer entry點位</para>
        /// <para>8. Drawer回到Cabinet內</para>
        /// <para>9. 重複1 ~8步驟, 完成20個Drawer的光罩鐵盒的移動測試</para>
        /// <para>10. 重複1 ~8步驟, 完成20個Drawer的光罩水晶盒的移動測試</para>
        /// </summary>
        /// <param name="boxType"></param>
        /// <param name="autoConnect"></param>
        [TestMethod]
       [DataRow(EnumMacMaskBoxType.IronBox,false)] // 鐵盒
        //[DataRow(BoxType.CrystalBox,false)]// 水晶盒
        public void Test_Ut001_BT(EnumMacMaskBoxType boxType,bool autoConnect) 
        {

            var BREAK_POINT = 0;
            using (var halContext = MacHalContextExtends.Create_MacHalContext_Instance())
            {
                try
                {
                    
                    var universal = halContext.GetUniversalAssembly(autoConnect);
                    var boxTransfer = halContext.GetBoxTransferAssembly(autoConnect);
                    var cabinet = halContext.GetCabinetAssembly(autoConnect);
                    var openStage = halContext.GetOpenStageAssembly(autoConnect);

                    if (!autoConnect)
                    {
                        universal.HalConnect();
                        boxTransfer.HalConnect();
                        cabinet.HalConnect();
                        openStage.HalConnect();
                    }

                    // Initial OpenStage
                    openStage.Initial();
                    openStage.SetBoxType((uint)boxType);
                    openStage.SortClamp();
                    // Initial BoxTransfer
                    boxTransfer.Initial();
                    // 關燈
                    boxTransfer.TurnOffCameraLight();
                   

                    // connect 所有 Drawer
                    halContext.DrawersConnect();

                   

                    for (int i = 0; 0 < DrawerKeys.Count; i++)
                    {

                        try
                        {
                            // 取得 Drawer
                            var drawer = halContext.GetDrawer(DrawerKeys[i]);
                            // Drawer  代碼
                            var drawerLocation = DrawerLocations[i];
                            // Drawer Home
                            var drawerHome = DrawerLocations[i].GetCabinetHomeCode().Item2;
                            // Drawer Initial
                            drawer.Initial();
                            // boxTransfer 轉向 Cabitnet 1 Home
                            boxTransfer.TurnToCB1Home();


                            BREAK_POINT = 0;


                            /** 1 光罩鐵盒放置於Open Stage平台上*/
                       

                            BREAK_POINT = 0;   // 確認 光罩鐵盒放置於Open Stage平台上*/


                            /** 2. Drawer往機台內部移動到光罩盒可放置的位置 */
                            // 將 Drawer 的Tray 移到 In 
                            drawer.MoveTrayToIn();


                            BREAK_POINT = 0;

                            /** 3. Box Robot從Home點至Open Stage進行光罩鐵盒夾取*/
                            //  3.1 能否入侵 Open Stage?
                            openStage.SetRobotIntrude(true, null);
                            //  3.2 將 Boxrobot 移到 OpenStage
                            var path = pathFileObj.FromCabinet01HomeToOpenStage_GET_PathFile();
                            openStage.SortUnclamp();
                            openStage.Lock();
                            boxTransfer.Move(path);
                            
                            boxTransfer.Clamp((uint)boxType);

                            BREAK_POINT = 0;
                           
                            /** 4. Box Robot從Open Stage夾持光罩至Drawer放置點位之前一個點位 (暫且定義為 CB1 Home 或CB2 Home) */
                            path = pathFileObj.FromOpenStageToCabinet01Home_GET_PathFile();
                            boxTransfer.Move(path);
                            if (drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                            {
                                boxTransfer.TurnToCB2Home();
                            }

                            #region 拍照 , 移到 6~7 間
                            // 5. (編號13-CCD): 開啟光源 -> 拍照(FOV正確) -> 關閉光源, 確認Drawer中無光罩盒
                            #endregion


                            BREAK_POINT = 0;

                            /** 6. Box Robot從Open Stage夾持光罩至Drawer放置點位 (不做放置光罩鐵盒於drawer的動作) */
                               //  將 BoxRobot 移到 Drawer
                            if(drawerHome == BoxrobotTransferLocation.Cabinet_01_Home)
                            {
                                path = pathFileObj.FromCabinet01HomeToDrawer_PUT_PathFile(drawerLocation);
                            }
                            else  //if(drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                            {
                                boxTransfer.TurnToCB2Home();
                               
                                path = pathFileObj.FromCabinet02HomeToDrawer_PUT_PathFile(drawerLocation);
                            }


                            boxTransfer.Move(path);
                            
                            //  拍照  
                          //  boxTransfer.CameraShot("Ut001_BT_" + drawerLocation);
                            

                            BREAK_POINT = 0;


                            /** 7. Box Robot夾持光罩回到Drawer entry點位 (目前没有 teach Drawer entry 點 這個點位, 改以 回到 CB1_HOME 或 CB2_Home)*/
                            //   7.1 以Get 方式回到 Cb1Home(目前有盒子)
                            if (drawerHome == BoxrobotTransferLocation.Cabinet_01_Home)
                            {  // cabinet 1
                                path = pathFileObj.FromDrawerToCabinet01Home_GET_PathFile(drawerLocation);
                            }
                            else if (drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                            {   // cabinet 2
                                path = pathFileObj.FromDrawerToCabinet02Home_GET_PathFile(drawerLocation);
                            }
                            boxTransfer.Move(path);
                            //   7.2 如果 Drawer 是 Cabinet 2的Drawer, 再回到 Cabitnet 1 Home
                            if (drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                            {
                                boxTransfer.TurnToCB1Home();
                            }
                            //   7.3 檢查入侵到 Open Stage 
                            openStage.SetRobotIntrude(true,null); // 入侵
                            path = pathFileObj.FromCabinet01HomeToOpenStage_PUT_PathFile();
                            //    7.4 BoxTransfer 移到 OpenStage
                            boxTransfer.Move(path);
                            //   7.5 放下盒子
                            boxTransfer.Unclamp();    // => 第二 個以後Drawer(01_03, 01_04 ,......, 07_03)  的 1. 
                            //   7.6 Box robot 回CB1Home
                            path = pathFileObj.FromOpenStageToCabinet01Home_PUT_PathFile();
                            boxTransfer.Move(path);
                            


                            BREAK_POINT = 0;


                            // 8. Drawer回到Cabinet內
                            drawer.MoveTrayToHome();

                            // 9. 重複1~8步驟, 完成20個Drawer的光罩鐵盒的移動測試(下個 Cycle)
                            //10.重複1~8步驟, 完成20個Drawer的光罩水晶盒的移動測試

                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                catch(Exception ex)
                {
                    CtkLog.WarnNs(this, ex);

                }


            };
        }
        #endregion
     

        
    }
}
