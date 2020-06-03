
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.DeviceDrive.LeimacLight;
using MvAssistant.Mac.v1_0.Hal.CompLight;
using MvAssistant.Mac.v1_0.Hal.CompPlc;
using MvAssistant.Mac.v1_0.Manifest;
using System.IO;

namespace MvAssistant.Mac.v1_0.GenCfg.Manifest
{
    [TestClass]

    public class UtGenManifestReal : ManifestBase
    {

        string plcIp = "192.168.0.200";
        int plcPortId = 2;

        [TestMethod]
        public void GenManifestCfgReal()
        {
            var menifest = new MacManifestCfg();

            menifest.Devices.Add(DE_LP_A_ASB());
            menifest.Devices.Add(DE_LP_B_ASB());
            menifest.Devices.Add(DE_IC_A_ASB());
            menifest.Devices.Add(DE_CC_A_ASB());
            menifest.Devices.Add(DE_MT_A_ASB());
            menifest.Devices.Add(DE_CB_A_ASB());
            menifest.Devices.Add(DE_OS_A_ASB());
            menifest.Devices.Add(DE_BT_A_ASB());
            menifest.Devices.Add(DE_UNI_A_ASB());

            menifest.Drivers.AddRange(DriverRealAll());
            menifest.Drivers.AddRange(DriverFakeAll());

            var fn = Path.Combine(@"../../", "GenCfg/Manifest/Manifest.xml.real");
            menifest.SaveToXmlFile(fn);
        }


        MacManifestDeviceCfg DE_BT_A_ASB()
        {
            var rs = new MacManifestDeviceCfg()
            {

                ID = EnumMachineId.DE_BT_A_ASB.ToString(),
                DevConnStr = "127.0.0.1;3039",
                DeviceName = MacEnumDevice.boxtransfer_assembly.ToString(),
                DriverId = ManifestDriverId.BoxTransfer.ToString(),
                PositionId = MacEnumPositionId.BoxTrasnfer01.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3}" , MacHalPlcBase.DevConnStr_Ip, plcIp,MacHalPlcBase.DevConnStr_PortId, plcPortId),
                        DeviceName = MacEnumDevice.boxtransfer_plc.ToString(),
                        DriverId = ManifestDriverId.BoxTransferPlc.ToString(),
                    },

