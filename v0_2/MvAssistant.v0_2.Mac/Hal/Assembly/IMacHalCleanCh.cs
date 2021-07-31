using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace MvAssistant.v0_2.Mac.Hal.Assembly
{
    [GuidAttribute("D86FA618-FDA6-4B8C-B8D2-E1FBF5F824A6")]
    public interface IMacHalCleanCh : IMacHalAssembly
    {
        Bitmap Camera_Cap();

        void Camera_Insp_CapToSave(string SavePath, string FileType);


        /// <summary>
        /// 調整燈光亮度
        /// </summary>
        /// <param name="value"></param>
        void LightForInspSetValue(int value);

        /// <summary> 讀取實際壓差 </summary>
        /// <returns></returns>
        int ReadChamberPressureDiff();

        /// <summary> 讀取實際吹氣壓力 </summary>
        /// <returns></returns>
        Single ReadGasValvePressure();

        /// <summary> 讀取吹氣壓力設定值 </summary>
        /// <returns></returns>
        double ReadGasValvePressureVar();

        /// <summary> 遮斷為True，由Chamber開口往內看，依序由左到右 </summary>
        /// <returns>遮斷為True，由Chamber開口往內看，依序由左到右</returns>
        Tuple<bool, bool, bool, bool> ReadLightCurtain();

        int ReadLightForInsp();

        /// <summary> 讀取壓力表數值 </summary>
        /// <returns></returns>
        double ReadManometerPressure();

        /// <summary> 讀取壓力表壓差限制設定 </summary>
        /// <returns></returns>
        int ReadManometerPressureLimit();

        /// <summary> 讀取Mask水平 </summary>
        /// <returns></returns>
        Tuple<double, double, double> ReadMaskLevel();

        /// <summary> 讀取各種大小Particle的數量限制設定，大Particle、中Particle、小Particle的數量 </summary>
        /// <returns></returns>
        Tuple<int, int, int> ReadParticleCntLimit();

        /// <summary> 讀取各種大小Particle的數量，大Particle、中Particle、小Particle的數量 </summary>
        /// <returns></returns>
        Tuple<int, int, int> ReadParticleCount();

        /// <summary> 讀取手臂橫向位置(左右區間) </summary>
        /// <returns></returns>
        double ReadRobotPosLeftRight();

        /// <summary> 讀取手臂入侵的左右區間極限設定，左極限、右極限 </summary>
        /// <returns>左極限、右極限</returns>
        Tuple<double, double> ReadRobotPosLeftRightLimit();

        /// <summary> 讀取手臂直向位置(上下區間) </summary>
        /// <returns></returns>
        double ReadRobotPosUpDown();

        /// <summary> 讀取手臂入侵的上下區間極限設定，上極限、下極限 </summary>
        /// <returns>上極限、下極限</returns>
        Tuple<double, double> ReadRobotPosUpDownLimit();

        /// <summary> 設定吹氣壓力值 </summary>
        /// <param name="AirPressure"></param>
        void SetGasValvePressurVar(double AirPressure);

        /// <summary> 空氣閥吹風(BlowTime單位為100ms) </summary>
        /// <param name="purgeTime">(100ms)</param>
        /// <returns></returns>
        string SetGasValveTime(uint purgeTime);
        /// <summary> 設定壓力表壓差限制 </summary>
        /// <param name="PressureLimit"></param>
        void SetManometerPressureLimit(uint PressureLimit);

        /// <summary> 設定各種大小Particle的數量限制 </summary>
        /// <param name="L_Limit">Large Particle Qty</param>
        /// <param name="M_Limit">Medium Particle Qty</param>
        /// <param name="S_Limit">Small Particle Qty</param>
        void SetParticleCntLimit(uint? L_Limit, uint? M_Limit, uint? S_Limit);

        /// <summary> 設定手臂入侵的左右區間極限，左極限、右極限 </summary>
        /// <param name="Limit_L">左極限</param>
        /// <param name="Limit_R">右極限</param>
        void SetRobotLeftRightLimit(double? Limit_L, double? Limit_R);

        /// <summary> 設定手臂入侵的上下區間極限，上極限、下極限 </summary>
        /// <param name="Limit_U">上極限</param>
        /// <param name="Limit_D">下極限</param>
        void SetRobotUpDownLimit(double? Limit_U, double? Limit_D);
    }
}
