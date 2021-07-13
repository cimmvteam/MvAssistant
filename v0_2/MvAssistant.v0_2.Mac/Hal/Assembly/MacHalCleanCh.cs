using MvAssistant.v0_2.Mac.Hal.CompCamera;
using MvAssistant.v0_2.Mac.Hal.CompLight;
using MvAssistant.v0_2.Mac.Hal.CompPlc;
using MvAssistant.v0_2.Mac.Manifest;
using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace MvAssistant.v0_2.Mac.Hal.Assembly
{
    [GuidAttribute("25B4A570-8696-4726-AB5A-CF22161DFA19")]
    public class MacHalCleanCh : MacHalAssemblyBase, IMacHalCleanCh
    {
        #region Device Components


        public IMacHalPlcCleanCh Plc { get { return (IMacHalPlcCleanCh)this.GetHalDevice(EnumMacDeviceId.cleanch_plc); } }
        public IMacHalLight LightSideInsp { get { return (IMacHalLight)this.GetHalDevice(EnumMacDeviceId.cleanch_inspection_spot_light_001); } }
        public IHalCamera CameraInsp { get { return (IHalCamera)this.GetHalDevice(EnumMacDeviceId.clean_camera_particle_1); } }


        #endregion Device Components











        /// <summary>
        /// �Ů�֧j��(BlowTime��쬰100ms)
        /// </summary>
        /// <param name="BlowTime">(100ms)</param>
        /// <returns></returns>
        public string SetGasValveTime(uint BlowTime)
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
        public void SetRobotLeftRightLimit(double? Limit_L, double? Limit_R)
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
        public void SetManometerPressureLimit(uint PressureLimit)
        { Plc.SetPressureDiffLimit(PressureLimit); }

        /// <summary>
        /// �]�w�j�����O��
        /// </summary>
        /// <param name="AirPressure"></param>
        public void SetGasValvePressurVar(double AirPressure)
        { Plc.SetPressureCtrl(AirPressure); }
        #endregion

        #region Read Parameter
        /// <summary>
        /// Ū���U�ؤj�pParticle���ƶq����]�w�A�jParticle�B��Particle�B�pParticle���ƶq
        /// </summary>
        /// <returns></returns>
        public Tuple<int, int, int> ReadParticleCntLimit()
        { return Plc.ReadParticleCntLimitSetting(); }

        /// <summary>
        /// Ū�����u�J�I�����k�϶������]�w�A�������B�k����
        /// </summary>
        /// <returns>�������B�k����</returns>
        public Tuple<double, double> ReadRobotPosLeftRightLimit()
        { return Plc.ReadRobotAboutLimitSetting(); }

        /// <summary>
        /// Ū�����u�J�I���W�U�϶������]�w�A�W�����B�U����
        /// </summary>
        /// <returns>�W�����B�U����</returns>
        public Tuple<double, double> ReadRobotPosUpDownLimit()
        { return Plc.ReadRobotUpDownLimitSetting(); }

        /// <summary>
        /// Ū�����O�����t����]�w
        /// </summary>
        /// <returns></returns>
        public int ReadManometerPressureLimit()
        { return Plc.ReadPressureDiffLimitSetting(); }

        /// <summary>
        /// Ū���j�����O�]�w��
        /// </summary>
        /// <returns></returns>
        public double ReadGasValvePressureVar()
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
        public double ReadRobotPosLeftRight()
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
        public int ReadChamberPressureDiff()
        { return Plc.ReadPressureDiff(); }

        /// <summary>
        /// Ū����ڧj�����O
        /// </summary>
        /// <returns></returns>
        public Single ReadGasValvePressure()
        { return Plc.ReadBlowPressure(); }

        /// <summary>
        /// Ū�����O��ƭ�
        /// </summary>
        /// <returns></returns>
        public double ReadManometerPressure()
        { return Plc.ReadPressure(); }

        /// <summary>
        /// Ū�����h�A�@�Ƥ@�� �U�ۿW�ߡA�B�_��True�AReset time 500ms
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, bool, bool, bool> ReadLightCurtain()
        { return Plc.ReadLightCurtain(); }
        #endregion

        public Bitmap Camera_Cap()
        {
            return CameraInsp.Shot();
        }

        public void Camera_Insp_CapToSave(string SavePath, string FileType)
        {
            CameraInsp.ShotToSaveImage(SavePath, FileType);
        }

        /// <summary>
        /// �վ�O���G��
        /// </summary>
        /// <param name="value"></param>
        public void LightForInspSetValue(int value)
        {
            LightSideInsp.TurnOn(value);
        }

        public int ReadLightForInsp()
        { return LightSideInsp.GetValue(); }
    }
}