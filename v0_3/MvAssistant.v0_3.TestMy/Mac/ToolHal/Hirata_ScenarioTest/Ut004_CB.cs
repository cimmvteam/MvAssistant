using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.v0_3.Mac.TestMy.ToolHal.Hirata_ScenarioTest.Extends;
using MvAssistant.v0_3.Mac;
using MvAssistant.v0_3.Mac.JSon;
using MvAssistant.v0_3.Mac.JSon.RobotTransferFile;
using MvAssistant.v0_3.Mac.Manifest;

namespace MvAssistant.v0_3.Mac.TestMy.ToolHal.Hirata_ScenarioTest
{
    [TestClass]
    public class Ut004_CB
    {
        List<EnumMacDeviceId> DrawerKeys;
        List<BoxrobotTransferLocation> DrawerLocations;
        BoxrobotTransferPathFile pathFileObj;//= new BoxrobotTransferPathFile(PositionInstance.BTR_Path);
        public Ut004_CB()
        {
            DrawerKeys = HalDrawerExtends.DrawerKeys;
            DrawerLocations = HalDrawerExtends.DrawerLocations;

            PositionInstance.Load(); // 在這裏載入所有(Boxtransfer 及 Masktransfer)的路徑點位資料
            pathFileObj = new BoxrobotTransferPathFile(PositionInstance.BTR_Path);
        }

        /// <summary>
        /// <para>1. 光罩鐵盒放置於Drawer內</para>
        /// <para>2. Drawer往機台內部移動到Box Robot可以取得光罩的位置</para>
        /// <para>3. Box Robot從Home點至Drawer夾取前的檢查點位</para>
        /// <para>4. (編號13-CCD): 開啟光源 -> 拍照(FOV正確) -> 關閉光源, 確認Drawer中有光罩鐵盒</para>
        /// <para>5. Box Robot移動至Drawer夾取點, 進行光罩鐵盒夾取 (clamp光罩鐵盒)</para>
        /// <para>6. Box Robot將光罩鐵盒從Drawer移動至Open Stage Entry處</para>
        /// <para>7. Drawer回到Cabinet內</para>
        /// <para>8. Box Robot將光罩鐵盒放在Open Stage平台上</para>
        /// <para>9. Box Robot(無夾持光罩盒) 從Drawer移回Home點</para>
        /// <para>10. 重複1 ~9步驟, 完成20個Drawer的光罩鐵盒測試</para>
        /// <para>12. 重複1 ~9步驟, 完成20個Drawer的光罩盒測試</para>
        /// </summary>
        [TestMethod]
        //[DataRow(BoxType.IronBox,true)] // 鐵盒
         [DataRow(EnumMacMaskBoxType.CrystalBox, true)]  // 水晶盒
        public void Test_Ut004_CB(EnumMacMaskBoxType boxType, bool autoConnect)
        {
            var BREAK_POINT = 0;
            using (var halContext = MacHalContextExtends.Create_MacHalContext_Instance())
            {
                try
                {

                    var universal = halContext.GetUniversalAssembly(autoConnect);
                    var boxTransfer = halContext.GetBoxTransferAssembly(autoConnect);
                    var openStage = halContext.GetOpenStageAssembly(autoConnect);
                    var cabinet = halContext.GetCabinetAssembly(autoConnect);
                    string btMovePathFile = default(string);
                    if (!autoConnect)
                    {
                        universal.HalConnect();
                        boxTransfer.HalConnect();
                        cabinet.HalConnect();
                        openStage.HalConnect();
                    }


                    openStage.Initial();
                    boxTransfer.Initial();
                    boxTransfer.TurnOffCameraLight();


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


                            /** 04 (編號13-CCD): 開啟光源 -> 拍照(FOV正確) -> 關閉光源, 確認Drawer中有光罩鐵盒*/ //(移到5.1 ~ 5.2)



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


                            // 照相
                           // boxTransfer.CameraShot("Ut002_BT_" + drawerLocation);


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
                            openStage.SetRobotIntrude(true, null);
                            btMovePathFile = pathFileObj.FromCabinet01HomeToOpenStage_PUT_PathFile(); // boxrobot 目前有盒子, 要到 Open Stage, 用PUT
                            boxTransfer.Move(btMovePathFile);


                            BREAK_POINT = 0;

                            /** 7.Drawer回到Cabinet內 */
                            drawer.MoveTrayToHome();

                            /** 8. Box Robot將光罩鐵盒放在Open Stage平台上*/
                            boxTransfer.Unclamp();

                            /**9. Box Robot (無夾持光罩盒) 從Drawer移回Home點*/
                            btMovePathFile = pathFileObj.FromOpenStageToCabinet01Home_PUT_PathFile();
                            boxTransfer.Move(btMovePathFile);
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
