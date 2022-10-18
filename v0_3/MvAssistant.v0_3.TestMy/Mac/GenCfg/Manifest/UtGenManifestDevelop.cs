
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.v0_3.DeviceDrive.LeimacLight;
using MvAssistant.v0_3.Mac.Hal.CompDrawer;
using MvAssistant.v0_3.Mac.Hal.CompLight;
using MvAssistant.v0_3.Mac.Hal.CompLoadPort;
using MvAssistant.v0_3.Mac.Hal.CompPlc;
using MvAssistant.v0_3.Mac.Manifest;
using System.IO;

namespace MvAssistant.v0_3.Mac.TestMy.GenCfg.Manifest
{
    [TestClass]

    public class UtGenManifestDevelop : MacManifestGenCfgBase
    {

        string plcIp = "192.168.0.200";
        int plcPortId = 2;

        [TestMethod]
        public void GenManifestCfgReal()
        {
            var menifest = new MacManifestCfg();

            menifest.Devices.Add(HID_LP_A_ASB());
            menifest.Devices.Add(HID_LP_B_ASB());
            menifest.Devices.Add(HID_IC_A_ASB());
            menifest.Devices.Add(DE_CC_A_ASB());
            menifest.Devices.Add(HID_MT_A_ASB());
            menifest.Devices.Add(DE_CB_A_ASB());

            #region drawer
            // menifest.Devices.Add(DE_CB_A_01_01());
            menifest.Devices.Add(DE_CB_A_01_02());
            menifest.Devices.Add(DE_CB_A_01_03());
            menifest.Devices.Add(DE_CB_A_01_04());
            //menifest.Devices.Add(DE_CB_A_01_05());

            //menifest.Devices.Add(DE_CB_A_02_01());
            menifest.Devices.Add(DE_CB_A_02_02());
            menifest.Devices.Add(DE_CB_A_02_03());
            menifest.Devices.Add(DE_CB_A_02_04());
            //menifest.Devices.Add(DE_CB_A_02_05());

            //menifest.Devices.Add(DE_CB_A_03_01());
            menifest.Devices.Add(DE_CB_A_03_02());
            menifest.Devices.Add(DE_CB_A_03_03());
            menifest.Devices.Add(DE_CB_A_03_04());
            //menifest.Devices.Add(DE_CB_A_03_05());

            // menifest.Devices.Add(DE_CB_A_04_01());
            menifest.Devices.Add(DE_CB_A_04_02());
            menifest.Devices.Add(DE_CB_A_04_03());
            menifest.Devices.Add(DE_CB_A_04_04());
            //menifest.Devices.Add(DE_CB_A_04_05());

            // menifest.Devices.Add(DE_CB_A_05_01());
            menifest.Devices.Add(DE_CB_A_05_02());
            menifest.Devices.Add(DE_CB_A_05_03());
            menifest.Devices.Add(DE_CB_A_05_04());
            // menifest.Devices.Add(DE_CB_A_05_05());

            //menifest.Devices.Add(DE_CB_A_06_01());
            menifest.Devices.Add(DE_CB_A_06_02());
            menifest.Devices.Add(DE_CB_A_06_03());
            menifest.Devices.Add(DE_CB_A_06_04());
            //menifest.Devices.Add(DE_CB_A_06_05());

            //menifest.Devices.Add(DE_CB_A_07_01());
            menifest.Devices.Add(DE_CB_A_07_02());
            menifest.Devices.Add(DE_CB_A_07_03());
            //menifest.Devices.Add(DE_CB_A_07_04());
            //menifest.Devices.Add(DE_CB_A_07_05());


            #endregion drawer

            menifest.Devices.Add(HID_OS_A_ASB());
            menifest.Devices.Add(DE_BT_A_ASB());
            menifest.Devices.Add(HID_EQP_A_ASSY());

            menifest.Drivers.AddRange(DriverRealAll());
            //menifest.Drivers.AddRange(DriverFakeAll());//Pure real HAL

            var fn = Path.Combine(@"../../", "UserData/Manifest/Manifest.xml.develop");
            menifest.SaveToXmlFile(fn);
        }


