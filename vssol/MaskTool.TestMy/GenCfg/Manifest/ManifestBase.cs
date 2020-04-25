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
        public List<MacMachineDriverCfg> DriverFakeAll()
        {
            var list = new List<MacMachineDriverCfg>();
            list.AddRange(DriverFakeAssembly());
            list.AddRange(DriverFakeDevice());
            list.AddRange(DriverFakeOpticSensor());
            list.AddRange(DriverFakeCamera());
            list.AddRange(DriverFakeMotion());
            list.AddRange(DriverFakeLight());
            return list;
        }
        public List<MacMachineDriverCfg> DriverFakeAssembly()
        {
            return new MacMachineDriverCfg[]{

            }.ToList();
        }
        public List<MacMachineDriverCfg> DriverFakeCamera()
        {
            return new MacMachineDriverCfg[]{


            }.ToList();
        }
        public List<MacMachineDriverCfg> DriverFakeDevice()
        {
            return new MacMachineDriverCfg[]{

            }.ToList();
        }
        public List<MacMachineDriverCfg> DriverFakeLight()
        {
            return new MacMachineDriverCfg[]{
            }.ToList();
        }

        public List<MacMachineDriverCfg> DriverFakeMotion()
        {
            return new MacMachineDriverCfg[]{


                new MacMachineDriverCfg(){
                    Vendor = "Fake",
                    Product = "Mask Gripper",
                    Remark = null,
                    AssignType = typeof(Hal.ComponentFake.Gripper.HalGripperFake),
                    DriverId = ManifestDriverId.MaskGripperFake.ToString(),
                    DriverPath = null,
                },
                new MacMachineDriverCfg(){
                    Vendor = "Fake",
                    Product = "Box Gripper",
                    Remark = null,
                    AssignType = typeof(Hal.ComponentFake.Gripper.HalGripperFake),
                    DriverId = ManifestDriverId.BoxGripperFake.ToString(),
                    DriverPath = null,
                },
            }.ToList();
        }

        public List<MacMachineDriverCfg> DriverFakeOpticSensor()
        {
            return new MacMachineDriverCfg[]{

                new MacMachineDriverCfg(){
                    Vendor = "Fake",
                    Product = "Fake Laser Sensor",
                    Remark = null,
                    AssignType = typeof(Hal.ComponentFake.Laser.HalLaserFake),
                    DriverId = ManifestDriverId.Laser_Fake.ToString(),
                    DriverPath = null,
                },
                }.ToList();
        }
        /// <summary>
        /// 在此 Maintain Real Device Drive的宣告
        /// </summary>
        /// <returns></returns>
        public List<MacMachineDriverCfg> DriverRealAll()
        {
            var dict = new Dictionary<string, MacMachineDriverCfg>();

            var list = new List<MacMachineDriverCfg>();
            list.AddRange(DriverRealAssembly());
            list.AddRange(DriverRealDevice());
            list.AddRange(DriverRealOpticSensor());
            list.AddRange(DriverRealCamera());
            list.AddRange(DriverRealMotion());
            list.AddRange(DriverRealLight());
            return list;
        }
        public List<MacMachineDriverCfg> DriverRealAssembly()
        {
            return new MacMachineDriverCfg[]{
                 new MacMachineDriverCfg(){
                    Vendor = "Hirata",
                    Product = "Load Port",
                    Remark = null,
                    AssignType = typeof(Hal.Assembly.MacHalLoadPort),
                    DriverId = ManifestDriverId.LoadPort.ToString(),
                },

            }.ToList();
        }
        public List<MacMachineDriverCfg> DriverRealCamera()
        {
            return new MacMachineDriverCfg[]{

            }.ToList();
        }
        public List<MacMachineDriverCfg> DriverRealDevice()
        {
            return new MacMachineDriverCfg[]{
                 new MacMachineDriverCfg(){
                    Vendor = "Omron",
                    Product = "PLC",
                    Remark = null,
                    AssignType = typeof(Hal.Component.Plc.MacHalPlcOmron),
                    DriverId = ManifestDriverId.Plc_Omron.ToString(),
                    DriverPath =null,
                },
                 new MacMachineDriverCfg(){
                    Vendor = "Omron",
                    Product = "Inclinometer MPU6050",
                    Remark = "Angle Sensor",
                    AssignType = typeof(Hal.Component.Inclinometer.InclinometerOmronPlc),
                    DriverId = ManifestDriverId.Inclinometer_OmronPlc.ToString(),
                    DriverPath =null,
                },
            }.ToList();
        }
        public List<MacMachineDriverCfg> DriverRealLight()
        {
            return new MacMachineDriverCfg[]{
            }.ToList();
        }

        public List<MacMachineDriverCfg> DriverRealMotion()
        {
            return new MacMachineDriverCfg[]{
                   new MacMachineDriverCfg(){
                    Vendor = "Fanuc",
                    Product = "Fanuc Robot LR Mate 200iD",
                    Remark = null,
                    AssignType = typeof(Hal.Component.Robot.HalRobotFanuc),
                    DriverId = ManifestDriverId.FanucRobot.ToString(),
                    DriverPath = null,
                },
                new MacMachineDriverCfg(){
                    Vendor = "NRC",
                    Product = "Mask Gripper",
                    Remark = null,
                    AssignType = typeof(Hal.Component.Gripper.HalPlcOmronCustom01),
                    DriverId = ManifestDriverId.MaskGripperNrc.ToString(),
                    DriverPath = null,
                },

            }.ToList();
        }

        public List<MacMachineDriverCfg> DriverRealOpticSensor()
        {
            return new MacMachineDriverCfg[]{

            }.ToList();
        }
    }
}
