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
        /// <para>1-07. Box Robot(無夾持光罩盒) 從Open Stage 移回 Home 點
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

                // 1-03 Box Robot 從Home 點至Drawer 夾取點, 進行光罩鐵盒夾取(Clamp光鐵盒)
                BREAK_POINT++;

                if (drawerHome == BoxrobotTransferLocation.Cabinet_01_Home)
                {
                   filePath = pathFileObj.FromCabinet01HomeToDrawer_GET_PathFile(drawerLocation);
                   
                }
                else //if (drawerHome == BoxrobotTransferLocation.Cabinet_02_Home)
                {
                    HalBoxTransfer.TurnToCB2Home();
                    var filePath = pathFileObj.FromCabinet02HomeToDrawer_GET_PathFile(drawerLocation);
                }
                HalBoxTransfer.Move(filePath);
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
                    Debug.WriteLine("Invoke OnPositionStatusHandler,  Drawer= " + rtnDrawer.DeviceIndex + ", IHOStatus=" + eventArgs.IHOStatus);
                };
                drawer.OnTrayArriveOutHandler += (sender, e) =>
                {
                    var rtnDrawer = ((IMacHalDrawer)sender);
                    Debug.WriteLine("Invoke OnPositionStatusHandler,  Drawer= " + rtnDrawer.DeviceIndex + ", IHOStatus=" + eventArgs.IHOStatus);
                };
                drawer.OnTrayArriveInHandler += (sender, e) =>
                {
                    var rtnDrawer = ((IMacHalDrawer)sender);
                    Debug.WriteLine("Invoke OnPositionStatusHandler,  Drawer= " + rtnDrawer.DeviceIndex + ", IHOStatus=" + eventArgs.IHOStatus);
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
