using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Manifest;

namespace MvAssistant.Mac.TestMy.MachineRealHal
{

    [TestClass]
    public class UtHalBoxTransfer
    {
        /// <summary>路徑測試</summary>
        /// <remarks>King, 2020/05/25</remarks>
        [TestMethod]
        public void TestPathMove()
        {
            int drawerIndex = 1;//            default(int);
            int boxIndex = default(int);
            try
            {
                using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
                {
                    halContext.Load();


                    var mt = halContext.HalDevices[MacEnumDevice.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;

                    if (mt.HalConnect() != 0)
                    {
                        System.Diagnostics.Debug.WriteLine("Connect Fail");
                    }
                    mt.BackCabinet1Home();//[V] 

                    mt.ChangeDirectionToFaceCabinet(drawerIndex);// 執行前先調整 drawerIndex 變數
                    mt.ForwardToCabinet(drawerIndex,boxIndex); // 執行前先調整 drawerIndex 及 boxIndex變數
                    mt.BackwardFromDrawer(drawerIndex, boxIndex);// 執行前先調整 drawerIndex 及 boxIndex變數
                    mt.ChangeDirectionToFaceOpenStage();
                    mt.ForwardToOpenStage();
                    mt.BackwardFromOpenStage();


                }
            }
            catch (Exception ex) { throw ex; }

        }
    

        [TestMethod]
        public void TestSetParameter()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.Load();

                var bt = halContext.HalDevices[MacEnumDevice.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;

                bt.SetSpeed(20);
                bt.SetHandSpaceLimit(10, 20);
                bt.SetClampToCabinetSpaceLimit(50);
                bt.SetLevelSensorLimit(5, 6);
                bt.SetSixAxisSensorLimit(1, 2, 3, 4, 5, 6);
            }
        }

        [TestMethod]
        public void TestReadParameter()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.Load();

                var bt = halContext.HalDevices[MacEnumDevice.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;

                bt.ReadSpeedSetting();
                bt.ReadHandSpaceLimitSetting();
                bt.ReadClampToCabinetSpaceLimitSetting();
                bt.ReadLevelSensorLimitSetting();
                bt.ReadSixAxisSensorLimitSetting();
            }
        }

        [TestMethod]
        public void TestReadComponentValue()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.Load();

                var bt = halContext.HalDevices[MacEnumDevice.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;

                bt.ReadHandPos();
                bt.ReadBoxDetect();
                bt.ReadHandPosByLSR();
                bt.ReadClampDistance();
                bt.ReadLevelSensor();
                bt.ReadSixAxisSensor();
                bt.ReadHandVacuum();
            }
        }

        [TestMethod]
        public void TestAssemblyWork()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.Load();

                var bt = halContext.HalDevices[MacEnumDevice.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;

                bt.Clamp(1);
                bt.Unclamp();
                bt.LevelReset();
                bt.ReadBTRobotStatus();
                bt.RobotMoving(false);
                bt.Initial();
            }
        }
    }
}
