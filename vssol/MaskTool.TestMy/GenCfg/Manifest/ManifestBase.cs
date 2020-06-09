using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.GenCfg.Manifest
{
    public class ManifestBase
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

            }.ToList();
        }
        public List<MacManifestDriverCfg> DriverFakeCamera()
        {
            return new MacManifestDriverCfg[]{


            }.ToList();
        }
        public List<MacManifestDriverCfg> DriverFakeDevice()
        {
            return new MacManifestDriverCfg[]{

            }.ToList();
        }
        public List<MacManifestDriverCfg> DriverFakeLight()
        {
            return new MacManifestDriverCfg[]{
            }.ToList();
        }
        public List<MacManifestDriverCfg> DriverFakeMotion()
        {
            return new MacManifestDriverCfg[]{



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
                    DriverId = ManifestDriverId.BoxTransfer.ToString(),
                },
                 new MacManifestDriverCfg(){
                    Vendor = "Hirata",
                    Product = "Cabinet",
                    Remark = null,
                    AssignType = typeof(Hal.Assembly.MacHalCabinet),
                    DriverId = ManifestDriverId.Cabinet.ToString(),
                },
                 new MacManifestDriverCfg(){
                    Vendor = "Hirata",
                    Product = "Clean Chamber",
                    Remark = null,
                    AssignType = typeof(Hal.Assembly.MacHalCleanCh),
                    DriverId = ManifestDriverId.CleanCh.ToString(),
                },
                 new MacManifestDriverCfg(){
                    Vendor = "Hirata",
                    Product = "Inspection Chamber",
                    Remark = null,
                    AssignType = typeof(Hal.Assembly.MacHalInspectionCh),
                    DriverId = ManifestDriverId.InspectionCh.ToString(),
                },
                 new MacManifestDriverCfg(){
                    Vendor = "Hirata",
                    Product = "Load Port",
                    Remark = null,
                    AssignType = typeof(Hal.Assembly.MacHalLoadPort),
                    DriverId = ManifestDriverId.LoadPort.ToString(),
                },
                 new MacManifestDriverCfg(){
                    Vendor = "Hirata",
                    Product = "Mask Transfer",
                    Remark = null,
                    AssignType = typeof(Hal.Assembly.MacHalMaskTransfer),
                    DriverId = ManifestDriverId.MaskTransfer.ToString(),
                },
                 new MacManifestDriverCfg(){
                    Vendor = "Hirata",
                    Product = "Open Stage",
                    Remark = null,
                    AssignType = typeof(Hal.Assembly.MacHalOpenStage),
                    DriverId = ManifestDriverId.OpenStage.ToString(),
                },
                 new MacManifestDriverCfg(){
                    Vendor = "Hirata",
                    Product = "Universal",
                    Remark = null,
                    AssignType = typeof(Hal.Assembly.MacHalUniversal),
                    DriverId = ManifestDriverId.Universal.ToString(),
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
                    AssignType = typeof(Hal.CompPlc.MacHalPlcUniversal),
                    DriverId = ManifestDriverId.UniversalPlc.ToString(),
                    DriverPath =null,
                },

                new MacManifestDriverCfg(){
                    Vendor = "Hirata",
                    Product = "Box Transfer PLC",
                    Remark = null,
                    AssignType = typeof(Hal.CompPlc.MacHalPlcBoxTransfer),
                    DriverId = ManifestDriverId.BoxTransferPlc.ToString(),
                },
                 new MacManifestDriverCfg(){
                    Vendor = "Hirata",
                    Product = "Cabinet PLC",
                    Remark = null,
                    AssignType = typeof(Hal.CompPlc.MacHalPlcCabinet),
                    DriverId = ManifestDriverId.CabinetPlc.ToString(),
                },
                 new MacManifestDriverCfg(){
                    Vendor = "Hirata",
                    Product = "Clean Chamber PLC",
                    Remark = null,
                    AssignType = typeof(Hal.CompPlc.MacHalPlcCleanCh),
                    DriverId = ManifestDriverId.CleanChPlc.ToString(),
                },
                 new MacManifestDriverCfg(){
                    Vendor = "Hirata",
                    Product = "Inspection Chamber PLC",
                    Remark = null,
                    AssignType = typeof(Hal.CompPlc.MacHalPlcInspectionCh),
                    DriverId = ManifestDriverId.InspectionChPlc.ToString(),
                },
                 new MacManifestDriverCfg(){
                    Vendor = "Hirata",
                    Product = "Load Port PLC",
                    Remark = null,
                    AssignType = typeof(Hal.CompPlc.MacHalPlcLoadPort),
                    DriverId = ManifestDriverId.LoadPortPlc.ToString(),
                },
                 new MacManifestDriverCfg(){
                    Vendor = "Hirata",
                    Product = "Mask Transfer PLC",
                    Remark = null,
                    AssignType = typeof(Hal.CompPlc.MacHalPlcMaskTransfer),
                    DriverId = ManifestDriverId.MaskTransferPlc.ToString(),
                },
                 new MacManifestDriverCfg(){
                    Vendor = "Hirata",
                    Product = "Open Stage PLC",
                    Remark = null,
                    AssignType = typeof(Hal.CompPlc.MacHalPlcOpenStage),
                    DriverId = ManifestDriverId.OpenStagePlc.ToString(),
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
                    AssignType=typeof(Hal.CompCamera.HalCameraSenTech),
                    DriverId=ManifestDriverId.SentechCamera.ToString(),
                }
            }.ToList();
        }
        public List<MacManifestDriverCfg> DriverRealDevice()
        {
            return new MacManifestDriverCfg[]{

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
                    DriverId = ManifestDriverId.LightLeimac.ToString(),
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
                    AssignType = typeof(Hal.Component.Robot.HalRobotFanuc),
                    DriverId = ManifestDriverId.FanucRobot.ToString(),
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
