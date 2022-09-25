using MvAssistant.v0_2.Mac.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Mac.Manifest
{
    public class MacManifestGenCfgBase
    {

        /// <summary>
        /// 在此 Maintain Fake Device 的宣告
        /// </summary>
        /// <returns></returns>
        public List<MacManifestDriverCfg> DriverFakeAll()
        {
            var list = new List<MacManifestDriverCfg>();
            list.AddRange(DriverFakeAssembly());
            list.AddRange(DriverFakeDevice());
            list.AddRange(DriverFakeOpticSensor());
            list.AddRange(DriverFakeCamera());
            list.AddRange(DriverFakeMotion());
            list.AddRange(DriverFakeLight());
            return list;
        }
        public List<MacManifestDriverCfg> DriverFakeAssembly()
        {
            return new MacManifestDriverCfg[]{
                new MacManifestDriverCfg(){
                    Vendor = "Fake",
                    Product = "Fake Box Transfer",
                    Remark = null,
                    AssignType = typeof(Hal.Assembly.MacHalBoxTransferFake),
                    DriverId = MacManifestDriverId.BoxTransferFake.ToString(),
                },
                 new MacManifestDriverCfg(){
                    Vendor = "Fake",
                    Product = "Fake Cabinet",
                    Remark = null,
                    AssignType = typeof(Hal.Assembly.MacHalCabinetFake),
                    DriverId = MacManifestDriverId.CabinetFake.ToString(),
                },
                 new MacManifestDriverCfg(){
                    Vendor = "Fake",
                    Product = "Fake Clean Chamber",
                    Remark = null,
                    AssignType = typeof(Hal.Assembly.MacHalCleanChFake),
                    DriverId = MacManifestDriverId.CleanChFake.ToString(),
                },
                 new MacManifestDriverCfg(){
                    Vendor = "Fake",
                    Product = "Fake Inspection Chamber",
                    Remark = null,
                    AssignType = typeof(Hal.Assembly.MacHalInspectionChFake),
                    DriverId = MacManifestDriverId.InspectionChFake.ToString(),
                },
                 new MacManifestDriverCfg(){
                    Vendor = "Fake",
                    Product = "Fake Load Port",
                    Remark = null,
                    AssignType = typeof(Hal.Assembly.MacHalLoadPortFake),
                    DriverId = MacManifestDriverId.LoadPortFake.ToString(),
                },
                 new MacManifestDriverCfg(){
                    Vendor = "Fake",
                    Product = "Fake Mask Transfer",
                    Remark = null,
                    AssignType = typeof(Hal.Assembly.MacHalMaskTransferFake),
                    DriverId = MacManifestDriverId.MaskTransferFake.ToString(),
                },
                 new MacManifestDriverCfg(){
                    Vendor = "Fake",
                    Product = "Fake Open Stage",
                    Remark = null,
                    AssignType = typeof(Hal.Assembly.MacHalOpenStageFake),
                    DriverId = MacManifestDriverId.OpenStageFake.ToString(),
                },
                 new MacManifestDriverCfg(){
                    Vendor = "Fake",
                    Product = "Fake Equipment",
                    Remark = null,
                    AssignType = typeof(Hal.Assembly.MacHalEqpFake),
                    DriverId = MacManifestDriverId.UniversalFake.ToString(),
                },

            }.ToList();
        }
        public List<MacManifestDriverCfg> DriverFakeCamera()
        {
            return new MacManifestDriverCfg[]{
                new MacManifestDriverCfg(){
                    Vendor="Fake",
                    Product ="Fake Camera",
                    Remark=null,
                    AssignType=typeof(Hal.CompCamera.MacHalCameraFake),
                    DriverId=MacManifestDriverId.CameraFake.ToString(),
                },

            }.ToList();
        }
        public List<MacManifestDriverCfg> DriverFakeDevice()
        {
            return new MacManifestDriverCfg[]{
                new MacManifestDriverCfg(){
                    Vendor = "Fake",
                    Product = "Fake Drawer",
                    Remark = null,
                    AssignType = typeof(Hal.CompDrawer.MacHalDrawerFake),
                    DriverId = MacManifestDriverId.DrawerFake.ToString(),
                },
                new MacManifestDriverCfg(){
                    Vendor = "Fake",
                    Product = "Fake Load Port Unit",
                    Remark = null,
                    AssignType = typeof(Hal.CompLoadPort.MacHalLoadPortFake),
                    DriverId = MacManifestDriverId.LoadPortUnitFake.ToString(),
                }
            }.ToList();
        }
        public List<MacManifestDriverCfg> DriverFakeLight()
        {
            return new MacManifestDriverCfg[]{
                new MacManifestDriverCfg(){
                    Vendor = "Fake",
                    Product = "Fake Light Controller",
                    Remark = null,
                    AssignType = typeof(Hal.CompLight.MacHalLightLeimac),
                    DriverId = MacManifestDriverId.LightLeimac.ToString(),
                    DriverPath = null,
                },
            }.ToList();
        }
        public List<MacManifestDriverCfg> DriverFakeMotion()
        {
            return new MacManifestDriverCfg[]{
                new MacManifestDriverCfg(){
                    Vendor="Fake",
                    Product ="Fake Robot",
                    Remark=null,
                    AssignType=typeof(Hal.CompRobot.MacHalRobotFake),
                    DriverId=MacManifestDriverId.RobotFake.ToString(),
                },


            }.ToList();
        }
        public List<MacManifestDriverCfg> DriverFakeOpticSensor()
        {
            return new MacManifestDriverCfg[]{

            }.ToList();
        }


        /// <summary>
        /// 在此 Maintain Real Device Drive的宣告
        /// </summary>
        /// <returns></returns>
        public List<MacManifestDriverCfg> DriverRealAll()
        {
            var dict = new Dictionary<string, MacManifestDriverCfg>();

            var list = new List<MacManifestDriverCfg>();
            list.AddRange(DriverRealAssembly());
            list.AddRange(DriverRealAssemblyPlc());
            list.AddRange(DriverRealDevice());
            list.AddRange(DriverRealOpticSensor());
            list.AddRange(DriverRealCamera());
            list.AddRange(DriverRealMotion());
            list.AddRange(DriverRealLight());
            return list;
        }
        public List<MacManifestDriverCfg> DriverRealAssembly()
        {
            return new MacManifestDriverCfg[]{
                new MacManifestDriverCfg(){
                    Vendor = "Hirata",
                    Product = "Box Transfer",
                    Remark = null,
                    AssignType = typeof(Hal.Assembly.MacHalBoxTransfer),
                    DriverId = MacManifestDriverId.BoxTransfer.ToString(),
                },
                 new MacManifestDriverCfg(){
                    Vendor = "Hirata",
                    Product = "Cabinet",
                    Remark = null,
                    AssignType = typeof(Hal.Assembly.MacHalCabinet),
                    DriverId = MacManifestDriverId.Cabinet.ToString(),
                },
                 new MacManifestDriverCfg(){
                    Vendor = "Hirata",
                    Product = "Clean Chamber",
                    Remark = null,
                    AssignType = typeof(Hal.Assembly.MacHalCleanCh),
                    DriverId = MacManifestDriverId.CleanCh.ToString(),
                },
                 new MacManifestDriverCfg(){
                    Vendor = "Hirata",
                    Product = "Inspection Chamber",
                    Remark = null,
                    AssignType = typeof(Hal.Assembly.MacHalInspectionCh),
                    DriverId = MacManifestDriverId.InspectionCh.ToString(),
                },
                 new MacManifestDriverCfg(){
                    Vendor = "Hirata",
                    Product = "Load Port",
                    Remark = null,
                    AssignType = typeof(Hal.Assembly.MacHalLoadPort),
                    DriverId = MacManifestDriverId.LoadPort.ToString(),
                },
                 new MacManifestDriverCfg(){
                    Vendor = "Hirata",
                    Product = "Mask Transfer",
                    Remark = null,
                    AssignType = typeof(Hal.Assembly.MacHalMaskTransfer),
                    DriverId = MacManifestDriverId.MaskTransfer.ToString(),
                },
                 new MacManifestDriverCfg(){
                    Vendor = "Hirata",
                    Product = "Open Stage",
                    Remark = null,
                    AssignType = typeof(Hal.Assembly.MacHalOpenStage),
                    DriverId = MacManifestDriverId.OpenStage.ToString(),
                },
                 new MacManifestDriverCfg(){
                    Vendor = "Hirata",
                    Product = "Equipment",
                    Remark = null,
                    AssignType = typeof(Hal.Assembly.MacHalEqp),
                    DriverId = MacManifestDriverId.Eqp.ToString(),
                },

            }.ToList();
        }
        public List<MacManifestDriverCfg> DriverRealAssemblyPlc()
        {
            return new MacManifestDriverCfg[]{
                 new MacManifestDriverCfg(){
                    Vendor = "Omron",
                    Product = "PLC",
                    Remark = null,
                    AssignType = typeof(Hal.CompPlc.MacHalPlcEqp),
                    DriverId = MacManifestDriverId.UniversalPlc.ToString(),
                    DriverPath =null,
                },

                new MacManifestDriverCfg(){
                    Vendor = "Hirata",
                    Product = "Box Transfer PLC",
                    Remark = null,
                    AssignType = typeof(Hal.CompPlc.MacHalPlcBoxTransfer),
                    DriverId = MacManifestDriverId.BoxTransferPlc.ToString(),
                },
                 new MacManifestDriverCfg(){
                    Vendor = "Hirata",
                    Product = "Cabinet PLC",
                    Remark = null,
                    AssignType = typeof(Hal.CompPlc.MacHalPlcCabinet),
                    DriverId = MacManifestDriverId.CabinetPlc.ToString(),
                },
                 new MacManifestDriverCfg(){
                    Vendor = "Hirata",
                    Product = "Clean Chamber PLC",
                    Remark = null,
                    AssignType = typeof(Hal.CompPlc.MacHalPlcCleanCh),
                    DriverId = MacManifestDriverId.CleanChPlc.ToString(),
                },
                 new MacManifestDriverCfg(){
                    Vendor = "Hirata",
                    Product = "Inspection Chamber PLC",
                    Remark = null,
                    AssignType = typeof(Hal.CompPlc.MacHalPlcInspectionCh),
                    DriverId = MacManifestDriverId.InspectionChPlc.ToString(),
                },
                 new MacManifestDriverCfg(){
                    Vendor = "Hirata",
                    Product = "Load Port PLC",
                    Remark = null,
                    AssignType = typeof(Hal.CompPlc.MacHalPlcLoadPort),
                    DriverId = MacManifestDriverId.LoadPortPlc.ToString(),
                },
                 new MacManifestDriverCfg(){
                    Vendor = "Hirata",
                    Product = "Mask Transfer PLC",
                    Remark = null,
                    AssignType = typeof(Hal.CompPlc.MacHalPlcMaskTransfer),
                    DriverId = MacManifestDriverId.MaskTransferPlc.ToString(),
                },
                 new MacManifestDriverCfg(){
                    Vendor = "Hirata",
                    Product = "Open Stage PLC",
                    Remark = null,
                    AssignType = typeof(Hal.CompPlc.MacHalPlcOpenStage),
                    DriverId = MacManifestDriverId.OpenStagePlc.ToString(),
                },

            }.ToList();
        }
        public List<MacManifestDriverCfg> DriverRealCamera()
        {
            return new MacManifestDriverCfg[]{
                new MacManifestDriverCfg(){
                    Vendor="Sentech",
                    Product ="Camera Controller",
                    Remark=null,
                    AssignType=typeof(Hal.CompCamera.MacHalCameraSenTech),
                    DriverId=MacManifestDriverId.CameraSentech.ToString(),
                },
                new MacManifestDriverCfg(){
                    Vendor="Sentech",
                    Product ="Camera Link",
                    Remark=null,
                    AssignType=typeof(Hal.CompCamera.MacHalCameraLink),
                    DriverId=MacManifestDriverId.CameraLink.ToString(),
                }
            }.ToList();
        }
        public List<MacManifestDriverCfg> DriverRealDevice()
        {
            return new MacManifestDriverCfg[]{
                new MacManifestDriverCfg(){
                    Vendor = "KJ Machine",
                    Product = "Drawer",
                    Remark = null,
                    AssignType = typeof(Hal.CompDrawer.MacHalDrawerKjMachine),
                    DriverId = MacManifestDriverId.DrawerKjMachine.ToString(),
                },
                new MacManifestDriverCfg(){
                    Vendor = "Gudeng",
                    Product = "Load Port",
                    Remark = null,
                    AssignType = typeof(Hal.CompLoadPort.MacHalLoadPortGudeng),
                    DriverId = MacManifestDriverId.LoadPortGudeng.ToString(),
                }
            }.ToList();
        }
        public List<MacManifestDriverCfg> DriverRealLight()
        {
            return new MacManifestDriverCfg[]{
                new MacManifestDriverCfg(){
                    Vendor = "Leimac",
                    Product = "Light Controller",
                    Remark = null,
                    AssignType = typeof(Hal.CompLight.MacHalLightLeimac),
                    DriverId = MacManifestDriverId.LightLeimac.ToString(),
                    DriverPath = null,
                },
            }.ToList();
        }
        public List<MacManifestDriverCfg> DriverRealMotion()
        {
            return new MacManifestDriverCfg[]{
                   new MacManifestDriverCfg(){
                    Vendor = "Fanuc",
                    Product = "Fanuc Robot LR Mate 200iD",
                    Remark = null,
                    AssignType = typeof(Hal.CompRobot. MacHalRobotFanuc),
                    DriverId = MacManifestDriverId.RobotFanuc.ToString(),
                    DriverPath = null,
                },

            }.ToList();
        }
        public List<MacManifestDriverCfg> DriverRealOpticSensor()
        {
            return new MacManifestDriverCfg[]{

            }.ToList();
        }
    }
}