        MacManifestDeviceCfg DE_BT_A_ASB()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = null,
                DeviceId = EnumMacDeviceId.boxtransfer_assembly.ToString(),
                DriverId = MacManifestDriverId.BoxTransfer.ToString(),
                PositionId = MacEnumPositionId.BoxTrasnfer01.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3}" , MacHalPlcBase.DevConnStr_Ip, plcIp,MacHalPlcBase.DevConnStr_PortId, plcPortId),
                        DeviceId = EnumMacDeviceId.boxtransfer_plc.ToString(),
                        DriverId = MacManifestDriverId.BoxTransferPlc.ToString(),
                    },

                     new MacManifestDeviceCfg(){
                        DevConnStr = "ip=192.168.0.150",
                        DeviceId = EnumMacDeviceId.boxtransfer_robot_1.ToString(),
                        DriverId = MacManifestDriverId.RobotFanuc.ToString(),
                    },

                     new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalLightLeimac.DevConnStr_Ip, "192.168.0.155",
                            MacHalLightLeimac.DevConnStr_Port, 1000,
                            MacHalLightLeimac.DevConnStr_Model, MvaEnumLeimacModel.IDGB_50M4PG_24_TP,
                            MacHalLightLeimac.DevConnStr_Channel, 1),
                        DeviceId = EnumMacDeviceId.boxtransfer_light_1.ToString(),
                        DriverId = MacManifestDriverId.LightLeimac.ToString(),
                    },

                      new MacManifestDeviceCfg(){
                        DevConnStr = "id=00:11:1C:F9:A3:23",
                        DeviceId = EnumMacDeviceId.boxtransfer_camera_gripper_1.ToString(),
                        DriverId = MacManifestDriverId.CameraSentech.ToString(),
                    },
                },
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_ASB()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = null,
                DeviceId = EnumMacDeviceId.cabinet_assembly.ToString(),
                DriverId = MacManifestDriverId.Cabinet.ToString(),
                PositionId = MacEnumPositionId.Cabinet01.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3}" , MacHalPlcBase.DevConnStr_Ip, plcIp,MacHalPlcBase.DevConnStr_PortId, plcPortId),
                        DeviceId = EnumMacDeviceId.cabinet_plc.ToString(),
                        DriverId = MacManifestDriverId.CabinetPlc.ToString(),
                    },

                    /**
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalDrawerKjMachine.DevConnStr_Ip, "192.168.0.34",
                            MacHalDrawerKjMachine.DevConnStr_Port, 5000,
                            MacHalDrawerKjMachine.DevConnStr_LocalIp, "192.168.0.14",
                            MacHalDrawerKjMachine.DevConnStr_LocalPort, 6000,
                            MacHalDrawerKjMachine.DevConnStr_StartPort,"5001",
                            MacHalDrawerKjMachine.DevConnStr_EndPort,"5999",
                            MacHalDrawerKjMachine.DevConnStr_Index,"01_01"),
                        ID = EnumMachineId.DE_CB_A_01_01.ToString(),
                        DeviceName = MacEnumDevice.cabinet_drawer_01_01.ToString(),
                        DriverId = ManifestDriverId.DrawerKjMachine.ToString(),
                    },*/
                    /**
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalDrawerKjMachine.DevConnStr_Ip, "192.168.0.42",
                            MacHalDrawerKjMachine.DevConnStr_Port, 5000,
                            MacHalDrawerKjMachine.DevConnStr_LocalIp, "192.168.0.14",
                            MacHalDrawerKjMachine.DevConnStr_LocalPort, 6000,
                            MacHalDrawerKjMachine.DevConnStr_StartPort,"5001",
                            MacHalDrawerKjMachine.DevConnStr_EndPort,"5999",
                            MacHalDrawerKjMachine.DevConnStr_Index,"01_02"),
                        ID = EnumMachineId.DE_CB_A_01_02.ToString(),
                        DeviceName = MacEnumDevice.cabinet_drawer_01_02.ToString(),
                        DriverId = ManifestDriverId.DrawerKjMachine.ToString(),
                    },*/
                    /**
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalDrawerKjMachine.DevConnStr_Ip, "192.168.0.50",
                            MacHalDrawerKjMachine.DevConnStr_Port, 5000,
                            MacHalDrawerKjMachine.DevConnStr_LocalIp, "192.168.0.14",
                            MacHalDrawerKjMachine.DevConnStr_LocalPort, 6000,
                            MacHalDrawerKjMachine.DevConnStr_StartPort,"5001",
                            MacHalDrawerKjMachine.DevConnStr_EndPort,"5999",
                            MacHalDrawerKjMachine.DevConnStr_Index,"01_03"),
                        ID = EnumMachineId.DE_CB_A_01_03.ToString(),
                        DeviceName = MacEnumDevice.cabinet_drawer_01_03.ToString(),
                        DriverId = ManifestDriverId.DrawerKjMachine.ToString(),
                    },*/
                    /**
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalDrawerKjMachine.DevConnStr_Ip, "192.168.0.54",
                            MacHalDrawerKjMachine.DevConnStr_Port, 5000,
                            MacHalDrawerKjMachine.DevConnStr_LocalIp, "192.168.0.14",
                            MacHalDrawerKjMachine.DevConnStr_LocalPort, 6000,
                            MacHalDrawerKjMachine.DevConnStr_StartPort,"5001",
                            MacHalDrawerKjMachine.DevConnStr_EndPort,"5999",
                            MacHalDrawerKjMachine.DevConnStr_Index,"01_04"),
                        ID = EnumMachineId.DE_CB_A_01_04.ToString(),
                        DeviceName = MacEnumDevice.cabinet_drawer_01_04.ToString(),
                        DriverId = ManifestDriverId.DrawerKjMachine.ToString(),
                    },*/
                },
            };
            return rs;
        }

        #region Drawer
        const string drawerControlIP = "192.168.0.14";
        MacManifestDeviceCfg DE_CB_A_01_01()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = "ip=192.168.0.xx;port=5000;local_ip=" + drawerControlIP + ";local_port=6000;startPort=5001;endPort=5999;index=01_01",
                DeviceId = EnumMacDeviceId.cabinet_drawer_01_01.ToString(),
                DriverId = MacManifestDriverId.DrawerKjMachine.ToString(),
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_01_02()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = "ip=192.168.0.31;port=5000;local_ip=" + drawerControlIP + ";local_port=6000;startPort=5001;endPort=5999;index=01_02",
                DeviceId = EnumMacDeviceId.cabinet_drawer_01_02.ToString(),
                DriverId = MacManifestDriverId.DrawerKjMachine.ToString(),
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_01_03()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = "ip=192.168.0.32;port=5000;local_ip=" + drawerControlIP + ";local_port=6000;startPort=5001;endPort=5999;index=01_03",
                DeviceId = EnumMacDeviceId.cabinet_drawer_01_03.ToString(),
                DriverId = MacManifestDriverId.DrawerKjMachine.ToString(),
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_01_04()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = "ip=192.168.0.33;port=5000;local_ip=" + drawerControlIP + ";local_port=6000;startPort=5001;endPort=5999;index=01_04",
                DeviceId = EnumMacDeviceId.cabinet_drawer_01_04.ToString(),
                DriverId = MacManifestDriverId.DrawerKjMachine.ToString(),
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_01_05()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = "ip=192.168.0.xx;port=5000;local_ip=" + drawerControlIP + ";local_port=6000;startPort=5001;endPort=5999;index=01_05",
                DeviceId = EnumMacDeviceId.cabinet_drawer_01_05.ToString(),
                DriverId = MacManifestDriverId.DrawerKjMachine.ToString(),
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_02_01()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = "ip=192.168.0.xx;port=5000;local_ip=" + drawerControlIP + ";local_port=6000;startPort=5001;endPort=5999;index=02_01",
                DeviceId = EnumMacDeviceId.cabinet_drawer_02_01.ToString(),
                DriverId = MacManifestDriverId.DrawerKjMachine.ToString(),
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_02_02()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = "ip=192.168.0.41;port=5000;local_ip=" + drawerControlIP + ";local_port=6000;startPort=5001;endPort=5999;index=02_02",
                DeviceId = EnumMacDeviceId.cabinet_drawer_02_02.ToString(),
                DriverId = MacManifestDriverId.DrawerKjMachine.ToString(),
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_02_03()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = "ip=192.168.0.42;port=5000;local_ip=" + drawerControlIP + ";local_port=6000;startPort=5001;endPort=5999;index=02_03",
                DeviceId = EnumMacDeviceId.cabinet_drawer_02_03.ToString(),
                DriverId = MacManifestDriverId.DrawerKjMachine.ToString(),
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_02_04()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = "ip=192.168.0.43;port=5000;local_ip=" + drawerControlIP + ";local_port=6000;startPort=5001;endPort=5999;index=02_04",
                DeviceId = EnumMacDeviceId.cabinet_drawer_02_04.ToString(),
                DriverId = MacManifestDriverId.DrawerKjMachine.ToString(),
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_02_05()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = "ip=192.168.0.XX;port=5000;local_ip=" + drawerControlIP + ";local_port=6000;startPort=5001;endPort=5999;index=02_05",
                DeviceId = EnumMacDeviceId.cabinet_drawer_02_05.ToString(),
                DriverId = MacManifestDriverId.DrawerKjMachine.ToString(),
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_03_01()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = "ip=192.168.0.XX;port=5000;local_ip=" + drawerControlIP + ";local_port=6000;startPort=5001;endPort=5999;index=03_01",
                DeviceId = EnumMacDeviceId.cabinet_drawer_03_01.ToString(),
                DriverId = MacManifestDriverId.DrawerKjMachine.ToString(),
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_03_02()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = "ip=192.168.0.51;port=5000;local_ip=" + drawerControlIP + ";local_port=6000;startPort=5001;endPort=5999;index=03_02",
                DeviceId = EnumMacDeviceId.cabinet_drawer_03_02.ToString(),
                DriverId = MacManifestDriverId.DrawerKjMachine.ToString(),
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_03_03()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = "ip=192.168.0.52;port=5000;local_ip=" + drawerControlIP + ";local_port=6000;startPort=5001;endPort=5999;index=03_03",
                DeviceId = EnumMacDeviceId.cabinet_drawer_03_03.ToString(),
                DriverId = MacManifestDriverId.DrawerKjMachine.ToString(),
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_03_04()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = "ip=192.168.0.53;port=5000;local_ip=" + drawerControlIP + ";local_port=6000;startPort=5001;endPort=5999;index=03_04",
                DeviceId = EnumMacDeviceId.cabinet_drawer_03_04.ToString(),
                DriverId = MacManifestDriverId.DrawerKjMachine.ToString(),
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_03_05()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = "ip=192.168.0.xx;port=5000;local_ip=" + drawerControlIP + ";local_port=6000;startPort=5001;endPort=5999;index=03_05",
                DeviceId = EnumMacDeviceId.cabinet_drawer_03_05.ToString(),
                DriverId = MacManifestDriverId.DrawerKjMachine.ToString(),
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_04_01()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = "ip=192.168.0.xx;port=5000;local_ip=" + drawerControlIP + ";local_port=6000;startPort=5001;endPort=5999;index=04_01",
                DeviceId = EnumMacDeviceId.cabinet_drawer_04_01.ToString(),
                DriverId = MacManifestDriverId.DrawerKjMachine.ToString(),
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_04_02()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = "ip=192.168.0.61;port=5000;local_ip=" + drawerControlIP + ";local_port=6000;startPort=5001;endPort=5999;index=04_02",
                DeviceId = EnumMacDeviceId.cabinet_drawer_04_02.ToString(),
                DriverId = MacManifestDriverId.DrawerKjMachine.ToString(),
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_04_03()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = "ip=192.168.0.62;port=5000;local_ip=" + drawerControlIP + ";local_port=6000;startPort=5001;endPort=5999;index=04_03",
                DeviceId = EnumMacDeviceId.cabinet_drawer_04_03.ToString(),
                DriverId = MacManifestDriverId.DrawerKjMachine.ToString(),
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_04_04()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = "ip=192.168.0.63;port=5000;local_ip=" + drawerControlIP + ";local_port=6000;startPort=5001;endPort=5999;index=04_04",
                DeviceId = EnumMacDeviceId.cabinet_drawer_04_04.ToString(),
                DriverId = MacManifestDriverId.DrawerKjMachine.ToString(),
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_04_05()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = "ip=192.168.0.xx;port=5000;local_ip=" + drawerControlIP + ";local_port=6000;startPort=5001;endPort=5999;index=04_05",
                DeviceId = EnumMacDeviceId.cabinet_drawer_04_05.ToString(),
                DriverId = MacManifestDriverId.DrawerKjMachine.ToString(),
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_05_01()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = "ip=192.168.0.xx;port=5000;local_ip=" + drawerControlIP + ";local_port=6000;startPort=5001;endPort=5999;index=05_01",
                DeviceId = EnumMacDeviceId.cabinet_drawer_05_01.ToString(),
                DriverId = MacManifestDriverId.DrawerKjMachine.ToString(),
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_05_02()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = "ip=192.168.0.71;port=5000;local_ip=" + drawerControlIP + ";local_port=6000;startPort=5001;endPort=5999;index=05_02",
                DeviceId = EnumMacDeviceId.cabinet_drawer_05_02.ToString(),
                DriverId = MacManifestDriverId.DrawerKjMachine.ToString(),
            };
            return rs;
        }

        MacManifestDeviceCfg DE_CB_A_05_03()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = "ip=192.168.0.72;port=5000;local_ip=" + drawerControlIP + ";local_port=6000;startPort=5001;endPort=5999;index=05_03",
                DeviceId = EnumMacDeviceId.cabinet_drawer_05_03.ToString(),
                DriverId = MacManifestDriverId.DrawerKjMachine.ToString(),
            };
            return rs;
        }

        MacManifestDeviceCfg DE_CB_A_05_04()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = "ip=192.168.0.73;port=5000;local_ip=" + drawerControlIP + ";local_port=6000;startPort=5001;endPort=5999;index=05_04",
                DeviceId = EnumMacDeviceId.cabinet_drawer_05_04.ToString(),
                DriverId = MacManifestDriverId.DrawerKjMachine.ToString(),
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_05_05()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = "ip=192.168.0.xx;port=5000;local_ip=" + drawerControlIP + ";local_port=6000;startPort=5001;endPort=5999;index=05_05",
                DeviceId = EnumMacDeviceId.cabinet_drawer_05_05.ToString(),
                DriverId = MacManifestDriverId.DrawerKjMachine.ToString(),
            };
            return rs;
        }

        MacManifestDeviceCfg DE_CB_A_06_01()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = "ip=192.168.0.xx;port=5000;local_ip=" + drawerControlIP + ";local_port=6000;startPort=5001;endPort=5999;index=06_01",
                DeviceId = EnumMacDeviceId.cabinet_drawer_06_01.ToString(),
                DriverId = MacManifestDriverId.DrawerKjMachine.ToString(),
            };

            return rs;
        }

        MacManifestDeviceCfg DE_CB_A_06_02()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = "ip=192.168.0.81;port=5000;local_ip=" + drawerControlIP + ";local_port=6000;startPort=5001;endPort=5999;index=06_02",
                DeviceId = EnumMacDeviceId.cabinet_drawer_06_02.ToString(),
                DriverId = MacManifestDriverId.DrawerKjMachine.ToString(),
            };

            return rs;
        }

        MacManifestDeviceCfg DE_CB_A_06_03()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = "ip=192.168.0.82;port=5000;local_ip=" + drawerControlIP + ";local_port=6000;startPort=5001;endPort=5999;index=06_03",
                DeviceId = EnumMacDeviceId.cabinet_drawer_06_03.ToString(),
                DriverId = MacManifestDriverId.DrawerKjMachine.ToString(),
            };

            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_06_04()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = "ip=192.168.0.83;port=5000;local_ip=" + drawerControlIP + ";local_port=6000;startPort=5001;endPort=5999;index=06_04",
                DeviceId = EnumMacDeviceId.cabinet_drawer_06_04.ToString(),
                DriverId = MacManifestDriverId.DrawerKjMachine.ToString(),
            };

            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_06_05()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = "ip=192.168.0.xx;port=5000;local_ip=" + drawerControlIP + ";local_port=6000;startPort=5001;endPort=5999;index=06_05",
                DeviceId = EnumMacDeviceId.cabinet_drawer_06_05.ToString(),
                DriverId = MacManifestDriverId.DrawerKjMachine.ToString(),
            };

            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_07_01()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = "ip=192.168.0.xx;port=5000;local_ip=" + drawerControlIP + ";local_port=6000;startPort=5001;endPort=5999;index=07_01",
                DeviceId = EnumMacDeviceId.cabinet_drawer_07_01.ToString(),
                DriverId = MacManifestDriverId.DrawerKjMachine.ToString(),
            };

            return rs;
        }

        MacManifestDeviceCfg DE_CB_A_07_02()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = "ip=192.168.0.91;port=5000;local_ip=" + drawerControlIP + ";local_port=6000;startPort=5001;endPort=5999;index=07_02",
                DeviceId = EnumMacDeviceId.cabinet_drawer_07_02.ToString(),
                DriverId = MacManifestDriverId.DrawerKjMachine.ToString(),
            };

            return rs;
        }

        MacManifestDeviceCfg DE_CB_A_07_03()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = "ip=192.168.0.92;port=5000;local_ip=" + drawerControlIP + ";local_port=6000;startPort=5001;endPort=5999;index=07_03",
                DeviceId = EnumMacDeviceId.cabinet_drawer_07_03.ToString(),
                DriverId = MacManifestDriverId.DrawerKjMachine.ToString(),
            };

            return rs;
        }

        MacManifestDeviceCfg DE_CB_A_07_04()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = "ip=192.168.0.XX;port=5000;local_ip=" + drawerControlIP + ";local_port=6000;startPort=5001;endPort=5999;index=07_04",
                DeviceId = EnumMacDeviceId.cabinet_drawer_07_04.ToString(),
                DriverId = MacManifestDriverId.DrawerKjMachine.ToString(),
            };

            return rs;
        }

        MacManifestDeviceCfg DE_CB_A_07_05()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = "ip=192.168.0.XX;port=5000;local_ip=" + drawerControlIP + ";local_port=6000;startPort=5001;endPort=5999;index=07_05",
                DeviceId = EnumMacDeviceId.cabinet_drawer_07_05.ToString(),
                DriverId = MacManifestDriverId.DrawerKjMachine.ToString(),
            };

            return rs;
        }
        #endregion drawer


        MacManifestDeviceCfg DE_CC_A_ASB()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = "127.0.0.1;3039",
                DeviceId = EnumMacDeviceId.clean_assembly.ToString(),
                DriverId = MacManifestDriverId.CleanCh.ToString(),
                PositionId = MacEnumPositionId.CleanCh01.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3}" , MacHalPlcBase.DevConnStr_Ip, plcIp, MacHalPlcBase.DevConnStr_PortId, plcPortId),
                        DeviceId = EnumMacDeviceId.cleanch_plc.ToString(),
                        DriverId = MacManifestDriverId.CleanChPlc.ToString(),
                    },

                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalLightLeimac.DevConnStr_Ip, "192.168.0.129",
                            MacHalLightLeimac.DevConnStr_Port, 1000,
                            MacHalLightLeimac.DevConnStr_Model, MvaEnumLeimacModel.IWDV_100S_24,
                            MacHalLightLeimac.DevConnStr_Channel, 1),
                        DeviceId = EnumMacDeviceId.cleanch_inspection_spot_light_001.ToString(),
                        DriverId = MacManifestDriverId.LightLeimac.ToString(),
                    },

                    new MacManifestDeviceCfg(){
                        DevConnStr = "id=00:11:1C:F9:A4:08",
                        DeviceId = EnumMacDeviceId.clean_camera_particle_1.ToString(),
                        DriverId = MacManifestDriverId.CameraSentech.ToString(),
                    },

                },
            };
            return rs;
        }
        MacManifestDeviceCfg HID_IC_A_ASB()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = null,
                DeviceId = EnumMacDeviceId.inspectionch_assembly.ToString(),
                DriverId = MacManifestDriverId.InspectionCh.ToString(),
                PositionId = MacEnumPositionId.InspectionCh01.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3}" , MacHalPlcBase.DevConnStr_Ip, plcIp,MacHalPlcBase.DevConnStr_PortId, plcPortId),
                        DeviceId = EnumMacDeviceId.inspectionch_plc.ToString(),
                        DriverId = MacManifestDriverId.InspectionChPlc.ToString(),
                    },
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalLightLeimac.DevConnStr_Ip, "192.168.0.160",
                            MacHalLightLeimac.DevConnStr_Port, 1000,
                            MacHalLightLeimac.DevConnStr_Model, MvaEnumLeimacModel.IDGB_50M4PG_24_TP,
                            MacHalLightLeimac.DevConnStr_Channel, 1),
                        DeviceId = EnumMacDeviceId.inspectionch_light_circle_defense_top_001.ToString(),
                        DriverId = MacManifestDriverId.LightLeimac.ToString(),
                    },
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalLightLeimac.DevConnStr_Ip, "192.168.0.160",
                            MacHalLightLeimac.DevConnStr_Port, 1000,
                            MacHalLightLeimac.DevConnStr_Model, MvaEnumLeimacModel.IDGB_50M4PG_24_TP,
                            MacHalLightLeimac.DevConnStr_Channel, 2),
                        DeviceId = EnumMacDeviceId.inspectionch_light_line_left_001.ToString(),
                        DriverId = MacManifestDriverId.LightLeimac.ToString(),
                    },
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalLightLeimac.DevConnStr_Ip, "192.168.0.160",
                            MacHalLightLeimac.DevConnStr_Port, 1000,
                            MacHalLightLeimac.DevConnStr_Model, MvaEnumLeimacModel.IDGB_50M4PG_24_TP,
                            MacHalLightLeimac.DevConnStr_Channel, 3),
                        DeviceId = EnumMacDeviceId.inspectionch_light_line_back_001.ToString(),
                        DriverId = MacManifestDriverId.LightLeimac.ToString(),
                    },
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalLightLeimac.DevConnStr_Ip, "192.168.0.161",
                            MacHalLightLeimac.DevConnStr_Port, 1000,
                            MacHalLightLeimac.DevConnStr_Model, MvaEnumLeimacModel.IDGB_50M2PG_12_TP,
                            MacHalLightLeimac.DevConnStr_Channel, 1),
                        DeviceId = EnumMacDeviceId.inspectionch_light_circle_inspection_top_001.ToString(),
                        DriverId = MacManifestDriverId.LightLeimac.ToString(),
                    },
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalLightLeimac.DevConnStr_Ip, "192.168.0.162",
                            MacHalLightLeimac.DevConnStr_Port, 1000,
                            MacHalLightLeimac.DevConnStr_Model, MvaEnumLeimacModel.IWDV_600M2_24,
                            MacHalLightLeimac.DevConnStr_Channel, 1),
                        DeviceId = EnumMacDeviceId.inspectionch_light_bar_left_001.ToString(),
                        DriverId = MacManifestDriverId.LightLeimac.ToString(),
                    },
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalLightLeimac.DevConnStr_Ip, "192.168.0.162",
                            MacHalLightLeimac.DevConnStr_Port, 1000,
                            MacHalLightLeimac.DevConnStr_Model, MvaEnumLeimacModel.IWDV_600M2_24,
                            MacHalLightLeimac.DevConnStr_Channel, 2),
                        DeviceId = EnumMacDeviceId.inspectionch_light_bar_right_001.ToString(),
                        DriverId = MacManifestDriverId.LightLeimac.ToString(),
                    },

                    new MacManifestDeviceCfg(){
                        DevConnStr = "id=00:11:1C:F8:C6:26",
                        DeviceId = EnumMacDeviceId.inspectionch_camera_inspect_side_001.ToString(),
                        DriverId = MacManifestDriverId.CameraSentech.ToString(),
                    },

                    new MacManifestDeviceCfg(){
                        DevConnStr = "id=TXF-MDT1X150-D35",
                        DeviceId = EnumMacDeviceId.inspectionch_camera_inspect_top_001.ToString(),
                        DriverId = MacManifestDriverId.CameraLink.ToString(),
                    },

                    new MacManifestDeviceCfg(){
                        DevConnStr = "id=00:11:1C:F9:A4:0A",
                        DeviceId = EnumMacDeviceId.inspectionch_camera_defense_side_001.ToString(),
                        DriverId = MacManifestDriverId.CameraSentech.ToString(),
                    },

                    new MacManifestDeviceCfg(){
                        DevConnStr = "id=00:11:1C:F9:A4:0D",
                        DeviceId = EnumMacDeviceId.inspectionch_camera_defense_top_001.ToString(),
                        DriverId = MacManifestDriverId.CameraSentech.ToString(),
                    },
                },
            };
            return rs;
        }
        MacManifestDeviceCfg HID_LP_A_ASB()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = null,
                DeviceId = EnumMacDeviceId.loadportA_assembly.ToString(),
                DriverId = MacManifestDriverId.LoadPort.ToString(),
                PositionId = MacEnumPositionId.LoadPort01.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3}", MacHalPlcBase.DevConnStr_Ip, plcIp,MacHalPlcBase.DevConnStr_PortId, plcPortId),
                        DeviceId = EnumMacDeviceId.loadport_plc.ToString(),
                        DriverId = MacManifestDriverId.LoadPortPlc.ToString(),
                    },

                    //new MacManifestDeviceCfg(){
                    //    DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                    //        MacHalLoadPortCellGudeng.DevConnStr_Ip, "192.168.0.119",
                    //        MacHalLoadPortCellGudeng.DevConnStr_Port, 1000,
                    //        MacHalLoadPortCellGudeng.DevConnStr_LocalIp, null,
                    //        MacHalLoadPortCellGudeng.DevConnStr_LocalPort, 0),
                    //    DeviceName = MacEnumDevice.loadport_cell_001.ToString(),
                    //    DriverId = ManifestDriverId.LoadPortCellGudeng.ToString(),
                    //},

                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalLightLeimac.DevConnStr_Ip, "192.168.0.119",
                            MacHalLightLeimac.DevConnStr_Port, 1000,
                            MacHalLightLeimac.DevConnStr_Model, MvaEnumLeimacModel.IDGB_50M4PG_24_TP,
                            MacHalLightLeimac.DevConnStr_Channel, 1),
                        DeviceId = EnumMacDeviceId.loadport_light_bar_001.ToString(),
                        DriverId = MacManifestDriverId.LightLeimac.ToString(),
                    },

                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalLightLeimac.DevConnStr_Ip, "192.168.0.119",
                            MacHalLightLeimac.DevConnStr_Port, 1000,
                            MacHalLightLeimac.DevConnStr_Model, MvaEnumLeimacModel.IDGB_50M4PG_24_TP,
                            MacHalLightLeimac.DevConnStr_Channel, 3),
                        DeviceId = EnumMacDeviceId.loadport_light_bar_003.ToString(),
                        DriverId = MacManifestDriverId.LightLeimac.ToString(),
                    },

                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3}",
                            MacHalLoadPortGudeng.DevConnStr_Ip, "192.168.0.20",
                            MacHalLoadPortGudeng.DevConnStr_Port, 1024),
                        DeviceId = EnumMacDeviceId.loadport_1.ToString(),
                        DriverId = MacManifestDriverId.LoadPortGudeng.ToString(),
                    },

                    new MacManifestDeviceCfg(){
                        DevConnStr = "id=00:11:1C:F9:A8:BB",
                        DeviceId = EnumMacDeviceId.loadportA_camera_inspect.ToString(),
                        DriverId = MacManifestDriverId.CameraSentech.ToString(),
                    },

                    new MacManifestDeviceCfg(){
                        DevConnStr = "id=00:11:1C:F9:A3:22",
                        DeviceId = EnumMacDeviceId.loadport_camera_barcode_inspect.ToString(),
                        DriverId = MacManifestDriverId.CameraSentech.ToString(),
                    },
                },
            };
            return rs;
        }
        MacManifestDeviceCfg HID_LP_B_ASB()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = null,
                DeviceId = EnumMacDeviceId.loadportB_assembly.ToString(),
                DriverId = MacManifestDriverId.LoadPort.ToString(),
                PositionId = MacEnumPositionId.LoadPort02.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3}" , MacHalPlcBase.DevConnStr_Ip, plcIp,MacHalPlcBase.DevConnStr_PortId, plcPortId),
                        DeviceId = EnumMacDeviceId.loadport_plc.ToString(),
                        DriverId = MacManifestDriverId.LoadPortPlc.ToString(),
                    },


                    //new MacManifestDeviceCfg(){
                    //    DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                    //        MacHalLoadPortCellGudeng.DevConnStr_Ip, "192.168.0.119",
                    //        MacHalLoadPortCellGudeng.DevConnStr_Port, 1000,
                    //        MacHalLoadPortCellGudeng.DevConnStr_LocalIp, null,
                    //        MacHalLoadPortCellGudeng.DevConnStr_LocalPort, 0),
                    //    DeviceName = MacEnumDevice.loadport_cell_001.ToString(),
                    //    DriverId = ManifestDriverId.LoadPortCellGudeng.ToString(),
                    //},


                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalLightLeimac.DevConnStr_Ip, "192.168.0.119",
                            MacHalLightLeimac.DevConnStr_Port, 1000,
                            MacHalLightLeimac.DevConnStr_Model, MvaEnumLeimacModel.IDGB_50M4PG_24_TP,
                            MacHalLightLeimac.DevConnStr_Channel, 2),
                        DeviceId = EnumMacDeviceId.loadport_light_bar_002.ToString(),
                        DriverId = MacManifestDriverId.LightLeimac.ToString(),
                    },

                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3}",
                            MacHalLoadPortGudeng.DevConnStr_Ip, "192.168.0.21",
                            MacHalLoadPortGudeng.DevConnStr_Port, 1024),
                        DeviceId = EnumMacDeviceId.loadport_2.ToString(),
                        DriverId = MacManifestDriverId.LoadPortGudeng.ToString(),
                    },

                    new MacManifestDeviceCfg(){
                        DevConnStr = "id=00:11:1C:F9:C8:14",
                        DeviceId = EnumMacDeviceId.loadportB_camera_inspect.ToString(),
                        DriverId = MacManifestDriverId.CameraSentech.ToString(),
                    },
                },
            };
            return rs;
        }
        MacManifestDeviceCfg HID_MT_A_ASB()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = "127.0.0.1;3039",
                DeviceId = EnumMacDeviceId.masktransfer_assembly.ToString(),
                DriverId = MacManifestDriverId.MaskTransfer.ToString(),
                PositionId = MacEnumPositionId.MaskTransfer01.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3}" , MacHalPlcBase.DevConnStr_Ip, plcIp,MacHalPlcBase.DevConnStr_PortId, plcPortId),
                        DeviceId = EnumMacDeviceId.masktransfer_plc.ToString(),
                        DriverId = MacManifestDriverId.MaskTransferPlc.ToString(),
                    },


                    new MacManifestDeviceCfg(){
                        DevConnStr = "ip=192.168.0.140",
                        DeviceId = EnumMacDeviceId.masktransfer_robot_1.ToString(),
                        DriverId = MacManifestDriverId.RobotFanuc.ToString(),
                    },





                },
            };
            return rs;
        }
        MacManifestDeviceCfg HID_OS_A_ASB()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = null,
                DeviceId = EnumMacDeviceId.openstage_assembly.ToString(),
                DriverId = MacManifestDriverId.OpenStage.ToString(),
                PositionId = MacEnumPositionId.OpenStage01.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3}" , MacHalPlcBase.DevConnStr_Ip, plcIp,MacHalPlcBase.DevConnStr_PortId, plcPortId),
                        DeviceId = EnumMacDeviceId.openstage_plc.ToString(),
                        DriverId = MacManifestDriverId.OpenStagePlc.ToString(),
                    },
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalLightLeimac.DevConnStr_Ip, "192.168.0.139",
                            MacHalLightLeimac.DevConnStr_Port, 1000,
                            MacHalLightLeimac.DevConnStr_Model, MvaEnumLeimacModel.IDGB_50M4PG_24_TP,
                            MacHalLightLeimac.DevConnStr_Channel, 1),
                        DeviceId = EnumMacDeviceId.openstage_light_bar_defense_top_001.ToString(),
                        DriverId = MacManifestDriverId.LightLeimac.ToString(),
                    },

                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalLightLeimac.DevConnStr_Ip, "192.168.0.139",
                            MacHalLightLeimac.DevConnStr_Port, 1000,
                            MacHalLightLeimac.DevConnStr_Model, MvaEnumLeimacModel.IDGB_50M4PG_24_TP,
                            MacHalLightLeimac.DevConnStr_Channel, 2),
                        DeviceId = EnumMacDeviceId.openstage_light_bar_defense_front_001.ToString(),
                        DriverId = MacManifestDriverId.LightLeimac.ToString(),
                    },

                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalLightLeimac.DevConnStr_Ip, "192.168.0.139",
                            MacHalLightLeimac.DevConnStr_Port, 1000,
                            MacHalLightLeimac.DevConnStr_Model, MvaEnumLeimacModel.IDGB_50M4PG_24_TP,
                            MacHalLightLeimac.DevConnStr_Channel, 3),
                        DeviceId = EnumMacDeviceId.openstage_light_bar_defense_side_001.ToString(),
                        DriverId = MacManifestDriverId.LightLeimac.ToString(),
                    },

                    new MacManifestDeviceCfg(){
                        DevConnStr = "id=00:11:1C:F9:A3:45",
                        DeviceId = EnumMacDeviceId.openstage_camera_side_1.ToString(),
                        DriverId = MacManifestDriverId.CameraSentech.ToString(),
                    },

                    new MacManifestDeviceCfg(){
                        DevConnStr = "id=00:11:1C:F9:15:E4",
                        DeviceId = EnumMacDeviceId.openstage_camera_top_1.ToString(),
                        DriverId = MacManifestDriverId.CameraSentech.ToString(),
                    },

                    new MacManifestDeviceCfg(){
                        DevConnStr = "id=00:11:1C:F9:A8:BE",
                        DeviceId = EnumMacDeviceId.openstage_camera_left_1.ToString(),
                        DriverId = MacManifestDriverId.CameraSentech.ToString(),
                    },

                    new MacManifestDeviceCfg(){
                        DevConnStr = "id=00:11:1C:F9:A3:46",
                        DeviceId = EnumMacDeviceId.openstage_camera_right_1.ToString(),
                        DriverId = MacManifestDriverId.CameraSentech.ToString(),
                    },
                },
            };
            return rs;
        }
        MacManifestDeviceCfg HID_EQP_A_ASSY()
        {
            var rs = new MacManifestDeviceCfg()
            {
                DevConnStr = null,
                DeviceId = EnumMacDeviceId.eqp_assembly.ToString(),
                DriverId = MacManifestDriverId.Eqp.ToString(),
                PositionId = null,
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3}" , MacHalPlcBase.DevConnStr_Ip, plcIp,MacHalPlcBase.DevConnStr_PortId, plcPortId),
                        DeviceId = EnumMacDeviceId.eqp_plc_01.ToString(),
                        DriverId = MacManifestDriverId.UniversalPlc.ToString(),
                    },

                },
            };
            return rs;
        }

    }
}