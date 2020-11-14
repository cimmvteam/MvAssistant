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

        MacEnumDevice DrawerID = MacEnumDevice.cabinet_drawer_01_02;
        BoxrobotTransferLocation DrawerLocation = BoxrobotTransferLocation.Drawer_01_02;
        BoxType boxType = BoxType.CrystalBox;
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
        /// </summary>
        [TestMethod]
        public void Test_Step_01_02_03_04_05_06_07_08_09_10_11_12()
        {
            var BREAK_POINT = 0;
            using (var halContext = MacHalContextExtends.Create_MacHalContext_Instance())
            {
               try
                {
                    var drawerHome = DrawerLocation.GetCabinetHomeCode().Item2;
                    var universal = halContext.GetUniversalAssembly(true);
                    var boxTransfer = halContext.GetBoxTransferAssembly(true);
                    var openStage=halContext.GetOpenStageAssembly(true);
                    var drawer = halContext.GetDrawer(DrawerID,true);
                    string btMovePathFile = default(string);

                    boxTransfer.Initial();
                
                    drawer.Initial();
   
                    /** 01 */
                    // [1.1] Tray 移到 Out
                    drawer.MoveTrayToOut();
                    // [1.2] 移到 Out 之後等待手動放入鐵盒

                    BREAK_POINT = 0;

                    /** 02 */
                    drawer.MoveTrayToHome(); 
                    drawer.MoveTrayToIn();

                    BREAK_POINT = 0;


                    /** 03 */
                    // 3.1 BoxTransfer 轉到 Cabitnet 1 Home 
                    boxTransfer.TurnToCB1Home();
                    //*******[BR]*******
                    // 3.2 如果是 Cabitnet 2 的Drawer, 再轉到 Cabinet 2 Home
                    if (drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                    {
                        boxTransfer.TurnToCB2Home();
                    }

                    /** 04*/ //(先不測)

                    /** 5*/
                    // 5.1 Boxtransfer 移到夾取點
                    if(drawerHome== BoxrobotTransferLocation.Cabinet_01_Home)
                    {  // Cabinet 1  Drawer 的 Path
                        btMovePathFile = pathFileObj.FromCabinet01HomeToDrawer_GET_PathFile(DrawerLocation); // Box Transfer 要去 Drawer 夾盒子, 所以用 GET
                    }
                    else //if(drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                    {
                        //Cabinet 2 Drawer 的 Path
                        btMovePathFile = pathFileObj.FromCabinet02HomeToDrawer_GET_PathFile(DrawerLocation); // Box Transfer 要去 Drawer 夾盒子, 所以用 GET
                    }
                    boxTransfer.Move(btMovePathFile);  
                    // 5.2 夾取
                    boxTransfer.Clamp((uint)boxType);

                    /** 6*/
                    // 6.1 回到 Cabinet 1 Home
                    if (drawerHome == BoxrobotTransferLocation.Cabinet_01_Home)
                    {
                        btMovePathFile = pathFileObj.FromDrawerToCabinet01Home_GET_PathFile(DrawerLocation); // Box Transfer 夾到盒子後,由 Drawer 回到 Home, 用GET
                    }
                    else // if(drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                    {
                        btMovePathFile = pathFileObj.FromDrawerToCabinet02Home_GET_PathFile(DrawerLocation);// Box Transfer 夾到盒子後,由 Drawer 回到 Home, 用GET
                    }
                    boxTransfer.Move(btMovePathFile);
                    boxTransfer.TurnToCB1Home();
                    // 是否轉向 到 Open Stage?
                    // 6.2 boxrobot 移到 OpenStage
                    btMovePathFile = pathFileObj.FromCabinet01HomeToOpenStage_PUT_PathFile(); // boxrobot 目前有盒子, 要到 Open Stage, 用PUT
                    boxTransfer.Move(btMovePathFile);


                    BREAK_POINT = 0;

                    /** 7*/
                    // 7.1 Boxtransfer 回到 Cabinet 1 Home
                    btMovePathFile = pathFileObj.FromOpenStageToCabinet01Home_GET_PathFile(); // boxrobot 目前有盒子,要回到 Cabinet 1 Home, 用 GET
                    boxTransfer.Move(btMovePathFile);
                    // 7.2 如果是 Cabinet 2 的 Drawer, 轉向 Cabinet 2 Home
                    if (drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                    {
                        boxTransfer.TurnToCB2Home();
                    }
                    // 7.3 移到 Drawer
                    if(drawerHome== BoxrobotTransferLocation.Cabinet_01_Home)
                    {
                        btMovePathFile = pathFileObj.FromCabinet01HomeToDrawer_PUT_PathFile(DrawerLocation); // boxrobot 目前有盒子,要到 Drawer, 所以用 PUT
                    }
                    else //if(drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                    {
                        btMovePathFile = pathFileObj.FromCabinet01HomeToDrawer_PUT_PathFile(DrawerLocation); // boxrobot 目前有盒子,要到 Drawer, 所以用 PUT
                    }
                    boxTransfer.Move(btMovePathFile);
                    // 7.3 放下 Box
                    boxTransfer.Unclamp();

                    /** 8 */
                    if (drawerHome == BoxrobotTransferLocation.Cabinet_01_Home)
                    {
                        btMovePathFile = pathFileObj.FromDrawerToCabinet01Home_PUT_PathFile(DrawerLocation); // 没有盒子 用GET

                    }
                    else //if(drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                    {
                        btMovePathFile = pathFileObj.FromDrawerToCabinet02Home_PUT_PathFile(DrawerLocation); // 没有盒子 用GET
                    }
                    boxTransfer.Move(btMovePathFile);
                    boxTransfer.TurnToCB1Home();

                    /** 9 */
                    drawer.MoveTrayToHome();



                }
                catch (Exception ex)
                {


                }

            }
        }

     

        [TestMethod]
        public void Test_Step02()
        {

        }

        [TestMethod]
        [DataRow(BoxType.IronBox,false)]// 鐵盒
        //[DataRow(BoxType.CrystalBox,false)]// 水晶盒
        public void Test_Ut002_BT(BoxType boxType, bool autoConnect)
        {
            using (var halContext = MacHalContextExtends.Create_MacHalContext_Instance())
            {
                var universal = halContext.GetUniversalAssembly(false);
                var boxRobot = halContext.GetBoxTransferAssembly(autoConnect);
                var openStage = halContext.GetOpenStageAssembly(autoConnect);

                // Assembly 連線 
                if (!autoConnect)
                {
                    halContext.HalConnect();
                    boxRobot.HalConnect();
                }
                // 所有 Drawer 連線
                halContext.DrawersConnect();
            }

        }
        #endregion
    }
}
