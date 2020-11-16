using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.TestMy.MachineRealHal.Hirata_ScenarioTest.Extends;
using MvAssistant.Mac.v1_0;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.JSon;
using MvAssistant.Mac.v1_0.JSon.RobotTransferFile;
using MvAssistant.Mac.v1_0.Manifest;

namespace MvAssistant.Mac.TestMy.MachineRealHal.Hirata_ScenarioTest
{
    /// <summary>
    /// <para>ID: MI-CT02-ST-002</para>
    /// <para>項目: To drawer clamp/release * 20</para>
    /// </summary>
    [TestClass]
    public class Ut002_BT
    {

        List<MacEnumDevice> DrawerKeys;
        List<BoxrobotTransferLocation> DrawerLocations;
        BoxrobotTransferPathFile pathFileObj;//= new BoxrobotTransferPathFile(PositionInstance.BTR_Path);

        /// <summary>建構式</summary>
        public Ut002_BT()
        {
            DrawerKeys = HalDrawerExtends.DrawerKeys;
            DrawerLocations = HalDrawerExtends.DrawerLocations;

            PositionInstance.Load(); // 在這裏載入所有(Boxtransfer 及 Masktransfer)的路徑點位資料
            pathFileObj = new BoxrobotTransferPathFile(PositionInstance.BTR_Path);
        }

        #region this is the real unit test functions


        // BoxType boxType = BoxType.CrystalBox;

        /// <summary>
        /// <para>1. 光罩鐵盒放置於Drawer內</para>
        /// <para>2. Drawer往機台內部移動到Box Robot可以取得光罩的位置</para>
        /// <para>3. Box Robot從Home點至Drawer夾取前的檢查點位 (目前没有這個點, 所以 移到 CB1 Home)</para>
        /// <para>4. (編號13-CCD): 開啟光源 -> 拍照(FOV正確) -> 關閉光源, 確認Drawer中有光罩鐵盒(先不測)</para>
        /// <para>5. Box Robot移動至Drawer夾取點, 進行光罩鐵盒夾取 </para>
        /// <para>6. Box Robot將光罩鐵盒從Drawer移動至Open Stage Entry處</para>
        /// <para>7. Box Robot將光罩鐵盒從Open Stage Entry處, 移至Drawer可放置光罩盒的位置, 並且放置光罩鐵盒 (release光罩鐵盒)</para>
        /// <para>8. Box Robot (無夾持光罩盒) 從Drawer移回Home點</para>
        /// <para>9. Drawer回到Cabinet內</para>
        /// <para>10.重複1~9步驟, 完成20個Drawer的光罩鐵盒測試</para>
        /// <para>12. 重複1~9步驟, 完成20個Drawer的光罩盒測試</para>
        /// </summary>
        [TestMethod]
        //[DataRow(BoxType.IronBox,true)] // 鐵盒
        [DataRow(BoxType.CrystalBox,true)]  // 水晶盒
        public void Test_Ut002_BT(BoxType boxType,bool autoConnect)
        {
            var BREAK_POINT = 0;
            using (var halContext = MacHalContextExtends.Create_MacHalContext_Instance())
            {
               try
                {
                   
                    var universal = halContext.GetUniversalAssembly(autoConnect);
                    var boxTransfer = halContext.GetBoxTransferAssembly(autoConnect);
                    var openStage=halContext.GetOpenStageAssembly(autoConnect);
                    var cabinet = halContext.GetCabinetAssembly(autoConnect);
                    string btMovePathFile = default(string);
                    if(!autoConnect)
                    {
                        universal.HalConnect();
                        boxTransfer.HalConnect();
                        cabinet.HalConnect();
                        openStage.HalConnect();
                    }

                    boxTransfer.Initial();
                    boxTransfer.TurnOffCameraLight();
                    openStage.Initial();

                    halContext.DrawersConnect();

                
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

                            /** 02 Drawer往機台內部移動到Box Robot可以取得光罩的位置*/
                            drawer.MoveTrayToHome();
                            drawer.MoveTrayToIn();

                            BREAK_POINT = 0;


                            /** 03 Box Robot從Home點至Drawer夾取前的檢查點位*/
                            // 3.1 BoxTransfer 轉到 Cabitnet 1 Home 
                            boxTransfer.TurnToCB1Home();
                            //*******[BR]*******
                            // 3.2 如果是 Cabitnet 2 的Drawer, 再轉到 Cabinet 2 Home
                            if (drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                            {
                                boxTransfer.TurnToCB2Home();
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
                            boxTransfer.Move(btMovePathFile);

                            boxTransfer.TurnOnCameraLight();
                            // 照相
                            
                            boxTransfer.CameraShot("Ut002_BT_" + drawerLocation);


                            // 5.2 夾取
                            boxTransfer.Clamp((uint)boxType);

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
                            boxTransfer.Move(btMovePathFile);
                            if (drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                            { boxTransfer.TurnToCB1Home(); }
                            // 6.2 boxrobot 移到 OpenStage
                            openStage.ReadRobotIntrude(true, null);
                            btMovePathFile = pathFileObj.FromCabinet01HomeToOpenStage_PUT_PathFile(); // boxrobot 目前有盒子, 要到 Open Stage, 用PUT
                            boxTransfer.Move(btMovePathFile);


                            BREAK_POINT = 0;

                            /** 7 Box Robot將光罩鐵盒從Open Stage Entry處, 移至Drawer可放置光罩盒的位置, 並且放置光罩鐵盒 (release光罩鐵盒)*/
                            // 7.1 Boxtransfer 回到 Cabinet 1 Home
                            btMovePathFile = pathFileObj.FromOpenStageToCabinet01Home_GET_PathFile(); // boxrobot 目前有盒子,要回到 Cabinet 1 Home, 用 GET
                            boxTransfer.Move(btMovePathFile);
                            // 7.2 如果是 Cabinet 2 的 Drawer, 轉向 Cabinet 2 Home
                            if (drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                            {
                                boxTransfer.TurnToCB2Home();
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
                            boxTransfer.Move(btMovePathFile);
                            // 7.3 放下 Box
                            boxTransfer.Unclamp();

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
                            boxTransfer.Move(btMovePathFile);
                            if (drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                            {
                                boxTransfer.TurnToCB1Home();
                            }
                            BREAK_POINT = 0;

                            /** 9 Drawer回到Cabinet內*/
                            drawer.MoveTrayToHome();

                            // 10.重複1~9步驟, 完成20個Drawer的光罩鐵盒測試
                        }
                        catch(Exception ex)
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

        [TestMethod]
        public void test()
        {
            using (var halContext = MacHalContextExtends.Create_MacHalContext_Instance())
            {

                var boxTransfer = halContext.GetBoxTransferAssembly(false);
                //   boxTransfer.TurnToCB1Home();
                // boxTransfer.TurnToCB2Home();
                var path1= pathFileObj.FromCabinet01HomeToDrawer_PUT_PathFile(BoxrobotTransferLocation.Drawer_01_05);
                var path2 = pathFileObj.FromCabinet02HomeToDrawer_PUT_PathFile(BoxrobotTransferLocation.Drawer_04_05);

            }
        }
    

       
        #endregion
    }
}
