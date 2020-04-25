
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0.Manifest;
using System.IO;

namespace MvAssistant.Mac.v1_0.GenCfg.Manifest
{
    [TestClass]

    public class UtGenManifestReal : ManifestBase
    {
        [TestMethod]
        public void TestMethod()
        {
            var menifest = new MachineManifest();

            menifest.Devices.Add(DE_LP_A_ASB());
            menifest.Devices.Add(DE_LP_B_ASB());
            menifest.Devices.Add(DE_IC_A_ASB());
            menifest.Devices.Add(DE_CC_A_ASB());
            menifest.Devices.Add(DE_MT_A_ASB());
            menifest.Devices.Add(DE_DR_A_ASB());
            menifest.Devices.Add(DE_OS_A_ASB());
            menifest.Devices.Add(DE_BT_A_ASB());
            menifest.Devices.Add(DE_UNI_A_ASB());

            menifest.Drivers.AddRange(DriverFakeAll());
            menifest.Drivers.AddRange(DriverRealAll());

            var fn = Path.Combine(@"../../", "GenCfg/Manifest/Manifest.xml.real");
            menifest.SaveToXmlFile(fn);
        }
        MachineDevice DE_BT_A_ASB()
        {
            var rs = new MachineDevice()
            {

                ID = EnumMachineId.DE_BT_A_ASB.ToString(),
                DevConnStr = "127.0.0.1;3039",
                DeviceName = EnumDevice.boxtransfer_assembly.ToString(),
                Level = "1",
                DriverId = "5af84829-2bf1-4f16-a41c-e07fdc9b0cdd",
                PositionId = EnumPositionId.BoxTrasnfer01.ToString(),
                Devices = new MachineDevice[] {

                },
            };
            return rs;
        }
        MachineDevice DE_CC_A_ASB()
        {
            var rs = new MachineDevice()
            {

                ID = EnumMachineId.DE_CC_A_ASB.ToString(),
                DevConnStr = "127.0.0.1;3039",
                DeviceName = EnumDevice.clean_assembly.ToString(),
                Level = "1",
                DriverId = "11294958-669b-45c1-957a-bd0ed029e274",
                PositionId = EnumPositionId.CleanCh01.ToString(),
                Devices = new MachineDevice[] {

                    new MachineDevice(){
                        ID = EnumMachineId.DE_CC_A_33.ToString(),
                        DevConnStr = "DE_PLC_A;CC_laser1",
                        DeviceName = EnumDevice.clean_laser_entry_1.ToString(),
                        Level = "2",
                        DriverId = ManifestDriverId.LaserEntry_OmronPlc.ToString(),
                    },
                    new MachineDevice(){
                        ID = EnumMachineId.DE_CC_A_34.ToString(),
                        DevConnStr = "DE_PLC_A;CC_laser2",
                        DeviceName = EnumDevice.clean_laser_entry_2.ToString(),
                        Level = "2",
                        DriverId = ManifestDriverId.LaserEntry_OmronPlc.ToString(),
                    },

                    new MachineDevice(){
                        ID = EnumMachineId.DE_CC_A_35.ToString(),
                        DevConnStr = "DE_PLC_A;CC_collision_laser_1",
                        DeviceName = EnumDevice.clean_laser_prevent_collision_1.ToString(),
                        Level = "2",
                        DriverId = ManifestDriverId.LaserCollision_OmronPlc.ToString(),
                    },
                    new MachineDevice(){
                        ID = EnumMachineId.DE_CC_A_36.ToString(),
                        DevConnStr = "DE_PLC_A;CC_collision_laser_2",
                        DeviceName = EnumDevice.clean_laser_prevent_collision_2.ToString(),
                        Level = "2",
                        DriverId = ManifestDriverId.LaserCollision_OmronPlc.ToString(),
                    },
                    new MachineDevice(){
                        ID = EnumMachineId.DE_CC_A_37.ToString(),
                        DevConnStr = "DE_PLC_A;CC_collision_laser_3",
                        DeviceName = EnumDevice.clean_laser_prevent_collision_3.ToString(),
                        Level = "2",
                        DriverId = ManifestDriverId.LaserCollision_OmronPlc.ToString(),
                    },


                },
            };
            return rs;
        }
        MachineDevice DE_DR_A_ASB()
        {
            var rs = new MachineDevice()
            {

                ID = EnumMachineId.DE_CB_A_ASB.ToString(),
                DevConnStr = "127.0.0.1;3039",
                DeviceName = EnumDevice.cabinet_assembly.ToString(),
                Level = "1",
                DriverId = "ad0ef118-fd2b-459a-903c-3658c7361965",
                PositionId = EnumPositionId.Cabinet.ToString(),
                Devices = new MachineDevice[] {

                },
            };
            return rs;
        }
        MachineDevice DE_IC_A_ASB()
        {
            var rs = new MachineDevice()
            {

                ID = EnumMachineId.DE_IC_A_ASB.ToString(),
                DevConnStr = "127.0.0.1;3039",
                DeviceName = EnumDevice.inspection_assembly.ToString(),
                Level = "1",
                DriverId = "c71bb691-70bb-453f-8edd-3bd98d441e37",
                PositionId = EnumPositionId.InspectionCh01.ToString(),
                Devices = new MachineDevice[] {


                    new MachineDevice(){
                        ID = EnumMachineId.DE_IC_A_11.ToString(),
                        DevConnStr = "DE_PLC_A;IC_laser1",
                        DeviceName = EnumDevice.inspection_laser_entry_1.ToString(),
                        Level = "2",
                        DriverId = ManifestDriverId.LaserEntry_OmronPlc.ToString(),
                    },
                    new MachineDevice(){
                        ID = EnumMachineId.DE_IC_A_20.ToString(),
                        DevConnStr = "DE_PLC_A;IC_laser2",
                        DeviceName = EnumDevice.inspection_laser_entry_2.ToString(),
                        Level = "2",
                        DriverId = ManifestDriverId.LaserEntry_OmronPlc.ToString(),
                    },
                    new MachineDevice(){
                        ID = EnumMachineId.DE_IC_A_06.ToString(),
                        DevConnStr = "DE_PLC_A;IC_lightbar1",
                        DeviceName = EnumDevice.inspection_lightbar_1.ToString(),
                        Level = "2",
                        DriverId = "16de9985-6f20-4e13-bf78-79b86c86c191",
                    },
                    new MachineDevice(){
                        ID = EnumMachineId.DE_IC_A_07.ToString(),
                        DevConnStr = "DE_PLC_A;IC_lightsource1",
                        DeviceName = EnumDevice.inspection_linesource_1.ToString(),
                        Level = "2",
                        DriverId = "16de9985-6f20-4e13-bf78-79b86c86c191",
                    },

                },
            };
            return rs;
        }
        MachineDevice DE_LP_A_ASB()
        {
            var rs = new MachineDevice()
            {

                ID = EnumMachineId.DE_LP_A_ASB.ToString(),
                DevConnStr = "127.0.0.1;3038",
                DeviceName = EnumDevice.loadport_assembly.ToString(),
                Level = "1",
                DriverId = "8526f536-37d5-4f6e-bbf7-f2e24e3c6136",
                PositionId = EnumPositionId.LoadPort01.ToString(),
                Devices = new MachineDevice[] {


                },
            };
            return rs;
        }
        MachineDevice DE_LP_B_ASB()
        {
            var rs = new MachineDevice()
            {

                ID = EnumMachineId.DE_LP_B_ASB.ToString(),
                DevConnStr = "127.0.0.1;3039",
                DeviceName = EnumDevice.loadport_assembly.ToString(),
                Level = "1",
                DriverId = "8526f536-37d5-4f6e-bbf7-f2e24e3c6136",
                PositionId = EnumPositionId.LoadPort02.ToString(),
                Devices = new MachineDevice[] {


                },
            };
            return rs;
        }
        MachineDevice DE_MT_A_ASB()
        {
            var rs = new MachineDevice()
            {

                ID = EnumMachineId.DE_MT_A_ASB.ToString(),
                DevConnStr = "127.0.0.1;3039",
                DeviceName = EnumDevice.masktransfer_assembly.ToString(),
                Level = "1",
                DriverId = "609d889b-0bb7-43a4-b71e-0e1a68e1a832",
                PositionId = EnumPositionId.MaskTransfer01.ToString(),
                Devices = new MachineDevice[] {

                    new MachineDevice(){
                        ID = EnumMachineId.DE_MT_A_02.ToString(),
                        DevConnStr = "IP=192.168.1.31",
                        DeviceName = EnumDevice.masktransfer_robot_1.ToString(),
                        Level = "2",
                        DriverId = ManifestDriverId.FanucRobot.ToString(),
                    },

                    new MachineDevice(){
                        ID = EnumMachineId.DE_MT_A_10.ToString(),
                        DevConnStr = string.Format(""),
                        DeviceName = EnumDevice.masktransfer_inclinometer01.ToString(),
                        Level = "2",
                        DriverId = ManifestDriverId.Inclinometer_OmronPlc.ToString(),
                    },
                    new MachineDevice(){
                        ID = EnumMachineId.DE_MT_A_23.ToString(),
                        DevConnStr = "Assembly=DE_UNI_A_ASB;Component=DE_UNI_A_01;",
                        DeviceName = EnumDevice.masktransfer_gripper_01.ToString(),
                        Level = "2",
                        DriverId = ManifestDriverId.MaskGripperNrc.ToString(),
                    },
                    new MachineDevice(){
                        ID = EnumMachineId.DE_MT_A_24.ToString(),
                        DevConnStr = "Assembly=DE_UNI_A_ASB;Component=DE_UNI_A_01;",
                        DeviceName = EnumDevice.masktransfer_gripper_02.ToString(),
                        Level = "2",
                        DriverId = ManifestDriverId.MaskGripperNrc.ToString(),
                    },
                    new MachineDevice(){
                        ID = EnumMachineId.DE_MT_A_25.ToString(),
                        DevConnStr = "DE_PLC_A;masktransfer_gripper_03",
                        DeviceName = EnumDevice.masktransfer_gripper_03.ToString(),
                        Level = "2",
                        DriverId =ManifestDriverId.MaskGripperFake.ToString(),
                    },
                    new MachineDevice(){
                        ID = EnumMachineId.DE_MT_A_26.ToString(),
                        DevConnStr = "DE_PLC_A;masktransfer_gripper_04",
                        DeviceName = EnumDevice.masktransfer_gripper_04.ToString(),
                        Level = "2",
                        DriverId = ManifestDriverId.MaskGripperFake.ToString(),
                    },

                },
            };
            return rs;
        }
        MachineDevice DE_OS_A_ASB()
        {
            var rs = new MachineDevice()
            {

                ID = EnumMachineId.DE_OS_A_ASB.ToString(),
                DevConnStr = "127.0.0.1;3039",
                DeviceName = EnumDevice.openstage_assembly.ToString(),
                Level = "1",
                DriverId = "d291cc0e-6ab5-4967-87a2-40f5ebbfb6d9",
                PositionId = EnumPositionId.OpenStage01.ToString(),
                Devices = new MachineDevice[] {

                },
            };
            return rs;
        }
        MachineDevice DE_UNI_A_ASB()
        {
            var rs = new MachineDevice()
            {

                ID = EnumMachineId.DE_UNI_A_ASB.ToString(),
                DevConnStr = null,
                DeviceName = EnumDevice.universal_assembly.ToString(),
                Level = "1",
                DriverId = "4FA545DC-E3E9-441C-9896-D27270EF4D8D",
                PositionId = null,
                Devices = new MachineDevice[] {
                    new MachineDevice(){
                        ID = EnumMachineId.DE_UNI_A_01.ToString(),
                        DevConnStr = string.Format("Assembly={0};IP={1};PortId={2}", EnumMachineId.DE_UNI_A_ASB, "192.168.1.21", 2),
                        DeviceName = EnumDevice.universal_plc_01.ToString(),
                        Level = "2",
                        DriverId = ManifestDriverId.Plc_Omron.ToString(),
                    },
                    new MachineDevice(){
                        ID = EnumMachineId.DE_UNI_A_02.ToString(),
                        DevConnStr = string.Format("Assembly={0};IP={1};PortId={2}", EnumMachineId.DE_UNI_A_ASB, "192.168.1.22", 3),
                        DeviceName = EnumDevice.universal_plc_02.ToString(),
                        Level = "2",
                        DriverId = ManifestDriverId.Plc_Omron.ToString(),
                    },

                },
            };
            return rs;
        }

    }
}