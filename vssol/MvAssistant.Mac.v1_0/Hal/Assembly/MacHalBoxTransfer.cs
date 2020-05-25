using MvAssistant.Mac.v1_0.Hal.Component;
using MvAssistant.Mac.v1_0.Hal.Component.Camera;
using MvAssistant.Mac.v1_0.Hal.Component.Force6Axis;
using MvAssistant.Mac.v1_0.Hal.Component.Gripper;
using MvAssistant.Mac.v1_0.Hal.Component.Robot;
using MvAssistant.Mac.v1_0.Hal.CompPlc;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Runtime.InteropServices;

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{
    [GuidAttribute("53CD572B-CAB0-498A-8005-B56AB7ED745F")]
    public class MacHalBoxTransfer : MacHalAssemblyBase, IMacHalBoxTransfer
    {
        #region Device Components



        public IMacHalPlcBoxTransfer Plc { get { return (IMacHalPlcBoxTransfer)this.GetMachine(MacEnumDevice.boxtransfer_plc); } }


        public IHalRobot Robot { get { return (IHalRobot)this.GetMachine(MacEnumDevice.boxtransfer_robot_1); } }
        public IHalForce6Axis Force6Axis { get { return (IHalForce6Axis)this.GetMachine(MacEnumDevice.boxtransfer_force_6axis_sensor_1); } }
        public IHalCamera Camera_BoxSlot_Direction { get { return (IHalCamera)this.GetMachine(MacEnumDevice.boxtransfer_ccd_gripper_1); } }
        public IHalLight CameraCircleLight { get { return (IHalLight)this.GetMachine(MacEnumDevice.boxtransfer_ringlight_1); } }
        public IHalGripper Gripper { get { return (IHalGripper)this.GetMachine(MacEnumDevice.boxtransfer_gripper_1); } }
        public IHalRobotSkin RobotSkin { get { return (IHalRobotSkin)this.GetMachine(MacEnumDevice.boxtransfer_robot_skin_1); } }
        public IHalLaser Laser_BoxSlot_Z { get { return (IHalLaser)this.GetMachine(MacEnumDevice.boxtransfer_laser_gripper_1); } }




        #endregion Device Components




        public void MoveToDrawer(int number)
        {



            this.Robot.HalMoveStraightAsyn(new HalRobotMotion()
            {
                 X= 1,
                 Y=2,
                 Z=3,
                 W=4,
                 P=5,
                 R=6,
                 Speed=20,
            });

        }





        /// <summary>
        /// 夾取，1：鐵盒、2：水晶盒
        /// </summary>
        /// <param name="BoxType">1：鐵盒、2：水晶盒</param>
        /// <returns></returns>
        public string Clamp(uint BoxType)
        { return Plc.Clamp(BoxType); }

        public string Unclamp()
        { return Plc.Unclamp(); }

        public string Initial()
        { return Plc.Initial(); }

        /// <summary>
        /// 重置夾爪XY軸水平
        /// </summary>
        /// <returns></returns>
        public bool LevelReset()
        { return Plc.LevelReset(); }

        public string ReadBTRobotStatus()
        { return Plc.ReadBTRobotStatus(); }

        /// <summary>
        /// 當手臂作動或停止時，需要下指令讓PLC知道目前Robot是移動或靜止狀態
        /// </summary>
        /// <param name="isMoving">手臂是否要移動</param>
        public void RobotMoving(bool isMoving)
        { Plc.RobotMoving(isMoving); }

        #region Set Parameter
        /// <summary>
        /// 夾爪速度設定，單位(mm/sec)
        /// </summary>
        /// <param name="ClampSpeed">夾爪速度</param>
        public void SetSpeed(double ClampSpeed)
        { Plc.SetSpeed(ClampSpeed); }

        /// <summary>
        /// 設定夾爪間距的極限值，最小間距、最大間距
        /// </summary>
        /// <param name="Minimum">最小間距</param>
        /// <param name="Maximum">最大間距</param>
        public void SetHandSpaceLimit(double? Minimum, double? Maximum)
        { Plc.SetHandSpaceLimit(Minimum, Maximum); }

        /// <summary>
        /// 設定Clamp與Cabinet的最小間距限制
        /// </summary>
        /// <param name="Minimum">最小間距</param>
        public void SetClampToCabinetSpaceLimit(double Minimum)
        { Plc.SetClampToCabinetSpaceLimit(Minimum); }

        /// <summary>
        /// 設定水平Sensor的XY軸極限值，X軸水平極限、Y軸水平極限
        /// </summary>
        /// <param name="Level_X">X軸水平極限</param>
        /// <param name="Level_Y">Y軸水平極限</param>
        public void SetLevelSensorLimit(double? Level_X, double? Level_Y)
        { Plc.SetLevelSensorLimit(Level_X, Level_Y); }

        /// <summary>
        /// 設定六軸力覺Sensor的壓力極限值
        /// </summary>
        /// <param name="Fx"></param>
        /// <param name="Fy"></param>
        /// <param name="Fz"></param>
        /// <param name="Mx"></param>
        /// <param name="My"></param>
        /// <param name="Mz"></param>
        public void SetSixAxisSensorLimit(uint? Fx, uint? Fy, uint? Fz, uint? Mx, uint? My, uint? Mz)
        { Plc.SetSixAxisSensorLimit(Fx, Fy, Fz, Mx, My, Mz); }
        #endregion

        #region Read Parameter
        /// <summary>
        /// 讀取夾爪速度設定
        /// </summary>
        /// <returns></returns>
        public double ReadSpeedSetting()
        { return Plc.ReadSpeedSetting(); }

        /// <summary>
        /// 讀取夾爪間距的極限值設定，最小夾距、最大夾距
        /// </summary>
        /// <returns>最小夾距、最大夾距</returns>
        public Tuple<double, double> ReadHandSpaceLimitSetting()
        { return Plc.ReadHandSpaceLimitSetting(); }

        /// <summary>
        /// 讀取Clamp與Cabinet的最小間距設定
        /// </summary>
        /// <returns>最小間距</returns>
        public double ReadClampToCabinetSpaceLimitSetting()
        { return Plc.ReadClampToCabinetSpaceLimitSetting(); }

        /// <summary>
        /// 設定XY軸水平Sensor限制，X軸水平限制、Y軸水平限制
        /// </summary>
        /// <returns>X軸水平限制、Y軸水平限制</returns>
        public Tuple<double, double> ReadLevelSensorLimitSetting()
        { return Plc.ReadLevelSensorLimitSetting(); }

        /// <summary>
        /// 讀取六軸力覺Sensor的壓力極限值設定
        /// </summary>
        /// <returns></returns>
        public Tuple<int, int, int, int, int, int> ReadSixAxisSensorLimitSetting()
        { return Plc.ReadSixAxisSensorLimitSetting(); }
        #endregion

        #region Read Component Value
        /// <summary>
        /// 讀取軟體記憶的夾爪位置
        /// </summary>
        /// <returns></returns>
        public double ReadHandPos()
        { return Plc.ReadHandPos(); }

        /// <summary>
        /// 讀取夾爪前方是否有Box
        /// </summary>
        /// <returns></returns>
        public bool ReadBoxDetect()
        { return Plc.ReadBoxDetect(); }

        /// <summary>
        /// 讀取由雷射檢測的夾爪位置
        /// </summary>
        /// <returns></returns>
        public double ReadHandPosByLSR()
        { return Plc.ReadHandPosByLSR(); }

        /// <summary>
        /// 讀取Clamp前方物體距離
        /// </summary>
        /// <returns></returns>
        public double ReadClampDistance()
        { return Plc.ReadClampDistance(); }

        /// <summary>
        /// 讀取XY軸水平Sensor目前數值，X軸水平數值、Y軸水平數值
        /// </summary>
        /// <returns>X軸水平數值、Y軸水平數值</returns>
        public Tuple<double, double> ReadLevelSensor()
        { return Plc.ReadLevelSensor(); }

        /// <summary>
        /// 讀取六軸力覺Sensor數值
        /// </summary>
        /// <returns></returns>
        public Tuple<int, int, int, int, int, int> ReadSixAxisSensor()
        { return Plc.ReadSixAxisSensor(); }

        /// <summary>
        /// 確認Hand吸塵狀態
        /// </summary>
        /// <returns></returns>
        public bool ReadHandVacuum()
        { return Plc.ReadHandVacuum(); }
        #endregion

    }
}
