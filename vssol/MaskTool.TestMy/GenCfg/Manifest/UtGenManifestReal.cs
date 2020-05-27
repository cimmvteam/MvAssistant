
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
                Level = "1",
                DriverId = ManifestDriverId.BoxTransfer.ToString(),
                PositionId = MacEnumPositionId.BoxTrasnfer01.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("ip={0};port={1}", plcIp, plcPortId),
                        DeviceName = MacEnumDevice.boxtransfer_plc.ToString(),
                        Level = "2",
                        DriverId = ManifestDriverId.BoxTransferPlc.ToString(),
                    },

                     new MacManifestDeviceCfg(){
                        ID = EnumMachineId.DE_BT_02.ToString(),
                        DevConnStr = "ip=192.168.0.51",
                        DeviceName = MacEnumDevice.boxtransfer_robot_1.ToString(),
                        Level = "2",
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
                DevConnStr = "127.0.0.1;3039",
                DeviceName = MacEnumDevice.cabinet_assembly.ToString(),
                Level = "1",
                DriverId = ManifestDriverId.Cabinet.ToString(),
                PositionId = MacEnumPositionId.Cabinet01.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("ip={0};port={1}", plcIp, plcPortId),
                        DeviceName = MacEnumDevice.cabinet_plc.ToString(),
                        Level = "2",
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
                Level = "1",
                DriverId = ManifestDriverId.CleanCh.ToString(),
                PositionId = MacEnumPositionId.CleanCh01.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("ip={0};port={1}", plcIp, plcPortId),
                        DeviceName = MacEnumDevice.cleanch_plc.ToString(),
                        Level = "2",
                        DriverId = ManifestDriverId.CleanChPlc.ToString(),
                    },

                    new MacManifestDeviceCfg(){
                        ID = EnumMachineId.DE_CC_A_33.ToString(),
                        DevConnStr = "DE_PLC_A;CC_laser1",
                        DeviceName = MacEnumDevice.clean_laser_entry_1.ToString(),
                        Level = "2",
                        DriverId = ManifestDriverId.LaserEntry_OmronPlc.ToString(),
                    },
                    new MacManifestDeviceCfg(){
                        ID = EnumMachineId.DE_CC_A_34.ToString(),
                        DevConnStr = "DE_PLC_A;CC_laser2",
                        DeviceName = MacEnumDevice.clean_laser_entry_2.ToString(),
                        Level = "2",
                        DriverId = ManifestDriverId.LaserEntry_OmronPlc.ToString(),
                    },

                    new MacManifestDeviceCfg(){
                        ID = EnumMachineId.DE_CC_A_35.ToString(),
                        DevConnStr = "DE_PLC_A;CC_collision_laser_1",
                        DeviceName = MacEnumDevice.clean_laser_prevent_collision_1.ToString(),
                        Level = "2",
                        DriverId = ManifestDriverId.LaserCollision_OmronPlc.ToString(),
                    },
                    new MacManifestDeviceCfg(){
                        ID = EnumMachineId.DE_CC_A_36.ToString(),
                        DevConnStr = "DE_PLC_A;CC_collision_laser_2",
                        DeviceName = MacEnumDevice.clean_laser_prevent_collision_2.ToString(),
                        Level = "2",
                        DriverId = ManifestDriverId.LaserCollision_OmronPlc.ToString(),
                    },
                    new MacManifestDeviceCfg(){
                        ID = EnumMachineId.DE_CC_A_37.ToString(),
                        DevConnStr = "DE_PLC_A;CC_collision_laser_3",
                        DeviceName = MacEnumDevice.clean_laser_prevent_collision_3.ToString(),
                        Level = "2",
                        DriverId = ManifestDriverId.LaserCollision_OmronPlc.ToString(),
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
                Level = "1",
                DriverId = ManifestDriverId.InspectionCh.ToString(),
                PositionId = MacEnumPositionId.InspectionCh01.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("ip={0};port={1}", plcIp, plcPortId),
                        DeviceName = MacEnumDevice.inspectionch_plc.ToString(),
                        Level = "2",
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
                Level = "1",
                DriverId = ManifestDriverId.LoadPort.ToString(),
                PositionId = MacEnumPositionId.LoadPort01.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("ip={0};port={1}", plcIp, plcPortId),
                        DeviceName = MacEnumDevice.loadport_plc.ToString(),
                        Level = "2",
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
                Level = "1",
                DriverId = ManifestDriverId.LoadPort.ToString(),
                PositionId = MacEnumPositionId.LoadPort02.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("ip={0};port={1}", plcIp, plcPortId),
                        DeviceName = MacEnumDevice.loadport_plc.ToString(),
                        Level = "2",
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
                Level = "1",
                DriverId = ManifestDriverId.MaskTransfer.ToString(),
                PositionId = MacEnumPositionId.MaskTransfer01.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("ip={0};port={1}", plcIp, plcPortId),
                        DeviceName = MacEnumDevice.masktransfer_plc.ToString(),
                        Level = "2",
                        DriverId = ManifestDriverId.MaskTransferPlc.ToString(),
                    },


                    new MacManifestDeviceCfg(){
                        ID = EnumMachineId.DE_MT_A_02.ToString(),
                        DevConnStr = "ip=192.168.0.50",
                        DeviceName = MacEnumDevice.masktransfer_robot_1.ToString(),
                        Level = "2",
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
                Level = "1",
                DriverId = ManifestDriverId.OpenStage.ToString(),
                PositionId = MacEnumPositionId.OpenStage01.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("ip={0};port={1}", plcIp, plcPortId),
                        DeviceName = MacEnumDevice.openstage_plc.ToString(),
                        Level = "2",
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
                Level = "1",
                DriverId = ManifestDriverId.Universal.ToString(),
                PositionId = null,
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        ID = EnumMachineId.DE_UNI_A_01.ToString(),
                        DevConnStr = string.Format("ip={0};portid={1}", this.plcIp, this.plcPortId),
                        DeviceName = MacEnumDevice.universal_plc_01.ToString(),
                        Level = "2",
                        DriverId = ManifestDriverId.UniversalPlc.ToString(),
                    },

                },
            };
            return rs;
        }

    }
}