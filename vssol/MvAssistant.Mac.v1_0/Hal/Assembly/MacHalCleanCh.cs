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
        /// �Ů�֧j��(BlowTime��쬰100ms)
        /// </summary>
        /// <param name="BlowTime">(100ms)</param>
        /// <returns></returns>
        public string GasValveBlow(uint BlowTime)
        { return Plc.GasValveBlow(BlowTime); }

        #region Set Parameter
        /// <summary>
        /// �]�w�U�ؤj�pParticle���ƶq����
        /// </summary>
        /// <param name="L_Limit">Large Particle Qty</param>
        /// <param name="M_Limit">Medium Particle Qty</param>
        /// <param name="S_Limit">Small Particle Qty</param>
        public void SetParticleCntLimit(uint? L_Limit, uint? M_Limit, uint? S_Limit)
        { Plc.SetParticleCntLimit(L_Limit, M_Limit, S_Limit); }

        /// <summary>
        /// �]�w���u�J�I�����k�϶������A�������B�k����
        /// </summary>
        /// <param name="Limit_L">������</param>
        /// <param name="Limit_R">�k����</param>
        public void SetRobotAboutLimit(double? Limit_L, double? Limit_R)
        { Plc.SetRobotAboutLimit(Limit_L, Limit_R); }

        /// <summary>
        /// �]�w���u�J�I���W�U�϶������A�W�����B�U����
        /// </summary>
        /// <param name="Limit_U">�W����</param>
        /// <param name="Limit_D">�U����</param>
        public void SetRobotUpDownLimit(double? Limit_U, double? Limit_D)
        { Plc.SetRobotUpDownLimit(Limit_U, Limit_D); }

        /// <summary>
        /// �]�w���O�����t����
        /// </summary>
        /// <param name="PressureLimit"></param>
        public void SetPressureDiffLimit(uint PressureLimit)
        { Plc.SetPressureDiffLimit(PressureLimit); }

        /// <summary>
        /// �]�w�j�����O��
        /// </summary>
        /// <param name="AirPressure"></param>
        public void SetPressureCtrl(double AirPressure)
        { Plc.SetPressureCtrl(AirPressure); }
        #endregion

        #region Read Parameter
        /// <summary>
        /// Ū���U�ؤj�pParticle���ƶq����]�w�A�jParticle�B��Particle�B�pParticle���ƶq
        /// </summary>
        /// <returns></returns>
        public Tuple<int, int, int> ReadParticleCntLimitSetting()
        { return Plc.ReadParticleCntLimitSetting(); }

        /// <summary>
        /// Ū�����u�J�I�����k�϶������]�w�A�������B�k����
        /// </summary>
        /// <returns>�������B�k����</returns>
        public Tuple<double, double> ReadRobotAboutLimitSetting()
        { return Plc.ReadRobotAboutLimitSetting(); }

        /// <summary>
        /// Ū�����u�J�I���W�U�϶������]�w�A�W�����B�U����
        /// </summary>
        /// <returns>�W�����B�U����</returns>
        public Tuple<double, double> ReadRobotUpDownLimitSetting()
        { return Plc.ReadRobotUpDownLimitSetting(); }

        /// <summary>
        /// Ū�����O�����t����]�w
        /// </summary>
        /// <returns></returns>
        public int ReadPressureDiffLimitSetting()
        { return Plc.ReadPressureDiffLimitSetting(); }

        /// <summary>
        /// Ū���j�����O�]�w��
        /// </summary>
        /// <returns></returns>
        public double ReadPressureCtrlSetting()
        { return Plc.ReadPressureCtrlSetting(); }
        #endregion

        #region Read Component Value

        /// <summary>
        /// Ū���U�ؤj�pParticle���ƶq�A�jParticle�B��Particle�B�pParticle���ƶq
        /// </summary>
        /// <returns></returns>
        public Tuple<int, int, int> ReadParticleCount()
        { return Plc.ReadParticleCount(); }

        /// <summary>
        /// Ū��Mask����
        /// </summary>
        /// <returns></returns>
        public Tuple<double, double, double> ReadMaskLevel()
        { return Plc.ReadMaskLevel(); }

        /// <summary>
        /// Ū�����u��V��m(���k�϶�)
        /// </summary>
        /// <returns></returns>
        public double ReadRobotPosAbout()
        { return Plc.ReadRobotPosAbout(); }

        /// <summary>
        /// Ū�����u���V��m(�W�U�϶�)
        /// </summary>
        /// <returns></returns>
        public double ReadRobotPosUpDown()
        { return Plc.ReadRobotPosUpDown(); }

        /// <summary>
        /// Ū��������t
        /// </summary>
        /// <returns></returns>
        public int ReadPressureDiff()
        { return Plc.ReadPressureDiff(); }

        /// <summary>
        /// Ū����ڧj�����O
        /// </summary>
        /// <returns></returns>
        public Single ReadBlowPressure()
        { return Plc.ReadBlowPressure(); }

        /// <summary>
        /// Ū�����O��ƭ�
        /// </summary>
        /// <returns></returns>
        public double ReadPressure()
        { return Plc.ReadPressure(); }

        /// <summary>
        /// Ū�����h�A�@�Ƥ@�� �U�ۿW�ߡA�B�_��True�AReset time 500ms
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, bool, bool> ReadLightCurtain()
        { return Plc.ReadLightCurtain(); }
        #endregion

    }
}