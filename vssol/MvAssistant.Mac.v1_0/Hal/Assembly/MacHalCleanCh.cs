using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Hal.Component;
using MvAssistant.Mac.v1_0.Hal.Component.AirPressure;
using MvAssistant.Mac.v1_0.Hal.Component.Camera;
using MvAssistant.Mac.v1_0.Hal.CompPlc;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{
    [GuidAttribute("25B4A570-8696-4726-AB5A-CF22161DFA19")]
    public class MacHalCleanCh : MacHalAssemblyBase, IMacHalCleanCh
    {
        #region Device Components


        public IMacHalPlcCleanCh Plc { get { return (IMacHalPlcCleanCh)this.GetHalDevice(MacEnumDevice.cleanch_plc); } }


        public IHalPressureCtrl Clean_air_pressure_controller_1 { get { return (IHalPressureCtrl)this.GetHalDevice(MacEnumDevice.clean_air_pressure_controller_1); } }
        public IHalDiffPressure Clean_air_pressure_diff_sensor_1 { get { return (IHalDiffPressure)this.GetHalDevice(MacEnumDevice.clean_air_pressure_diff_sensor_1); } }
        public IHalPressureSensor Clean_air_pressure_sensor_1 { get { return (IHalPressureSensor)this.GetHalDevice(MacEnumDevice.clean_air_pressure_sensor_1); } }
        public IHalCamera Clean_ccd_particle_1 { get { return (IHalCamera)this.GetHalDevice(MacEnumDevice.clean_ccd_particle_1); } }
        public IHalGasValve Clean_gas_valve_1 { get { return (IHalGasValve)this.GetHalDevice(MacEnumDevice.clean_gas_valve_1); } }
        public IHalIonizer Clean_iozonier_1 { get { return (IHalIonizer)this.GetHalDevice(MacEnumDevice.clean_ionizer_1); } }
        public IHalLaser Clean_laser_entry_1 { get { return (IHalLaser)this.GetHalDevice(MacEnumDevice.clean_laser_entry_1); } }
        public IHalLaser Clean_laser_entry_2 { get { return (IHalLaser)this.GetHalDevice(MacEnumDevice.clean_laser_entry_2); } }
        public IHalLaser Clean_laser_prevent_collision_1 { get { return (IHalLaser)this.GetHalDevice(MacEnumDevice.clean_laser_prevent_collision_1); } }
        public IHalLaser Clean_laser_prevent_collision_2 { get { return (IHalLaser)this.GetHalDevice(MacEnumDevice.clean_laser_prevent_collision_2); } }
        public IHalLaser Clean_laser_prevent_collision_3 { get { return (IHalLaser)this.GetHalDevice(MacEnumDevice.clean_laser_prevent_collision_3); } }
        public IHalLight Clean_linesource_1 { get { return (IHalLight)this.GetHalDevice(MacEnumDevice.clean_linesource_1); } }
        public IHalParticleCounter Clean_particle_counter_1 { get { return (IHalParticleCounter)this.GetHalDevice(MacEnumDevice.clean_particle_counter_1); } }



        #endregion Device Components

        /// <summary>
        /// 空氣閥吹風(BlowTime單位為100ms)
        /// </summary>
        /// <param name="BlowTime">(100ms)</param>
        /// <returns></returns>
        public string GasValveBlow(uint BlowTime)
        { return Plc.GasValveBlow(BlowTime); }

        #region Set Parameter
        /// <summary>
        /// 設定各種大小Particle的數量限制
        /// </summary>
        /// <param name="L_Limit">Large Particle Qty</param>
        /// <param name="M_Limit">Medium Particle Qty</param>
        /// <param name="S_Limit">Small Particle Qty</param>
        public void SetParticleCntLimit(uint? L_Limit, uint? M_Limit, uint? S_Limit)
        { Plc.SetParticleCntLimit(L_Limit, M_Limit, S_Limit); }

        /// <summary>
        /// 設定手臂入侵的左右區間極限，左極限、右極限
        /// </summary>
        /// <param name="Limit_L">左極限</param>
        /// <param name="Limit_R">右極限</param>
        public void SetRobotAboutLimit(double? Limit_L, double? Limit_R)
        { Plc.SetRobotAboutLimit(Limit_L, Limit_R); }

        /// <summary>
        /// 設定手臂入侵的上下區間極限，上極限、下極限
        /// </summary>
        /// <param name="Limit_U">上極限</param>
        /// <param name="Limit_D">下極限</param>
        public void SetRobotUpDownLimit(double? Limit_U, double? Limit_D)
        { Plc.SetRobotUpDownLimit(Limit_U, Limit_D); }

        /// <summary>
        /// 設定壓力表壓差限制
        /// </summary>
        /// <param name="PressureLimit"></param>
        public void SetPressureDiffLimit(uint PressureLimit)
        { Plc.SetPressureDiffLimit(PressureLimit); }

        /// <summary>
        /// 設定吹氣壓力值
        /// </summary>
        /// <param name="AirPressure"></param>
        public void SetPressureCtrl(double AirPressure)
        { Plc.SetPressureCtrl(AirPressure); }
        #endregion

        #region Read Parameter
        /// <summary>
        /// 讀取各種大小Particle的數量限制設定，大Particle、中Particle、小Particle的數量
        /// </summary>
        /// <returns></returns>
        public Tuple<int, int, int> ReadParticleCntLimitSetting()
        { return Plc.ReadParticleCntLimitSetting(); }

        /// <summary>
        /// 讀取手臂入侵的左右區間極限設定，左極限、右極限
        /// </summary>
        /// <returns>左極限、右極限</returns>
        public Tuple<double, double> ReadRobotAboutLimitSetting()
        { return Plc.ReadRobotAboutLimitSetting(); }

        /// <summary>
        /// 讀取手臂入侵的上下區間極限設定，上極限、下極限
        /// </summary>
        /// <returns>上極限、下極限</returns>
        public Tuple<double, double> ReadRobotUpDownLimitSetting()
        { return Plc.ReadRobotUpDownLimitSetting(); }

        /// <summary>
        /// 讀取壓力表壓差限制設定
        /// </summary>
        /// <returns></returns>
        public int ReadPressureDiffLimitSetting()
        { return Plc.ReadPressureDiffLimitSetting(); }

        /// <summary>
        /// 讀取吹氣壓力設定值
        /// </summary>
        /// <returns></returns>
        public double ReadPressureCtrlSetting()
        { return Plc.ReadPressureCtrlSetting(); }
        #endregion

        #region Read Component Value

        /// <summary>
        /// 讀取各種大小Particle的數量，大Particle、中Particle、小Particle的數量
        /// </summary>
        /// <returns></returns>
        public Tuple<int, int, int> ReadParticleCount()
        { return Plc.ReadParticleCount(); }

        /// <summary>
        /// 讀取Mask水平
        /// </summary>
        /// <returns></returns>
        public Tuple<double, double, double> ReadMaskLevel()
        { return Plc.ReadMaskLevel(); }

        /// <summary>
        /// 讀取手臂橫向位置(左右區間)
        /// </summary>
        /// <returns></returns>
        public double ReadRobotPosAbout()
        { return Plc.ReadRobotPosAbout(); }

        /// <summary>
        /// 讀取手臂直向位置(上下區間)
        /// </summary>
        /// <returns></returns>
        public double ReadRobotPosUpDown()
        { return Plc.ReadRobotPosUpDown(); }

        /// <summary>
        /// 讀取實際壓差
        /// </summary>
        /// <returns></returns>
        public int ReadPressureDiff()
        { return Plc.ReadPressureDiff(); }

        /// <summary>
        /// 讀取實際吹氣壓力
        /// </summary>
        /// <returns></returns>
        public Single ReadBlowPressure()
        { return Plc.ReadBlowPressure(); }

        /// <summary>
        /// 讀取壓力表數值
        /// </summary>
        /// <returns></returns>
        public double ReadPressure()
        { return Plc.ReadPressure(); }

        /// <summary>
        /// 讀取光閘，一排一個 各自獨立，遮斷時True，Reset time 500ms
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, bool, bool> ReadLightCurtain()
        { return Plc.ReadLightCurtain(); }
        #endregion

    }
}