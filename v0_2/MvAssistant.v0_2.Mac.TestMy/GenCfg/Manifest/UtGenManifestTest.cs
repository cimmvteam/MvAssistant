
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.v0_2.DeviceDrive.LeimacLight;
using MvAssistant.v0_2.Mac.Hal.CompDrawer;
using MvAssistant.v0_2.Mac.Hal.CompLight;
using MvAssistant.v0_2.Mac.Hal.CompLoadPort;
using MvAssistant.v0_2.Mac.Hal.CompPlc;
using MvAssistant.v0_2.Mac.Manifest;
using System.IO;

namespace MvAssistant.v0_2.Mac.TestMy.GenCfg.Manifest
{
    [TestClass]

    public class UtGenManifestTest : ManifestBase
    {

        string plcIp = "192.168.0.200";
        int plcPortId = 2;

        [TestMethod]
        public void GenManifestCfgTest()
        {
            var menifest = new MacManifestCfg();

            menifest.Devices.Add(DE_LP_A_ASB());
            menifest.Devices.Add(DE_LP_B_ASB());
            menifest.Devices.Add(DE_IC_A_ASB());
            menifest.Devices.Add(DE_CC_A_ASB());
            menifest.Devices.Add(DE_MT_A_ASB());
            menifest.Devices.Add(DE_CB_A_ASB());

            #region drawer
            menifest.Devices.Add(DE_CB_A_01_01());
            menifest.Devices.Add(DE_CB_A_01_02());
            menifest.Devices.Add(DE_CB_A_01_03());
            menifest.Devices.Add(DE_CB_A_01_04());
            menifest.Devices.Add(DE_CB_A_01_05());

            menifest.Devices.Add(DE_CB_A_02_01());
            menifest.Devices.Add(DE_CB_A_02_02());
            menifest.Devices.Add(DE_CB_A_02_03());
            menifest.Devices.Add(DE_CB_A_02_04());
            menifest.Devices.Add(DE_CB_A_02_05());

            menifest.Devices.Add(DE_CB_A_03_01());
            menifest.Devices.Add(DE_CB_A_03_02());
            menifest.Devices.Add(DE_CB_A_03_03());
            menifest.Devices.Add(DE_CB_A_03_04());
            menifest.Devices.Add(DE_CB_A_03_05());

            menifest.Devices.Add(DE_CB_A_04_01());
            menifest.Devices.Add(DE_CB_A_04_02());
            menifest.Devices.Add(DE_CB_A_04_03());
            menifest.Devices.Add(DE_CB_A_04_04());
            menifest.Devices.Add(DE_CB_A_04_05());

            #endregion drawer

            menifest.Devices.Add(DE_OS_A_ASB());
            menifest.Devices.Add(DE_BT_A_ASB());
            menifest.Devices.Add(DE_UNI_A_ASB());

            menifest.Drivers.AddRange(DriverRealAll());
            menifest.Drivers.AddRange(DriverFakeAll());

            var fn = Path.Combine(@"../../", "UserData/Manifest/Manifest.xml.test");
            menifest.SaveToXmlFile(fn);
        }


