using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.TestMy.MachineRealHal.Hirata_ScenarioTest.Extends;
using MvAssistant.Mac.v1_0;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Hal.CompDrawer;
using MvAssistant.Mac.v1_0.JSon;
using MvAssistant.Mac.v1_0.JSon.RobotTransferFile;
using MvAssistant.Mac.v1_0.Manifest;
using static System.Net.Mime.MediaTypeNames;

namespace MvAssistant.Mac.TestMy.MachineRealHal.Hirata_ScenarioTest
{
    /// <summary>MI-CT02-ST-001</summary>
    [TestClass]
    public class Ut001_BT
    {
      
        List<MacEnumDevice> DrawerKeys;
        List<BoxrobotTransferLocation> DrawerLocations;
        BoxrobotTransferPathFile pathFileObj;//= new BoxrobotTransferPathFile(PositionInstance.BTR_Path);
        public Ut001_BT()
        {
            DrawerKeys = HalDrawerExtends.DrawerKeys;
            DrawerLocations = HalDrawerExtends.DrawerLocations;

            PositionInstance.Load(); // 在這裏載入所有(Boxtransfer 及 Masktransfer)的路徑點位資料
            pathFileObj = new BoxrobotTransferPathFile(PositionInstance.BTR_Path);
        }
            
        [TestMethod]
        public void TestCreateDrawerInstance()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                var s=System.Environment.CurrentDirectory;
                halContext.MvCfInit();
                halContext.MvCfLoad();
                var universal = halContext.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
                var boxTransfer = halContext.HalDevices[MacEnumDevice.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;
                var openStage = halContext.HalDevices[MacEnumDevice.openstage_assembly.ToString()] as MacHalOpenStage;
                
                 universal.HalConnect();

                /**
                 openStage.HalConnect();
                 boxTransfer.HalConnect();
    */            
                var cabinet = halContext.HalDevices[MacEnumDevice.cabinet_assembly.ToString()];
                cabinet.HalConnect();
                for (var i = 0; i < DrawerKeys.Count; i++)
                {
                    var drawerCab = halContext.HalDevices[DrawerKeys[i].ToString()] as MacHalCabinet;
                    var drawer = drawerCab.MacHalDrawer;
                    drawerCab.HalConnect();

                    drawer.Initial();
                    drawer.MoveTrayToIn();
                }

                var lpB = halContext.HalDevices[MacEnumDevice.loadportB_assembly.ToString()] as MacHalLoadPort;
                lpB.HalConnect();
                var lb = lpB.LoadPortUnit;
                var lpA = halContext.HalDevices[MacEnumDevice.loadportA_assembly.ToString()] as MacHalLoadPort;
                lpA.HalConnect();
                var la = lpA.LoadPortUnit;
                
            }

        }

       
        [TestMethod]
        //[DataRow(BoxType.IronBox,false)]
        [DataRow(BoxType.CrystalBox,false)]
        public void TestMethod(BoxType boxType,bool autoConnect) 
        {
           
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.InitialAndLoad();
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

                // connect 所有 Drawer
                halContext.DrawersConnect();
                
                // boxTransfer 
                boxTransfer.Initial();
                boxTransfer.TurnToCB1Home();
             
                // 1. 光罩鐵盒放置於Open Stage平台上  => 假設 OK
                
                 
                
               for(int i=0;0< DrawerKeys.Count;i++)
                {
                    // 2. Drawer往機台內部移動到光罩盒可放置的位置
                    var drawer = halContext.GetDrawer(DrawerKeys[i]);
                    drawer.Initial();
                    drawer.MoveTrayToIn();

                    // 3. Box Robot從Home點至Open Stage進行光罩鐵盒夾取
                    //  3.1 能否入侵 Open Stage?
                    openStage.ReadRobotIntrude(true, null);
                    //  3.2 將 Boxrobot 移到 OpenStage
                    var path = pathFileObj.FromCabinet01HomeToOpenStage_GET_PathFile();
                    boxTransfer.Move(path);
                    boxTransfer.Clamp((uint)boxType);

                    #region 拍照 , 暫緩(4,5)
                    // 4. Box Robot從Open Stage夾持光罩至Drawer放置點位之前一個點位 (暫且定義為 CB1 Home)
                    // 5. (編號13-CCD): 開啟光源 -> 拍照(FOV正確) -> 關閉光源, 確認Drawer中無光罩盒
                    #endregion

                    // 6. Box Robot從Open Stage夾持光罩至Drawer放置點位 (不做放置光罩鐵盒於drawer的動作)
                    //   6.1 先回 Cabinet 1Home
                    path = pathFileObj.FromOpenStageToCabinet01Home_GET_PathFile();
                    boxTransfer.Move(path);
                    //   6.2 將BoxRobot 轉回 CB1 HOME 方向
                    boxTransfer.TurnToCB1Home();
                    //   6.3 將 BoxRobot 伸到 Drawer
                    path = pathFileObj.FromCabinet01HomeToDrawer_PUT_PathFile(DrawerLocations[i]);
                    boxTransfer.Move(path);

                    // 7. Box Robot夾持光罩回到Drawer entry點位 (目前没有 teach 這個點位, 改以 回到 CB1_HOME)
                    //   7.1 以Get 方式回到 Cb1Home
                    path = pathFileObj.FromDrawerToCabinet01Home_GET_PathFile(DrawerLocations[i]);
                    boxTransfer.Move(path);
                    //   7.2 將Cabnet 移到 OpenStage 
                    path = pathFileObj.FromCabinet01HomeToOpenStage_PUT_PathFile();
                    boxTransfer.Move(path);
                    //   7.3 放下盒子
                    boxTransfer.Unclamp();
                    //   7.4 Box robot 回CB1Home
                    path = pathFileObj.FromOpenStageToCabinet01Home_GET_PathFile();
                    boxTransfer.Move(path);
                    //   7.5 轉回 CB1 HOme
                    boxTransfer.TurnToCB1Home();

                    // 8. Drawer回到Cabinet內
                    drawer.MoveTrayToHome();

                    // 9. 重複1~8步驟, 完成20個Drawer的光罩鐵盒的移動測試(下個 Cycle)

                }
               // 9.重複1~8步驟, 完成20個Drawer的光罩鐵盒的移動測試
                  //10.重複1~8步驟, 完成20個Drawer的光罩水晶盒的移動測試

            };


        }

        public void TestMethod6_10()
        {
            // 6.光罩水晶盒放置於Open Stage平台上
            // 7.Box Robot從Home點至Open Stage進行光罩水晶盒夾取
            // 8.Box Robot將光罩水晶盒從Open Stage移動, 移入Drawer內部 (夾爪夾持光罩水晶盒, 不做放置光罩水晶盒於drawer的動作)
            // 9.Box Robot將光罩水晶盒從Drawer內部, 移到Drawer外部(進入drawer內部的前一個點位)
            //10.重複7~9步驟, 完成20個Drawer的光罩水晶盒的移動測試
        }

       


        
    }
}
