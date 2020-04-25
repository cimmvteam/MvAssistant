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
        public List<MachineDriver> DriverFakeAll()
        {
            var list = new List<MachineDriver>();
            list.AddRange(DriverFakeAssembly());
            list.AddRange(DriverFakeDevice());
            list.AddRange(DriverFakeOpticSensor());
            list.AddRange(DriverFakeCamera());
            list.AddRange(DriverFakeMotion());
            list.AddRange(DriverFakeLight());
            return list;
        }
        public List<MachineDriver> DriverFakeAssembly()
        {
            return new MachineDriver[]{

            }.ToList();
        }
        public List<MachineDriver> DriverFakeCamera()
        {
            return new MachineDriver[]{


            }.ToList();
        }
        public List<MachineDriver> DriverFakeDevice()
        {
            return new MachineDriver[]{

            }.ToList();
        }
        public List<MachineDriver> DriverFakeOpticSensor()
        {
            return new MachineDriver[]{

                new MachineDriver(){
                    Vendor = "Fake",
                    Product = "Fake Laser Sensor-1",
                    Description = "雷射測距Sensor",
                    AssignType = typeof(Hal.ComponentFake.Laser.HalLaserFake),
                    DriverPath = "MaskAutoCleaner.HalFake.dll",
                    DriverId = ManifestDriverId.LaserEntry_OmronPlc.ToString(),
                }, new MachineDriver(){
                    Vendor = "Fake",
                    Product = "Fake Laser Sensor-2",
                    Description = "雷射測距Sensor - Clean Chamber 防碰撞",
                    AssignType = typeof(Hal.ComponentFake.Laser.HalLaserFake),
                    DriverPath = "MaskAutoCleaner.HalFake.dll",
                    DriverId = ManifestDriverId.LaserCollision_OmronPlc.ToString(),
                },
                }.ToList();
        }
        public List<MachineDriver> DriverFakeMotion()
        {
            return new MachineDriver[]{


                new MachineDriver(){
                    Vendor = "Fake",
                    Product = "Mask Gripper",
                    Description = "Mask Gripper",
                    AssignType = typeof(Hal.ComponentFake.Gripper.HalGripperFake),
                    DriverPath = "MaskAutoCleaner.HalFake.dll",
                    DriverId = ManifestDriverId.MaskGripperFake.ToString(),
                },
                new MachineDriver(){
                    Vendor = "Fake",
                    Product = "Box Gripper",
                    Description = "Box Gripper",
                    AssignType = typeof(Hal.ComponentFake.Gripper.HalGripperFake),
                    DriverPath = "MaskAutoCleaner.HalFake.dll",
                    DriverId = ManifestDriverId.BoxGripperFake.ToString(),
                },
            }.ToList();
        }
        public List<MachineDriver> DriverFakeLight()
        {
            return new MachineDriver[]{
            }.ToList();
        }

        /// <summary>
        /// 在此 Maintain Real Device Drive的宣告
        /// </summary>
        /// <returns></returns>
        public List<MachineDriver> DriverRealAll()
        {
            var dict = new Dictionary<string, MachineDriver>();

            var list = new List<MachineDriver>();
            list.AddRange(DriverRealAssembly());
            list.AddRange(DriverRealDevice());
            list.AddRange(DriverRealOpticSensor());
            list.AddRange(DriverRealCamera());
            list.AddRange(DriverRealMotion());
            list.AddRange(DriverRealLight());
            return list;
        }
        public List<MachineDriver> DriverRealAssembly()
        {
            return new MachineDriver[]{
                 new MachineDriver(){
                    Vendor = "Hirata",
                    Product = "Load Port",
                    AssignType = typeof(Hal.Assembly.HalLoadPort),
                    DriverId = ManifestDriverId.LoadPort.ToString(),
                },

            }.ToList();
        }
        public List<MachineDriver> DriverRealCamera()
        {
            return new MachineDriver[]{

            }.ToList();
        }
        public List<MachineDriver> DriverRealDevice()
        {
            return new MachineDriver[]{
                 new MachineDriver(){
                    Vendor = "Omron",
                    Product = "PLC",
                    Description = "Omron PLC",
                    AssignType = typeof(Hal.Component.Plc.HalPlcOmron),
                    DriverPath =null,
                    DriverId = ManifestDriverId.Plc_Omron.ToString(),
                },
                 new MachineDriver(){
                    Vendor = "Omron",
                    Product = "Inclinometer MPU6050",
                    Description = "Angle Sensor",
                    AssignType = typeof(Hal.Component.Inclinometer.InclinometerOmronPlc),
                    DriverPath =null,
                    DriverId = ManifestDriverId.Inclinometer_OmronPlc.ToString(),
                },
            }.ToList();
        }
        public List<MachineDriver> DriverRealOpticSensor()
        {
            return new MachineDriver[]{

            }.ToList();
        }
        public List<MachineDriver> DriverRealMotion()
        {
            return new MachineDriver[]{
                   new MachineDriver(){
                    Vendor = "Fanuc",
                    Product = "Fanuc Robot LR Mate 200iD",
                    Description = "Fanuc Robot",
                    AssignType = typeof(Hal.Component.Robot.HalRobotFanuc),
                    DriverPath = "MaskAutoCleaner.Hal.dll",
                    DriverId = ManifestDriverId.FanucRobot.ToString(),
                },
                new MachineDriver(){
                    Vendor = "NRC",
                    Product = "Mask Gripper",
                    Description = "Mask Gripper",
                    AssignType = typeof(Hal.Component.Gripper.HalPlcOmronCustom01),
                    DriverPath = "MaskAutoCleaner.HalFake.dll",
                    DriverId = ManifestDriverId.MaskGripperNrc.ToString(),
                },

            }.ToList();
        }
        public List<MachineDriver> DriverRealLight()
        {
            return new MachineDriver[]{
            }.ToList();
        }



    }
}
