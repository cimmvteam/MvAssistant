
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.DeviceDrive.LeimacLight;
using MvAssistant.Mac.v1_0.Hal.CompDrawer;
using MvAssistant.Mac.v1_0.Hal.CompLight;
using MvAssistant.Mac.v1_0.Hal.CompLoadPort;
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
                DevConnStr = null,
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
                        DevConnStr = "ip=192.168.0.150",
                        DeviceName = MacEnumDevice.boxtransfer_robot_1.ToString(),
                        DriverId = ManifestDriverId.FanucRobot.ToString(),
                    },

                      new MacManifestDeviceCfg(){
                        DevConnStr = "id=00:11:1C:F9:A3:23",
                        DeviceName = MacEnumDevice.boxtransfer_camera_gripper_1.ToString(),
                        DriverId = ManifestDriverId.CameraSentech.ToString(),
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


                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalDrawerKjMachine.DevConnStr_Ip, "192.168.0.xxx",
                            MacHalDrawerKjMachine.DevConnStr_Port, 5000,
                            MacHalDrawerKjMachine.DevConnStr_LocalIp, "",
                            MacHalDrawerKjMachine.DevConnStr_LocalPort, 6000),
                        DeviceName = MacEnumDevice.cabinet_drawer_01_01.ToString(),
                        DriverId = ManifestDriverId.DrawerKjMachine.ToString(),
                    },
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalDrawerKjMachine.DevConnStr_Ip, "192.168.0.xxx",
                            MacHalDrawerKjMachine.DevConnStr_Port, 5000,
                            MacHalDrawerKjMachine.DevConnStr_LocalIp, "",
                            MacHalDrawerKjMachine.DevConnStr_LocalPort, 6000),
                        DeviceName = MacEnumDevice.cabinet_drawer_01_02.ToString(),
                        DriverId = ManifestDriverId.DrawerKjMachine.ToString(),
                    },
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalDrawerKjMachine.DevConnStr_Ip, "192.168.0.xxx",
                            MacHalDrawerKjMachine.DevConnStr_Port, 5000,
                            MacHalDrawerKjMachine.DevConnStr_LocalIp, "",
                            MacHalDrawerKjMachine.DevConnStr_LocalPort, 6000),
                        DeviceName = MacEnumDevice.cabinet_drawer_01_03.ToString(),
                        DriverId = ManifestDriverId.DrawerKjMachine.ToString(),
                    },
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalDrawerKjMachine.DevConnStr_Ip, "192.168.0.xxx",
                            MacHalDrawerKjMachine.DevConnStr_Port, 5000,
                            MacHalDrawerKjMachine.DevConnStr_LocalIp, "",
                            MacHalDrawerKjMachine.DevConnStr_LocalPort, 6000),
                        DeviceName = MacEnumDevice.cabinet_drawer_01_04.ToString(),
                        DriverId = ManifestDriverId.DrawerKjMachine.ToString(),
                    },
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalDrawerKjMachine.DevConnStr_Ip, "192.168.0.xxx",
                            MacHalDrawerKjMachine.DevConnStr_Port, 5000,
                            MacHalDrawerKjMachine.DevConnStr_LocalIp, "",
                            MacHalDrawerKjMachine.DevConnStr_LocalPort, 6000),
                        DeviceName = MacEnumDevice.cabinet_drawer_01_05.ToString(),
                        DriverId = ManifestDriverId.DrawerKjMachine.ToString(),
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
                            MacHalLightLeimac.DevConnStr_Ip, "192.168.0.129",
                            MacHalLightLeimac.DevConnStr_Port, 1000,
                            MacHalLightLeimac.DevConnStr_Model, MvEnumLeimacModel.IWDV_100S_24,
                            MacHalLightLeimac.DevConnStr_Channel, 1),
                        DeviceName = MacEnumDevice.cleanch_inspection_spot_light_001.ToString(),
                        DriverId = ManifestDriverId.LightLeimac.ToString(),
                    },

                    new MacManifestDeviceCfg(){
                        DevConnStr = "id=00:11:1C:F9:A4:08",
                        DeviceName = MacEnumDevice.clean_camera_particle_1.ToString(),
                        DriverId = ManifestDriverId.CameraSentech.ToString(),
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
                DevConnStr = null,
                DeviceName = MacEnumDevice.inspection_assembly.ToString(),
                DriverId = ManifestDriverId.InspectionCh.ToString(),
                PositionId = MacEnumPositionId.InspectionCh01.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3}" , MacHalPlcBase.DevConnStr_Ip, plcIp,MacHalPlcBase.DevConnStr_PortId, plcPortId),
                        DeviceName = MacEnumDevice.inspectionch_plc.ToString(),
                        DriverId = ManifestDriverId.InspectionChPlc.ToString(),
                    },
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalLightLeimac.DevConnStr_Ip, "192.168.0.160",
                            MacHalLightLeimac.DevConnStr_Port, 1000,
                            MacHalLightLeimac.DevConnStr_Model, MvEnumLeimacModel.IDGB_50M4PG_24_TP,
                            MacHalLightLeimac.DevConnStr_Channel, 1),
                        DeviceName = MacEnumDevice.inspectionch_light_circle_defense_top_001.ToString(),
                        DriverId = ManifestDriverId.LightLeimac.ToString(),
                    },
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalLightLeimac.DevConnStr_Ip, "192.168.0.160",
                            MacHalLightLeimac.DevConnStr_Port, 1000,
                            MacHalLightLeimac.DevConnStr_Model, MvEnumLeimacModel.IDGB_50M4PG_24_TP,
                            MacHalLightLeimac.DevConnStr_Channel, 2),
                        DeviceName = MacEnumDevice.inspectionch_light_bar_inspection_side_001.ToString(),
                        DriverId = ManifestDriverId.LightLeimac.ToString(),
                    },
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalLightLeimac.DevConnStr_Ip, "192.168.0.160",
                            MacHalLightLeimac.DevConnStr_Port, 1000,
                            MacHalLightLeimac.DevConnStr_Model, MvEnumLeimacModel.IDGB_50M4PG_24_TP,
                            MacHalLightLeimac.DevConnStr_Channel, 3),
                        DeviceName = MacEnumDevice.inspectionch_light_bar_denfese_side_001.ToString(),
                        DriverId = ManifestDriverId.LightLeimac.ToString(),
                    },
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalLightLeimac.DevConnStr_Ip, "192.168.0.161",
                            MacHalLightLeimac.DevConnStr_Port, 1000,
                            MacHalLightLeimac.DevConnStr_Model, MvEnumLeimacModel.IDGB_50M2PG_12_TP,
                            MacHalLightLeimac.DevConnStr_Channel, 1),
                        DeviceName = MacEnumDevice.inspectionch_light_circle_inspection_top_001.ToString(),
                        DriverId = ManifestDriverId.LightLeimac.ToString(),
                    },
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalLightLeimac.DevConnStr_Ip, "192.168.0.162",
                            MacHalLightLeimac.DevConnStr_Port, 1000,
                            MacHalLightLeimac.DevConnStr_Model, MvEnumLeimacModel.IWDV_600M2_24,
                            MacHalLightLeimac.DevConnStr_Channel, 1),
                        DeviceName = MacEnumDevice.inspectionch_light_spot_inspection_left_001.ToString(),
                        DriverId = ManifestDriverId.LightLeimac.ToString(),
                    },
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalLightLeimac.DevConnStr_Ip, "192.168.0.162",
                            MacHalLightLeimac.DevConnStr_Port, 1000,
                            MacHalLightLeimac.DevConnStr_Model, MvEnumLeimacModel.IWDV_600M2_24,
                            MacHalLightLeimac.DevConnStr_Channel, 2),
                        DeviceName = MacEnumDevice.inspectionch_light_spot_inspection_right_001.ToString(),
                        DriverId = ManifestDriverId.LightLeimac.ToString(),
                    },

                    new MacManifestDeviceCfg(){
                        DevConnStr = "id=00:11:1C:F8:C6:26",
                        DeviceName = MacEnumDevice.inspectionch_camera_inspect_side_1.ToString(),
                        DriverId = ManifestDriverId.CameraSentech.ToString(),
                    },

                    new MacManifestDeviceCfg(){
                        DevConnStr = "id=TXF-MDT1X150-D35",
                        DeviceName = MacEnumDevice.inspection_camera_inspect_top_1.ToString(),
                        DriverId = ManifestDriverId.CameraSentech.ToString(),
                    },

                    new MacManifestDeviceCfg(){
                        DevConnStr = "id=00:11:1C:F9:A4:0A",
                        DeviceName = MacEnumDevice.inspection_camera_defense_side_1.ToString(),
                        DriverId = ManifestDriverId.CameraSentech.ToString(),
                    },

                    new MacManifestDeviceCfg(){
                        DevConnStr = "id=00:11:1C:F9:A4:0D",
                        DeviceName = MacEnumDevice.inspection_camera_defense_top_1.ToString(),
                        DriverId = ManifestDriverId.CameraSentech.ToString(),
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
                DevConnStr = null,
                DeviceName = MacEnumDevice.loadport_assembly.ToString(),
                DriverId = ManifestDriverId.LoadPort.ToString(),
                PositionId = MacEnumPositionId.LoadPort01.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("ip={0};port={1}", plcIp, plcPortId),
                        DeviceName = MacEnumDevice.loadport_plc.ToString(),
                        DriverId = ManifestDriverId.LoadPortPlc.ToString(),
                    },

                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalLoadPortCellGudeng.DevConnStr_Ip, "192.168.0.119",
                            MacHalLoadPortCellGudeng.DevConnStr_Port, 1000,
                            MacHalLoadPortCellGudeng.DevConnStr_LocalIp, null,
                            MacHalLoadPortCellGudeng.DevConnStr_LocalPort, 0),
                        DeviceName = MacEnumDevice.loadport_cell_001.ToString(),
                        DriverId = ManifestDriverId.LoadPortCellGudeng.ToString(),
                    },

                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalLightLeimac.DevConnStr_Ip, "192.168.0.119",
                            MacHalLightLeimac.DevConnStr_Port, 1000,
                            MacHalLightLeimac.DevConnStr_Model, MvEnumLeimacModel.IDGB_50M4PG_24_TP,
                            MacHalLightLeimac.DevConnStr_Channel, 1),
                        DeviceName = MacEnumDevice.loadport_light_bar_001.ToString(),
                        DriverId = ManifestDriverId.LightLeimac.ToString(),
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
                DevConnStr = null,
                DeviceName = MacEnumDevice.loadport_assembly.ToString(),
                DriverId = ManifestDriverId.LoadPort.ToString(),
                PositionId = MacEnumPositionId.LoadPort02.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3}" , MacHalPlcBase.DevConnStr_Ip, plcIp,MacHalPlcBase.DevConnStr_PortId, plcPortId),
                        DeviceName = MacEnumDevice.loadport_plc.ToString(),
                        DriverId = ManifestDriverId.LoadPortPlc.ToString(),
                    },


                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalLoadPortCellGudeng.DevConnStr_Ip, "192.168.0.119",
                            MacHalLoadPortCellGudeng.DevConnStr_Port, 1000,
                            MacHalLoadPortCellGudeng.DevConnStr_LocalIp, null,
                            MacHalLoadPortCellGudeng.DevConnStr_LocalPort, 0),
                        DeviceName = MacEnumDevice.loadport_cell_001.ToString(),
                        DriverId = ManifestDriverId.LoadPortCellGudeng.ToString(),
                    },


                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalLightLeimac.DevConnStr_Ip, "192.168.0.119",
                            MacHalLightLeimac.DevConnStr_Port, 1000,
                            MacHalLightLeimac.DevConnStr_Model, MvEnumLeimacModel.IDGB_50M4PG_24_TP,
                            MacHalLightLeimac.DevConnStr_Channel, 2),
                        DeviceName = MacEnumDevice.loadport_light_bar_001.ToString(),
                        DriverId = ManifestDriverId.LightLeimac.ToString(),
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
                        DevConnStr = "ip=192.168.0.140",
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
                DevConnStr = null,
                DeviceName = MacEnumDevice.openstage_assembly.ToString(),
                DriverId = ManifestDriverId.OpenStage.ToString(),
                PositionId = MacEnumPositionId.OpenStage01.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3}" , MacHalPlcBase.DevConnStr_Ip, plcIp,MacHalPlcBase.DevConnStr_PortId, plcPortId),
                        DeviceName = MacEnumDevice.openstage_plc.ToString(),
                        DriverId = ManifestDriverId.OpenStagePlc.ToString(),
                    },
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalLightLeimac.DevConnStr_Ip, "192.168.0.139",
                            MacHalLightLeimac.DevConnStr_Port, 1000,
                            MacHalLightLeimac.DevConnStr_Model, MvEnumLeimacModel.IDGB_50M4PG_24_TP,
                            MacHalLightLeimac.DevConnStr_Channel, 1),
                        DeviceName = MacEnumDevice.openstage_light_bar_defense_top_001.ToString(),
                        DriverId = ManifestDriverId.LightLeimac.ToString(),
                    },
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalLightLeimac.DevConnStr_Ip, "192.168.0.139",
                            MacHalLightLeimac.DevConnStr_Port, 1000,
                            MacHalLightLeimac.DevConnStr_Model, MvEnumLeimacModel.IDGB_50M4PG_24_TP,
                            MacHalLightLeimac.DevConnStr_Channel, 2),
                        DeviceName = MacEnumDevice.openstage_light_bar_defense_side_001.ToString(),
                        DriverId = ManifestDriverId.LightLeimac.ToString(),
                    },

                    new MacManifestDeviceCfg(){
                        DevConnStr = "id=00:11:1C:F9:A3:45",
                        DeviceName = MacEnumDevice.openstage_camera_side_1.ToString(),
                        DriverId = ManifestDriverId.CameraSentech.ToString(),
                    },

                    new MacManifestDeviceCfg(){
                        DevConnStr = "id=00:11:1C:F9:15:E4",
                        DeviceName = MacEnumDevice.openstage_camera_top_1.ToString(),
                        DriverId = ManifestDriverId.CameraSentech.ToString(),
                    },

                    new MacManifestDeviceCfg(){
                        DevConnStr = "id=00:11:1C:F9:A8:BE",
                        DeviceName = MacEnumDevice.openstage_camera_front_1.ToString(),
                        DriverId = ManifestDriverId.CameraSentech.ToString(),
                    },

                    new MacManifestDeviceCfg(){
                        DevConnStr = "id=00:11:1C:F9:A3:46",
                        DeviceName = MacEnumDevice.openstage_camera_barcode_1.ToString(),
                        DriverId = ManifestDriverId.CameraSentech.ToString(),
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