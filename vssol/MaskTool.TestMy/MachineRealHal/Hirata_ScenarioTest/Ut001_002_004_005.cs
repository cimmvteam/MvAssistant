using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.DeviceDrive.KjMachineDrawer.DrawerEventArgs;
using MvAssistant.Mac.TestMy.MachineRealHal.Hirata_ScenarioTest.Extends;
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
        string filePath;
        public Ut001_002_004_005()
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
            HalOpenStage.Initial();
            HalBoxTransfer.Initial();

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
        /// <paramref name="boxType"/>
        /// <paramref name="autoConnect"/>
        /// <paramref name="getComeraShot"/>
        [TestMethod]
        [DataRow(BoxType.IronBox,false)]
        [DataRow(BoxType.CrystalBox,false)]
        public void Test_MainMethod(BoxType boxType, bool getComeraShot)
        {
            int BREAK_POINT = 0;
          
            
            // Connect, 所有Drawer 
            var failedConnectDrawers =HalContext.DrawersConnect();

            
            var drawer0101 = HalContext.GetDrawer(DrawerKeys[0]);
            drawer0101.Initial();
            drawer0101.MoveTrayToOut();

            // 確認 光置在 01_01
            BREAK_POINT++;

            drawer0101.MoveTrayToHome();
           
            //BindEvent(failedConnectDrawers);
            for(var i = 0; i < DrawerKeys.Count; i++)
             {
                    var drawerKey = DrawerKeys[i];
                    var drawerLocation = DrawerLocations[i];
                    var drawer = HalContext.GetDrawer(drawerKey);
                var drawerHome = drawerLocation.GetCabinetHomeCode().Item2;
                HalBoxTransfer.TurnToCB1Home();
                
                // 1-01 確認光罩盒在 Drawer 內
                BREAK_POINT++;// 確認

                // 1-02  Drawer 往機台內部移動 到Box Robot 可以存光罩鐵盒的位置
                drawer.MoveTrayToIn();

                
                BREAK_POINT++;

                // 1-03 Box Robot 從Home 點至Drawer 夾取點, 進行光罩鐵盒夾取(Clamp光鐵盒)
                if (drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                {
                    HalBoxTransfer.TurnToCB2Home();
                 }
                filePath = pathFileObj.GetFromCabinetHomeToDrawerGetPath(drawerLocation);
                HalBoxTransfer.Move(filePath);

                HalBoxTransfer.Clamp((uint)boxType);


                BREAK_POINT++;

                //1-04. Box Robot 將光罩盒從Drawer 移動到Open stage Entry</para>
                filePath = pathFileObj.GetFromDrawerToCabitnetHomeGetPath(drawerLocation);
                HalBoxTransfer.Move(filePath);

                if (drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                {
                    HalBoxTransfer.TurnToCB1Home();
                }

                HalOpenStage.ReadRobotIntrude(true, null);
                HalOpenStage.SortUnclamp();
                HalOpenStage.Lock();

                filePath = pathFileObj.FromCabinet01HomeToOpenStage_PUT_PathFile();
                HalBoxTransfer.Move(filePath);

                BREAK_POINT++;


                // 1-05. Drawer 回到Cabinet 內
                drawer.MoveTrayToHome();

                // 1-06. Box Robot將光罩盒放在 Open Stage平台上
                HalBoxTransfer.Unclamp();

                // 1-07.Box Robot(無夾持光罩盒) 從Open Stage 移回 Home 點
                filePath = pathFileObj.FromOpenStageToCabinet01Home_PUT_PathFile();
                HalBoxTransfer.Move(filePath);
                HalOpenStage.SetBoxTypeAndSortClamp(boxType);

                BREAK_POINT++;

                // 2-01 光罩鐵盒位於於Open Stage平台上
                // 目視,　確認
                BREAK_POINT++;

                // 2-02. Drawer往機台內部移動到Box Robot可以存取光罩鐵盒的位置
                drawer.CommandTrayMotionIn();

                // 2-03. Box Robot從Home點至Open Stage進行光罩鐵盒夾取
                HalOpenStage.ReadRobotIntrude(true,null);
                HalOpenStage.SortUnclampAndLock();
                filePath = pathFileObj.FromCabinet01HomeToOpenStage_GET_PathFile();
                HalBoxTransfer.Move(filePath);

                // 2-04. Box Robot將光罩鐵盒從Open Stage夾取並放置於Drawer內 (release光罩鐵盒)
                HalBoxTransfer.Clamp((uint)boxType);
                filePath = pathFileObj.FromOpenStageToCabinet01Home_GET_PathFile();
                HalBoxTransfer.Move(filePath);

                if (drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                {
                    HalBoxTransfer.TurnToCB2Home();
                }

                filePath = pathFileObj.GetFromCabinetHomeToDrawerPutPath(drawerLocation);
                HalBoxTransfer.Move(filePath);

                BREAK_POINT++; // 1. 確認 DRAWER Tray 是否在In, 確認 BoxTRansfer 是否到位 

                HalBoxTransfer.Unclamp();

                BREAK_POINT++;// 確認 Box 是否已在 Tray 上 

                // 2-05. Box Robot退回Home點
                filePath = pathFileObj.GetFromDrawerToCabitnetHomeGetPath(drawerLocation);
                HalBoxTransfer.Move(filePath);

                // 2-06. Drawer回到Cabinet內
                drawer.Initial();

                // 2-07.測試(編號13-CCD): 開啟光源 -> 拍照-> 關閉光源, 功能是否正常
                HalBoxTransfer.CameraShot("Ut001_002_004_005");

                BREAK_POINT++;
            }

            Repeat();
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
