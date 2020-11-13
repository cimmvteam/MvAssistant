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
      
        List<MacEnumDevice> Drawers;
        List<BoxrobotTransferLocation> DrawerLocations;
        BoxrobotTransferPathFile pathFileObj;//= new BoxrobotTransferPathFile(PositionInstance.BTR_Path);
        public Ut001_BT()
        {
             Drawers = HalDrawerExtends.DrawerKeys;
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
                for (var i = 0; i < Drawers.Count; i++)
                {
                    var drawerCab = halContext.HalDevices[Drawers[i].ToString()] as MacHalCabinet;
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
                // 1. 光罩鐵盒放置於Open Stage平台上  => 假設 OK
                
                 
                var path = pathFileObj.FromOpenStageToCabinet01Home_GET_PathFile();
                boxTransfer.Move(path);
               for(int i=0;0< Drawers.Count;i++)
                {
                    // 取得 Drawer
                    var drawer = halContext.GetDrawer(Drawers[i]);
                    var drawerLocation = DrawerLocations[i];
                    // 2. Box Robot從Home點至Open Stage進行光罩鐵盒夾取
                    //   2.1 BoxRobot 從Home點至Open Stage進行光罩鐵盒夾取
                    //   1.2 Boxrobot 移到 OpenStage
                    //   1.3 檢查入侵Open State
                    //   1.4 夾取 
                    //   1.5 回到 Home
                    var home = DrawerLocations[i].GetCabinetHomeCode();
                    if (home.Item1)
                    {
                        // 2. Box Robot 從Home點至Open Stage進行光罩鐵盒夾取
                        //   2.1 檢查侵入Open Stage
                        //   2.2 移到 Open Stage
                        //   2.3 抓取 盒子
                        //   2.4 回到 Home

                        // 3. Box Robot將光罩鐵盒從Open Stage移動, 移入Drawer內部 (夾爪夾持光罩鐵盒, 不做放置光罩鐵盒於drawer的動作)
                        //   3.1  drawer Tray 移到 In
                        drawer.Initial();
                        drawer.MoveTrayToIn();
                        if (home.Item2 == BoxrobotTransferLocation.Cabinet_01_Home)
                        {
                            path = pathFileObj.FromCabinet01HomeToDrawer_PUT_PathFile(drawerLocation);
                            boxTransfer.Move(path);
                        }
                        else //if(home.Item2 == BoxrobotTransferLocation.Cabinet_02_Home)
                        {
                            path = pathFileObj.FromCabinet01HomeToDrawer_PUT_PathFile(drawerLocation);
                            boxTransfer.Move(path);
                        }
                    }
               }
                // 1. 光罩鐵盒放置於Open Stage平台上

                // 2. Box Robot從Home點至Open Stage進行光罩鐵盒夾取

                // 3. Box Robot將光罩鐵盒從Open Stage移動, 移入Drawer內部 (夾爪夾持光罩鐵盒, 不做放置光罩鐵盒於drawer的動作)

                // 4. Box Robot將光罩鐵盒從Drawer內部, 移到Drawer外部 (進入drawer內部的前一個點位)

                // 5. 重複2~4步驟, 完成20個Drawer的光罩鐵盒的移動測試

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