        MacManifestDeviceCfg DE_BT_A_ASB()
        {
            var rs = new MacManifestDeviceCfg()
            {

                ID = EnuDeviceId.DE_BT_A_ASB.ToString(),
                DeviceName = MacEnumDevice.boxtransfer_assembly.ToString(),
                DevConnStr = null,
                DriverId = ManifestDriverId.BoxTransfer.ToString(),
                PositionId = MacEnumPositionId.BoxTrasnfer01.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DeviceName = MacEnumDevice.boxtransfer_plc.ToString(),
                        DevConnStr = string.Format("{0}={1};{2}={3}" , MacHalPlcBase.DevConnStr_Ip, plcIp,MacHalPlcBase.DevConnStr_PortId, plcPortId),
                        DriverId = ManifestDriverId.BoxTransferPlc.ToString(),
                    },

                     new MacManifestDeviceCfg(){
                        DeviceName = MacEnumDevice.boxtransfer_robot_1.ToString(),
                        DevConnStr = "ip=192.168.0.150",
                        DriverId = ManifestDriverId.RobotFanuc.ToString(),
                    },

                      new MacManifestDeviceCfg(){
                        DeviceName = MacEnumDevice.boxtransfer_camera_gripper_1.ToString(),
                        DevConnStr = "id=00:11:1C:F9:A3:23",
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

                ID = EnuDeviceId.DE_CB_A_ASB.ToString(),
                DevConnStr = null,
                DeviceName = MacEnumDevice.cabinet_assembly.ToString(),
                DriverId = ManifestDriverId.Cabinet.ToString(),
                PositionId = MacEnumPositionId.Cabinet01.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DeviceName = MacEnumDevice.cabinet_plc.ToString(),
                        DevConnStr = string.Format("{0}={1};{2}={3}" , MacHalPlcBase.DevConnStr_Ip, plcIp,MacHalPlcBase.DevConnStr_PortId, plcPortId),
                        DriverId = ManifestDriverId.CabinetPlc.ToString(),
                    },

                 

                },
            };
            return rs;
        }

        #region Drawer
        MacManifestDeviceCfg DE_CB_A_01_01()
        {
            var rs = new MacManifestDeviceCfg()
            {

                ID = EnuDeviceId.DE_CB_A_01_01.ToString(),
                DevConnStr = null,
                DeviceName = MacEnumDevice.cabinet_drawer_01_01.ToString(),
                DriverId = ManifestDriverId.Cabinet.ToString(),
                PositionId = MacEnumPositionId.Drawer01.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = "ip=192.168.0.31;port=5000;local_ip=192.168.0.14;local_port=6000;startPort=5001;endPort=5999;index=01_01",
                        DeviceName = MacEnumDevice.cabinet_drawer_01_01.ToString(),
                        DriverId = ManifestDriverId.DrawerKjMachine.ToString(),
                        ID= EnuDeviceId.DE_CB_A_01_01.ToString(),
                    },
                  
                },
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_01_02()
        {
            var rs = new MacManifestDeviceCfg()
            {

                ID = EnuDeviceId.DE_CB_A_01_02.ToString(),
                DevConnStr = null,
                DeviceName = MacEnumDevice.cabinet_drawer_01_02.ToString(),
                DriverId = ManifestDriverId.Cabinet.ToString(),
                PositionId = MacEnumPositionId.Drawer02.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = "ip=192.168.0.32;port=5000;local_ip=192.168.0.14;local_port=6000;startPort=5001;endPort=5999;index=01_02",
                        DeviceName = MacEnumDevice.cabinet_drawer_01_02.ToString(),
                        DriverId = ManifestDriverId.DrawerKjMachine.ToString(),
                        ID= EnuDeviceId.DE_CB_A_01_02.ToString(),
                    },

                },
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_01_03()
        {
            var rs = new MacManifestDeviceCfg()
            {

                ID = EnuDeviceId.DE_CB_A_01_03.ToString(),
                DevConnStr = null,
                DeviceName = MacEnumDevice.cabinet_drawer_01_03.ToString(),
                DriverId = ManifestDriverId.Cabinet.ToString(),
                PositionId = MacEnumPositionId.Drawer03.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = "ip=192.168.0.33;port=5000;local_ip=192.168.0.14;local_port=6000;startPort=5001;endPort=5999;index=01_03",
                        DeviceName = MacEnumDevice.cabinet_drawer_01_03.ToString(),
                        DriverId = ManifestDriverId.DrawerKjMachine.ToString(),
                        ID= EnuDeviceId.DE_CB_A_01_03.ToString(),
                    },

                },
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_01_04()
        {
            var rs = new MacManifestDeviceCfg()
            {

                ID = EnuDeviceId.DE_CB_A_01_04.ToString(),
                DevConnStr = null,
                DeviceName = MacEnumDevice.cabinet_drawer_01_04.ToString(),
                DriverId = ManifestDriverId.Cabinet.ToString(),
                PositionId = MacEnumPositionId.Drawer04.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = "ip=192.168.0.41;port=5000;local_ip=192.168.0.14;local_port=6000;startPort=5001;endPort=5999;index=01_04",
                        DeviceName = MacEnumDevice.cabinet_drawer_01_04.ToString(),
                        DriverId = ManifestDriverId.DrawerKjMachine.ToString(),
                        ID= EnuDeviceId.DE_CB_A_01_04.ToString(),
                    },

                },
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_01_05()
        {
            var rs = new MacManifestDeviceCfg()
            {

                ID = EnuDeviceId.DE_CB_A_01_05.ToString(),
                DevConnStr = null,
                DeviceName = MacEnumDevice.cabinet_drawer_01_05.ToString(),
                DriverId = ManifestDriverId.Cabinet.ToString(),
                PositionId = MacEnumPositionId.Drawer05.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = "ip=192.168.0.42;port=5000;local_ip=192.168.0.14;local_port=6000;startPort=5001;endPort=5999;index=01_05",
                        DeviceName = MacEnumDevice.cabinet_drawer_01_05.ToString(),
                        DriverId = ManifestDriverId.DrawerKjMachine.ToString(),
                        ID= EnuDeviceId.DE_CB_A_01_05.ToString(),
                    },

                },
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_02_01()
        {
            var rs = new MacManifestDeviceCfg()
            {

                ID = EnuDeviceId.DE_CB_A_02_01.ToString(),
                DevConnStr = null,
                DeviceName = MacEnumDevice.cabinet_drawer_02_01.ToString(),
                DriverId = ManifestDriverId.Cabinet.ToString(),
                PositionId = MacEnumPositionId.Drawer06.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = "ip=192.168.0.43;port=5000;local_ip=192.168.0.14;local_port=6000;startPort=5001;endPort=5999;index=02_01",
                        DeviceName = MacEnumDevice.cabinet_drawer_02_01.ToString(),
                        DriverId = ManifestDriverId.DrawerKjMachine.ToString(),
                        ID= EnuDeviceId.DE_CB_A_02_01.ToString(),
                    },
                },
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_02_02()
        {
            var rs = new MacManifestDeviceCfg()
            {

                ID = EnuDeviceId.DE_CB_A_02_02.ToString(),
                DevConnStr = null,
                DeviceName = MacEnumDevice.cabinet_drawer_02_02.ToString(),
                DriverId = ManifestDriverId.Cabinet.ToString(),
                PositionId = MacEnumPositionId.Drawer07.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = "ip=192.168.0.51;port=5000;local_ip=192.168.0.14;local_port=6000;startPort=5001;endPort=5999;index=02_02",
                        DeviceName = MacEnumDevice.cabinet_drawer_02_02.ToString(),
                        DriverId = ManifestDriverId.DrawerKjMachine.ToString(),
                        ID= EnuDeviceId.DE_CB_A_02_02.ToString(),
                    },
                },
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_02_03()
        {
            var rs = new MacManifestDeviceCfg()
            {

                ID = EnuDeviceId.DE_CB_A_02_03.ToString(),
                DevConnStr = null,
                DeviceName = MacEnumDevice.cabinet_drawer_02_03.ToString(),
                DriverId = ManifestDriverId.Cabinet.ToString(),
                PositionId = MacEnumPositionId.Drawer08.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = "ip=192.168.0.52;port=5000;local_ip=192.168.0.14;local_port=6000;startPort=5001;endPort=5999;index=02_03",
                        DeviceName = MacEnumDevice.cabinet_drawer_02_03.ToString(),
                        DriverId = ManifestDriverId.DrawerKjMachine.ToString(),
                        ID= EnuDeviceId.DE_CB_A_02_03.ToString(),
                    },
                },
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_02_04()
        {
            var rs = new MacManifestDeviceCfg()
            {

                ID = EnuDeviceId.DE_CB_A_02_04.ToString(),
                DevConnStr = null,
                DeviceName = MacEnumDevice.cabinet_drawer_02_04.ToString(),
                DriverId = ManifestDriverId.Cabinet.ToString(),
                PositionId = MacEnumPositionId.Drawer09.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = "ip=192.168.0.53;port=5000;local_ip=192.168.0.14;local_port=6000;startPort=5001;endPort=5999;index=02_04",
                        DeviceName = MacEnumDevice.cabinet_drawer_02_04.ToString(),
                        DriverId = ManifestDriverId.DrawerKjMachine.ToString(),
                        ID= EnuDeviceId.DE_CB_A_02_04.ToString(),
                    },
                },
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_02_05()
        {
            var rs = new MacManifestDeviceCfg()
            {

                ID = EnuDeviceId.DE_CB_A_02_05.ToString(),
                DevConnStr = null,
                DeviceName = MacEnumDevice.cabinet_drawer_02_05.ToString(),
                DriverId = ManifestDriverId.Cabinet.ToString(),
                PositionId = MacEnumPositionId.Drawer10.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = "ip=192.168.0.61;port=5000;local_ip=192.168.0.14;local_port=6000;startPort=5001;endPort=5999;index=02_05",
                        DeviceName = MacEnumDevice.cabinet_drawer_02_05.ToString(),
                        DriverId = ManifestDriverId.DrawerKjMachine.ToString(),
                        ID= EnuDeviceId.DE_CB_A_02_05.ToString(),
                    },
                },
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_03_01()
        {
            var rs = new MacManifestDeviceCfg()
            {

                ID = EnuDeviceId.DE_CB_A_03_01.ToString(),
                DevConnStr = null,
                DeviceName = MacEnumDevice.cabinet_drawer_03_01.ToString(),
                DriverId = ManifestDriverId.Cabinet.ToString(),
                PositionId = MacEnumPositionId.Drawer11.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = "ip=192.168.0.62;port=5000;local_ip=192.168.0.14;local_port=6000;startPort=5001;endPort=5999;index=03_01",
                        DeviceName = MacEnumDevice.cabinet_drawer_03_01.ToString(),
                        DriverId = ManifestDriverId.DrawerKjMachine.ToString(),
                        ID= EnuDeviceId.DE_CB_A_03_01.ToString(),
                    },
                },
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_03_02()
        {
            var rs = new MacManifestDeviceCfg()
            {

                ID = EnuDeviceId.DE_CB_A_03_02.ToString(),
                DevConnStr = null,
                DeviceName = MacEnumDevice.cabinet_drawer_03_02.ToString(),
                DriverId = ManifestDriverId.Cabinet.ToString(),
                PositionId = MacEnumPositionId.Drawer12.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = "ip=192.168.0.63;port=5000;local_ip=192.168.0.14;local_port=6000;startPort=5001;endPort=5999;index=03_02",
                        DeviceName = MacEnumDevice.cabinet_drawer_03_02.ToString(),
                        DriverId = ManifestDriverId.DrawerKjMachine.ToString(),
                        ID= EnuDeviceId.DE_CB_A_03_02.ToString(),
                    },
                },
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_03_03()
        {
            var rs = new MacManifestDeviceCfg()
            {

                ID = EnuDeviceId.DE_CB_A_03_03.ToString(),
                DevConnStr = null,
                DeviceName = MacEnumDevice.cabinet_drawer_03_03.ToString(),
                DriverId = ManifestDriverId.Cabinet.ToString(),
                PositionId = MacEnumPositionId.Drawer13.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = "ip=192.168.0.71;port=5000;local_ip=192.168.0.14;local_port=6000;startPort=5001;endPort=5999;index=03_03",
                        DeviceName = MacEnumDevice.cabinet_drawer_03_03.ToString(),
                        DriverId = ManifestDriverId.DrawerKjMachine.ToString(),
                        ID= EnuDeviceId.DE_CB_A_03_03.ToString(),
                    },
                },
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_03_04()
        {
            var rs = new MacManifestDeviceCfg()
            {

                ID = EnuDeviceId.DE_CB_A_03_04.ToString(),
                DevConnStr = null,
                DeviceName = MacEnumDevice.cabinet_drawer_03_04.ToString(),
                DriverId = ManifestDriverId.Cabinet.ToString(),
                PositionId = MacEnumPositionId.Drawer14.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = "ip=192.168.0.72;port=5000;local_ip=192.168.0.14;local_port=6000;startPort=5001;endPort=5999;index=03_04",
                        DeviceName = MacEnumDevice.cabinet_drawer_03_04.ToString(),
                        DriverId = ManifestDriverId.DrawerKjMachine.ToString(),
                        ID= EnuDeviceId.DE_CB_A_03_04.ToString(),
                    },
                },
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_03_05()
        {
            var rs = new MacManifestDeviceCfg()
            {

                ID = EnuDeviceId.DE_CB_A_03_05.ToString(),
                DevConnStr = null,
                DeviceName = MacEnumDevice.cabinet_drawer_03_05.ToString(),
                DriverId = ManifestDriverId.Cabinet.ToString(),
                PositionId = MacEnumPositionId.Drawer15.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = "ip=192.168.0.73;port=5000;local_ip=192.168.0.14;local_port=6000;startPort=5001;endPort=5999;index=03_05",
                        DeviceName = MacEnumDevice.cabinet_drawer_03_05.ToString(),
                        DriverId = ManifestDriverId.DrawerKjMachine.ToString(),
                        ID= EnuDeviceId.DE_CB_A_03_05.ToString(),
                    },
                },
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_04_01()
        {
            var rs = new MacManifestDeviceCfg()
            {

                ID = EnuDeviceId.DE_CB_A_04_01.ToString(),
                DevConnStr = null,
                DeviceName = MacEnumDevice.cabinet_drawer_04_01.ToString(),
                DriverId = ManifestDriverId.Cabinet.ToString(),
                PositionId = MacEnumPositionId.Drawer16.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = "ip=192.168.0.81;port=5000;local_ip=192.168.0.14;local_port=6000;startPort=5001;endPort=5999;index=04_01",
                        DeviceName = MacEnumDevice.cabinet_drawer_04_01.ToString(),
                        DriverId = ManifestDriverId.DrawerKjMachine.ToString(),
                        ID= EnuDeviceId.DE_CB_A_04_01.ToString(),
                    },
                },
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_04_02()
        {
            var rs = new MacManifestDeviceCfg()
            {

                ID = EnuDeviceId.DE_CB_A_04_02.ToString(),
                DevConnStr = null,
                DeviceName = MacEnumDevice.cabinet_drawer_04_02.ToString(),
                DriverId = ManifestDriverId.Cabinet.ToString(),
                PositionId = MacEnumPositionId.Drawer17.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = "ip=192.168.0.82;port=5000;local_ip=192.168.0.14;local_port=6000;startPort=5001;endPort=5999;index=04_02",
                        DeviceName = MacEnumDevice.cabinet_drawer_04_02.ToString(),
                        DriverId = ManifestDriverId.DrawerKjMachine.ToString(),
                        ID= EnuDeviceId.DE_CB_A_04_02.ToString(),
                    },
                },
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_04_03()
        {
            var rs = new MacManifestDeviceCfg()
            {

                ID = EnuDeviceId.DE_CB_A_04_03.ToString(),
                DevConnStr = null,
                DeviceName = MacEnumDevice.cabinet_drawer_04_03.ToString(),
                DriverId = ManifestDriverId.Cabinet.ToString(),
                PositionId = MacEnumPositionId.Drawer18.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = "ip=192.168.0.83;port=5000;local_ip=192.168.0.14;local_port=6000;startPort=5001;endPort=5999;index=04_03",
                        DeviceName = MacEnumDevice.cabinet_drawer_04_03.ToString(),
                        DriverId = ManifestDriverId.DrawerKjMachine.ToString(),
                        ID= EnuDeviceId.DE_CB_A_04_03.ToString(),
                    },
                },
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_04_04()
        {
            var rs = new MacManifestDeviceCfg()
            {

                ID = EnuDeviceId.DE_CB_A_04_04.ToString(),
                DevConnStr = null,
                DeviceName = MacEnumDevice.cabinet_drawer_04_04.ToString(),
                DriverId = ManifestDriverId.Cabinet.ToString(),
                PositionId = MacEnumPositionId.Drawer19.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = "ip=192.168.0.91;port=5000;local_ip=192.168.0.14;local_port=6000;startPort=5001;endPort=5999;index=04_04",
                        DeviceName = MacEnumDevice.cabinet_drawer_04_04.ToString(),
                        DriverId = ManifestDriverId.DrawerKjMachine.ToString(),
                        ID= EnuDeviceId.DE_CB_A_04_04.ToString(),
                    },
                },
            };
            return rs;
        }
        MacManifestDeviceCfg DE_CB_A_04_05()
        {
            var rs = new MacManifestDeviceCfg()
            {

                ID = EnuDeviceId.DE_CB_A_04_05.ToString(),
                DevConnStr = null,
                DeviceName = MacEnumDevice.cabinet_drawer_04_05.ToString(),
                DriverId = ManifestDriverId.Cabinet.ToString(),
                PositionId = MacEnumPositionId.Drawer20.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = "ip=192.168.0.92;port=5000;local_ip=192.168.0.14;local_port=6000;startPort=5001;endPort=5999;index=04_05",
                        DeviceName = MacEnumDevice.cabinet_drawer_04_05.ToString(),
                        DriverId = ManifestDriverId.DrawerKjMachine.ToString(),
                        ID= EnuDeviceId.DE_CB_A_04_05.ToString(),
                    },
                },
            };
            return rs;
        }
        #endregion drawer


        MacManifestDeviceCfg DE_CC_A_ASB()
        {
            var rs = new MacManifestDeviceCfg()
            {

                ID = EnuDeviceId.DE_CC_A_ASB.ToString(),
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
                            MacHalLightLeimac.DevConnStr_Model, MvaEnumLeimacModel.IWDV_100S_24,
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

                ID = EnuDeviceId.DE_IC_A_ASB.ToString(),
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
                            MacHalLightLeimac.DevConnStr_Model, MvaEnumLeimacModel.IDGB_50M4PG_24_TP,
                            MacHalLightLeimac.DevConnStr_Channel, 1),
                        DeviceName = MacEnumDevice.inspectionch_light_circle_defense_top_001.ToString(),
                        DriverId = ManifestDriverId.LightLeimac.ToString(),
                    },
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalLightLeimac.DevConnStr_Ip, "192.168.0.160",
                            MacHalLightLeimac.DevConnStr_Port, 1000,
                            MacHalLightLeimac.DevConnStr_Model, MvaEnumLeimacModel.IDGB_50M4PG_24_TP,
                            MacHalLightLeimac.DevConnStr_Channel, 2),
                        DeviceName = MacEnumDevice.inspectionch_light_line_left_001.ToString(),
                        DriverId = ManifestDriverId.LightLeimac.ToString(),
                    },
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalLightLeimac.DevConnStr_Ip, "192.168.0.160",
                            MacHalLightLeimac.DevConnStr_Port, 1000,
                            MacHalLightLeimac.DevConnStr_Model, MvaEnumLeimacModel.IDGB_50M4PG_24_TP,
                            MacHalLightLeimac.DevConnStr_Channel, 3),
                        DeviceName = MacEnumDevice.inspectionch_light_line_back_001.ToString(),
                        DriverId = ManifestDriverId.LightLeimac.ToString(),
                    },
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalLightLeimac.DevConnStr_Ip, "192.168.0.161",
                            MacHalLightLeimac.DevConnStr_Port, 1000,
                            MacHalLightLeimac.DevConnStr_Model, MvaEnumLeimacModel.IDGB_50M2PG_12_TP,
                            MacHalLightLeimac.DevConnStr_Channel, 1),
                        DeviceName = MacEnumDevice.inspectionch_light_circle_inspection_top_001.ToString(),
                        DriverId = ManifestDriverId.LightLeimac.ToString(),
                    },
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalLightLeimac.DevConnStr_Ip, "192.168.0.162",
                            MacHalLightLeimac.DevConnStr_Port, 1000,
                            MacHalLightLeimac.DevConnStr_Model, MvaEnumLeimacModel.IWDV_600M2_24,
                            MacHalLightLeimac.DevConnStr_Channel, 1),
                        DeviceName = MacEnumDevice.inspectionch_light_bar_left_001.ToString(),
                        DriverId = ManifestDriverId.LightLeimac.ToString(),
                    },
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalLightLeimac.DevConnStr_Ip, "192.168.0.162",
                            MacHalLightLeimac.DevConnStr_Port, 1000,
                            MacHalLightLeimac.DevConnStr_Model, MvaEnumLeimacModel.IWDV_600M2_24,
                            MacHalLightLeimac.DevConnStr_Channel, 2),
                        DeviceName = MacEnumDevice.inspectionch_light_bar_right_001.ToString(),
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
                        DriverId = ManifestDriverId.CameraFake.ToString(),
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

                ID = EnuDeviceId.DE_LP_A_ASB.ToString(),
                DevConnStr = null,
                DeviceName = MacEnumDevice.loadportA_assembly.ToString(),
                DriverId = ManifestDriverId.LoadPort.ToString(),
                PositionId = MacEnumPositionId.LoadPort01.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3}", MacHalPlcBase.DevConnStr_Ip, plcIp,MacHalPlcBase.DevConnStr_PortId, plcPortId),
                        DeviceName = MacEnumDevice.loadport_plc.ToString(),
                        DriverId = ManifestDriverId.LoadPortPlc.ToString(),
                    },

               

                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalLightLeimac.DevConnStr_Ip, "192.168.0.119",
                            MacHalLightLeimac.DevConnStr_Port, 1000,
                            MacHalLightLeimac.DevConnStr_Model, MvaEnumLeimacModel.IDGB_50M4PG_24_TP,
                            MacHalLightLeimac.DevConnStr_Channel, 1),
                        DeviceName = MacEnumDevice.loadport_light_bar_001.ToString(),
                        DriverId = ManifestDriverId.LightLeimac.ToString(),
                    },

                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3}",
                            MacHalLoadPortGudeng.DevConnStr_Ip, "192.168.0.20",
                            MacHalLoadPortGudeng.DevConnStr_Port, 1024),
                        DeviceName = MacEnumDevice.loadport_1.ToString(),
                        DriverId = ManifestDriverId.LoadPortGudeng.ToString(),
                    },
                },
            };
            return rs;
        }
        MacManifestDeviceCfg DE_LP_B_ASB()
        {
            var rs = new MacManifestDeviceCfg()
            {

                ID = EnuDeviceId.DE_LP_B_ASB.ToString(),
                DevConnStr = null,
                DeviceName = MacEnumDevice.loadportB_assembly.ToString(),
                DriverId = ManifestDriverId.LoadPort.ToString(),
                PositionId = MacEnumPositionId.LoadPort02.ToString(),
                Devices = new MacManifestDeviceCfg[] {
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3}" , MacHalPlcBase.DevConnStr_Ip, plcIp,MacHalPlcBase.DevConnStr_PortId, plcPortId),
                        DeviceName = MacEnumDevice.loadport_plc.ToString(),
                        DriverId = ManifestDriverId.LoadPortPlc.ToString(),
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
                        DeviceName = MacEnumDevice.loadport_light_bar_001.ToString(),
                        DriverId = ManifestDriverId.LightLeimac.ToString(),
                    },

                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3}",
                            MacHalLoadPortGudeng.DevConnStr_Ip, "192.168.0.21",
                            MacHalLoadPortGudeng.DevConnStr_Port, 1024),
                        DeviceName = MacEnumDevice.loadport_2.ToString(),
                        DriverId = ManifestDriverId.LoadPortGudeng.ToString(),
                    },
                },
            };
            return rs;
        }
        MacManifestDeviceCfg DE_MT_A_ASB()
        {
            var rs = new MacManifestDeviceCfg()
            {

                ID = EnuDeviceId.DE_MT_A_ASB.ToString(),
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
                        DriverId = ManifestDriverId.RobotFanuc.ToString(),
                    },





                },
            };
            return rs;
        }
        MacManifestDeviceCfg DE_OS_A_ASB()
        {
            var rs = new MacManifestDeviceCfg()
            {

                ID = EnuDeviceId.DE_OS_A_ASB.ToString(),
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
                            MacHalLightLeimac.DevConnStr_Model, MvaEnumLeimacModel.IDGB_50M4PG_24_TP,
                            MacHalLightLeimac.DevConnStr_Channel, 1),
                        DeviceName = MacEnumDevice.openstage_light_bar_defense_top_001.ToString(),
                        DriverId = ManifestDriverId.LightLeimac.ToString(),
                    },
                    new MacManifestDeviceCfg(){
                        DevConnStr = string.Format("{0}={1};{2}={3};{4}={5};{6}={7}",
                            MacHalLightLeimac.DevConnStr_Ip, "192.168.0.139",
                            MacHalLightLeimac.DevConnStr_Port, 1000,
                            MacHalLightLeimac.DevConnStr_Model, MvaEnumLeimacModel.IDGB_50M4PG_24_TP,
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
                        DeviceName = MacEnumDevice.openstage_camera_left_1.ToString(),
                        DriverId = ManifestDriverId.CameraSentech.ToString(),
                    },

                    new MacManifestDeviceCfg(){
                        DevConnStr = "id=00:11:1C:F9:A3:46",
                        DeviceName = MacEnumDevice.openstage_camera_right_1.ToString(),
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

                ID = EnuDeviceId.DE_UNI_A_ASB.ToString(),
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