                     new MacManifestDeviceCfg(){
                        ID = EnumMachineId.DE_BT_02.ToString(),
                        DevConnStr = "ip=192.168.0.51",
                        DeviceName = MacEnumDevice.boxtransfer_robot_1.ToString(),
                        DriverId = ManifestDriverId.FanucRobot.ToString(),
                    },

                },
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_ASB()
        {
            var rs = new MacManifestDeviceCfg()
            {

                ID = EnumMachineId.DE_CB_A_ASB.ToString(),
                DevConnStr = null,
                DeviceName = MacEnumDevice.cabinet_assembly.ToString(),
                DriverId = ManifestDriverId.Cabinet.ToString(),
                PositionId = MacEnumPositionId.Cabinet01.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3}" , MacHalPlcBase.DevConnStr_Ip, plcIp,MacHalPlcBase.DevConnStr_PortId, plcPortId),
                        DeviceName = MacEnumDevice.cabinet_plc.ToString(),
                        DriverId = ManifestDriverId.CabinetPlc.ToString(),
                    },


                },
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CC_A_ASB()
        {
            var rs = new MacManifestDeviceCfg()
            {

                ID = EnumMachineId.DE_CC_A_ASB.ToString(),
                DevConnStr = "127.0.0.1;3039",
                DeviceName = MacEnumDevice.clean_assembly.ToString(),
                DriverId = ManifestDriverId.CleanCh.ToString(),
                PositionId = MacEnumPositionId.CleanCh01.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3}" , MacHalPlcBase.DevConnStr_Ip, plcIp, MacHalPlcBase.DevConnStr_PortId, plcPortId),
                        DeviceName = MacEnumDevice.cleanch_plc.ToString(),
                        DriverId = ManifestDriverId.CleanChPlc.ToString(),
                    },

                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                        MacHalLightLeimac.DevConnStr_Ip,
                        "192.168.0.129",
                        MacHalLightLeimac.DevConnStr_Port,
                        1000,
                        MacHalLightLeimac.DevConnStr_Model,
                        MvEnumLeimacModel.IWDV_100S_24,
                        MacHalLightLeimac.DevConnStr_Channel,
                        1),
                        DeviceName = MacEnumDevice.cleanch_inspection_spot_light_001.ToString(),
                        DriverId = ManifestDriverId.LightLeimac.ToString(),
                    },



                },
            };
            return rs;
        }
        MacManifestDeviceCfg DE_IC_A_ASB()
        {
            var rs = new MacManifestDeviceCfg()
            {

                ID = EnumMachineId.DE_IC_A_ASB.ToString(),
                DevConnStr = "127.0.0.1;3039",
                DeviceName = MacEnumDevice.inspection_assembly.ToString(),
                DriverId = ManifestDriverId.InspectionCh.ToString(),
                PositionId = MacEnumPositionId.InspectionCh01.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3}" , MacHalPlcBase.DevConnStr_Ip, plcIp,MacHalPlcBase.DevConnStr_PortId, plcPortId),
                        DeviceName = MacEnumDevice.inspectionch_plc.ToString(),
                        DriverId = ManifestDriverId.InspectionChPlc.ToString(),
                    },


                },
            };
            return rs;
        }
        MacManifestDeviceCfg DE_LP_A_ASB()
        {
            var rs = new MacManifestDeviceCfg()
            {

                ID = EnumMachineId.DE_LP_A_ASB.ToString(),
                DevConnStr = "127.0.0.1;3038",
                DeviceName = MacEnumDevice.loadport_assembly.ToString(),
                DriverId = ManifestDriverId.LoadPort.ToString(),
                PositionId = MacEnumPositionId.LoadPort01.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("ip={0};port={1}", plcIp, plcPortId),
                        DeviceName = MacEnumDevice.loadport_plc.ToString(),
                        DriverId = ManifestDriverId.LoadPortPlc.ToString(),
                    },

                },
            };
            return rs;
        }
        MacManifestDeviceCfg DE_LP_B_ASB()
        {
            var rs = new MacManifestDeviceCfg()
            {

                ID = EnumMachineId.DE_LP_B_ASB.ToString(),
                DevConnStr = "127.0.0.1;3039",
                DeviceName = MacEnumDevice.loadport_assembly.ToString(),
                DriverId = ManifestDriverId.LoadPort.ToString(),
                PositionId = MacEnumPositionId.LoadPort02.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3}" , MacHalPlcBase.DevConnStr_Ip, plcIp,MacHalPlcBase.DevConnStr_PortId, plcPortId),
                        DeviceName = MacEnumDevice.loadport_plc.ToString(),
                        DriverId = ManifestDriverId.LoadPortPlc.ToString(),
                    },

                },
            };
            return rs;
        }
        MacManifestDeviceCfg DE_MT_A_ASB()
        {
            var rs = new MacManifestDeviceCfg()
            {

                ID = EnumMachineId.DE_MT_A_ASB.ToString(),
                DevConnStr = "127.0.0.1;3039",
                DeviceName = MacEnumDevice.masktransfer_assembly.ToString(),
                DriverId = ManifestDriverId.MaskTransfer.ToString(),
                PositionId = MacEnumPositionId.MaskTransfer01.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3}" , MacHalPlcBase.DevConnStr_Ip, plcIp,MacHalPlcBase.DevConnStr_PortId, plcPortId),
                        DeviceName = MacEnumDevice.masktransfer_plc.ToString(),
                        DriverId = ManifestDriverId.MaskTransferPlc.ToString(),
                    },


                    new MacManifestDeviceCfg(){
                        ID = EnumMachineId.DE_MT_A_02.ToString(),
                        DevConnStr = "ip=192.168.0.50",
                        DeviceName = MacEnumDevice.masktransfer_robot_1.ToString(),
                        DriverId = ManifestDriverId.FanucRobot.ToString(),
                    },





                },
            };
            return rs;
        }
        MacManifestDeviceCfg DE_OS_A_ASB()
        {
            var rs = new MacManifestDeviceCfg()
            {

                ID = EnumMachineId.DE_OS_A_ASB.ToString(),
                DevConnStr = "127.0.0.1;3039",
                DeviceName = MacEnumDevice.openstage_assembly.ToString(),
                DriverId = ManifestDriverId.OpenStage.ToString(),
                PositionId = MacEnumPositionId.OpenStage01.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3}" , MacHalPlcBase.DevConnStr_Ip, plcIp,MacHalPlcBase.DevConnStr_PortId, plcPortId),
                        DeviceName = MacEnumDevice.openstage_plc.ToString(),
                        DriverId = ManifestDriverId.OpenStagePlc.ToString(),
                    },
                },
            };
            return rs;
        }
        MacManifestDeviceCfg DE_UNI_A_ASB()
        {
            var rs = new MacManifestDeviceCfg()
            {

                ID = EnumMachineId.DE_UNI_A_ASB.ToString(),
                DevConnStr = null,
                DeviceName = MacEnumDevice.universal_assembly.ToString(),
                DriverId = ManifestDriverId.Universal.ToString(),
                PositionId = null,
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        ID = EnumMachineId.DE_UNI_A_01.ToString(),
                        DevConnStr = string.Format("{0}={1};{2}={3}" , MacHalPlcBase.DevConnStr_Ip, plcIp,MacHalPlcBase.DevConnStr_PortId, plcPortId),
                        DeviceName = MacEnumDevice.universal_plc_01.ToString(),
                        DriverId = ManifestDriverId.UniversalPlc.ToString(),
                    },

                },
            };
            return rs;
        }

    }
}